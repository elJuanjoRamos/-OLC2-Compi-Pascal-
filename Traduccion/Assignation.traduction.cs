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
        public string VAR_ASSIGNATE(ParseTreeNode actual, int cant_tabs)
        {

            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + "  ";
            }

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
                exp = (new ExpressionTraduccion()).getExpresion(actual.ChildNodes[3]);
            }
            //ES UNA LLAMADA
            else
            {
                exp = (new Call_Exp_Traduccion()).CALLFUNCTION(actual.ChildNodes[3].ChildNodes[0]);
            }

            return tabs + identifier + exp +"\n";

        }

        #endregion

    }
}
