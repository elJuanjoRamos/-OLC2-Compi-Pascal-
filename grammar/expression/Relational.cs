using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.expression
{
    class Relational: Expression
    {
        private Expression left;
        private Expression right;
        private string type;
        public  int row;
        public  int column;

        public Relational(Expression left, Expression right, string type, int row, int column)
        {
            this.left = left;
            this.right = right;
            this.type = type;
            this.row = row;
            this.column = column;
        }

        public override Returned Execute(Ambit ambit)
        {
            var valIz = this.left.Execute(ambit);
            var valDer = this.right.Execute(ambit);
            var result = new Returned();
            var op = GetType(this.type);

            if (valIz.getDataType == DataType.INTEGER || valIz.getDataType == DataType.REAL)
            {
                if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                {

                    switch (op)
                    {   
                        case OpRelational.EQUALS:
                            result = new Returned((double.Parse(valIz.Value.ToString()) == double.Parse(valDer.Value.ToString())), DataType.BOOLEAN);
                            break;
                        case OpRelational.DISCTINCT:
                            result = new Returned((double.Parse(valIz.Value.ToString()) != double.Parse(valDer.Value.ToString())), DataType.BOOLEAN);
                            break;
                        case OpRelational.LESS:
                            result = new Returned((double.Parse(valIz.Value.ToString()) < double.Parse(valDer.Value.ToString())), DataType.BOOLEAN);
                            break;
                        case OpRelational.LESS_EQUALS:
                            result = new Returned((double.Parse(valIz.Value.ToString()) <= double.Parse(valDer.Value.ToString())), DataType.BOOLEAN);
                            break;
                        case OpRelational.HIGHER:
                            result = new Returned((double.Parse(valIz.Value.ToString()) > double.Parse(valDer.Value.ToString())), DataType.BOOLEAN);
                            break;
                        case OpRelational.HIGHER_EQUALS:
                            result = new Returned((double.Parse(valIz.Value.ToString()) >= double.Parse(valDer.Value.ToString())), DataType.BOOLEAN);
                            break;
                        default:
                            break;
                    }
                }
                else if (valIz.getDataType == DataType.BOOLEAN)
                {
                    if (valDer.getDataType == DataType.BOOLEAN)
                    {
                        result = new Returned((bool.Parse(valIz.Value.ToString()) == bool.Parse(valDer.Value.ToString())), DataType.BOOLEAN);
                    }
                    else
                    {
                        var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType;
                        //ErrorController.Instance.add(texto)
                    }
                }
                else
                {
                    var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType;
                    //ErrorController.Instance.add(texto)
                }
            }
            else
            {
                var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType;
                //ErrorController.Instance.add(texto)
            }

            

            


            return result;
        }
        public OpRelational GetType(string simb)
        {
            if (simb.Equals(">"))
            {
                return OpRelational.HIGHER;
            }
            if (simb.Equals("<"))
            {
                return OpRelational.LESS;
            }
            if (simb.Equals(">="))
            {
                return OpRelational.HIGHER_EQUALS;
            }
            if (simb.Equals("<"))
            {
                return OpRelational.HIGHER;
            }
            if (simb.Equals("<>"))
            {
                return OpRelational.DISCTINCT;
            }

            return OpRelational.EQUALS;
        }
    }
}
