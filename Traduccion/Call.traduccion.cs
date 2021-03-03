using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class CallTraduccion
    {
        public CallTraduccion()
        {

        }


        public string CALL(ParseTreeNode actual)
        {
            // CALL.Rule = IDENTIFIER + PAR_IZQ + CALL_PARAMETERS + PAR_DER + PUNTO_COMA;

            var ident = actual.ChildNodes[0].Token.Text;

            string prametros_llamada = "";
            
            prametros_llamada = (new ParametersTraduccion()).CALL_PARAMETERS(actual.ChildNodes[2], prametros_llamada);

            return ident + "(" + prametros_llamada + ");";
        }
    }
}
