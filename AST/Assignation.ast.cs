using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class AssignationAST
    {
        public AssignationAST()
        {

        }


        #region ASIGNACION
        public Assignation VAR_ASSIGNATE(ParseTreeNode actual)
        {
            //VAR_ASSIGNATE.Rule = IDENTIFIER + DOS_PUNTOS + EQUALS + LOGIC_EXPRESION + PUNTO_COMA;
            var identifier = actual.ChildNodes[0].Token.Text;

            Expression exp = null;


            var encontrado = false;
            for (int i = 0; i < actual.ChildNodes[3].ChildNodes.Count; i++)
            {
                var a = actual.ChildNodes[3].ChildNodes[i];
                if (a.Term.ToString().Equals("CALL_FUNCTION_PROCEDURE"))
                {
                    encontrado = true;
                    break;
                }
            }
            //SOLO ES UNA EXPRESION
            if (!encontrado)
            {
                exp = (new ExpressionAST()).getExpresion(actual.ChildNodes[3]);

                return new Assignation(identifier, exp);
            }
            //ES UNA LLAMADA
            else
            {
                var llamada_funcion = (new Call_Expression()).CALLFUNCTION(actual.ChildNodes[3].ChildNodes[0]);
                return new Assignation(identifier, llamada_funcion);
            }

        }

        #endregion

    }
}
