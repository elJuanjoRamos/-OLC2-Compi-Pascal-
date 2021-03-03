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
        public string getExpresion(ParseTreeNode actual)
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
                    var iz = getExpresion(actual.ChildNodes[0]);
                    var der = getExpresion(actual.ChildNodes[2]);

                    return iz + " " + simb + " " + der;
                }
            }
            else if (actual.ChildNodes.Count == 2)
            {
                var simb = actual.ChildNodes[0].Token.Text;


                if (simb.Equals("-"))
                {
                    var iz = getExpresion(actual.ChildNodes[1]);
                    return "-" + iz;
                }
                else
                {
                    var iz = GetLiteral(actual.ChildNodes[1].ChildNodes[0]);

                    return "!" + iz;
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

            return "";

        }

        public string GetLiteral(ParseTreeNode node)
        {

            if (node.Term.ToString().ToString().Equals("NUMERO"))
            {
                return node.Token.Value.ToString();
            }
            else if (node.Term.ToString().Equals("CADENA"))
            {
                return  "'"+node.Token.Value.ToString()+"'";
            }
            else if (node.Term.ToString().Equals("RESERV_TRUE") || node.Term.ToString().Equals("RESERV_FALSE"))
            {
                return node.Token.Value.ToString();
            }
            else if (node.Term.ToString().Equals("REAL"))
            {
                return node.Token.Value.ToString();
            }
            else if (node.Term.ToString().Equals("TYPE"))
            {
                return node.Token.Value.ToString();
            }
            else if (node.Term.ToString().Equals("ARRAY"))
            {
                return node.Token.Value.ToString();
            }
            else if (node.Term.ToString().Equals("IDENTIFIER"))
            {
                return node.Token.Value.ToString();
            }

            return "";
        }
        #endregion

    }
}
