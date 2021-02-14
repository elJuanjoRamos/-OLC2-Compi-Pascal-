using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.expression
{
    class Logical : Expression
    {
        private Expression left;
        private Expression right;
        private string type ;
        public  int row;
        public int column;


        public Logical(Expression l, Expression r, string t, int ro, int c) 
        : base("Logical")
        {
            this.left = l;
            this.right = r;
            this.type = t;
            this.row = ro;
            this.column = c;
        }

        public override Returned Execute(Ambit ambit)
        {
            var result = new Returned();
            var varIz = this.left.Execute(ambit);

            if (varIz.Value is bool)
            {
                var izq = (bool)varIz.Value;

                var operacion = GetOpLogical(this.type);

                switch (operacion)
                {
                    case OpLogical.AND:
                        var valDer = right.Execute(ambit);
                        var der = (bool)(valDer.Value);
                        result = new Returned((izq && der), DataType.BOOLEAN);

                        break;
                    case OpLogical.OR:
                        var valDere = right.Execute(ambit);
                        var dere = (bool)(valDere.Value);
                        result = new Returned((izq || dere), DataType.BOOLEAN);


                        break;
                    case OpLogical.NOT:

                        result = new Returned(!izq, DataType.BOOLEAN);

                        break;
                    default:
                        break;
                }

            }
            else
            {
                //var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                //ErrorController.Instance.add(texto)
            }

            return result;
        }

        public OpLogical GetOpLogical(string simb)
        {
            if (simb.Equals("and"))
            {
                return OpLogical.AND;
            }
            if (simb.Equals("or"))
            {
                return OpLogical.OR;
            }
            return OpLogical.NOT;
        }
    }
}
