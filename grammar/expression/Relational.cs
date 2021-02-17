using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using CompiPascal.Model;
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


            TablaTipo tablaTipo = new TablaTipo();

            var res = tablaTipo.getTipoRel(valIz.getDataType, valDer.getDataType);

            if (res == DataType.ERROR)
            {
                ErrorController.Instance.SemantycErrors("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, 0, 0);
                return new Returned();
            }


            var result = new Returned();
            var op = GetType(this.type);

            switch (op)
            {


                case OpRelational.EQUALS:

                    var equals = (bool)(valIz.Value.ToString() == valDer.Value.ToString());
                    result = new Returned(equals, DataType.BOOLEAN);
                    break;
                case OpRelational.DISCTINCT:
                    var distict = (bool)(valIz.Value.ToString() != valDer.Value.ToString());
                    result = new Returned(distict, DataType.BOOLEAN);
                    break;
                case OpRelational.LESS:
                    var less = (bool)(double.Parse(valIz.Value.ToString()) < double.Parse(valDer.Value.ToString()));
                    result = new Returned(less, DataType.BOOLEAN);

                    /*if (valIz.getDataType == DataType.INTEGER || valIz.getDataType == DataType.REAL)
                    {
                        if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                        {
                            var less = (bool)(double.Parse(valIz.Value.ToString()) < double.Parse(valDer.Value.ToString()));
                            result = new Returned(less, DataType.BOOLEAN);
                        }
                        else
                        {
                            ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType,0,0);
                            return new Returned();
                        }

                    } else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, 0, 0);
                        return new Returned();
                    }*/



                    break;
                case OpRelational.LESS_EQUALS:
                    var lessE = (bool)(double.Parse(valIz.Value.ToString()) <= double.Parse(valDer.Value.ToString()));
                    result = new Returned(lessE, DataType.BOOLEAN);

                    /*if (valIz.getDataType == DataType.INTEGER || valIz.getDataType == DataType.REAL)
                    {
                        if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                        {
                            var less = (bool)(double.Parse(valIz.Value.ToString()) <= double.Parse(valDer.Value.ToString()));
                            result = new Returned(less, DataType.BOOLEAN);
                        }
                        else
                        {
                            ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, 0, 0);
                            return new Returned();
                        }

                    }
                    else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, 0, 0);
                        return new Returned();
                    }*/
                    break;
                case OpRelational.HIGHER:
                    var higher = (bool)(double.Parse(valIz.Value.ToString()) > double.Parse(valDer.Value.ToString()));
                    result = new Returned(higher, DataType.BOOLEAN);

                    /*if (valIz.getDataType == DataType.INTEGER || valIz.getDataType == DataType.REAL)
                    {
                        if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                        {
                            var higher = (bool)(double.Parse(valIz.Value.ToString()) > double.Parse(valDer.Value.ToString()));
                            result = new Returned(higher, DataType.BOOLEAN);
                        }
                        else
                        {
                            ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, 0, 0);
                            return new Returned();
                        }

                    }
                    else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, 0, 0);
                        return new Returned();
                    }*/
                    break;
                case OpRelational.HIGHER_EQUALS:
                    var higherE = (bool)(double.Parse(valIz.Value.ToString()) >= double.Parse(valDer.Value.ToString()));
                    result = new Returned(higherE, DataType.BOOLEAN);

                    /*if (valIz.getDataType == DataType.INTEGER || valIz.getDataType == DataType.REAL)
                    {
                        if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                        {
                            var higher = (bool)(double.Parse(valIz.Value.ToString()) >= double.Parse(valDer.Value.ToString()));
                            result = new Returned(higher, DataType.BOOLEAN);
                        }
                        else
                        {
                            ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, 0, 0);
                            return new Returned();
                        }

                    }
                    else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, 0, 0);
                        return new Returned();
                    }*/
                    break;
                default:
                    break;
            }

            return result;
        }
        public OpRelational GetType(string simb)
        {
            if (simb.Equals(">"))
            {
                return OpRelational.HIGHER;
            }
            else if (simb.Equals("<"))
            {
                return OpRelational.LESS;
            }
            else if (simb.Equals(">="))
            {
                return OpRelational.HIGHER_EQUALS;
            }
            else if (simb.Equals("<"))
            {
                return OpRelational.HIGHER;
            }
            else if (simb.Equals("<="))
            {
                return OpRelational.LESS_EQUALS;
            }
            else if (simb.Equals("<>"))
            {
                return OpRelational.DISCTINCT;
            }

            return OpRelational.EQUALS;
        }
    }
}
