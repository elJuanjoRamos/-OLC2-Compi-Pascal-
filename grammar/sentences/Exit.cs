using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class Exit : Instruction
    {
        private Expression value;
        public int row;
        private int column;

        public Expression Value { get => value; set => this.value = value; }

        public Exit(Expression value) :
            base(0,0, "Exit")
        {
            this.value = value;
        }

        public override object Execute(Ambit ambit)
        {
            return this;
        }
    }
}
