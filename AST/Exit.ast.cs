using CompiPascal.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class ExitAst
    {
        //variables


        public ExitAst()
        {

        }


        public Exit getExit(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count != 0)
            {
                var exp = (new ExpressionAST()).getExpresion(actual.ChildNodes[0]);
                return new Exit(exp);
            }

            return new Exit();
        }
    }
}
