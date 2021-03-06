using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.expresion
{
    class Logical_Trad : Expresion_Trad
    {
        private Expresion_Trad left;
        private Expresion_Trad right;
        private string type;
        public int row;
        public int column;


        public Logical_Trad(Expresion_Trad l, Expresion_Trad r, string t, int ro, int c)
        : base("Logical")
        {
            this.left = l;
            this.right = r;
            this.type = t;
            this.row = ro;
            this.column = c;
        }

        public override string Execute(Ambit_Trad ambit)
        {
            var result = "";

            if (!this.type.ToLower().Equals("not"))
            {
                var varIz = this.left.Execute(ambit);
                var valDer = right.Execute(ambit);

                result = varIz + " " + this.type + " " + valDer;
            }
            else
            {
                var varIz = this.left.Execute(ambit);
                result = "not " + varIz;

            }
            return result;
        }
    }
}
