using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.expresion
{
    class Literal_Trad : Expresion_Trad
    {

        private string value;
        private int type;
        private bool isNull;


        public Literal_Trad(string v, int t) :
            base("Literal")
        {
            this.value = v;
            this.type = t;
            this.isNull = false;
        }
        public Literal_Trad() :
            base("Literal")
        {
            this.isNull = true;
        }
        public override string Execute(Ambit_Trad ambit)
        {
            return this.value;
        }
    }
}
