using CompiPascal.Traduccion.grammar.abstracts;
using CompiPascal.Traduccion.grammar.expresion;
using CompiPascal.Traduccion.grammar.sentences;
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


        public LinkedList<Instruction_Trad> LIST_DECLARATIONS(ParseTreeNode actual, 
            LinkedList<Instruction_Trad> lista_actual, ArrayList elementos_her, int cant_tabs)
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
                    var identifier = actual.ChildNodes[1].Token.Text;
                    lista_actual.AddLast(new Declaration_Trad(identifier, "any" ,  expressionAST.getExpresion(actual.ChildNodes[3]), true, cant_tabs, false));
                    lista_actual = CONST_DECLARATION(actual.ChildNodes[5], lista_actual, elementos_her, cant_tabs);
                    lista_actual = LIST_DECLARATIONS(actual.ChildNodes[6], lista_actual, elementos_her, cant_tabs);
                }
                //ES VAR
                else
                {
                    var identifier = actual.ChildNodes[1].Token.Text;
                    elementos_her.Add(identifier);

                    lista_actual = DECLARATION_BODY(actual.ChildNodes[2], lista_actual, elementos_her, cant_tabs);
                    lista_actual = VAR_DECLARATION(actual.ChildNodes[3], lista_actual, elementos_her, cant_tabs);
                    lista_actual = LIST_DECLARATIONS(actual.ChildNodes[4], lista_actual, elementos_her, cant_tabs);

                }

                return lista_actual;


            }


            return lista_actual;
        }

        public LinkedList<Instruction_Trad> DECLARATION_BODY(ParseTreeNode actual, LinkedList<Instruction_Trad> lista_actual, ArrayList elementos_her, int cant_Tabs)
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
                elementos_her.Add(identifier);
                //OBTENGO LOS DEMAS IDENTIFICADORES
                elementos_her = MORE_ID_DECLARATION(actual.ChildNodes[2], elementos_her);
                //OBTENGO EL TIPO
                var datatype = actual.ChildNodes[4].ChildNodes[0].Token.Text;

                foreach (var item in elementos_her)
                {
                    lista_actual.AddLast(GetDeclarationValue(item.ToString(), datatype, false, cant_Tabs, false));
                }
                elementos_her.Clear();

            }
            //SI VIENE UN SOLO ID
            else
            {
                var datatype = actual.ChildNodes[1].ChildNodes[0].Token.Text;
                elementos_her.Add(datatype);
                lista_actual = ASSIGNATION_VARIABLE(actual.ChildNodes[2], lista_actual, elementos_her, cant_Tabs);
            }
            return lista_actual;
        }
        public LinkedList<Instruction_Trad> VAR_DECLARATION(ParseTreeNode actual, 
            LinkedList<Instruction_Trad> lista_actual, ArrayList elementos_her, int cant_tabs)
        {
            /*
               = RESERV_VAR + IDENTIFIER + DECLARATION_BODY + VAR_DECLARATION + DECLARATION_LIST
               ;
             */

            if (actual.ChildNodes.Count > 0)
            {
                var identifier = actual.ChildNodes[0].Token.Text;
                elementos_her.Add(identifier);
                lista_actual = DECLARATION_BODY(actual.ChildNodes[1], lista_actual, elementos_her, cant_tabs);
                lista_actual = VAR_DECLARATION(actual.ChildNodes[2], lista_actual, elementos_her, cant_tabs);

            }

            return lista_actual;
        }
        public LinkedList<Instruction_Trad> CONST_DECLARATION(ParseTreeNode actual, 
            LinkedList<Instruction_Trad> lista_actual, ArrayList elementos_her, int cant_Tabs)
        {
            /*
             *  CONST_DECLARATION.Rule = IDENTIFIER + EQUALS + LOGIC_EXPRESION + PUNTO_COMA + CONST_DECLARATION
                | Empty
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                var identifier = actual.ChildNodes[0].Token.Text;
                lista_actual.AddLast(new Declaration_Trad(identifier, "any",expressionAST.getExpresion(actual.ChildNodes[2]),true, cant_Tabs, false));
                lista_actual = CONST_DECLARATION(actual.ChildNodes[4], lista_actual, elementos_her, cant_Tabs);
            }
            return lista_actual;
        }

        public LinkedList<Instruction_Trad> ASSIGNATION_VARIABLE(ParseTreeNode actual, 
            LinkedList<Instruction_Trad> lista_actual, ArrayList elementos_her, int cant_tabs)
        {
            //VAR A: TIPO = EXP;
            if (actual.ChildNodes.Count > 0)
            {
                var exp = expressionAST.getExpresion(actual.ChildNodes[1]);
                lista_actual.AddLast(new Declaration_Trad(elementos_her[0].ToString(), elementos_her[1].ToString(), exp, false, cant_tabs, false));
                elementos_her.Clear();
            }
            // VAR A:TIPO;
            else
            {
                lista_actual.AddLast(GetDeclarationValue(elementos_her[0].ToString(), elementos_her[1].ToString(), false, cant_tabs, false));
                elementos_her.Clear();
            }
            return lista_actual;
        }

        public ArrayList MORE_ID_DECLARATION(ParseTreeNode actual, ArrayList elementos_her)
        {

            if (actual.ChildNodes.Count > 0)
            {
                var identifier = actual.ChildNodes[1].Token.Text;
                elementos_her.Add(identifier);
                elementos_her = MORE_ID_DECLARATION(actual.ChildNodes[2], elementos_her);
            }
            return elementos_her;
        }
        public Declaration_Trad GetDeclarationValue(string identifier, string datatype, bool perteneceFuncion, int cant_tabs, bool refer)
        {
            if (datatype.Equals("integer"))
            {
                return new Declaration_Trad(identifier.ToString(), datatype, new Literal_Trad("0", 1), false, cant_tabs, refer);
            }
            else if (datatype.Equals("real"))
            {
                return new Declaration_Trad(identifier.ToString(), datatype, new Literal_Trad("0", 4), false, cant_tabs, refer);
            }
            else if (datatype.Equals("string"))
            {
                return new Declaration_Trad(identifier.ToString(), datatype, new Literal_Trad("", 2), false, cant_tabs, refer);
            }
            else if (datatype.Equals("boolean"))
            {
                return new Declaration_Trad(identifier.ToString(), datatype, new Literal_Trad("false", 3), false, cant_tabs, refer);
            }
            return null;
        }

        #endregion
    }
}
