using CompiPascal.Traduccion.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class ExitTraduccion
    {
        public ExitTraduccion()
        {

        }

        public Exit_Trad getExit(ParseTreeNode actual, int cant_Tabs)
        {
            if (actual.ChildNodes.Count != 0)
            {
                var exp = (new ExpressionTraduccion()).getExpresion(actual);

                return new Exit_Trad(exp, cant_Tabs);
            }

            return new Exit_Trad(cant_Tabs);
        }
    }
}
