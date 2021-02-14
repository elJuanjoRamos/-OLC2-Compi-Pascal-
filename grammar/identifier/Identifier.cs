using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.grammar.abstracts;

namespace CompiPascal.grammar.identifier
{
    public class Identifier
    {
        private object _value;
        private string id;
        private DataType type;
        private bool esconsante;
        private bool isNull;
        public Identifier(object v, string i, DataType t, bool ec)
        {
            this._value = v;
            this.id = i;
            this.type = t;
            this.esconsante = ec;
            this.isNull = false;
        }
        public Identifier()
        {
            this.isNull = true;
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public DataType DataType
        {
            get { return type; }
            set { type = value; }
        }

        public bool Esconstante
        {
            get { return esconsante; }
            set { esconsante = value; }
        }
        public bool IsNull
        {
            get { return isNull; }
            set { isNull = value; }
        }

    }
}
