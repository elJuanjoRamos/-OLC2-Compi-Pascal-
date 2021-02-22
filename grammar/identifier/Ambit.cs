using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.sentences;

namespace CompiPascal.grammar.identifier
{
    public class Ambit
    {
        Dictionary<string, Identifier> variables;
        Dictionary<string, Function> functions;
        private string ambit_name = "";
        private string ambit_name_inmediato = "";
        public Ambit anterior;
        public Boolean ambit_null;

        public Ambit(Ambit a, string n, string ni, bool isnull)
        {
            this.variables = new Dictionary<string, Identifier>();
            this.functions = new Dictionary<string, Function>();
            this.ambit_name = n;
            this.ambit_name_inmediato = ni;
            this.anterior = a;
            this.ambit_null = isnull;

        }
        public Ambit()
        {

            this.variables = new Dictionary<string, Identifier>();
            this.functions = new Dictionary<string, Function>();
            this.ambit_null = true;
            this.ambit_name = "General";
            this.ambit_name_inmediato = "General";
        }



        public void save(string id, object valor, DataType type, bool esconstante, bool isAssigned)
        {
            Ambit amb = this;

            if (!amb.Ambit_name_inmediato.Equals("Function"))
            {
                if (!amb.variables.ContainsKey(id))
                {
                    amb.variables[id] = (new Identifier(valor, id, type, esconstante, isAssigned, false));
                }
            }
            else
            {
                saveVarFunction(id, valor, type, esconstante, isAssigned);
            }

           
            /*Ambit amb = this;

            while (amb != null)
            {
                if (amb.variables.ContainsKey(id))
                {
                    amb.variables[id] = (new Identifier(valor, id, type, esconstante, isAssigned));
                    return;
                }

                amb = amb.anterior;
            }
            variables.Add(id, new Identifier(valor, id, type, esconstante, isAssigned));*/
        }

        public void saveVarFunction(string id, object valor, DataType type, bool esconstante, bool isAssigned)
        {
            Ambit amb = this;

            if (!amb.variables.ContainsKey(id))
            {
                amb.variables[id] = (new Identifier(valor, id, type, esconstante, isAssigned, true));
            }
            
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
                    break; 
                }
                amb = amb.anterior;
            }
            return identifier;
        }

        public Identifier getVariableFunction(string id)
        {
            Identifier identifier = new Identifier();
            Ambit amb = this;
            if (amb.Variables.ContainsKey(id))
            {
                identifier = amb.Variables[id];
            }
            return identifier;
        }

        public Function getFuncion(string id)
        {
            Ambit amb = this;
            while (amb != null)
            {
                if (amb.Functions.ContainsKey(id))
                {
                    return amb.Functions[id];
                }
                amb = amb.anterior;
            }
            return null;
        }

        public void setVariable(string id, object valor, DataType type, bool isAssigned)
        {
            Ambit env = this;
            
            while (env != null)
            {
                if (env.Variables.ContainsKey(id))
                {
                    env.Variables[id] = new Identifier(valor, id, type, false, isAssigned, false);
                    return;
                }
                env = env.anterior;
            }
        }

        public void setVariableFuncion(string id, object valor, DataType type, bool isAssigned)
        {
            Ambit env = this;

            if (env.Variables.ContainsKey(id))
            {
                env.Variables[id] = new Identifier(valor, id, type, false, isAssigned, true);
            }
        }

        public void saveFuncion(string id, Function function)
        {
            Ambit amb = this;

            if (!amb.functions.ContainsKey(id))
            {
                amb.functions[id] = function;
            }

            /*while (amb != null)
            {
                if (amb.functions.ContainsKey(id))
                {
                    amb.functions[id] = function;
                    return;
                }

                amb = amb.anterior;
            }
            functions.Add(id, function);*/
        }

        public Ambit getGeneral()
        {
            Ambit amb = this;

            while (amb.anterior != null)
            {
                amb = amb.anterior;
            }
            return amb;
        }

        public string Ambit_name { get => ambit_name; set => ambit_name = value; }
        public bool IsNull { get => ambit_null; set => ambit_null = value; }
        internal Dictionary<string, Identifier> Variables { get => variables; set => variables = value; }
        public string Ambit_name_inmediato { get => ambit_name_inmediato; set => ambit_name_inmediato = value; }
        internal Dictionary<string, Function> Functions { get => functions; set => functions = value; }
    }
}
