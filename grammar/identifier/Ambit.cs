using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.grammar.abstracts;

namespace CompiPascal.grammar.identifier
{
    public class Ambit
    {
        private Dictionary<string, Identifier> variables;
        //private Dictionary<string, Function> functions = new Dictionary<string, Function>();
        private Dictionary<string, Identifier> types;
        private string ambit_name = "";
        public Ambit anterior;
        public int ambit_null;

        public string Ambit_name { get => ambit_name; set => ambit_name = value; }

        public Ambit(Ambit a, string n, int isnull)
        {
            this.variables = new Dictionary<string, Identifier>();
            this.types = new Dictionary<string, Identifier>();
            this.ambit_name = n;
            this.anterior = a;
            this.ambit_null = isnull;

        }

        public Ambit(string n, int isnull)
        {
            this.variables = new Dictionary<string, Identifier>();
            this.types = new Dictionary<string, Identifier>();
            this.ambit_name = n;
            this.ambit_null = isnull;
        }

        public Ambit()
        {
            this.variables = new Dictionary<string, Identifier>();
            this.types = new Dictionary<string, Identifier>();

        }



        public void save(string id, object valor, DataType type, bool esconstante)
        {
            Ambit amb = this;

            while (amb != null)
            {
                if (amb.variables.ContainsKey(id))
                {
                    amb.variables[id] = (new Identifier(valor, id, type, esconstante));
                    break;
                }

                amb = amb.anterior;
            }
            this.variables.Add(id, new Identifier(valor, id, type, esconstante));
        }



    }
}
