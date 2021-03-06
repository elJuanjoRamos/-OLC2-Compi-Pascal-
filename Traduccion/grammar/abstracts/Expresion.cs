using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.abstracts
{
    public abstract class Expresion_Trad
    {
        //public int row;
        //public int column;
        public string name;
        public Expresion_Trad(/*int r, int c, */string n)
        {
            //this.row = r;
            //this.column = c;
            this.name = n;
        }
        public Expresion_Trad()
        {
        }

        public abstract string Execute(Ambit_Trad ambit);
    }
}
