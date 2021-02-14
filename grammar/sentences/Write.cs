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
               var element = el.Execute(a);

                if (element.getDataType == DataType.ERROR)
                {
                    return null;
                } else
                {
                    if (this.isln)
                    {
                        texto = texto + "\n" + (element.Value.ToString());
                    }
                    else
                    {
                        texto = texto + " " + (element.Value.ToString());
                    }
                }

                
                
            }
            ConsolaController.Instance.Add(texto);
            return 0;
        }
    }
}
