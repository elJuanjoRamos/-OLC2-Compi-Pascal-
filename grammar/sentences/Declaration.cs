using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;

namespace CompiPascal.grammar.sentences
{
    public class Declaration : Instruction
    {
        private string id;
        private DataType type;
        private Expression value;
        public int row;
        public int column;


        public Declaration(string i, String d, Expression e, int r, int c)
            : base(r, c)
        {
            this.id = i;
            this.type = GetDataType(d);
            this.value = e;
            this.row = r;
            this.column = c;
        }

        public override void Execute(Ambit ambit)
        {
            try
            {
                var val = this.value.Execute(ambit);

                if (val.getDataType == this.type)
                {
                    ambit.save(this.id, val.Value, val.getDataType, false);
                    SimbolTableController.Instance.add(this.id, this.type, ambit.Ambit_name, ((Expression)val.Value), true, false);


                } else
                {
                    //ErrorController.Instance.add("El tipo " + val.Value.ToString() + " no es asignable con " + this.type.ToString());
                }

            }
            catch (Exception)
            {

            }

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
