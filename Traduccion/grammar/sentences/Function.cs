using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    public class Function_Trad : Instruction_Trad
    {

        private string id;
        private LinkedList<Instruction_Trad> parametos;
        private LinkedList<Instruction_Trad> declaraciones;
        private LinkedList<Instruction_Trad> funciones_hijas;
        private string tipe;
        private Instruction_Trad sentences;
        private bool isProcedure;
        private string retorno;
        private int cant_tabs;
        private string uniqId;
        private bool esHija;
        private string padre_inmediato;



        public Function_Trad(string id, LinkedList<Instruction_Trad> parametos, 
            LinkedList<Instruction_Trad> declas, LinkedList<Instruction_Trad> hijas,
            string tipe, Instruction_Trad sentences, bool isProcedure, int cant_Tabs, bool eh, string pi)
       : base(0, 0, "Function")
        {
            this.retorno = "-";
            this.id = id;
            this.parametos = parametos;
            this.declaraciones = declas;
            this.funciones_hijas = hijas;
            this.tipe = tipe;
            this.sentences = sentences;
            this.isProcedure = isProcedure;
            this.cant_tabs = cant_Tabs;
            this.uniqId = id;
            this.esHija = eh;
            this.padre_inmediato = pi;
        }

        public string Id { get => id; set => id = value; }
        public LinkedList<Instruction_Trad> Parametos { get => parametos; set => parametos = value; }
        public LinkedList<Instruction_Trad> Declaraciones { get => declaraciones; set => declaraciones = value; }
        public LinkedList<Instruction_Trad> Funciones_hijas { get => funciones_hijas; set => funciones_hijas = value; }
        public string Tipe { get => tipe; set => tipe = value; }
        public bool IsProcedure { get => isProcedure; set => isProcedure = value; }
        public string Retorno { get => retorno; set => retorno = value; }
        public string UniqId { get => uniqId; set => uniqId = value; }
        public bool EsHija { get => esHija; set => esHija = value; }
        public string Padre_inmediato { get => padre_inmediato; set => padre_inmediato = value; }

        public override string Execute(Ambit_Trad ambit)
        {
            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs += "  ";
            }

            ambit.saveFuncion(this.id, this);

            
            if (ambit.Anterior != null)
            {
                this.UniqId = ambit.Ambit_name + "_" + id;
                ambit.setFunction(Id, this);
            }

            Ambit_Trad ambit_Trad = new Ambit_Trad(ambit, this.uniqId, ambit.Ambit_name + "_Function", false);




            var parametros_fun = "";
            foreach (var param in parametos)
            {
                Declaration_Trad dec = (Declaration_Trad)param;
                parametros_fun += dec.Id + ":" + dec.Type + ";";
                ambit_Trad.saveVarFunction(dec.Id, "0", dec.Type);

            }


            //VERIFICA SI ES PADRE O HIJO
            if (esHija)
            {
                int cont = 1;

                Function_Trad funcion_padre = ambit.getFuncion(padre_inmediato);
                foreach (var param in funcion_padre.Declaraciones)
                {
                    Declaration_Trad dec = (Declaration_Trad)param;
                    if (cont < funcion_padre.Declaraciones.Count)
                    {
                        parametros_fun += "var " + funcion_padre.UniqId + "_" + dec.Id + ":" + dec.Type + ";";
                    } else
                    {
                        parametros_fun += "var " + funcion_padre.UniqId + "_" + dec.Id + ":" + dec.Type;
                    }
                    cont++;
                }
            }


            


            var cadena = "";
            if (!isProcedure)
            {
                cadena = tabs + "function " + uniqId + "(" + parametros_fun + ") : " + tipe + ";\n";
            } else
            {
                cadena = tabs + "procedure " + uniqId + "(" + parametros_fun + ");\n";
            }

            //DECLARACIONES
            var declacion = "";
            foreach (var decla in declaraciones)
            {
                var res = decla.Execute(ambit_Trad);
                declacion += res;
            }

            //FUNCIONES HIJAS
            var res_hijas = "\n";
            foreach (var fun_hija in funciones_hijas)
            {
                res_hijas += fun_hija.Execute(ambit_Trad);
            }


            var begin = tabs + "begin\n";




            var instrucciones = sentences.Execute(ambit_Trad);



            var end = tabs + "end;\n";




            return res_hijas +  cadena + declacion + begin + instrucciones + end;
        }
    }
}
