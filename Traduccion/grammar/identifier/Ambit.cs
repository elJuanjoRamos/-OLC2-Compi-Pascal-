using CompiPascal.Traduccion.grammar.identifier;
using CompiPascal.Traduccion.grammar.sentences;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar
{
    public class Ambit_Trad
    {
        Dictionary<string, Identifier_Trad> variables;
        Dictionary<string, Function_Trad> functions;
        Dictionary<string, string> procedures;
        private string ambit_name = "";
        private string actualfunc = "";
        private string unicId = "";
        private string idParent = "";
        private string ambit_name_inmediato = "";
        private Ambit_Trad anterior;
        public Boolean ambit_null;


        public Ambit_Trad(Ambit_Trad a, string n, string ni, bool isnull)
        {
            this.variables = new Dictionary<string, Identifier_Trad>();
            this.functions = new Dictionary<string, Function_Trad>();
            this.procedures = new Dictionary<string, string>();
            this.ambit_name = n;
            this.ambit_name_inmediato = ni;
            this.anterior = a;
            this.ambit_null = isnull;
        }

        public Ambit_Trad()
        {

            this.variables = new Dictionary<string, Identifier_Trad>();
            this.functions = new Dictionary<string, Function_Trad>();
            this.procedures = new Dictionary<string, string>();
            this.ambit_null = true;
            this.ambit_name = "General";
            this.ambit_name_inmediato = "General";
        }


        public void save(string id, string valor, string type)
        {
            Ambit_Trad amb = this;

            if (!amb.Ambit_name_inmediato.Equals("Function"))
            {
                if (!amb.variables.ContainsKey(id))
                {
                    amb.variables[id] = (new Identifier_Trad(valor, id, type));
                }
            }
            else
            {
                saveVarFunction(id, valor, type);
            }


        }

        public void saveVarFunction(string id, string valor, string type)
        {
            Ambit_Trad amb = this;

            if (!amb.variables.ContainsKey(id))
            {
                amb.variables[id] = (new Identifier_Trad(valor, id, type));
            }

        }

        public Identifier_Trad getVariable(string id)
        {
            Identifier_Trad identifier = new Identifier_Trad();
            Ambit_Trad amb = this;
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

        public Function_Trad getFunction(string id)
        {
            Ambit_Trad amb = this;
            while (amb != null)
            {
                if (amb.Variables.ContainsKey(id))
                {
                    return amb.Functions[id];
                }
                amb = amb.anterior;
            }
            return null;
        }

        public Identifier_Trad getVariableFunction(string id)
        {
            Identifier_Trad identifier = new Identifier_Trad();
            Ambit_Trad amb = this;
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

        public Identifier_Trad getVariableFunctionInAmbit(string id)
        {
            Identifier_Trad identifier = new Identifier_Trad();
            Ambit_Trad amb = this;
            if (amb.Variables.ContainsKey(id))
            {
                identifier = amb.Variables[id];
            }
            return identifier;
        }

        public Function_Trad getFuncion(string id)
        {
            Ambit_Trad amb = this;
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


        public void setVariable(string id, string valor, string type)
        {
            Ambit_Trad env = this;

            while (env != null)
            {
                if (env.Variables.ContainsKey(id))
                {
                    env.Variables[id] = new Identifier_Trad(valor, id, type);
                    return;
                }
                env = env.anterior;
            }
        }

        public void setVariableFuncion(string id, string valor, string type)
        {
            Ambit_Trad env = this;

            if (env.Variables.ContainsKey(id))
            {
                env.Variables[id] = new Identifier_Trad(valor, id, type);
            }
        }

       public void saveFuncion(string id, Function_Trad function)
        {
            Ambit_Trad amb = this;

            if (!amb.functions.ContainsKey(id))
            {
                amb.functions[id] = function;
            }
        }


        public void setFunction(string id, Function_Trad function)
        {
            Ambit_Trad env = this;

            while (env != null)
            {
                if (env.Functions.ContainsKey(id))
                {
                    env.Functions[id] = function;
                    return;
                }
                env = env.anterior;
            }
        }

        public Ambit_Trad getGeneral()
        {
            Ambit_Trad amb = this;

            while (amb.anterior != null)
            {
                amb = amb.anterior;
            }
            return amb;
        }
        public string Ambit_name { get => ambit_name; set => ambit_name = value; }
        public bool IsNull { get => ambit_null; set => ambit_null = value; }
        internal Dictionary<string, Identifier_Trad> Variables { get => variables; set => variables = value; }
        public string Ambit_name_inmediato { get => ambit_name_inmediato; set => ambit_name_inmediato = value; }
        internal Dictionary<string, Function_Trad> Functions { get => functions; set => functions = value; }
        public Dictionary<string, string> Procedures { get => procedures; set => procedures = value; }
        public string Actualfunc { get => actualfunc; set => actualfunc = value; }
        public string UnicId { get => unicId; set => unicId = value; }
        public string IdParent { get => idParent; set => idParent = value; }
        public Ambit_Trad Anterior { get => anterior; set => anterior = value; }
    }
}
