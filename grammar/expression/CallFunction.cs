using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using CompiPascal.grammar.sentences;
using CompiPascal.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.expression
{
    class CallFunction  : Expression
    {
        private string id;
        private ArrayList parametros;
        private int row;
        private int column;

        public CallFunction(string id, ArrayList expresion, int row, int col) :
            base(row, col, "Call")
        {
            this.id = id;
            this.parametros = expresion;
            this.row = row;
            this.column = col;
        }

        public override Returned Execute(Ambit ambit)
        {
            {


                var funcion_llamada = ambit.getFuncion(this.id);
                if (funcion_llamada == null)
                {
                    set_error("La funcion '" + this.id + "' no esta definido",row, column);
                    return new Returned();
                }

                if (funcion_llamada.Parametos.Count != parametros.Count)
                {
                    set_error("La funcion '" + this.id + "' no recibe la misma cantidad de parametros",row, column);
                    return new Returned();

                }
                //GUARDAR LOS PARAMETROS EN LA TABLA DE SIMBOLOS

                Ambit function_ambit = new Ambit();


                if (funcion_llamada.IsProcedure)
                {
                    set_error("El procedimiento'" + this.id + "' no puede asignarse como valor de retorno",row, column);
                    return new Returned();

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
                        set_error("El tipo " + result.getDataType + " no es asignable con " + variable.getDataType, row, column);
                        return new Returned();
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

                //si viene null significa que viene error
                if (funcion_Elementos == null)
                {
                    return new Returned();
                }

                else
                {
                    if (funcion_llamada.IsProcedure)
                    {
                        if (funcion_Elementos is Instruction)
                        {
                            var inst = (Instruction)funcion_Elementos;
                            if (inst.Name.Equals("Exit"))
                            {
                                Exit ex = (Exit)funcion_Elementos;
                                set_error("Los procediminetos no pueden retornar ningun valor", ex.Row, ex.Column);
                                return new Returned();
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
                    } else
                    {
                        if (funcion_Elementos is Instruction)
                        {
                            var inst = (Instruction)funcion_Elementos;
                            if (inst.Name.Equals("Exit"))
                            {


                                var response = ((Exit)funcion_Elementos);

                                if (response.Return_func_return)
                                {

                                    GraphController.Instance.getAmbitoGraficar(function_ambit, false);
                                    //SINTETIZA LOS PARAMETROS POR REFERENCIA
                                    sintetizar_referencia(function_ambit, funcion_llamada);

                                    switch (funcion_llamada.Tipe)
                                    {
                                        case DataType.INTEGER:
                                            return new Returned(int.Parse(funcion_llamada.Retorno), funcion_llamada.Tipe);
                                        case DataType.STRING:
                                            return new Returned((funcion_llamada.Retorno), funcion_llamada.Tipe);
                                        case DataType.BOOLEAN:
                                            return new Returned(bool.Parse(funcion_llamada.Retorno), funcion_llamada.Tipe);
                                        case DataType.REAL:
                                            return new Returned(double.Parse(funcion_llamada.Retorno), funcion_llamada.Tipe);
                                    }

                                }
                                else
                                {
                                    var ext = (Exit)funcion_Elementos;
                                    var result = (ext).Value.Execute(function_ambit);
                                    //HAY ERROR
                                    if (result.getDataType == DataType.ERROR || result == null)
                                    {
                                        return new Returned();
                                    }


                                    //VERIFICA QUE EL TIPO DE RETORNO SEA VALIDO
                                    if (result.getDataType == funcion_llamada.Tipe)
                                    {
                                        //SINTETIZA LOS PARAMETROS POR REFERENCIA
                                        sintetizar_referencia(function_ambit, funcion_llamada);
                                        GraphController.Instance.getAmbitoGraficar(function_ambit, false);

                                        switch (funcion_llamada.Tipe)
                                        {
                                            case DataType.INTEGER:
                                                return new Returned((int)result.Value, result.getDataType);
                                            case DataType.STRING:
                                                return new Returned((string)result.Value, result.getDataType);
                                            case DataType.BOOLEAN:
                                                return new Returned((bool)result.Value, result.getDataType);
                                            case DataType.REAL:
                                                return new Returned((double)result.Value, result.getDataType);
                                        }                                        
                                    }
                                    else
                                    {
                                        set_error("Tipos incompatibles, la funcion '" + funcion_llamada.Id + "' retorna " + funcion_llamada.Tipe + " en lugar de" + result.getDataType, ext.Row, ext.Column);
                                        return new Returned();
                                    }
                                }
                            }
                        }
                        else if (funcion_Elementos is Returned)
                        {
                            //SINTETIZA LOS PARAMETROS POR REFERENCIA
                            sintetizar_referencia(function_ambit, funcion_llamada);
                            GraphController.Instance.getAmbitoGraficar(function_ambit, false);
                            return (Returned)funcion_Elementos;
                        }
                        else
                        {
                            //SINTETIZA LOS PARAMETROS POR REFERENCIA
                            sintetizar_referencia(function_ambit, funcion_llamada);
                            GraphController.Instance.getAmbitoGraficar(function_ambit, false);


                            switch (funcion_llamada.Tipe)
                            {
                                case DataType.INTEGER:
                                    return new Returned(int.Parse(funcion_llamada.Retorno), funcion_llamada.Tipe);
                                case DataType.STRING:
                                    return new Returned((funcion_llamada.Retorno), funcion_llamada.Tipe);
                                case DataType.BOOLEAN:
                                    return new Returned(bool.Parse(funcion_llamada.Retorno), funcion_llamada.Tipe);
                                case DataType.REAL:
                                    return new Returned(double.Parse(funcion_llamada.Retorno), funcion_llamada.Tipe);
                            }

                        }
                    }   
                }
                return new Returned();
            }
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
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
