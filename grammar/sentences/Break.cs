using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class Break : Instruction 
    {
        private int row;
        private int column;

        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public Break(int row, int column):
            base("Break")
        {
            this.row = row;
            this.column = column;
        }
        public Break() :
            base("Break")
        {
        }

        public override object Execute(Ambit ambit)
        {
            //ErrorController.Instance.SyntacticError("La sentencia Break solo puede aparece en ciclos o en la sentencia CASE", row,col);

            return this;
        }

        public bool getValidAmbit(string ambit_name, string ambit_padre)
        {
            switch (ambit_name)
            {
                case "for":
                    return true;
                case "while":
                    return true;
                case "repeat":
                    return true;
                case "case":
                    return true;
            }

            if (ambit_padre.Contains("for"))
            {
                return true;
            }
            if (ambit_padre.Contains("while"))
            {
                return true;
            }
            if (ambit_padre.Contains("repeat"))
            {
                return true;
            }
            if (ambit_padre.Contains("case"))
            {
                return true;
            }
            return false;
        }
    }
}
