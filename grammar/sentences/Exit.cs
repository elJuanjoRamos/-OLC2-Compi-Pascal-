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
        private int row;
        private int column;

        public Expression Value { get => value; set => this.value = value; }
        public bool Return_func_return { get => return_func_return; set => return_func_return = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public Exit(Expression value, int row, int col) :
            base("Exit")
        {
            this.value = value;
            this.return_func_return = false;
            this.row = row;
            this.column = col;
        }

        public Exit() :
            base("Exit")
        {
            this.return_func_return = true;
        }


        public override object Execute(Ambit ambit)
        {
            return this;
        }
    }
}
