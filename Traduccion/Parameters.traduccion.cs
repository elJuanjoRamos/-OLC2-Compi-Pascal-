﻿using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class ParametersTraduccion
    {
        //VARIABLES
        ExpressionTraduccion expressionTraduccion = new ExpressionTraduccion();
        public ParametersTraduccion()
        {

        }
        public string CALL_PARAMETERS(ParseTreeNode actual, string expresiones)
        {
            /*
             CALL_PARAMETERS.Rule
                = EXPRESION + CALL_PARAMETERS
                | COMA + EXPRESION + CALL_PARAMETERS 
                | Empty
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                if (actual.ChildNodes.Count == 2)
                {

                    var expr = (expressionTraduccion.getExpresion(actual.ChildNodes[0]));
                    expresiones = expresiones + expr;
                    expresiones = CALL_PARAMETERS(actual.ChildNodes[1], expresiones);
                }

                else
                {
                    var expr = expressionTraduccion.getExpresion(actual.ChildNodes[1]);

                    expresiones = expresiones + "," + expr;

                    expresiones = CALL_PARAMETERS(actual.ChildNodes[2], expresiones);
                }


            }
            return expresiones;
        }
    }
}