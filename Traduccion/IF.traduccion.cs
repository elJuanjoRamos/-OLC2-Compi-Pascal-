using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class IFTraduccion
    {
        //VARIABLES
        InstructionTraduccion instrucciones = new InstructionTraduccion();
        public IFTraduccion()
        {

        }



        #region IF



        public string IFTHEN(ParseTreeNode actual, int cantidad_tabs)
        {
            /*
              IFTHEN.Rule
                = RESERV_IF + EXPRESION
                    + RESERV_THEN
                        + IF_SENTENCE
                    + ELIF;
             */

            var if_sentencia = actual.ChildNodes[0].Token.Text;



            ExpressionTraduccion expressionAST = new ExpressionTraduccion();

            var condicion = expressionAST.getExpresion(actual.ChildNodes[1]);


            var reserv_then = actual.ChildNodes[2].Token.Text;



            var IF_Sentences = IF_SENTENCE(actual.ChildNodes[3], cantidad_tabs);
            
            var ELSE = ELIF(actual.ChildNodes[4], cantidad_tabs);


            var if_total =
                if_sentencia+ " " + condicion + " " + reserv_then + "\n" +
                IF_Sentences + "\n" +
                ELSE;

            return  if_total;
        }

        public string IF_SENTENCE(ParseTreeNode actual, int cant_tabs)
        {
            /*
               IF_SENTENCE.Rule = INSTRUCTIONS_BODY
                | Empty
                ;

             */
            var lista_instrucciones = "";
            if (actual.ChildNodes.Count > 0)
            {


                lista_instrucciones = (instrucciones).INSTRUCTIONS_BODY(actual.ChildNodes[0], cant_tabs);
                
            }

            return lista_instrucciones;
        }
        public string ELIF(ParseTreeNode actual, int cant_tabs)
        {
            var lista_instrucciones = "";
            if (actual.ChildNodes.Count > 0)
            {
                // ELSE 
                if (actual.ChildNodes[1].Term.ToString().Equals("IF_SENTENCE"))
                {
                    lista_instrucciones = " else " + "\n" + instrucciones.INSTRUCTIONS_BODY(actual.ChildNodes[1].ChildNodes[0], cant_tabs);

                }
                // ELSE IF
                else
                {
                    var ifs = " else " + IFTHEN(actual.ChildNodes[1], cant_tabs);
                    lista_instrucciones = lista_instrucciones +"\n" + ifs;
                }
            }
            return lista_instrucciones;
        }
        #endregion



    }
}
