using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.expresion
{
    class Arithmetic_Trad : Expresion_Trad
    {

        private Expresion_Trad left;
        private Expresion_Trad right;
        private String type;

        public Arithmetic_Trad(Expresion_Trad l, Expresion_Trad ri, String t)
        : base("Arithmetic")
        {
            this.right = ri;
            this.left = l;
            this.type = t;
        }
        public override string Execute(Ambit_Trad ambit)
        {
            var result = "";
            var varIz = this.left.Execute(ambit);
            var valDer = this.right.Execute(ambit);

            if (this.type.Equals("+"))
            {
                result = varIz.ToString() + "+" + valDer.ToString();
            }
            else if (this.type.Equals("-"))
            {
                result = varIz.ToString() + "-" + valDer.ToString();
            }
            else if (this.type.Equals("*"))
            {
                result = varIz.ToString() + "*" + valDer.ToString();
            }
            else if (this.type.Equals("/"))
            {
                result = varIz.ToString() + "/" + valDer.ToString();
            }
            else if (this.type.Equals("%"))
            {
                result = varIz.ToString() + "%" + valDer.ToString();
            }
            return result;

        }
    }
}
