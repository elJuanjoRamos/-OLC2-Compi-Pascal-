using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class Continue : Instruction
    {
        private int row;
        private int column;

        public Continue(int row, int column) :
            base(0, 0, "Continue")
        {
            this.row = row;
            this.column = column;
        }
        public Continue() :
            base(0, 0, "Continue")
        {
        }


        public override object Execute(Ambit ambit)
        {
            if (getValidAmbit(ambit.Ambit_name_inmediato.ToLower(), ambit.Ambit_name.ToLower()))
            {
                return new Continue();
            }
            ErrorController.Instance.SyntacticError("La sentencia Continue solo puede aparece en ciclos", 0, 0);
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
            return false;
        }
    }
}
