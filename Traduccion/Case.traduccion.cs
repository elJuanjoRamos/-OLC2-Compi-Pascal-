using CompiPascal.Traduccion.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections;
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
        public Switch_Trad SENTENCE_CASE(ParseTreeNode actual, int cant_tabs)
        {
            /*
             *  SENTENCE_CASE.Rule = RESERV_CASE  + LOGIC_EXPRESION + RESERV_OF + CASES + CASE_ELSE + RESERV_END + PUNTO_COMA;

          

            CASE_ELSE.Rule = RESERV_ELSE + INSTRUCTIONS
                | Empty
                ;
             */

            var condicion = expressionAST.getExpresion(actual.ChildNodes[1]);

            var lista_cases = CASES(actual.ChildNodes[3], cant_tabs+1);

            var else_case = CASE_ELSE(actual.ChildNodes[4], cant_tabs+1);
            return new Switch_Trad(condicion, lista_cases, else_case, cant_tabs);
        }

        public ArrayList CASES(ParseTreeNode actual, int cant_tabs)
        {
            /*
               CASES.Rule = MakePlusRule(CASES, CASE)
                | CASE
                ;
             */
            ArrayList lista_cases = new ArrayList();
            if (actual.ChildNodes.Count > 0)
            {
                foreach (var item in actual.ChildNodes)
                {
                    lista_cases.Add(CaseSing(item, cant_tabs));
                }
            }

            return lista_cases;
        }

        public Case_Trad CaseSing(ParseTreeNode actual, int cant_tabs)
        {
            /* CASE.Rule = LOGIC_EXPRESION + DOS_PUNTOS + INSTRUCTIONS;*/
            var condicion = expressionAST.getExpresion(actual.ChildNodes[0]);

            var lista_instrucciones = instructionAST.ISTRUCCIONES(actual.ChildNodes[2], cant_tabs+1);

            return new Case_Trad(condicion, new Sentence_Trad(lista_instrucciones), cant_tabs);

        }

        public Case_Trad CASE_ELSE(ParseTreeNode actual, int cant_tabs)
        {
            /*
             CASE_ELSE.Rule = RESERV_ELSE + INSTRUCTIONS
                | Empty
                ;
             */
            Case_Trad _case = new Case_Trad();
            if (actual.ChildNodes.Count > 0)
            {
                var lista_declaraciones = instructionAST.ISTRUCCIONES(actual.ChildNodes[1], cant_tabs+1);
                _case = new Case_Trad(new Sentence_Trad(lista_declaraciones), cant_tabs);
            }

            return _case;
        }
        #endregion
    }
}
