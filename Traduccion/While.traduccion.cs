﻿using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class WhileTraduccion
    {
        public WhileTraduccion()
        {

        }

        #region WHILE
        public string WHILE(ParseTreeNode actual, int cantidad_tab)
        {



            var tabs = "";
            for (int i = 0; i < cantidad_tab; i++)
            {
                tabs = tabs + "  ";
            }

            //WHILE.Rule = RESERV_WHILE + LOGIC_EXPRESION + RESERV_DO + INSTRUCTIONS_BODY;

            var reserv_while = actual.ChildNodes[0].Token.Text;

            var condition = (new ExpressionTraduccion()).getExpresion(actual.ChildNodes[1]);

            var reserv_do = actual.ChildNodes[2].Token.Text;


            
            var lista_instrucciones = (new InstructionTraduccion()).INSTRUCTIONS_BODY(actual.ChildNodes[3], cantidad_tab);

            var while_total =
                tabs + reserv_while + " " + condition + " " + reserv_do + "\n" +
                lista_instrucciones +";\n";


            return while_total;

        }

        #endregion

    }
}
