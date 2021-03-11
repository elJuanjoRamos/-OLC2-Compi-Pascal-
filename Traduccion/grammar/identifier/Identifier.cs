using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.identifier
{
    public class Identifier_Trad
    {
        private string _value;
        private string id;
        private string type;
        private bool isNull;
        private string tipo_dato;

        public Identifier_Trad(string v, string i, string t, string tipo)
        {
            this._value = v;
            this.id = i;
            this.type = t;
            this.isNull = false;
            this.tipo_dato = tipo;
        }
        public Identifier_Trad()
        {
            this.isNull = true;
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string DataType
        {
            get { return type; }
            set { type = value; }
        }

        public bool IsNull { get => isNull; set => isNull = value; }
        public string Tipo_dato { get => tipo_dato; set => tipo_dato = value; }
    }
}
