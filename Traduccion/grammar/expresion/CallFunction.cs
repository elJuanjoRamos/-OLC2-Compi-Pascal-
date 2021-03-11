using CompiPascal.controller;
using CompiPascal.Traduccion.grammar.abstracts;
using CompiPascal.Traduccion.grammar.sentences;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.expresion
{
    class CallFunction_Trad : Expresion_Trad
    {
        private string id;
        private ArrayList parametros;
        private int row;
        private int column;

        public CallFunction_Trad(string id, ArrayList expresion) :
            base("Call")
        {
            this.id = id;
            this.parametros = expresion;
        }

        public override string Execute(Ambit_Trad ambit)
        {


            var funcion_llamada = ambit.getFuncion(id);
            Ambit_Trad function_ambit = new Ambit_Trad();

            if (funcion_llamada != null)
            {

                if (funcion_llamada.IsProcedure)
                {
                    function_ambit = new Ambit_Trad(ambit, ambit.Ambit_name + "_Procedure_" + funcion_llamada.Id, "Procedure", false);
                }
                else
                {
                    function_ambit = new Ambit_Trad(ambit, ambit.Ambit_name + "_Function_" + funcion_llamada.Id, "Function", false);
                }
                foreach (var param in funcion_llamada.Parametos)
                {
                    param.Execute(function_ambit);
                }


                for (int i = 0; i < parametros.Count; i++)
                {
                    var param = (Literal_Trad)parametros[i];
                    var result = param.Execute(function_ambit);
                    var decla = (Declaration_Trad)funcion_llamada.getParameterAt(i);
                    function_ambit.setVariableFuncion(decla.Id, result, decla.Type, "Parametro");
                }
            }






            var params_call = "";
            var cont = 0;
            var res = "";

            foreach (var item in parametros)
            {
                cont++;
                if (item is Expresion_Trad)
                {
                    res = ((Expresion_Trad)item).Execute(ambit);
                }
                else
                {
                    res = item.ToString() + ",";
                }
                if (cont != parametros.Count)
                {
                    params_call += res + ",";
                }
                else
                {
                    params_call += res;
                }
                
            }

            Function_Trad func = ambit.getFuncion(id);
            if (func == null)
            {
                 return id + "(" + params_call + ")";
            }
            else
            {

                var parametros_padre = "";

                if (func.EsHija)
                {
                    Function_Trad funcpadres = ambit.getFuncion(func.Padre_inmediato);
                    foreach (var item in funcpadres.Declaraciones)
                    {
                        Declaration_Trad dec = ((Declaration_Trad)item);
                        parametros_padre += "," + dec.Id;

                    }
                }

                GraphController.Instance.getAmbitoGraficar_Trad(function_ambit, false);
                return func.UniqId + "(" + params_call + parametros_padre + ")";
            }

        }
    }
}


