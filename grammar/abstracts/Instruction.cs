using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.grammar.identifier;

namespace CompiPascal.grammar.abstracts
{
    public abstract class Instruction
    {
        private string name;
        private bool isNull;
        public Instruction(string name)
        {
            this.Name = name;
        }
        public Instruction()
        {
        }

        public string Name { get => name; set => name = value; }
        public bool IsNull { get => isNull; set => isNull = value; }
        public abstract object Execute(Ambit ambit);
    }
}
