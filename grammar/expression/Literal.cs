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
        private int row;
        private int column;
        public Literal(Object v, int t, int r, int c) :
            base(r,c,"Literal")
        {
            this.value = v;
            this.type = t;
            this.isNull = false;
            this.row = r;
            this.column = c;

        }
        public Literal(int r, int c) :
            base(r, c,"Literal")
        {
            this.isNull = true;
            this.row = r;
            this.column = c;
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
                if (this.value.ToString() == "false")
                {
                    returned = new Returned(false, DataType.BOOLEAN);
                }
                else
                {
                    returned = new Returned(true, DataType.BOOLEAN);
                }
            }
            else if (this.type == 4)
            {
                returned = new Returned(double.Parse(this.value.ToString()), DataType.REAL);
            }

            else if (this.type == 7)
            {
                returned = new Returned(this.value.ToString(), DataType.IDENTIFIER);
            }
            return returned;

        }

        public bool IsNull { get => isNull; set => isNull = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
    }
}
