using CompiPascal.Traduccion.grammar.expresion;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class Call_Exp_Traduccion
    {
        public Call_Exp_Traduccion()
        {

        }




        public CallFunction_Trad CALLFUNCTION(ParseTreeNode actual)
        {
            // CALL.Rule = IDENTIFIER + PAR_IZQ + CALL_PARAMETERS + PAR_DER + PUNTO_COMA;
            ArrayList prametros_llamada = new ArrayList();
            prametros_llamada = (new ParametersTraduccion()).CALL_PARAMETERS(actual.ChildNodes[2], prametros_llamada);

            return new CallFunction_Trad(actual.ChildNodes[0].Token.Text, prametros_llamada);
        }
    }
}
