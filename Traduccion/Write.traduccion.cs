﻿using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class WriteTraduccion
    {
        //VARIABLES
        ExpressionTraduccion expressionAST = new ExpressionTraduccion();

        public WriteTraduccion()
        {

        }




        #region WRITE
        public string getWrite(ParseTreeNode actual, int cant_tabs)
        {
            var write_instruction = actual.ChildNodes[0].Token.Text;

            var WRHITE_PARAMETER = WRITES(actual.ChildNodes[2]);

            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + "  ";
            }
            
            return tabs + write_instruction + "(" + WRHITE_PARAMETER + ");\n";
        }



        public string WRITES(ParseTreeNode actual)
        {
            string list = "";
            if (actual.ChildNodes.Count > 0)
            {
                var exp = expressionAST.getExpresion(actual.ChildNodes[0]);
                list = list + exp;
                list = WRHITE_PARAMETER(actual.ChildNodes[1], list);

            }
            return list;
        }
        public string WRHITE_PARAMETER(ParseTreeNode actual, string list)
        {
            if (actual.ChildNodes.Count > 0)
            {
                var exp = expressionAST.getExpresion(actual.ChildNodes[1]);
                list = list + exp;
                list = WRHITE_PARAMETER(actual.ChildNodes[2], list);

            }
            return list;
        }

        #endregion
    }
}