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

            if (actual.ChildNodes.Count == 3)
            {
                // VERIFICA SI EL LADO IZQUIERDO ES PARENTESIS OSEA ( EXPRESION )
                var izq = actual.ChildNodes[0];


                if (izq.Term.ToString().ToLower().Contains("tk"))
                {
                    return getExpresion(actual.ChildNodes[1]);
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
                }
            }
            else if (actual.ChildNodes.Count == 2)
            {
                var simb = actual.ChildNodes[0].Token.Text;


                if (simb.Equals("-"))
                {
                    var iz = getExpresion(actual.ChildNodes[1]);
                    return new Arithmetic(iz, new Literal("-1", 1), "*");
                }
                else
                {
                    var iz = GetLiteral(actual.ChildNodes[1].ChildNodes[0]);
                    return new Logical(iz, null, simb, 0, 0);
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

        }

        public Expression GetLiteral(ParseTreeNode node)
        {

            if (node.Term.ToString().ToString().Equals("NUMERO"))
            {
                return new Literal(node.Token.Value, 1);
            }
            else if (node.Term.ToString().Equals("CADENA"))
            {
                return new Literal(node.Token.Value, 2);
            }
            else if (node.Term.ToString().Equals("RESERV_TRUE") || node.Term.ToString().Equals("RESERV_FALSE"))
            {
                return new Literal(node.Token.Value, 3);
            }
            else if (node.Term.ToString().Equals("REAL"))
            {
                return new Literal(node.Token.Value, 4);
            }
            else if (node.Term.ToString().Equals("TYPE"))
            {
                return new Literal(node.Token.Value, 5);
            }
            else if (node.Term.ToString().Equals("ARRAY"))
            {
                return new Literal(node.Token.Value, 6);
            }
            else if (node.Term.ToString().Equals("IDENTIFIER"))
            {
                return new Access(node.Token.Value.ToString());
            }

            return null;
        }
        #endregion

    }
}
