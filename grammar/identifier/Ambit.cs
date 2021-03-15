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
        Dictionary<string, ArraysMultiple> arraysmulti;
        Dictionary<string, Types> types;
        private string ambit_name = "";
        private string ambit_name_inmediato = "";
        public Ambit anterior;
        public Boolean ambit_null;

        public Ambit(Ambit a, string n, string ni, bool isnull)
        {
            this.variables = new Dictionary<string, Identifier>();
            this.functions = new Dictionary<string, Function>();
            this.arrays = new Dictionary<string, Arrays>();
            this.arraysmulti = new Dictionary<string, ArraysMultiple>();
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
            this.arraysmulti = new Dictionary<string, ArraysMultiple>();
            this.ambit_null = true;
            this.ambit_name = "General";
            this.ambit_name_inmediato = "General";
        }



        public void save(string id, object valor, DataType type, bool esconstante, bool isAssigned, string tipo_dato)
        {
            Ambit amb = this;

            if (!amb.Ambit_name_inmediato.Equals("Function"))
            {
                if (!amb.variables.ContainsKey(id.ToLower()))
                {
                    amb.variables[id.ToLower()] = (new Identifier(valor, id, type, esconstante, isAssigned, false, tipo_dato));
                }
            }
            else
            {
                saveVarFunction(id, valor, type, esconstante, isAssigned);
            }

           
            /*Ambit amb = this;

            while (amb != null)
            {
                if (amb.variables.ContainsKey(id.ToLower()))
                {
                    amb.variables[id.ToLower()] = (new Identifier(valor, id, type, esconstante, isAssigned));
                    return;
                }

                amb = amb.anterior;
            }
            variables.Add(id, new Identifier(valor, id, type, esconstante, isAssigned));*/
        }





        public void saveVarFunction(string id, object valor, DataType type, bool esconstante, bool isAssigned)
        {
            Ambit amb = this;

            if (!amb.variables.ContainsKey(id.ToLower()))
            {
                amb.variables[id.ToLower()] = (new Identifier(valor, id, type, esconstante, isAssigned, true, "Variable"));
            }
            
        }

        public Identifier getVariable(string id)
        {
            Identifier identifier = new Identifier();
            Ambit amb = this;
            while (amb != null)
            {
                if (amb.Variables.ContainsKey(id.ToLower()))
                {
                    identifier = amb.Variables[id.ToLower()];
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
                if (amb.Variables.ContainsKey(id.ToLower()))
                {
                    identifier = amb.Variables[id.ToLower()];
                }
                amb = amb.anterior;
            }
            
            return identifier;
        }

        public Identifier getVariableFunctionInAmbit(string id)
        {
            Identifier identifier = new Identifier();
            Ambit amb = this;
            if (amb.Variables.ContainsKey(id.ToLower()))
            {
                identifier = amb.Variables[id.ToLower()];
            }
            return identifier;
        }

        public Function getFuncion(string id)
        {
            Ambit amb = this;
            while (amb != null)
            {
                if (amb.Functions.ContainsKey(id.ToLower()))
                {
                    return amb.Functions[id.ToLower()];
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
                if (env.Variables.ContainsKey(id.ToLower()))
                {
                    env.Variables[id.ToLower()] = new Identifier(valor, id, type, false, isAssigned, false, tipo_dato);
                    return;
                }
                env = env.anterior;
            }
        }

        public void setVariableInAmbit(string id, object valor, DataType type, bool isAssigned, string tipo_dato)
        {
            Ambit env = this;

            while (env != null)
            {
                if (env.Variables.ContainsKey(id.ToLower()))
                {
                    env.Variables[id.ToLower()] = new Identifier(valor, id, type, false, isAssigned, false, tipo_dato);
                }
                env = env.anterior;
            }
        }

        public void setVariableFuncion(string id, object valor, DataType type, bool isAssigned, string tipo_dato)
        {
            Ambit env = this;

            if (env.Variables.ContainsKey(id.ToLower()))
            {
                env.Variables[id.ToLower()] = new Identifier(valor, id, type, false, isAssigned, true, tipo_dato);
            }
        }

        public void saveFuncion(string id, Function function)
        {
            Ambit amb = this;

            if (!amb.functions.ContainsKey(id.ToLower()))
            {
                amb.functions[id.ToLower()] = function;
            }

            /*while (amb != null)
            {
                if (amb.functions.ContainsKey(id.ToLower()))
                {
                    amb.functions[id.ToLower()] = function;
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
                if (env.Functions.ContainsKey(id.ToLower()))
                {
                    env.Functions[id.ToLower()] = function;
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

            if (!amb.Arrays.ContainsKey(id.ToLower()))
            {
                amb.Arrays[id.ToLower()] = arrays;
            }
        }


        public Arrays getArray(string id)
        {
            Ambit amb = this;

            while (amb != null)
            {
                if (amb.Arrays.ContainsKey(id.ToLower()))
                {
                    return amb.Arrays[id.ToLower()];
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
                if (env.Arrays.ContainsKey(id.ToLower()))
                {
                    env.Arrays[id.ToLower()] = tipo_dato;
                    return;
                }
                env = env.anterior;
            }
        }

        public void saveArrayMultiple(string id, ArraysMultiple arrays)
        {
            Ambit amb = this;

            if (!amb.Arraysmulti.ContainsKey(id.ToLower()))
            {
                amb.Arraysmulti[id.ToLower()] = arrays;
            }
        }


        public ArraysMultiple getArrayMulti(string id)
        {
            Ambit amb = this;

            while (amb != null)
            {
                if (amb.Arraysmulti.ContainsKey(id.ToLower()))
                {
                    return amb.Arraysmulti[id.ToLower()];
                }
                amb = amb.anterior;
            }


            return null;
        }

        public void setArrayMulti(string id, ArraysMultiple tipo_dato)
        {
            Ambit env = this;

            while (env != null)
            {
                if (env.Arraysmulti.ContainsKey(id.ToLower()))
                {
                    env.Arraysmulti[id.ToLower()] = tipo_dato;
                    return;
                }
                env = env.anterior;
            }
        }
        #endregion

        #region Types 
        public void saveType(string id, Types types)
        {
            Ambit amb = this;

            if (!amb.Types.ContainsKey(id.ToLower()))
            {
                amb.Types[id.ToLower()] = types;
            }
        }
        #endregion
        public string Ambit_name { get => ambit_name; set => ambit_name = value; }
        public bool IsNull { get => ambit_null; set => ambit_null = value; }
        internal Dictionary<string, Identifier> Variables { get => variables; set => variables = value; }
        public string Ambit_name_inmediato { get => ambit_name_inmediato; set => ambit_name_inmediato = value; }
        internal Dictionary<string, Function> Functions { get => functions; set => functions = value; }
        internal Dictionary<string, Arrays> Arrays { get => arrays; set => arrays = value; }
        internal Dictionary<string, Types> Types { get => types; set => types = value; }
        internal Dictionary<string, ArraysMultiple> Arraysmulti { get => arraysmulti; set => arraysmulti = value; }
    }
}
