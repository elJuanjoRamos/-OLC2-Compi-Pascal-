using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class FunctionAST
    {

        //variables
        InstructionAST instructionAST = new InstructionAST();
        DeclarationAST declarationAST = new DeclarationAST();

        public FunctionAST()
        {

        }

        #region FUNCIONES


        public LinkedList<Instruction> FUNCTION_LIST(ParseTreeNode actual, LinkedList<Instruction> lista_funciones, ArrayList elementos_her)
        {
            /*
              FUNCTION_LIST.Rule
                = RESERV_FUNCTION + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA 
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                + FUNCTION_LIST
                | Empty
                ;
             */
            //EMPTY
            if (actual.ChildNodes.Count > 0)
            {


                var tipo = actual.ChildNodes[0].Term.ToString();

                if (tipo.Equals("RESERV_PROCEDURE"))
                {
                    lista_funciones = getProcedimientos(actual, lista_funciones, elementos_her);
                }
                else
                {
                    lista_funciones = getFunciones(actual, lista_funciones, elementos_her);
                }


            }
            return lista_funciones;
        }

        public LinkedList<Instruction> getFunciones(ParseTreeNode actual, LinkedList<Instruction> lista_funciones, ArrayList elementos_her)
        {
            /*
              FUNCTION_LIST.Rule
                = RESERV_FUNCTION + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA 
                + DECLARATION_LIST_HIJA
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                + FUNCTION_LIST
                | Empty
                ;
             */

            LinkedList<Instruction> parametros = new LinkedList<Instruction>();
            LinkedList<Instruction> declaraciones = new LinkedList<Instruction>();

            var identifier = actual.ChildNodes[1].Token.Text;

            parametros = PARAMETER(actual.ChildNodes[3], parametros, elementos_her);


            
            var function_type = actual.ChildNodes[6].ChildNodes[0].Token.Text;

            declaraciones = declarationAST.LIST_DECLARATIONS(actual.ChildNodes[8], declaraciones, new ArrayList());


            var function_instructions = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[9]);

            lista_funciones.AddLast(new Function(identifier, parametros, declaraciones,  function_type, new Sentence(function_instructions), false));

            elementos_her.Clear();
            lista_funciones = FUNCTION_LIST(actual.ChildNodes[11], lista_funciones, elementos_her);

            return lista_funciones;
        }


        public LinkedList<Instruction> getProcedimientos(ParseTreeNode actual, LinkedList<Instruction> lista_funciones, ArrayList elementos_her)
        {

            /*
             FUNCTION_LIST.Rule
                =  RESERV_PROCEDURE + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + PUNTO_COMA
                + DECLARATION_LIST_HIJA 
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                + FUNCTION_LIST
                | Empty
                ;
             */


            LinkedList<Instruction> parametros = new LinkedList<Instruction>();
            LinkedList<Instruction> declaracion = new LinkedList<Instruction>();

            var identifier = actual.ChildNodes[1].Token.Text;

            parametros = PARAMETER(actual.ChildNodes[3], parametros, elementos_her);


            declaracion = declarationAST.LIST_DECLARATIONS(actual.ChildNodes[6], declaracion, new ArrayList());

            var function_instructions = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[7]);


            lista_funciones.AddLast(new Function(identifier, parametros, declaracion, "any", new Sentence(function_instructions), true));

            elementos_her.Clear();
            lista_funciones = FUNCTION_LIST(actual.ChildNodes[9], lista_funciones, elementos_her);

            return lista_funciones;
        }


        public LinkedList<Instruction> PARAMETER(ParseTreeNode actual, LinkedList<Instruction> parametros, ArrayList elementos_her)
        {
            /*
               PARAMETER.Rule
                = RESERV_VAR + IDENTIFIER + PARAMETER_BODY + DOS_PUNTOS + DATA_TYPE + PARAMETER_END
                | IDENTIFIER + PARAMETER_BODY + DOS_PUNTOS + DATA_TYPE + PARAMETER_END
                | Empty;
             */
            if (actual.ChildNodes.Count > 0)
            {
                //CON RESERVADA VAR
                if (actual.ChildNodes.Count == 6)
                {
                    elementos_her.Add(actual.ChildNodes[1].Token.Text);
                    elementos_her = PARAMETER_BODY(actual.ChildNodes[2], elementos_her);
                    var dataType = actual.ChildNodes[4].ChildNodes[0].Token.Text;


                    foreach (var item in elementos_her)
                    {
                        parametros.AddLast(declarationAST.GetDeclarationValue(item.ToString(), dataType, true));
                    }

                    //SI VIENEN MAS PARAMETROS
                    elementos_her.Clear();
                    parametros = PARAMETER_END(actual.ChildNodes[5], parametros, elementos_her);



                }

                //SIN RESERVADA VAR
                else
                {
                    elementos_her.Add(actual.ChildNodes[0].Token.Text);
                    elementos_her = PARAMETER_BODY(actual.ChildNodes[1], elementos_her);
                    var dataType = actual.ChildNodes[3].ChildNodes[0].Token.Text;


                    foreach (var item in elementos_her)
                    {
                        parametros.AddLast(declarationAST.GetDeclarationValue(item.ToString(), dataType, true));
                    }

                    //SI VIENEN MAS PARAMETROS
                    elementos_her.Clear();
                    parametros = PARAMETER_END(actual.ChildNodes[4], parametros, elementos_her);

                }

            }
            return parametros;
        }
        public ArrayList PARAMETER_BODY(ParseTreeNode actual, ArrayList elementos_her)
        {

            /*
             PARAMETER_BODY.Rule 
                =  COMA + IDENTIFIER + PARAMETER_BODY
                | Empty
                ;
             */

            if (actual.ChildNodes.Count > 0)
            {
                elementos_her.Add(actual.ChildNodes[1].Token.Text);
                elementos_her = PARAMETER_BODY(actual.ChildNodes[2], elementos_her);
            }

            return elementos_her;
        }
        public LinkedList<Instruction> PARAMETER_END(ParseTreeNode actual, LinkedList<Instruction> parametros, ArrayList elementos_her)
        {
            /*
             PARAMETER_END.Rule = PUNTO_COMA + PARAMETER
                | Empty
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                parametros = PARAMETER(actual.ChildNodes[1], parametros, elementos_her);
            }

            return parametros;
        }
        #endregion

    }
}
