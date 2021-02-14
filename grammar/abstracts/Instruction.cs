using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.grammar.identifier;

namespace CompiPascal.grammar.abstracts
{
    public abstract class Instruction
    {
        private int row;
        private int column;

        public Instruction(int r, int c)
        {
            this.row = r;
            this.column = c;
        }
        public abstract object Execute(Ambit ambit);
    }
}
