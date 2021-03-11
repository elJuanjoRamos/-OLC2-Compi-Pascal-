using CompiPascal.controller;
using CompiPascal.Traduccion.grammar.abstracts;
using CompiPascal.Traduccion.grammar.identifier;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    class Call_Trad : Instruction_Trad
    {
        private string id;
        private ArrayList parametros;
        private int cant_tabs;
        private int row;
        private int column;
        public Call_Trad(string id, ArrayList expresion, int ct) :
           base(0, 0, "Call")
        {
            this.id = id;
            this.parametros = expresion;
            this.cant_tabs = ct;
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
                    var param = parametros[i];
                    var decla = (Declaration_Trad)funcion_llamada.getParameterAt(i);
                    function_ambit.setVariableFuncion(decla.Id, param.ToString(), decla.Type, "Parametro");
                }
            }
            


            var parametros_llam = "";
            var tabs = "";
            var cont = 0;
            var res = "";
            for (int j = 0; j < cant_tabs; j++)
            {
                tabs = tabs + "  ";
            }

            foreach (var item in parametros)
            {
                cont++;
                if (item is Expresion_Trad)
                {
                    res = ((Expresion_Trad)item).Execute(ambit);
                }
                else
                {
                    res = item.ToString();
                }

                if (cont == parametros.Count)
                {
                    parametros_llam += res;
                } else
                {
                    parametros_llam += res + ",";
                }
            }



            Function_Trad func = ambit.getFuncion(id);
            if (func == null)
            {
                return tabs+this.id + "(" + parametros_llam + ");\n";
            } else
            {
                var parametros_padre = "";
                cont = 0;
                if (func.EsHija)
                {
                    Function_Trad funcpadres = ambit.getFuncion(func.Padre_inmediato);
                    foreach (var item in funcpadres.Declaraciones)
                    {
                        cont++;
                        Declaration_Trad dec = ((Declaration_Trad)item);
                        if (cont == funcpadres.Declaraciones.Count)
                        {
                            parametros_padre += dec.Id;
                        } else
                        {
                            parametros_padre += dec.Id + ",";
                        }
                    }
                }
                return tabs+func.UniqId + "(" + parametros_llam + parametros_padre + ");\n";
            }

            
            
            

            
            
        }
    }
}
