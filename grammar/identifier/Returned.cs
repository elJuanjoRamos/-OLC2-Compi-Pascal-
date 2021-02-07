using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.grammar.abstracts;

namespace CompiPascal.grammar.identifier
{
    public class Returned
    {
        Object _value;
        DataType type;

        public Returned(Object v, DataType d)
        {
            this._value = v;
            this.type = d;
        }
        public Returned()
        {
        }

        public Object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public DataType DataType
        {
            get { return type; }
            set { type = value; }
        }

    }

}
