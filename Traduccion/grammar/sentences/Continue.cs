using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    class Continue_Trad : Instruction_Trad
    {
        int cant_tabs;
        public Continue_Trad(int ct)
            :base(0,0,"Continue")
        {
            this.cant_tabs = ct;
        }
        public override string Execute(Ambit_Trad ambit)
        {
            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + "  ";
            }
            return tabs+ "continue;";
        }
    }
}
