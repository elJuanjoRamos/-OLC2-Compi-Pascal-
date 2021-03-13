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
        public object VAR_ASSIGNATE(ParseTreeNode actual)
        {
            /*
             VAR_ASSIGNATE.Rule 
                = IDENTIFIER + VAR_ASSIGNATE_EXP
                ;
             
             */
            var identifier = actual.ChildNodes[0].Token.Text;
            var row = actual.ChildNodes[0].Token.Location.Line;
            var column = actual.ChildNodes[0].Token.Location.Column;

            return VAR_ASSIGNATE_EXP(identifier, row, column, actual.ChildNodes[1]);

            

        }


        public object VAR_ASSIGNATE_EXP(string identifier, int row, int column, ParseTreeNode actual)
        {
            /*
             
             VAR_ASSIGNATE_EXP.Rule
                = DOS_PUNTOS + EQUALS + EXPLOGICA + PUNTO_COMA
                | COR_IZQ + EXPLOGICA + COR_DER + DOS_PUNTOS + EQUALS + EXPLOGICA + PUNTO_COMA
                ;
             */
            if (actual.ChildNodes.Count == 4)
            {
                Expression exp = null;


                var encontrado = false;
                for (int i = 0; i < actual.ChildNodes[2].ChildNodes.Count; i++)
                {
                    var a = actual.ChildNodes[2].ChildNodes[i];
                    if (a.Term.ToString().Equals("CALL_FUNCTION_PROCEDURE"))
                    {
                        encontrado = true;
                        break;
                    }
                }
                //SOLO ES UNA EXPRESION
                if (!encontrado)
                {
                    exp = (new ExpressionAST()).getExpresion(actual.ChildNodes[2]);

                    return new Assignation(identifier, exp, row, column);
                }
                //ES UNA LLAMADA
                else
                {
                    var llamada_funcion = (new Call_Expression()).CALLFUNCTION(actual.ChildNodes[2].ChildNodes[0]);
                    return new Assignation(identifier, llamada_funcion, row, column);
                }
            }
            else if (actual.ChildNodes.Count == 7)
            {
                ExpressionAST expressionAST = new ExpressionAST();


                var index  = (expressionAST).getExpresion(actual.ChildNodes[1]);



                Expression exp = null;


                var encontrado = false;
                for (int i = 0; i < actual.ChildNodes[5].ChildNodes.Count; i++)
                {
                    var a = actual.ChildNodes[5].ChildNodes[i];
                    if (a.Term.ToString().Equals("CALL_FUNCTION_PROCEDURE"))
                    {
                        encontrado = true;
                        break;
                    }
                }
                //SOLO ES UNA EXPRESION
                if (!encontrado)
                {
                    exp = (new ExpressionAST()).getExpresion(actual.ChildNodes[5]);

                    return new Assignation_array(identifier, exp, row, column, index);
                }
                //ES UNA LLAMADA
                else
                {
                    var llamada_funcion = (new Call_Expression()).CALLFUNCTION(actual.ChildNodes[5].ChildNodes[0]);
                    return new Assignation_array(identifier, llamada_funcion, row, column, index);
                }
            }

            return null;
        }

        #endregion

    }
}
