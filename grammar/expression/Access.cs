using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.expression
{
    public class Access : Expression
    {
        private string id;  
        public  int row;
        public int column;

        public string Id { get => id; set => id = value; }

        public Access(string id)
            : base("Access")
        {
            this.Id = id;
        }

        public override Returned Execute(Ambit ambit)
        {
            Identifier value = ambit.getVariable(this.id.ToLower());
            if (value.IsNull)
            {
                ConsolaController.Instance.Add("Este es un error: La variable '" + this.id + "' no ha sido declarada o no existe en el ambito " + ambit.Ambit_name);
                ErrorController.Instance.SemantycErrors("Este es un error: La variable '" + this.id + "' no ha sido declarada o no existe en el ambito " + ambit.Ambit_name, 0, 0);

                return new Returned("Este es un error: La variable '" + this.id + "' no ha sido declarada o no existe en este ambito " +ambit.Ambit_name);

            }
            return new Returned(value.Value, value.DataType);
        }
    }
}
