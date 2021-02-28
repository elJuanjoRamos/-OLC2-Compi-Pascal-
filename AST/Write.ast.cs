using CompiPascal.grammar.abstracts;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class WriteAST
    {
        //VARIABLES
        ExpressionAST expressionAST = new ExpressionAST();
        public WriteAST()
        {

        }

        #region WRITE
        public LinkedList<Expression> WRITES(ParseTreeNode actual)
        {
            LinkedList<Expression> list = new LinkedList<Expression>();
            if (actual.ChildNodes.Count > 0)
            {
                var exp = expressionAST.getExpresion(actual.ChildNodes[0]);
                list.AddLast(exp);
                list = WRHITE_PARAMETER(actual.ChildNodes[1], list);

            }
            return list;
        }
        public LinkedList<Expression> WRHITE_PARAMETER(ParseTreeNode actual, LinkedList<Expression> list)
        {
            if (actual.ChildNodes.Count > 0)
            {
                var exp = expressionAST.getExpresion(actual.ChildNodes[1]);
                list.AddLast(exp);
                list = WRHITE_PARAMETER(actual.ChildNodes[2], list);

            }
            return list;
        }
        #endregion

    }
}
