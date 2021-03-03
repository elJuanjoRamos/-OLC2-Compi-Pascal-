using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class ForTraduccion
    {
        //VARIABLES
        InstructionTraduccion instructionAST = new InstructionTraduccion();
        ExpressionTraduccion expressionAST = new ExpressionTraduccion();

        public ForTraduccion()
        {

        }

        #region FOR
        public string SENCECIA_FOR(ParseTreeNode actual, int cant_tabs)
        {
            /*
             FOR.Rule
                = RESERV_FOR + IDENTIFIER + DOS_PUNTOS + EQUALS + LOGIC_EXPRESION + TODOWN + LOGIC_EXPRESION
                    + RESERV_DO
                        + INSTRUCTIONS_BODY //+ PUNTO_COMA
                ;

            TODOWN.Rule 
                = RESERV_TO
                | RESERV_DOWN + RESERV_TO
                ;
             */

            var reserv_for = actual.ChildNodes[0].Token.Text;

            var ident = actual.ChildNodes[1].Token.Text;
            
            var inicio = expressionAST.getExpresion(actual.ChildNodes[4]);
            var direccion = actual.ChildNodes[5].ChildNodes[0].Token.Text;
            var fin = expressionAST.getExpresion(actual.ChildNodes[6]);
            var lista_instrucciones = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[8], cant_tabs + 1);

            var reserv_do = actual.ChildNodes[7].Token.Text;

            //cantidad tabs
            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + " ";
            }


            var for_total =
                tabs + reserv_for + " " + ident + " := " + inicio + " " + direccion + " " + fin + " " + " " + reserv_do + "\n"
                + lista_instrucciones;

            return for_total;

        }
        #endregion
    }
}
