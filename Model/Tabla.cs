using CompiPascal.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Model
{
    class Tabla
    {
        private string name;
        private DataType type;
        private string ambit;
        private Expression _value;
        private Object returned;
        private bool variable;
        private bool funcion;
        private int row;
        private int column;

        public Tabla(string name, DataType type, string ambit, Expression value, bool variable, bool funcion)
        {
            this.name = name;
            this.type = type;
            this.ambit = ambit;
            this._value = value;
            this.returned = null;
            this.variable = variable;
            this.funcion = funcion;
            this.row = 0;
            this.column = 0;
        }

        public DataType Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public string Ambit { get => ambit; set => ambit = value; }
        public Expression Value { get => _value; set => _value = value; }
        public bool Variable { get => variable; set => variable = value; }
        public bool Funcion { get => funcion; set => funcion = value; }
    }
}
