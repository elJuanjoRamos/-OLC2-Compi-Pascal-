using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.expression;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class ExpressionAST
    {
        public ExpressionAST()
        {

        }


        #region EXPRESIONES


        public Expression getExpresion(ParseTreeNode actual)
        {

            return EXPLOGICA(actual);

        }

        public Expression GetLiteral(ParseTreeNode node)
        {
            var row = node.Token.Location.Line;
            var column = node.Token.Location.Column;

            if (node.Term.ToString().ToString().Equals("NUMERO"))
            {
                return new Literal(node.Token.Value, 1, row, column);
            }
            else if (node.Term.ToString().Equals("CADENA"))
            {
                return new Literal(node.Token.Value, 2, row, column);
            }
            else if (node.Term.ToString().Equals("RESERV_TRUE") || node.Term.ToString().Equals("RESERV_FALSE"))
            {
                return new Literal(node.Token.Value, 3, row, column);
            }
            else if (node.Term.ToString().Equals("REAL"))
            {
                return new Literal(node.Token.Value, 4, row, column);
            }
            else if (node.Term.ToString().Equals("TYPE"))
            {
                return new Literal(node.Token.Value, 5, row, column);
            }
            else if (node.Term.ToString().Equals("ARRAY"))
            {
                return new Literal(node.Token.Value, 6, row, column);
            }
            else if (node.Term.ToString().Equals("IDENTIFIER"))
            {
                return new Access(node.Token.Value.ToString(),  row, column);
            }

            return null;
        }
        #endregion



        public Expression EXPLOGICA(ParseTreeNode actual)
        {
            /*
              EXPLOGICA.Rule 
                = EXPRELACIONAL + EXPLOGICA_PRIMA
                | NOT + EXPRELACIONAL + EXPLOGICA_PRIMA;
             */

            if (actual.ChildNodes.Count == 2)
            {
                var relacional = EXPRELACIONAL(actual.ChildNodes[0]);
                return EXPLOGICA_PRIMA(actual.ChildNodes[1], relacional);
            } else
            {
                var not = actual.ChildNodes[0].Token.Text.ToLower();
                var izq = EXPRELACIONAL(actual.ChildNodes[1]);
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                var relacional = new Logical(izq, null, not,row,col);
                return EXPLOGICA_PRIMA(actual.ChildNodes[2], relacional);
            }

        }

        public Expression EXPLOGICA_PRIMA(ParseTreeNode actual, Expression izq)
        {
            /*
              EXPLOGICA_PRIMA.Rule
                = AND + EXPRELACIONAL + EXPLOGICA_PRIMA
                | OR + EXPRELACIONAL + EXPLOGICA_PRIMA
                | Empty
                ;
             */

            if (actual.ChildNodes.Count > 0)
            {
                var simb = actual.ChildNodes[0].Token.Text.ToLower();
                var derecho = EXPRELACIONAL(actual.ChildNodes[1]);
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                var relacional = new Logical(izq, derecho, simb, row, col);
                return EXPLOGICA_PRIMA(actual.ChildNodes[2], relacional);
            }
            return izq;
        }



        public Expression EXPRELACIONAL(ParseTreeNode actual)
        {
            //EXPRELACIONAL.Rule = EXPRESION + EXPRELACIONAL_PRIMA;

            var exp = EXPRESION(actual.ChildNodes[0]);

            return EXPRELACIONAL_PRIMA(actual.ChildNodes[1], exp);
        }

        public Expression EXPRESION(ParseTreeNode actual)
        {
            //EXPRESION.Rule = TERMINO + EXPRESION_PRIMA;
            var exp = TERMINO(actual.ChildNodes[0]);
            return EXPRESION_PRIMA(actual.ChildNodes[1], exp);

        }

        public Expression EXPRELACIONAL_PRIMA(ParseTreeNode actual, Expression izq)
        {
            /*
              EXPRELACIONAL_PRIMA.Rule
                = LESS + EXPRESION + EXPRELACIONAL_PRIMA
                | HIGHER + EXPRESION + EXPRELACIONAL_PRIMA
                | LESS_EQUAL + EXPRESION + EXPRELACIONAL_PRIMA
                | HIGHER_EQUAL + EXPRESION + EXPRELACIONAL_PRIMA
                | EQUALS + EXPRESION + EXPRELACIONAL_PRIMA
                | DISCTINCT + EXPRESION + EXPRELACIONAL_PRIMA
                | Empty
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                var simb = actual.ChildNodes[0].Token.Text;

                var derecho = EXPRESION(actual.ChildNodes[1]);

                int row = actual.ChildNodes[0].Token.Location.Line;
                int col = actual.ChildNodes[0].Token.Location.Column;

                var relacional = new Relational(izq, derecho, simb,row, col);


                return EXPRELACIONAL_PRIMA(actual.ChildNodes[2], relacional);


            }
            return izq;
        }


        public Expression TERMINO(ParseTreeNode actual)
        {
            //TERMINO.Rule = FACTOR + TERMINO_PRIMA;
            var fac = FACTOR(actual.ChildNodes[0]);
            return TERMINO_PRIMA(actual.ChildNodes[1], fac);

        }

        public Expression EXPRESION_PRIMA(ParseTreeNode actual, Expression izq)
        {
            /*
             EXPRESION_PRIMA.Rule
                = PLUS + TERMINO + EXPRESION_PRIMA
                | MIN + TERMINO + EXPRESION_PRIMA
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                var simb = actual.ChildNodes[0].Token.Text;

                var derecho = TERMINO(actual.ChildNodes[1]);
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                var aritmetica = new Arithmetic(izq, derecho, simb, row, col);

                return EXPRESION_PRIMA(actual.ChildNodes[2], aritmetica);

            }
            return izq;
        }

        public Expression TERMINO_PRIMA(ParseTreeNode actual, Expression izq)
        {
            /*
             TERMINO_PRIMA.Rule
                = POR + FACTOR + TERMINO_PRIMA
                | DIVI + FACTOR + TERMINO_PRIMA
                | MODULE + FACTOR + TERMINO_PRIMA
                | Empty
                ;
             */

            if (actual.ChildNodes.Count > 0)
            {
                var simb = actual.ChildNodes[0].Token.Text;
                var derecho = FACTOR(actual.ChildNodes[1]);
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                var aritmetica = new Arithmetic(izq, derecho, simb, row, col);
                return TERMINO_PRIMA(actual.ChildNodes[2], aritmetica);
            }
            return izq;
        }

        public Expression FACTOR(ParseTreeNode actual)
        {

            /*
             FACTOR.Rule
                = PAR_IZQ + EXPLOGICA + PAR_DER
                | REAL
                | CADENA
                | NUMERO
                | IDENTIFIER
                | RESERV_TRUE
                | RESERV_FALSE
                | CALL_FUNCTION_PROCEDURE
                | MIN + FACTOR
                ;
             */


           
           if (actual.ChildNodes.Count == 3)
           {
               var izq = actual.ChildNodes[0];

                return getExpresion(actual.ChildNodes[1]);

               /* if (izq.Term.ToString().ToLower().Contains("tk"))
               {
                  
               }
               //SI NO LO ES, ES PORQUE VIENE UNA EXPRESION COMUN
               else
               {
                   var simb = actual.ChildNodes[1].Token.Text;
                   var opcion = 0;


                   if (simb.Equals("+") || simb.Equals("-") || simb.Equals("*") || simb.Equals("/") || simb.Equals("%"))
                   {
                       opcion = 1;
                   }
                   else if (simb.Equals("<") || simb.Equals("<=") || simb.Equals(">") || simb.Equals(">=") || simb.Equals("=") || simb.Equals("<>"))
                   {
                       opcion = 2;
                   }
                   else if (simb.Equals("and") || simb.Equals("or") || simb.Equals("not"))
                   {
                       opcion = 3;
                   }
                   var iz = getExpresion(actual.ChildNodes[0]);
                   var der = getExpresion(actual.ChildNodes[2]);

                   switch (opcion)
                   {
                       case 1: //OPERACIONES ARITMETICAS
                           return new Arithmetic(iz, der, simb);
                       case 2: //OPERACIONES RELACIONALES
                           return new Relational(iz, der, simb, 0, 0);
                       case 3: //OPREACIONES LOGICAS
                           return new Logical(iz, der, simb, 0, 0);
                       default:
                           break;
                   }

                   return null;
               }*/
           }
           else if (actual.ChildNodes.Count == 2)
           {
               var simb = actual.ChildNodes[0].Token.Text;


               if (simb.Equals("-"))
               {
                    var iz = FACTOR(actual.ChildNodes[1]);
                    var row = actual.ChildNodes[0].Token.Location.Line;
                    var col = actual.ChildNodes[0].Token.Location.Column;

                    return new Arithmetic(iz, new Literal("-1", 1, row, col), "*", row, col);
               }
           }
           else
           {
               //verifica que no sea una llamada a funcion
               var a = actual.ChildNodes[0].Term;
               if (a.ToString().Equals("CALL_FUNCTION_PROCEDURE"))
               {
                   return (new Call_Expression()).CALLFUNCTION(actual.ChildNodes[0]);
               }
               else
               {
                   return GetLiteral(actual.ChildNodes[0]);
               }

           }
            return null;
        }
    }
}
