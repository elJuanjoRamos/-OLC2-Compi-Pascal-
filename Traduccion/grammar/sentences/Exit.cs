using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    class Exit_Trad : Instruction_Trad
    {
        private Expresion_Trad value;
        int cant_tabs;
        public Expresion_Trad Value { get => value; set => this.value = value; }
        
        public Exit_Trad(Expresion_Trad value, int ct) :
            base(0, 0, "Exit")
        {
            this.value = value;
            this.cant_tabs = ct;
        }

        public Exit_Trad(int ct) :
            base(0, 0, "Exit")
        {
            this.value = null ;
            this.cant_tabs = ct;
        }
        public override string Execute(Ambit_Trad ambit)
        {
            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + "  ";
            }
            if (value != null)
            {
                var val = value.Execute(ambit);
                return tabs +"exit(" + val + ");\n";   
            }
            return tabs+"exit();\n";
            
        }
    }
}
