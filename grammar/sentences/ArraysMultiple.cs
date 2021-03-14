using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class ArraysMultiple : Instruction
    {
        private string id;
        private Expression inf;
        private Expression sup;
        private ArrayList elementos;
        private DataType dataType;

        public ArraysMultiple(string id, Expression inf, Expression sup, string dt, ArrayList elems)
        {
            this.id = id;
            this.inf = inf;
            this.sup = sup;
            this.elementos = elems;
            this.dataType = (GetDataType(dt));
        }
        public override object Execute(Ambit ambit)
        {
            return 0;
        }


        public DataType GetDataType(string d)
        {
            if (d.Equals("integer"))
            {
                return DataType.INTEGER;
            }
            else if (d.Equals("boolean"))
            {
                return DataType.BOOLEAN;
            }
            else if (d.Equals("real"))
            {
                return DataType.REAL;
            }
            return DataType.STRING;

        }
    }
}
