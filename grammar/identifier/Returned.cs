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
        bool isNull;

        public Returned(Object v, DataType d)
        {
            this._value = v;
            this.type = d;
            this.isNull = false;
        }
        public Returned()
        {
            this.isNull = true;
        }

        public Object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public DataType getDataType
        {
            get { return type; }
            set { type = value; }
        }
        public bool IsNull
        {
            get { return isNull; }
            set { isNull = value; }
        }

    }

}
