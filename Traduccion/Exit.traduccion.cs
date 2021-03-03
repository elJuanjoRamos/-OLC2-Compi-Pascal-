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

        public string getExit(ParseTreeNode actual)
        {
            var retorno = (new ExpressionTraduccion()).getExpresion(actual.ChildNodes[2]);

            return retorno;

        }
    }
}
