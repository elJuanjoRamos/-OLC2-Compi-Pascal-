using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using CompiPascal.grammar.sentences;
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


                var funcion_llamada = ambit.getFuncion(this.id.ToLower());
                if (funcion_llamada == null)
                {
                    ErrorController.Instance.SyntacticError("La funcion '" + this.id + "' no esta definido",0,0);
                    return new Returned();
                }

                if (funcion_llamada.Parametos.Count != parametros.Count)
                {
                    ErrorController.Instance.SyntacticError("La funcion '" + this.id + "' no recibe la misma cantidad de parametros",0,0);
                    return new Returned();

                }
                //GUARDAR LOS PARAMETROS EN LA TABLA DE SIMBOLOS

                Ambit function_ambit = new Ambit();


                if (funcion_llamada.IsProcedure)
                {
                    ErrorController.Instance.SyntacticError("El procedimiento'" + this.id + "' no puede asignarse como valor de retorno",0,0);
                    return new Returned();

                }
                else
                {
                    function_ambit = new Ambit(ambit, ambit.Ambit_name + "_Function_" + funcion_llamada.Id, "Function", false);
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

                    var result = ((Expression)parametros[i]).Execute(ambit);

                    if (variable.getDataType == result.getDataType)
                    {
                        function_ambit.setVariableFuncion(variable.Id, result.Value, result.getDataType, false, "Parametro");
                    }
                    else
                    {
                        ErrorController.Instance.SyntacticError("El tipo " + result.getDataType + " no es asignable con " + variable.getDataType, 0, 0);
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
                                ErrorController.Instance.SemantycErrors("Los procediminetos no pueden retornar ningun valor", 0, 0);
                                return new Returned();
                            }
                        }
                        if (funcion_Elementos is Break)
                        {
                            var r = (Break)funcion_Elementos;
                            ErrorController.Instance.SyntacticError("La sentencia Break solo puede aparece en ciclos o en la sentencia CASE", r.Row, r.Column);
                        }
                        else if (funcion_Elementos is Continue)
                        {
                            var r = (Continue)funcion_Elementos;
                            ErrorController.Instance.SyntacticError("La sentencia Continue solo puede aparece en ciclos", r.Row, r.Column);
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
                                    //GraphController.Instance.getAmbitoGraficar(function_ambit, false);

                                    return new Returned(funcion_llamada.Retorno, funcion_llamada.Tipe);

                                }
                                else
                                {
                                    var result = ((Exit)funcion_Elementos).Value.Execute(function_ambit);
                                    //HAY ERROR
                                    if (result.getDataType == DataType.ERROR || result == null)
                                    {
                                        return new Returned();
                                    }


                                    //VERIFICA QUE EL TIPO DE RETORNO SEA VALIDO
                                    if (result.getDataType == funcion_llamada.Tipe)
                                    {
                                        //GraphController.Instance.getAmbitoGraficar(function_ambit, false);
                                        return new Returned(result.Value, result.getDataType);
                                    }
                                    else
                                    {
                                        ErrorController.Instance.SemantycErrors("Tipos incompatibles, la funcion '" + funcion_llamada.Id + "' retorna " + funcion_llamada.Tipe + " en lugar de" + result.getDataType, 0, 0);
                                        return new Returned();
                                    }
                                }
                            }
                        }
                        else if (funcion_Elementos is Returned)
                        {
                            //GraphController.Instance.getAmbitoGraficar(function_ambit, false);
                            return (Returned)funcion_Elementos;
                        }
                        else
                        {
                            //GraphController.Instance.getAmbitoGraficar(function_ambit, false);
                            return new Returned(funcion_llamada.Retorno, funcion_llamada.Tipe);
                        }
                    }   
                }
                return new Returned();
            }
        }
    }
}
