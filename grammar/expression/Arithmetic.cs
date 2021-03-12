using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using CompiPascal.Model;
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
        private int row;
        private int column;

        public Arithmetic(Expression l, Expression ri, String t, int r, int c)
        : base(r, c, "Arithmetic")
        {
            this.right = ri;
            this.left = l;
            this.type = t;
            this.row = r;
            this.column = c;
        }

        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public override Returned Execute(Ambit ambit)
        {
            var result = new Returned();
            var varIz = this.left.Execute(ambit);
            var valDer = this.right.Execute(ambit);


            //VERIFICA QUE NO HAYA ERROR
            TablaTipo tablaTipo = new TablaTipo();

            var res = tablaTipo.getTipoArith(varIz.getDataType, valDer.getDataType);

            if (res == DataType.ERROR)
            {
                ErrorController.Instance.SemantycErrors("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, Row, column);
                return new Returned();
            }



            if (this.type.Equals("+"))
            {
                /**
                    * SI EL IZQUIERDO ES INT
                    * INT + INT: INT
                    * INT + REAL: REAL
                    * INT + STRING : STRING
                    * INT + OTRO : ERROR
                    * 
                    */
                if (varIz.getDataType == DataType.INTEGER)
                {
                    if (valDer.getDataType == DataType.INTEGER)
                    {
                        result = new Returned((int.Parse(varIz.Value.ToString()) + int.Parse(valDer.Value.ToString())), DataType.INTEGER);
                    }
                    else if (valDer.getDataType == DataType.REAL)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) + double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else if (valDer.getDataType == DataType.STRING)
                    {
                        result = new Returned((varIz.Value.ToString() + valDer.Value.ToString()), DataType.STRING);
                    } else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, Row, column);
                        return new Returned();
                    }

                }
                /**
                    * SI EL IZQUIERDO ES STRING
                * STRING + OTHER : STRING
                * 
                */
                else if (varIz.getDataType == DataType.STRING)
                {
                    result = new Returned((varIz.Value.ToString() + valDer.Value.ToString()), DataType.STRING);
                }
                /**
                * SI EL IZQUIERDO ES REAL
                * REAL + REAL : REAL
                * REAL + INT: REAL
                * REAL + STRING : STRING
                * REAL + OTRO : ERROR
                * 
                */
                else if (varIz.getDataType == DataType.REAL)
                {
                    if (valDer.getDataType == DataType.REAL || valDer.getDataType == DataType.INTEGER)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) + double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else if (valDer.getDataType == DataType.STRING)
                    {
                        result = new Returned((varIz.Value.ToString() + valDer.Value.ToString()), DataType.STRING);
                    }
                    else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, Row, column);
                        return new Returned();
                    }
                    
                }
                /**
                * SI EL IZQUIERDO ES BOOLEAN
                * BOOLEAN + STRING : STRING
                * BOOLEAN + OTRO : ERROR
                * 
                */
                else if (varIz.getDataType == DataType.BOOLEAN)
                {
                    if (valDer.getDataType == DataType.STRING)
                    {
                        result = new Returned((varIz.Value.ToString() + valDer.Value.ToString()), DataType.STRING);
                    } else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, Row, Column);
                        return new Returned();
                    }
                }
                else 
                {
                    ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, Row, Column);
                    return new Returned();

                }
            }
            else if (this.type.Equals("-"))
            {
                /*
                    SI EL IZQUIERDO ES INT
                    INT - INT : INT
                    INT - REAL: REAL
                    INT - OTRO : ERROR
                 */
                
                if (varIz.getDataType == DataType.INTEGER)
                {
                    if (valDer.getDataType == DataType.INTEGER)
                    {
                        result = new Returned((int.Parse(varIz.Value.ToString()) - int.Parse(valDer.Value.ToString())), DataType.INTEGER);
                    }
                    else if (valDer.getDataType == DataType.REAL)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) - double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, Row, column);
                        return new Returned();
                    }

                }
                /*
                   SI EL IZQUIERDO ES REAL
                   REAL - REAL : REAL
                   REAL - INT : REAL
                   REAL - OTRO : ERROR
                */

                else if (varIz.getDataType == DataType.REAL)
                {
                    if (valDer.getDataType == DataType.REAL || valDer.getDataType == DataType.INTEGER)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) - double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, Row, column);
                        return new Returned();
                    }

                }
                else
                {
                    ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
                    return new Returned();
                }


            }
            else if (this.type.Equals("*"))
            {
                /*
                   SI EL IZQUIERDO ES INT
                   INT * INT  : INT
                   INT * REAL : REAL
                   INT * OTRO : ERROR
                */

                if (varIz.getDataType == DataType.INTEGER)
                {
                    if (valDer.getDataType == DataType.INTEGER)
                    {
                        result = new Returned((int.Parse(varIz.Value.ToString()) * int.Parse(valDer.Value.ToString())), DataType.INTEGER);
                    }
                    else if (valDer.getDataType == DataType.REAL)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) * double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
                        return new Returned();
                    }

                }
                /*
                   SI EL IZQUIERDO ES REAL
                   REAL * REAL : REAL
                   REAL * INT : REAL

                   REAL * OTRO : ERROR
                */

                else if (varIz.getDataType == DataType.REAL)
                {
                    if (valDer.getDataType == DataType.REAL || valDer.getDataType == DataType.INTEGER)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) * double.Parse(valDer.Value.ToString())), DataType.REAL);
                    } 
                    else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
                        return new Returned();
                    }

                }
                else
                {
                    ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
                    return new Returned();
                }

            }
            else if (this.type.Equals("/"))
            {
                /*
                   SI EL IZQUIERDO ES INT
                   INT / INT : REAL
                   INT / REAL : REAL 
                   INT / OTRO : ERROR
                */

                if (varIz.getDataType == DataType.INTEGER)
                {
                    if (valDer.getDataType == DataType.INTEGER)
                    { 
                        result = new Returned((double.Parse(varIz.Value.ToString()) / double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else if (valDer.getDataType == DataType.REAL)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) / double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
                        return new Returned();
                    }

                }
                /*
                   SI EL IZQUIERDO ES REAL
                   REAL / REAL : REAL
                   REAL / INT : REAL
                   REAL / OTRO : ERROR
                */

                else if (varIz.getDataType == DataType.REAL)
                {
                    if (valDer.getDataType == DataType.REAL || valDer.getDataType == DataType.INTEGER)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) / double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
                        return new Returned();
                    }

                }
                else
                {
                    ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
                }

            }
            else if (this.type.Equals("%"))
            {

                /*
                   SI EL IZQUIERDO ES INT
                   INT % INT : REAL
                   INT % REAL: REAL 
                   INT % OTRO : ERROR
                */

                if (varIz.getDataType == DataType.INTEGER)
                {
                    if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) % double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
                        return new Returned();
                    }

                }
                /*
                   SI EL IZQUIERDO ES REAL
                   REAL % REAL : REAL
                   REAL % INT : REAL
                   REAL % OTRO : ERROR
                */

                else if (varIz.getDataType == DataType.REAL)
                {
                    if (valDer.getDataType == DataType.REAL || valDer.getDataType == DataType.INTEGER)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) % double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else
                    {
                        ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
                        return new Returned();
                    }

                }
                else
                {
                    ErrorController.Instance.SemantycErrors("Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
                    return new Returned();
                }
            }




            return result;
        }
        

        public DataType GetDataType(DataType izq, DataType der)
        {

            
            if (izq == DataType.STRING)
            {
                if (der == DataType.ARRAY || der == DataType.TYPE || der == DataType.ERROR)
                {
                    return DataType.ERROR;
                } else
                {

                }  
            }


            return DataType.ERROR;

            
        }
    }
}
