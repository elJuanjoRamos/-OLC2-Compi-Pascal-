using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    class For_Trad : Instruction_Trad
    {

        private string initId;
        private Expresion_Trad inicializacion;
        private Expresion_Trad actualizacion;
        private Instruction_Trad sentence;
        private string direccion;
        private int cant_tabs;
        public For_Trad(string initId, Expresion_Trad inicializacion, Expresion_Trad actualizacion, 
            Instruction_Trad sentence, string dir, int ct)
            : base(0, 0, "For")
        {
            this.initId = initId;
            this.inicializacion = inicializacion;
            this.actualizacion = actualizacion;
            this.sentence = sentence;
            this.direccion = dir;
            this.cant_tabs = ct;
        }



        public override string Execute(Ambit_Trad ambit)
        {
            var ambitName = "Global_For";
            if (!ambit.IsNull)
            {
                ambitName = ambit.Ambit_name + "_For";
            }
            var forAmbit = new Ambit_Trad(ambit, ambitName, "For", false);

            var val_init = inicializacion.Execute(forAmbit);

            var val_fin = actualizacion.Execute(forAmbit);

            var sentencias = sentence.Execute(forAmbit);

            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + "  ";
            }

            return 
                tabs + "for " + initId + ":= " + val_init + " " + direccion + " " + val_fin + " do \n" +
                tabs + "begin\n" +
                sentencias+
                tabs + "end\n"; 

        }
    }
}
