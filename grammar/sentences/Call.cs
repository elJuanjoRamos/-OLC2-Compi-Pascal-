using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
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

        public Call(string id, ArrayList expresion): 
            base(0,0, "Call")
        {
            this.id = id;
            this.parametros = expresion;
        }

        public override object Execute(Ambit ambit)
        {
            var funcion_llamada = ambit.getFuncion(this.id);
            if (funcion_llamada == null)
            {
                ConsolaController.Instance.Add("La funcion '" + this.id + "' no esta declarada");
                return null;
            }

            if (funcion_llamada.Parametos.Count != parametros.Count)
            {
                ConsolaController.Instance.Add("La funcion '" + this.id + "' no recibe la misma cantidad de parametros");
                return null;

            }


            //GUARDAR LOS PARAMETROS EN LA TABLA DE SIMBOLOS


            Ambit function_ambit = new Ambit(ambit, ambit.Ambit_name + "_Function", "Function", false);

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

                var result = ((Expression)parametros[i]).Execute(function_ambit);

                if (variable.getDataType == result.getDataType)
                {
                    function_ambit.setVariableFuncion(variable.Id, result.Value, result.getDataType, false);   
                }
                else
                {
                    ConsolaController.Instance.Add("El tipo " + result.getDataType + " no es asignable con " + variable.getDataType);
                    ErrorController.Instance.SyntacticError("El tipo " + result.getDataType + " no es asignable con " + variable.getDataType, 0, 0);
                    return null;
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
                        var result = ((Exit)funcion_Elementos).Value.Execute(function_ambit);

                        return new Returned(result.Value, result.getDataType);
                    }
                }
            }


            return 0;
        }
    }



}
