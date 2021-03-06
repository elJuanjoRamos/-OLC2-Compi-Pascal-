using CompiPascal.Traduccion.grammar;
using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.sentences
{
    class Assignation_Trad : Instruction_Trad
    {
        private string id;
        private Expresion_Trad value;
        private int row;
        private int column;
        private int cant_tabs;
        public Assignation_Trad(string id, Expresion_Trad value, int ct) :
           base(0, 0, "Assignation")
        {
            this.id = id;
            this.value = value;
            this.cant_tabs = ct;
        }

        public override string Execute(Ambit_Trad ambit)
        {
            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + "  ";
            }
            var val = value.Execute(ambit);

            return tabs + this.id + ":= " + val + ";\n";
        }
    }
}
