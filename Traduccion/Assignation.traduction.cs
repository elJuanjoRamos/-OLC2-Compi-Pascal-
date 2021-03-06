using CompiPascal.Traduccion.grammar.abstracts;
using CompiPascal.Traduccion.sentences;
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
        public Assignation_Trad VAR_ASSIGNATE(ParseTreeNode actual, int cant_tabs)
        {
            //VAR_ASSIGNATE.Rule = IDENTIFIER + DOS_PUNTOS + EQUALS + LOGIC_EXPRESION + PUNTO_COMA;
            var identifier = actual.ChildNodes[0].Token.Text;

            Expresion_Trad exp = null;


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

                return new Assignation_Trad(identifier, exp,cant_tabs);
            }
            //ES UNA LLAMADA
            else
            {
                var llamada_funcion = (new Call_Exp_Traduccion()).CALLFUNCTION(actual.ChildNodes[3].ChildNodes[0]);
                return new Assignation_Trad(identifier, llamada_funcion, cant_tabs);
            }

        }

        #endregion

    }
}
