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
        private bool return_func_return;
        public int row;
        private int column;

        public Expression Value { get => value; set => this.value = value; }
        public bool Return_func_return { get => return_func_return; set => return_func_return = value; }

        public Exit(Expression value) :
            base(0,0, "Exit")
        {
            this.value = value;
            this.return_func_return = false;
        }

        public Exit() :
            base(0, 0, "Exit")
        {
            this.return_func_return = true;
        }


        public override object Execute(Ambit ambit)
        {
            return this;
        }
    }
}
