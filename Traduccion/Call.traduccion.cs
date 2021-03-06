using CompiPascal.Traduccion.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class CallTraduccion
    {
        public CallTraduccion()
        {

        }


        public Call_Trad CALL(ParseTreeNode actual, int cant_tabs)
        {
            // CALL.Rule = IDENTIFIER + PAR_IZQ + CALL_PARAMETERS + PAR_DER + PUNTO_COMA;
            ArrayList prametros_llamada = new ArrayList();

            prametros_llamada = ((new ParametersTraduccion())).CALL_PARAMETERS(actual.ChildNodes[2], prametros_llamada);

            return new Call_Trad(actual.ChildNodes[0].Token.Text, prametros_llamada, cant_tabs);
        }
    }
}
