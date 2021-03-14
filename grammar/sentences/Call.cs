using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.expression;
using CompiPascal.grammar.identifier;
using CompiPascal.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class Call : Instruction
    {
        private string id;
        private ArrayList parametros;
        private int row;
        private int column;

        public Call(string id, ArrayList expresion, int row, int col): 
            base(row,col,"Call")
        {
            this.id = id;
            this.parametros = expresion;
            this.row = row;
            this.column = col;
        }

        public override object Execute(Ambit ambit)
        {

            if (id.Equals("graficar_ts"))
            {
                //PRINT A LA TABLA SIMBOLOS
                GraphController.Instance.graficarTSAmbit(ambit);
                return 0;
            }
            else
            {

                var funcion_llamada = ambit.getFuncion(this.id);
                if (funcion_llamada == null)
                {
                    set_error("La funcion o procediminento '" + this.id + "' no esta definido", Row, Column);
                    return null;
                }

                if (funcion_llamada.Parametos.Count != parametros.Count)
                {
                    set_error("La funcion '" + this.id + "' no recibe la misma cantidad de parametros", Row, Column);
                    return null;

                }


                //GUARDAR LOS PARAMETROS EN LA TABLA DE SIMBOLOS

                Ambit function_ambit = new Ambit();

                if (funcion_llamada.IsProcedure)
                {
                    function_ambit = new Ambit(ambit, "Procedure_" + funcion_llamada.Id, "Procedure", false);
                }
                else
                {
                    function_ambit = new Ambit(ambit, "Function_" + funcion_llamada.Id, "Function", false);
                }

                //FOREACH PARA GUARDAR LOS PARAMETROS EN EL AMBITO,
                //LOS PARAMETROS QUE RECIBE LA FUNCION CUANDO SE DECLARA SON 'DECLARACIONES'
                //POR TANTO AL HACER EL EXECUTE LA VARIABLE SE GUARDA EN EL AMBITO QUE SE ENVIA
                foreach (var param in funcion_llamada.Parametos)
                {
                    param.Execute(function_ambit);
                }

                //SE ASIGNAN LOS VALORES RECIBIDOS A LOS PARAMETROS DE LA FUNCION
                
                
                for (int i = 0; i < parametros.Count; i++)
                {
                    var variable = (Declaration)(funcion_llamada.getParameterAt(i));

                    //saludar(var j; i:integer)
                    //saludar(variable, 0)
                    if (variable.Referencia)
                    {
                        if (parametros[i] is Access)
                        {
                            Access acceso = (Access)parametros[i];
                            funcion_llamada.Parametros_referencia.Add(new SimboloReferencia(variable.Id, acceso.Id));
                        }
                        else if (parametros[i] is Access_array)
                        {

                        }
                    }

                    var result = ((Expression)parametros[i]).Execute(ambit);


                    if (variable.getDataType == result.getDataType)
                    {
                        function_ambit.setVariableFuncion(variable.Id, result.Value, result.getDataType, false, "Parametro");
                    }
                    else
                    {
                        set_error("El tipo " + result.getDataType + " no es asignable con " + variable.getDataType, 0, 0);
                        return null;
                    }

                    
                }


                //GUARDA LAS VARIABLES QUE ESTEN DECLARADAS EN LA FUNCION

                if (funcion_llamada.Declaraciones.Count > 0)
                {

                    foreach (var declaracion in funcion_llamada.Declaraciones)
                    {
                        declaracion.Execute(function_ambit);
                    }
                }


                //EJECUCION DEL CODIGO


                var funcion_Elementos = funcion_llamada.Sentences.Execute(function_ambit);



                if (funcion_Elementos == null)
                {
                    return null;
                }

                else
                {
                    if (funcion_Elementos is Instruction)
                    {
                        var inst = (Instruction)funcion_Elementos;
                        if (inst.Name.Equals("Exit"))
                        {
                            if (!funcion_llamada.IsProcedure)
                            {
                                var response = ((Exit)funcion_Elementos);

                                if (response.Return_func_return)
                                {
                                    GraphController.Instance.getAmbitoGraficar(function_ambit, false);
                                    //SINTETIZA LOS PARAMETROS POR REFERENCIA
                                    sintetizar_referencia(function_ambit, funcion_llamada);


                                    


                                    return new Returned(funcion_llamada.Retorno, funcion_llamada.Tipe);

                                }
                                else
                                {
                                    var result = response.Value.Execute(function_ambit);

                                    //UPDATE FUNCION A LA TABLA SIMBOLOS
                                    funcion_llamada.Retorno = result.Value.ToString();
                                    ambit.setFunction(funcion_llamada.Id, funcion_llamada);
                                    //SINTETIZA LOS PARAMETROS POR REFERENCIA
                                    sintetizar_referencia(function_ambit, funcion_llamada);
                                    GraphController.Instance.getAmbitoGraficar(function_ambit, false);
                                    return new Returned(result.Value, result.getDataType);
                                }
                            }
                            else
                            {
                                set_error("Los procediminentos no retornan ningun valor", inst.Row, inst.Column);
                                return null;
                            }
                        }
                        if (funcion_Elementos is Break)
                        {
                            var r = (Break)funcion_Elementos;
                            set_error("La sentencia Break solo puede aparece en ciclos o en la sentencia CASE", r.Row, r.Column);
                        }
                        else if (funcion_Elementos is Continue)
                        {
                            var r = (Continue)funcion_Elementos;
                            set_error("La sentencia Continue solo puede aparece en ciclos", r.Row, r.Column);
                        }
                    }
                    sintetizar_referencia(function_ambit, funcion_llamada);
                    GraphController.Instance.getAmbitoGraficar(function_ambit, false);
                }
            }

            return 0;
            
        }
       
        //METODO PARA SINTETIZAR VALORES
        public void sintetizar_referencia(Ambit ambit, Function funcion_llamada)
        {
            if (funcion_llamada.Parametros_referencia.Count > 0)
            {
                foreach (SimboloReferencia simbolo in funcion_llamada.Parametros_referencia)
                {
                    var variable = ambit.getVariableFunctionInAmbit(simbolo.Actual);
                    ambit.setVariableInAmbit(simbolo.Padre, variable.Value, variable.DataType, false, "Variable");
                }
                funcion_llamada.Parametros_referencia.Clear();
            }
        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto+" - Row: " + row + "- Col: " + column + "\n");
        }
    }



}
