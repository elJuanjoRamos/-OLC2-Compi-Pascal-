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
            var valDer = this.right.Execute(ambit);
            var op = GetOpLogical(this.type);
            if (varIz.Value is bool)
            {
                var izq = (bool)varIz.Value;
                if (valDer.Value is bool)
                {

                    if (op != OpLogical.NOT)
                    {
                        var der = (bool)(valDer.Value);
                        var res = false;
                        if (op == OpLogical.AND)
                        {
                            res = izq && der;
                        }
                        if (op == OpLogical.OR)
                        {
                            res = izq || der;
                        }
                        result = new Returned(res, DataType.BOOLEAN);

                    } else
                    {
                        result = new Returned(!izq, DataType.BOOLEAN);

                    }
                }

                else
                {
                    var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                    ErrorController.Instance.add(texto);
                }

            }
            else
            {
                var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                ErrorController.Instance.add(texto);
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
