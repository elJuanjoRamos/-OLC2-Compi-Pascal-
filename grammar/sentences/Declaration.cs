using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.expression;
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
        public bool isConst;
        public bool isAssigned;

        public Declaration(string i, String d, Expression e, int r, int c, bool isAs)
            : base(r, c, "Declaration")
        {
            this.id = i;
            this.type = GetDataType(d);
            this.value = e;
            this.row = r;
            this.column = c;
            this.isConst = false;
            this.isAssigned = isAs;
        }
        public Declaration(string i, Expression e, int r, int c, bool isc)
            : base(r, c, "Declaration")
        {
            this.id = i;
            this.type = DataType.CONST;
            this.value = e;
            this.row = r;
            this.column = c;
            this.isConst = isc;
            this.isAssigned = true;
        }

        public override object Execute(Ambit ambit)
        {
            try
            {
                Returned val = this.value.Execute(ambit);

                //VERIFICA QUE NO HAYA ERROR
                if (val.getDataType == DataType.ERROR)
                {
                    return null;
                }


                if (this.type == DataType.CONST)
                {
                    ambit.save(this.id, val.Value, val.getDataType, true, true);
                    SimbolTableController.Instance.add(this.id, this.type, ambit.Ambit_name, new Literal(val.Value, 2), true, false);
                    return val.Value;

                } else
                {
                    if (val.getDataType == this.type)
                    {
                        ambit.save(this.id, val.Value, val.getDataType, false, isAssigned);
                        SimbolTableController.Instance.add(this.id, this.type, ambit.Ambit_name, new Literal(val.Value, 2), true, false);
                        return val.Value;
                    }
                    else
                    {
                        ConsolaController.Instance.Add("El tipo " + val.Value.ToString() + " no es asignable con " + this.type.ToString());
                        return null;
                    }
                }

            }
            catch (Exception)
            {

            }

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
