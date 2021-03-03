using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class Call_Exp_Traduccion
    {
        public Call_Exp_Traduccion()
        {

        }



        public string CALLFUNCTION(ParseTreeNode actual)
        {
            // CALL.Rule = IDENTIFIER + PAR_IZQ + CALL_PARAMETERS + PAR_DER + PUNTO_COMA;
            var identifier = actual.ChildNodes[0].Token.Text; 
           
            var prametros_llamada = "";
            prametros_llamada = (new ParametersTraduccion()).CALL_PARAMETERS(actual.ChildNodes[2], prametros_llamada);

            return identifier + "(" + prametros_llamada + ");";
        }
    }
}
