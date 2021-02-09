using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.instruction
{
    class Evaluar : Instruction
    {
        private Expression valor;
        public Evaluar(Expression valor):
            base(0, 0)
        {
            this.valor = valor;
        }
        public override void Execute(Ambit a)
        {
            System.Diagnostics.Debug.WriteLine("El valor es");
            System.Diagnostics.Debug.WriteLine((this.valor.Execute(a)).Value);
        }
    }
}
