using CompiPascal.Traduccion.grammar.abstracts;
using CompiPascal.Traduccion.grammar.sentences;
using Irony.Parsing;
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




        public Write_Trad getWrite(ParseTreeNode actual, int cant_tabs)
        {
            
            var WRHITE_PARAMETER = WRITES(actual.ChildNodes[2]);
            var isln = false;
            if (actual.ChildNodes[0].Term.ToString().Equals("RESERV_WRITEN"))
            {
                isln = true;
            }

            return new Write_Trad(WRHITE_PARAMETER, isln, cant_tabs);
        }


        #region WRITE
        public LinkedList<Expresion_Trad> WRITES(ParseTreeNode actual)
        {
            LinkedList<Expresion_Trad> list = new LinkedList<Expresion_Trad>();
            if (actual.ChildNodes.Count > 0)
            {
                var exp = expressionAST.getExpresion(actual.ChildNodes[0]);
                list.AddLast(exp);
                list = WRHITE_PARAMETER(actual.ChildNodes[1], list);

            }
            return list;
        }
        public LinkedList<Expresion_Trad> WRHITE_PARAMETER(ParseTreeNode actual, LinkedList<Expresion_Trad> list)
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