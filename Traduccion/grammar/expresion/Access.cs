using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.expresion
{
    public class Access_Trad : Expresion_Trad
    {
        private string id;
        public int row;
        public int column;

        public string Id { get => id; set => id = value; }

        public Access_Trad(string id)
            : base("Access")
        {
            this.Id = id;
        }

        public override string Execute(Ambit_Trad ambit)
        {
            return this.Id;
        }
    }
}
