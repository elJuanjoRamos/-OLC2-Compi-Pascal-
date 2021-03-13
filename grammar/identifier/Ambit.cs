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
        Dictionary<string, Arrays> arrays;
        private string ambit_name = "";
        private string ambit_name_inmediato = "";
        public Ambit anterior;
        public Boolean ambit_null;

        public Ambit(Ambit a, string n, string ni, bool isnull)
        {
            this.variables = new Dictionary<string, Identifier>();
            this.functions = new Dictionary<string, Function>();
            this.arrays = new Dictionary<string, Arrays>();
            this.ambit_name = n;
            this.ambit_name_inmediato = ni;
            this.anterior = a;
            this.ambit_null = isnull;

        }
        public Ambit()
        {

            this.variables = new Dictionary<string, Identifier>();
            this.functions = new Dictionary<string, Function>();
            this.arrays = new Dictionary<string, Arrays>();
            this.ambit_null = true;
            this.ambit_name = "General";
            this.ambit_name_inmediato = "General";
        }



        public void save(string id, object valor, DataType type, bool esconstante, bool isAssigned, string tipo_dato)
        {
            Ambit amb = this;

            if (!amb.Ambit_name_inmediato.Equals("Function"))
            {
                if (!amb.variables.ContainsKey(id))
                {
                    amb.variables[id] = (new Identifier(valor, id, type, esconstante, isAssigned, false, tipo_dato));
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
                amb.variables[id] = (new Identifier(valor, id, type, esconstante, isAssigned, true, "Variable"));
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

        public Identifier getVariableFunctionInAmbit(string id)
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
        
        public void setVariable(string id, object valor, DataType type, bool isAssigned, string tipo_dato)
        {
            Ambit env = this;
            
            while (env != null)
            {
                if (env.Variables.ContainsKey(id))
                {
                    env.Variables[id] = new Identifier(valor, id, type, false, isAssigned, false, tipo_dato);
                    return;
                }
                env = env.anterior;
            }
        }

        public void setVariableFuncion(string id, object valor, DataType type, bool isAssigned, string tipo_dato)
        {
            Ambit env = this;

            if (env.Variables.ContainsKey(id))
            {
                env.Variables[id] = new Identifier(valor, id, type, false, isAssigned, true, tipo_dato);
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


        public void setFunction(string id, Function function)
        {
            Ambit env = this;

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

       

        public Ambit getGeneral()
        {
            Ambit amb = this;

            while (amb.anterior != null)
            {
                amb = amb.anterior;
            }
            return amb;
        }


        #region ARREGLOS

        public void saveArray(string id, Arrays arrays)
        {
            Ambit amb = this;

            if (!amb.Arrays.ContainsKey(id))
            {
                amb.Arrays[id] = arrays;
            }
        }


        public Arrays getArray(string id)
        {
            Ambit amb = this;

            while (amb != null)
            {
                if (amb.Arrays.ContainsKey(id))
                {
                    return amb.Arrays[id];
                }
                amb = amb.anterior;
            }
            

            return null;
        }

        public void setArray(string id, Arrays tipo_dato)
        {
            Ambit env = this;

            while (env != null)
            {
                if (env.Arrays.ContainsKey(id))
                {
                    env.Arrays[id] = tipo_dato;
                    return;
                }
                env = env.anterior;
            }
        }
        #endregion


        public string Ambit_name { get => ambit_name; set => ambit_name = value; }
        public bool IsNull { get => ambit_null; set => ambit_null = value; }
        internal Dictionary<string, Identifier> Variables { get => variables; set => variables = value; }
        public string Ambit_name_inmediato { get => ambit_name_inmediato; set => ambit_name_inmediato = value; }
        internal Dictionary<string, Function> Functions { get => functions; set => functions = value; }
        internal Dictionary<string, Arrays> Arrays { get => arrays; set => arrays = value; }
    }
}
