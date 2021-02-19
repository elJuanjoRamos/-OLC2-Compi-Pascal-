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

        public Break(int row, int column):
            base(0,0, "Break")
        {
            this.row = row;
            this.column = column;
        }
        public Break() :
            base(0, 0, "Break")
        {
        }

        public override object Execute(Ambit ambit)
        {
            if (getValidAmbit(ambit.Ambit_name_inmediato.ToLower(), ambit.Ambit_name.ToLower()))
            {
                return new Break();
            }
            ErrorController.Instance.SyntacticError("La sentencia Break solo puede aparece en ciclos o en la sentencia CASE", 0,0);
            return null;
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
