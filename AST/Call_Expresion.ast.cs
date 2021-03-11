using CompiPascal.grammar.expression;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class Call_Expression
    {
        public Call_Expression()
        {

        }


        public CallFunction CALLFUNCTION(ParseTreeNode actual)
        {
            // CALL.Rule = IDENTIFIER + PAR_IZQ + CALL_PARAMETERS + PAR_DER + PUNTO_COMA;
            ArrayList prametros_llamada = new ArrayList();
            prametros_llamada = (new ParametersAST()).CALL_PARAMETERS(actual.ChildNodes[2], prametros_llamada);

            var identifier = actual.ChildNodes[0].Token.Text;
            var row = actual.ChildNodes[0].Token.Location.Line;
            var column = actual.ChildNodes[0].Token.Location.Column;

            return new CallFunction(identifier, prametros_llamada, row, column);
        }
    }
}
