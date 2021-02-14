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



            switch (op)
            {
                case OpRelational.EQUALS:
                    var equals = (bool)(valIz.Value == valDer.Value);
                    result = new Returned(equals, DataType.BOOLEAN);
                    break;
                case OpRelational.DISCTINCT:
                    var distict = (bool)(valIz.Value != valDer.Value);
                    result = new Returned(distict, DataType.BOOLEAN);
                    break;
                case OpRelational.LESS:

                    if (valIz.getDataType == DataType.INTEGER || valIz.getDataType == DataType.REAL)
                    {
                        if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                        {
                            var less = (bool)(double.Parse(valIz.Value.ToString()) < double.Parse(valDer.Value.ToString()));
                            result = new Returned(less, DataType.BOOLEAN);
                        }
                        else
                        {
                            throw new Exception("Los tipos no coinciden para la operacion relacional");
                        }

                    } else
                    {
                        throw new Exception("Los tipos no coinciden para la operacion relacional"); 
                    }

                    
                    
                    break;
                case OpRelational.LESS_EQUALS:
                    if (valIz.getDataType == DataType.INTEGER || valIz.getDataType == DataType.REAL)
                    {
                        if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                        {
                            var less = (bool)(double.Parse(valIz.Value.ToString()) <= double.Parse(valDer.Value.ToString()));
                            result = new Returned(less, DataType.BOOLEAN);
                        }
                        else
                        {
                            throw new Exception("Los tipos no coinciden para la operacion relacional");
                        }

                    }
                    else
                    {
                        throw new Exception("Los tipos no coinciden para la operacion relacional");
                    }
                    break;
                case OpRelational.HIGHER:
                    if (valIz.getDataType == DataType.INTEGER || valIz.getDataType == DataType.REAL)
                    {
                        if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                        {
                            var higher = (bool)(double.Parse(valIz.Value.ToString()) > double.Parse(valDer.Value.ToString()));
                            result = new Returned(higher, DataType.BOOLEAN);
                        }
                        else
                        {
                            throw new Exception("Los tipos no coinciden para la operacion relacional");
                        }
                        
                    }
                    else
                    {
                        throw new Exception("Los tipos no coinciden para la operacion relacional");
                    }
                    break;
                case OpRelational.HIGHER_EQUALS:
                    if (valIz.getDataType == DataType.INTEGER || valIz.getDataType == DataType.REAL)
                    {
                        if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                        {
                            var higher = (bool)(double.Parse(valIz.Value.ToString()) >= double.Parse(valDer.Value.ToString()));
                            result = new Returned(higher, DataType.BOOLEAN);
                        }
                        else
                        {
                            throw new Exception("Los tipos no coinciden para la operacion relacional");
                        }

                    }
                    else
                    {
                        throw new Exception("Los tipos no coinciden para la operacion relacional");
                    }
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
