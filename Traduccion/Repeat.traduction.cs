using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class RepeatTraduction
    {
        public RepeatTraduction()
        {

        }

        #region REPEAT UTIL
        public string REPEAT_UNTIL(ParseTreeNode actual, int cant_tabs)
        {
            //REPEAT_UNTIL.Rule = RESERV_REPEAT + INSTRUCTIONS + RESERV_UNTIL + LOGIC_EXPRESION + PUNTO_COMA;

            var reserv_repeat = actual.ChildNodes[0].Token.Text + "\n";


            //SE OBTIENEN LOS VALORES
            var lista_instrucciones = (new InstructionTraduccion()).ISTRUCCIONES(actual.ChildNodes[1], cant_tabs);


            var reserv_util = actual.ChildNodes[2].Token.Text;
            
            var condicion = (new ExpressionTraduccion()).getExpresion(actual.ChildNodes[3]);


            //OBTENGO LA LISTA DE INSTRUCCIONES

            var repeat_total =
                reserv_repeat + "\n" +
                lista_instrucciones + "\n"+
                reserv_util + " " + condicion + ";"
                ;



            return repeat_total;

        }
        #endregion

    }
}
