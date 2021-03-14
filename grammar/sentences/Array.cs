using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    public class Arrays : Instruction
    {
        private string id;
        private Expression inf;
        private Expression sup;
        private ArrayList elementos;
        private DataType dataType;

        public Arrays(string id, Expression inf, Expression sup, string dt)
        {
            this.id = id;
            this.inf = inf;
            this.sup = sup;
            this.elementos = new ArrayList();
            this.dataType = GetDataType(dt);
        }
        public Arrays(string id, Expression inf, Expression sup, DataType dt, ArrayList elems)
        {
            this.id = id;
            this.inf = inf;
            this.sup = sup;
            this.elementos = elems;
            this.dataType = (dt);
        }
        public Arrays(string id, Expression inf, Expression sup, DataType dt)
        {
            this.id = id;
            this.inf = inf;
            this.sup = sup;
            this.elementos = new ArrayList();
            this.dataType = (dt);
        }
        public Expression Inf { get => inf; set => inf = value; }
        public Expression Sup { get => sup; set => sup = value; }
        public ArrayList Elementos { get => elementos; set => elementos = value; }
        public DataType DataType { get => dataType; set => dataType = value; }

        public override object Execute(Ambit ambit)
        {
            var limite_inf = inf.Execute(ambit);

            var limite_sup = sup.Execute(ambit);

            var lf = int.Parse(limite_sup.Value.ToString()) - int.Parse(limite_inf.Value.ToString());

            switch (dataType)
            {
                case DataType.INTEGER:
                    for (int i = 0; i <= lf+1; i++)
                    {
                        elementos.Add(0);
                    }
                    break;
                case DataType.STRING:
                    for (int i = 0; i <= lf + 1; i++)
                    {
                        elementos.Add("");
                    }
                    break;
                case DataType.BOOLEAN:
                    for (int i = 0; i <= lf + 1; i++)
                    {
                        elementos.Add(false);
                    }
                    break;
                case DataType.REAL:
                    for (int i = 0; i <= lf + 1; i++)
                    {
                        elementos.Add(0.0);
                    }
                    break;
                default:
                    break;
            }

            ambit.saveArray(id.ToLower(), this);
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
