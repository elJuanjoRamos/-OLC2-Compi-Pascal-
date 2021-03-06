using CompiPascal.Traduccion.grammar.abstracts;
using CompiPascal.Traduccion.grammar.expresion;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class ExpressionTraduccion
    {
        public ExpressionTraduccion()
        {

        }




        #region EXPRESIONES
        public Expresion_Trad getExpresion(ParseTreeNode actual)
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
                            return new Arithmetic_Trad(iz, der, simb);
                        case 2: //OPERACIONES RELACIONALES
                            return new Relational_Trad(iz, der, simb);
                        case 3: //OPREACIONES LOGICAS
                            return new Logical_Trad(iz, der, simb, 0, 0);
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
                    return new Arithmetic_Trad(iz, new Literal_Trad("-1",1), "*");
                }
                else
                {
                    var iz = GetLiteral(actual.ChildNodes[1].ChildNodes[0]);
                    return new Logical_Trad(iz, null, simb, 0, 0);
                }



            }
            else
            {
                //verifica que no sea una llamada a funcion
                var a = actual.ChildNodes[0].Term;
                if (a.ToString().Equals("CALL_FUNCTION_PROCEDURE"))
                {
                    return (new Call_Exp_Traduccion()).CALLFUNCTION(actual.ChildNodes[0]);
                }
                else
                {
                    return GetLiteral(actual.ChildNodes[0]);
                }

            }

        }

        public Expresion_Trad GetLiteral(ParseTreeNode node)
        {

            if (node.Term.ToString().ToString().Equals("NUMERO"))
            {
                return new Literal_Trad(node.Token.Value.ToString(), 1);
            }
            else if (node.Term.ToString().Equals("CADENA"))
            {
                return new Literal_Trad("'" + node.Token.Value.ToString() + "'", 2);
            }
            else if (node.Term.ToString().Equals("RESERV_TRUE") || node.Term.ToString().Equals("RESERV_FALSE"))
            {
                return new Literal_Trad(node.Token.Value.ToString(), 3);
            }
            else if (node.Term.ToString().Equals("REAL"))
            {
                return new Literal_Trad(node.Token.Value.ToString(), 4);
            }
            else if (node.Term.ToString().Equals("TYPE"))
            {
                return new Literal_Trad(node.Token.Value.ToString(), 5);
            }
            else if (node.Term.ToString().Equals("ARRAY"))
            {
                return new Literal_Trad(node.Token.Value.ToString(), 6);
            }
            else if (node.Term.ToString().Equals("IDENTIFIER"))
            {
                return new Literal_Trad(node.Token.Value.ToString(), 7);
            }

            return null;
        }
        #endregion

    }
}
