using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.grammar.abstracts;

namespace CompiPascal.grammar.identifier
{
    public class Ambit
    {
        Dictionary<string, Identifier> variables;
        //private Dictionary<string, Function> functions = new Dictionary<string, Function>();
        Dictionary<string, Identifier> types;
        private string ambit_name = "";
        public Ambit anterior;
        public Boolean ambit_null;

        public Ambit(Ambit a, string n, bool isnull)
        {
            this.variables = new Dictionary<string, Identifier>();
            this.types = new Dictionary<string, Identifier>();
            this.ambit_name = n;
            this.anterior = a;
            this.ambit_null = isnull;

        }
        public Ambit()
        {

            this.variables = new Dictionary<string, Identifier>();
            this.types = new Dictionary<string, Identifier>();
            this.ambit_null = true;
            this.ambit_name = "General";
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
            variables.Add(id, new Identifier(valor, id, type, esconstante));
        }

        public Identifier getVariable(string id)
        {
            Identifier identifier = new Identifier();
            Ambit amb = this;
            while (amb != null)
            {
                if (amb.Variables.ContainsKey(id))
                {
                    identifier = amb.Variables[id];
                }
                amb = amb.anterior;
            }
            return identifier;
        }



        public string Ambit_name { get => ambit_name; set => ambit_name = value; }
        public bool IsNull { get => ambit_null; set => ambit_null = value; }
        internal Dictionary<string, Identifier> Variables { get => variables; set => variables = value; }
    }
}
