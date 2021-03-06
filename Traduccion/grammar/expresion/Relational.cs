using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.expresion
{
    class Relational_Trad : Expresion_Trad
    {
        private Expresion_Trad left;
        private Expresion_Trad right;
        private string type;


        public Relational_Trad(Expresion_Trad left, Expresion_Trad right, string type)
        {
            this.left = left;
            this.right = right;
            this.type = type;
        }
        public override string Execute(Ambit_Trad ambit)
        {
            var valIz = this.left.Execute(ambit);
            var valDer = this.right.Execute(ambit);

            return valIz + type + valDer;
        }
    }
}
