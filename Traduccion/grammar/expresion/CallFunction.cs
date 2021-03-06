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
                

                return func.UniqId + "(" + params_call + parametros_padre + ")";
            }

        }
    }
}


