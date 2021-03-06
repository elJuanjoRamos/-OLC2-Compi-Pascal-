using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    class Write_Trad : Instruction_Trad
    {
        private LinkedList<Expresion_Trad> value;
        private bool isln;
        private int cant_tabs;
        public Write_Trad(LinkedList<Expresion_Trad> v, bool s, int ct) :
            base(0, 0, "Write")
        {
            this.value = v;
            this.isln = s;
            this.cant_tabs = ct;
        }
        public override string Execute(Ambit_Trad ambit)
        {
            var texto = "";

            int cont = 0;
            foreach (var el in value)
            {
                cont++;

                var element = el.Execute(ambit);
                if (cont == value.Count)
                {
                    texto = texto + (element);

                }
                else
                {
                    texto = texto + (element) + ",";
                }

            }

            if (this.isln)
            {
                var text = texto;
                texto = "writeln(" + text + ");\n";
            } else
            {
                var text = texto;
                texto = "write(" + text + ");\n";
            }

            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + "  ";
            }

            return tabs + texto;
        }
    }
}
