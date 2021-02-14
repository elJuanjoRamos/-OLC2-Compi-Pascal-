using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class Write : Instruction
    {
        private LinkedList<Expression> value;
        private bool isln;
        public Write(LinkedList<Expression> v, bool s) : 
            base(0,0)
        {
            this.value = v;
            this.isln = s;
        }
        public override object Execute(Ambit a)
        {
            var texto = "";
            foreach (var el in value)
            {
                if (this.isln)
                {
                    texto = texto + "\n" + (el.Execute(a)).Value.ToString();
                } else
                {
                    texto = texto + " " + (el.Execute(a)).Value.ToString();
                }
                
            }
            ConsolaController.Instance.Add(texto);
            return 0;
        }
    }
}
