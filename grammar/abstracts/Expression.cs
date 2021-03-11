using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.abstracts
{
    public abstract class Expression
    {
        public int row;
        public int column;
        public string name;
        public Expression(int r, int c, string n)
        {
            this.row = r;
            this.column = c;
            this.name = n;
        }
        public Expression()
        {
        }

        public abstract Returned Execute(Ambit ambit);
    }
}
