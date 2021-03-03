using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class DeclarationTraduccion
    {

        public DeclarationTraduccion()
        {

        }


        //VARIABLES 
        ExpressionTraduccion expressionAST = new ExpressionTraduccion();


        #region DECLARACION


        public string LIST_DECLARATIONS(ParseTreeNode actual, string lista_actual, ArrayList elementos_her)
        {

            /*
             DECLARATION_LIST.Rule
               = RESERV_VAR + IDENTIFIER + DECLARATION_BODY + VAR_DECLARATION + DECLARATION_LIST
               | Empty
               ;
             */

            if (actual.ChildNodes.Count != 0)
            {


                //VERIFICA SI ES VAR O CONST
                var tipo = actual.ChildNodes[0];

                //ES CONST
                if (tipo.Term.ToString().Equals("RESERV_CONST"))
                {
                    var reserv_const = actual.ChildNodes[0].Token.Text;
                    
                    var identifier = actual.ChildNodes[1].Token.Text;

                    lista_actual = lista_actual + "\n" + reserv_const + " " + identifier + "= " + expressionAST.getExpresion(actual.ChildNodes[3]) + ";";

                    lista_actual = CONST_DECLARATION(actual.ChildNodes[5], lista_actual, elementos_her);
                    lista_actual = LIST_DECLARATIONS(actual.ChildNodes[6], lista_actual, elementos_her);
                }
                //ES VAR
                else
                {
                    var reserv_var = actual.ChildNodes[0].Token.Text;

                    var identifier = actual.ChildNodes[1].Token.Text;
                    
                    elementos_her.Add(reserv_var + " " +identifier);


                    lista_actual = DECLARATION_BODY(actual.ChildNodes[2], lista_actual, elementos_her);
                    lista_actual = VAR_DECLARATION(actual.ChildNodes[3], lista_actual, elementos_her);
                    lista_actual = LIST_DECLARATIONS(actual.ChildNodes[4], lista_actual, elementos_her);

                }

                return lista_actual;


            }


            return lista_actual;
        }

        public string DECLARATION_BODY(ParseTreeNode actual, string lista_actual, ArrayList elementos_her)
        {
            /*
             
              DECLARATION_BODY.Rule
                = DOS_PUNTOS + DATA_TYPE + ASSIGNATION + PUNTO_COMA
                | COMA + IDENTIFIER + MORE_ID + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA
                ;
             */
            var element = actual.ChildNodes[0];
            // SI VIENE VARIOS IDES 
            if (element.Term.ToString().ToLower().Equals("tk_coma"))
            {
                //OBTENGO EL IDENTIFICADOR
                var identifier = actual.ChildNodes[1].Token.Text;
                elementos_her.Add( "," + identifier);
                //OBTENGO LOS DEMAS IDENTIFICADORES
                elementos_her = MORE_ID_DECLARATION(actual.ChildNodes[2], elementos_her);
                //OBTENGO EL TIPO
                var datatype = actual.ChildNodes[4].ChildNodes[0].Token.Text;


                var variables = "";
                foreach (var item in elementos_her)
                {

                    variables = variables + item;
                }
                lista_actual = lista_actual + "\n" + variables + ":" + datatype + ";";

                elementos_her.Clear();

            }
            //SI VIENE UN SOLO ID
            else
            {
                var datatype = actual.ChildNodes[1].ChildNodes[0].Token.Text;
                elementos_her.Add(datatype);
                lista_actual = ASSIGNATION_VARIABLE(actual.ChildNodes[2], lista_actual, elementos_her);
            }
            return lista_actual;
        }
        public string VAR_DECLARATION(ParseTreeNode actual, string lista_actual, ArrayList elementos_her)
        {
            /*
               = RESERV_VAR + IDENTIFIER + DECLARATION_BODY + VAR_DECLARATION + DECLARATION_LIST
               ;
             */

            if (actual.ChildNodes.Count > 0)
            {
                var identifier = actual.ChildNodes[0].Token.Text;
                elementos_her.Add(identifier);
                lista_actual = DECLARATION_BODY(actual.ChildNodes[1], lista_actual, elementos_her);
                lista_actual = VAR_DECLARATION(actual.ChildNodes[2], lista_actual, elementos_her);

            }

            return lista_actual;
        }
        public string CONST_DECLARATION(ParseTreeNode actual, string lista_actual, ArrayList elementos_her)
        {
            /*
             *  CONST_DECLARATION.Rule = IDENTIFIER + EQUALS + LOGIC_EXPRESION + PUNTO_COMA + CONST_DECLARATION
                | Empty
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                var identifier = actual.ChildNodes[0].Token.Text;

                lista_actual =
                    lista_actual + "\n" +
                    identifier + "=" + expressionAST.getExpresion(actual.ChildNodes[2]) + ";";

                lista_actual = CONST_DECLARATION(actual.ChildNodes[4], lista_actual, elementos_her);
            }
            return lista_actual;
        }

        public string ASSIGNATION_VARIABLE(ParseTreeNode actual, string lista_actual, ArrayList elementos_her)
        {
            //VAR A: TIPO = EXP;
            if (actual.ChildNodes.Count > 0)
            {
                var exp = expressionAST.getExpresion(actual.ChildNodes[1]);


                lista_actual = lista_actual
                    + "\n" + elementos_her[0].ToString() + ":" + elementos_her[1].ToString() + "=" + exp + ";";
                elementos_her.Clear();
            }
            // VAR A:TIPO;
            else
            {
                lista_actual = lista_actual
                    + "\n" + elementos_her[0].ToString() + ":" +elementos_her[1].ToString() + ";";
                elementos_her.Clear();
            }
            return lista_actual;
        }

        public ArrayList MORE_ID_DECLARATION(ParseTreeNode actual, ArrayList elementos_her)
        {
            /*
             MORE_ID.Rule = COMA + IDENTIFIER + MORE_ID
                | Empty
                ;

             */
            if (actual.ChildNodes.Count > 0)
            {
                var identifier = actual.ChildNodes[1].Token.Text;
                elementos_her.Add( "," + identifier);
                elementos_her = MORE_ID_DECLARATION(actual.ChildNodes[2], elementos_her);
            }
            return elementos_her;
        }

        #endregion
    }
}
