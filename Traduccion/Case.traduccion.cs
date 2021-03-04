using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class CaseTraduccion
    {
        public CaseTraduccion()
        {

        }
        //VARIABLES 
        InstructionTraduccion instructionAST = new InstructionTraduccion();
        ExpressionTraduccion expressionAST = new ExpressionTraduccion();



        #region CASE
        public string SENTENCE_CASE(ParseTreeNode actual, int cant_tabs)
        {
            /*
             *  SENTENCE_CASE.Rule = RESERV_CASE  + LOGIC_EXPRESION + RESERV_OF + CASES + CASE_ELSE + RESERV_END + PUNTO_COMA;

          

            CASE_ELSE.Rule = RESERV_ELSE + INSTRUCTIONS
                | Empty
                ;
             */


            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + " ";
            }

            var reserv_case = actual.ChildNodes[0].Token.Text;


            var condicion = expressionAST.getExpresion(actual.ChildNodes[1]);

            var reserv_of = actual.ChildNodes[2].Token.Text;



            var lista_cases = CASES(actual.ChildNodes[3], cant_tabs+1);

            var else_case = CASE_ELSE(actual.ChildNodes[4], cant_tabs+1);


            var reserv_end = actual.ChildNodes[5].Token.Text + ";";


            var case_total =
                 tabs + reserv_case + " " + condicion + " " + reserv_of + "\n" 
                + lista_cases + "\n" 
                + else_case + "\n"
                + tabs +reserv_end;

            return case_total;
        }

        public string CASES(ParseTreeNode actual, int cant_tabs)
        {
            /*
               CASES.Rule = MakePlusRule(CASES, CASE)
                | CASE
                ;
             */
            string lista_cases = "";
            if (actual.ChildNodes.Count > 0)
            {
                foreach (var item in actual.ChildNodes)
                {
                    lista_cases = lista_cases + (CaseSing(item, cant_tabs)) + "\n";
                }
            }

            return lista_cases;
        }

        public string CaseSing(ParseTreeNode actual, int cant_tabs)
        {
            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + " ";
            }
            /* CASE.Rule = LOGIC_EXPRESION + DOS_PUNTOS + INSTRUCTIONS;*/
            
            var condicion = expressionAST.getExpresion(actual.ChildNodes[0]);

            var lista_instrucciones = instructionAST.ISTRUCCIONES(actual.ChildNodes[2], cant_tabs+1);

            return tabs + condicion + ":" + "\n" + lista_instrucciones;

        }

        public string CASE_ELSE(ParseTreeNode actual, int cant_tabs)
        {
            /*
             CASE_ELSE.Rule = RESERV_ELSE + INSTRUCTIONS
                | Empty
                ;
             */
            var _case = "";
            if (actual.ChildNodes.Count > 0)
            {
                var tabs = "";
                for (int i = 0; i < cant_tabs; i++)
                {
                    tabs = tabs + " ";
                }




                var lista_declaraciones = instructionAST.ISTRUCCIONES(actual.ChildNodes[1], cant_tabs);
                _case = tabs + "else " + "\n" + lista_declaraciones;
            }

            return _case;
        }
        #endregion

    }
}
