using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class FuncionTraduccion
    {
        public FuncionTraduccion()
        {

        }

        //variables
        InstructionTraduccion instructionAST = new InstructionTraduccion();
        DeclarationTraduccion declarationAST = new DeclarationTraduccion();


        #region FUNCIONES


        public string FUNCTION_LIST(ParseTreeNode actual, string lista_funciones, ArrayList parametros_her)
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
                    lista_funciones = getProcedimientos(actual, lista_funciones, parametros_her);
                }
                else
                {
                    lista_funciones = getFunciones(actual, lista_funciones, parametros_her);
                }


            }
            return lista_funciones;
        }

        public string getFunciones(ParseTreeNode actual, string  lista_funciones, ArrayList parametros_her)
        {
            /*
              FUNCTION_LIST.Rule
                = RESERV_FUNCTION + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA
                + FUNCION_HIJA
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                + FUNCTION_LIST
                | Empty
                ;
             */

            string parametros = "";

            var reserv_fun = actual.ChildNodes[0].Token.Text;
            var identifier = actual.ChildNodes[1].Token.Text;
            parametros = PARAMETER(actual.ChildNodes[3], parametros, parametros_her);
            var function_type = actual.ChildNodes[6].ChildNodes[0].Token.Text;


            var fuciones_hijas = FUNCION_HIJA(actual.ChildNodes[8], identifier);

            var function_instructions = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[9], 0);


            lista_funciones = lista_funciones + "\n" +

                fuciones_hijas + "\n\n"+

                reserv_fun + " " + identifier + "(" + parametros + "):" + function_type + ";\n" +   
                
                
                function_instructions +
                ";";            
            
            
            parametros_her.Clear();
            


            lista_funciones = FUNCTION_LIST(actual.ChildNodes[10], lista_funciones, parametros_her);

            return lista_funciones;
        }


       public string getProcedimientos(ParseTreeNode actual, string lista_funciones, ArrayList parametros_her)
        {

            /*
             FUNCTION_LIST.Rule
                =  RESERV_PROCEDURE + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + PUNTO_COMA
                + FUNCION_HIJA
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                + FUNCTION_LIST
                | Empty
                ;
             */


            var parametros = "";

            var identifier = actual.ChildNodes[1].Token.Text;

            parametros = PARAMETER(actual.ChildNodes[3], parametros, parametros_her);


            var function_instructions = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[6], 0);


            lista_funciones = lista_funciones + "\n" +
                "procedure " + identifier + "(" + parametros + ");" + "\n" +
                function_instructions
                + ";";



            parametros_her.Clear();
            lista_funciones = FUNCTION_LIST(actual.ChildNodes[8], lista_funciones, parametros_her);

            return lista_funciones;
        }
        

        public string PARAMETER(ParseTreeNode actual, string parametros, ArrayList parametros_her)
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
                    parametros_her.Add(actual.ChildNodes[1].Token.Text);
                    parametros_her = PARAMETER_BODY(actual.ChildNodes[2], parametros_her);
                    var dataType = actual.ChildNodes[4].ChildNodes[0].Token.Text;


                    var variables = "";
                    foreach (var item in parametros_her)
                    {
                        variables = variables + "," + item.ToString(); 
                    }
                    parametros = parametros+ " " + variables + ":" + dataType;

                    //SI VIENEN MAS PARAMETROS
                    parametros_her.Clear();
                    parametros = PARAMETER_END(actual.ChildNodes[5], parametros, parametros_her);



                }

                //SIN RESERVADA VAR
                else
                {
                    parametros_her.Add(actual.ChildNodes[0].Token.Text);
                    parametros_her = PARAMETER_BODY(actual.ChildNodes[1], parametros_her);
                    var dataType = actual.ChildNodes[3].ChildNodes[0].Token.Text;


                    var variable = "";
                    foreach (var item in parametros_her)
                    {
                        if (parametros_her.Count > 1)
                        {
                            variable = variable + item + ",";
                        } else
                        {
                            variable = variable + item;
                        }
                        
                    }

                    parametros = parametros + " " + variable + ":" + dataType;

                    //SI VIENEN MAS PARAMETROS
                    parametros_her.Clear();
                    parametros = PARAMETER_END(actual.ChildNodes[4], parametros, parametros_her);

                }

            }
            return parametros;
        }
        public ArrayList PARAMETER_BODY(ParseTreeNode actual, ArrayList parametros_her)
        {

            /*
             PARAMETER_BODY.Rule 
                =  COMA + IDENTIFIER + PARAMETER_BODY
                | Empty
                ;
             */

            if (actual.ChildNodes.Count > 0)
            {
                parametros_her.Add(actual.ChildNodes[1].Token.Text);
                parametros_her = PARAMETER_BODY(actual.ChildNodes[2], parametros_her);
            }

            return parametros_her;
        }
        public string PARAMETER_END(ParseTreeNode actual, string parametros, ArrayList parametros_her)
        {
            /*
             PARAMETER_END.Rule = PUNTO_COMA + PARAMETER
                | Empty
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                parametros = PARAMETER(actual.ChildNodes[1], parametros, parametros_her);
            }

            return parametros;
        }


        public string FUNCION_HIJA(ParseTreeNode actual, string padres)
        {


            var retorno = "";
            if (actual.ChildNodes.Count > 0)
            {
                //IDENTIFICADOR
                var id = "";

                if (padres != "")
                {
                    id = padres + "_" + actual.ChildNodes[1].Token.Text;
                }
                else
                {
                    id = actual.ChildNodes[1].Token.Text;

                }

                //PARAMETROS
                var parametros = "";
                parametros = PARAMETER(actual.ChildNodes[3], parametros, new ArrayList());


                //PROCEDIMIENTO 
                if (actual.ChildNodes[0].Term.ToString().Equals("RESERV_PROCEDURE"))
                {
                    /*
                     RESERV_PROCEDURE + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + PUNTO_COMA
                    + FUNCION_HIJA
                    + INSTRUCTIONS_BODY
                    + PUNTO_COMA
                    + FUNCION_HIJA
                    | Empty
                     */

                    var funciones_hijas = FUNCION_HIJA(actual.ChildNodes[6], id);

                    var function_instructions = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[7], 0);

                    var mas_funciones = FUNCION_HIJA(actual.ChildNodes[10], "");


                    retorno =
                        funciones_hijas + "\n" +
                        "procedure " + id + "(" + parametros + ");\n" +
                        function_instructions+ ";" + "\n\n" +
                        mas_funciones;

                }
                //FUNCION
                else
                {


                    /*
                     RESERV_FUNCTION + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA 
                        + FUNCION_HIJA
                        + INSTRUCTIONS_BODY
                        + PUNTO_COMA 
                        + FUNCION_HIJA
                        | Empty
                     */

                   
                    //DATATYPE
                    var function_type = actual.ChildNodes[6].ChildNodes[0].Token.Text;

                    var funciones_hijas = FUNCION_HIJA(actual.ChildNodes[8], id);


                    var function_instructions = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[9], 0);


                    var mas_funciones = FUNCION_HIJA(actual.ChildNodes[11], "");



                    retorno =
                        funciones_hijas + "\n" +
                        "funcion " + id + "(" + parametros + "):" + function_type + ";\n" +
                        function_instructions+ ";" + "\n\n"+
                        mas_funciones;
                }
                



            }

            return retorno;
        }


        #endregion


    }
}
