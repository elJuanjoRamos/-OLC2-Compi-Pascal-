using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class AssignationTraduction
    {
        public AssignationTraduction() { }


        #region ASIGNACION
        public string VAR_ASSIGNATE(ParseTreeNode actual)
        {
            //VAR_ASSIGNATE.Rule = IDENTIFIER + DOS_PUNTOS + EQUALS + LOGIC_EXPRESION + PUNTO_COMA;
            var identifier = actual.ChildNodes[0].Token.Text  + " := ";



            var exp = "";


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
                exp = (new ExpressionTraduccion()).getExpresion(actual.ChildNodes[3]) + ";";
            }
            //ES UNA LLAMADA
            else
            {
                //var llamada_funcion = (new Call_Expression()).CALLFUNCTION(actual.ChildNodes[3].ChildNodes[0]);
                //return new Assignation(identifier, llamada_funcion);
            }

            return identifier + exp +"\n";

        }

        #endregion

    }
}
