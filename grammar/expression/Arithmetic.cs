using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.expression
{
    public class Arithmetic : Expression
    {
        private Expression left;
        private Expression right;
        private String type;

        public Arithmetic(Expression l, Expression ri, String t)
        : base("Arithmetic")
        {
            this.right = ri;
            this.left = l;
            this.type = t;
        }

        public override Returned Execute(Ambit ambit)
        {
            var result = new Returned();
            var varIz = this.left.Execute(ambit);
            var valDer = this.right.Execute(ambit);

            switch (this.type)
            {
                case "+":

                    if (varIz.DataType == 0)
                    {
                        if (valDer.DataType == 0)
                        {
                            result = new Returned((int.Parse(varIz.Value.ToString()) + int.Parse(valDer.Value.ToString())), DataType.INTEGER);
                        }
                    }

                    break;
            }
            return result;
        }
    }
}
