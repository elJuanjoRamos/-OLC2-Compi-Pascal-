using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.abstracts
{
    public abstract class Instruction_Trad
    {
        private int row;
        private int column;
        private string name;
        private bool isNull;
        public Instruction_Trad(int r, int c, string name)
        {
            this.row = r;
            this.column = c;
            this.Name = name;
        }
        public Instruction_Trad()
        {
        }

        public string Name { get => name; set => name = value; }
        public bool IsNull { get => isNull; set => isNull = value; }

        public abstract string Execute(Ambit_Trad ambit);
    }
}
