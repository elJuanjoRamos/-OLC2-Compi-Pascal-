﻿using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;

namespace CompiPascal.grammar.expression
{
    public class Literal : Expression
    {
        private Object value;
        private int type;
        private bool isNull;

        public Literal(Object v, int t) :
            base("Literal")
        {
            this.value = v;
            this.type = t;
            this.isNull = false;
        }
        public Literal() :
            base("Literal")
        {
            this.isNull = true;
        }

        public override Returned Execute(Ambit ambit)
        {
            var returned = new Returned();
            if (this.type == 1)
            {
                returned = new Returned(int.Parse(this.value.ToString()), DataType.INTEGER);
            }
            else if (this.type == 2)
            {
                returned = new Returned(this.value.ToString(), DataType.STRING);
            }

            else if (this.type == 3)
            {
                returned = new Returned(this.value.ToString(), DataType.IDENTIFIER);
            }
            else if (this.type == 4)
            {
                returned = new Returned(this.value.ToString(), DataType.STRING);
            }

            else if (this.type == 5)
            {
                if (this.value.ToString() == "false")
                {
                    returned = new Returned(false, DataType.BOOLEAN);
                }
                else
                {
                    returned = new Returned(true, DataType.BOOLEAN);
                }
            }
            return returned;

        }
    }
}
