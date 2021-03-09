using CompiPascal.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class CaseAST
    {
        //VARIABLES 
        InstructionAST instructionAST = new InstructionAST();
        ExpressionAST expressionAST = new ExpressionAST();
        public CaseAST()
        {

        }


        #region CASE
        public Switch SENTENCE_CASE(ParseTreeNode actual)
        {
            /*
             *  SENTENCE_CASE.Rule = RESERV_CASE  + LOGIC_EXPRESION + RESERV_OF + CASES + CASE_ELSE + RESERV_END + PUNTO_COMA;

          

            CASE_ELSE.Rule = RESERV_ELSE + INSTRUCTIONS
                | Empty
                ;
             */

            var condicion = expressionAST.getExpresion(actual.ChildNodes[1]);
            ArrayList lista_cases = new ArrayList();
            lista_cases = CASES(actual.ChildNodes[3], lista_cases);

            var else_case = CASE_ELSE(actual.ChildNodes[4]);
            return new Switch(condicion, lista_cases, else_case);
        }

        public ArrayList CASES(ParseTreeNode actual, ArrayList lista_cases)
        {


            /*
               CASES.Rule 
                = CASE + CASES
                | Empty                
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                lista_cases.Add(CaseSing(actual.ChildNodes[0]));

                lista_cases = CASES(actual.ChildNodes[1], lista_cases);
            }

            return lista_cases;
        }

        public Case CaseSing(ParseTreeNode actual)
        {
            /* CASE.Rule = LOGIC_EXPRESION + DOS_PUNTOS + INSTRUCTIONS;*/
            var condicion = expressionAST.getExpresion(actual.ChildNodes[0]);

            var lista_instrucciones = instructionAST.ISTRUCCIONES(actual.ChildNodes[2]);

            return new Case(condicion, new Sentence(lista_instrucciones));

        }

        public Case CASE_ELSE(ParseTreeNode actual)
        {
            /*
             CASE_ELSE.Rule = RESERV_ELSE + INSTRUCTIONS
                | Empty
                ;
             */
            Case _case = new Case();
            if (actual.ChildNodes.Count > 0)
            {
                var lista_declaraciones = instructionAST.ISTRUCCIONES(actual.ChildNodes[1]);
                _case = new Case(new Sentence(lista_declaraciones));
            }

            return _case;
        }
        #endregion

    }
}
