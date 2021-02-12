using CompiPascal.controller;
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

            if (this.type.Equals("+"))
            {
                /**
                    * SI EL IZQUIERDO ES NUMBER
                    * NUMBER + NUMBER : NUMBER
                    * NUMBER + STRING : STRING
                    * NUMBER + OTRO : ERROR
                    * 
                    */
                if (varIz.getDataType == DataType.INTEGER)
                {
                    if (valDer.getDataType == DataType.INTEGER)
                    {
                        result = new Returned((int.Parse(varIz.Value.ToString()) + int.Parse(valDer.Value.ToString())), DataType.INTEGER);
                    }
                    else if (valDer.getDataType == DataType.STRING)
                    {
                        result = new Returned((varIz.Value.ToString() + valDer.Value.ToString()), DataType.STRING);
                    } else
                    {
                        var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                        //ErrorController.Instance.add(texto);
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
                * REAL + STRING : STRING
                * REAL + OTRO : ERROR
                * 
                */
                else if (varIz.getDataType == DataType.REAL)
                {
                    if (valDer.getDataType == DataType.REAL)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) + double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else if (valDer.getDataType == DataType.STRING)
                    {
                        result = new Returned((varIz.Value.ToString() + valDer.Value.ToString()), DataType.STRING);
                    }
                    else
                    {
                        var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                        //ErrorController.Instance.add(texto);
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
                        var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                        //ErrorController.Instance.add(texto);
                    }
                }
                else if (varIz.getDataType == DataType.TYPE)
                {
                    var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                    //ErrorController.Instance.add(texto);

                }
            }
            if (this.type.Equals("-"))
            {
                /*
                    SI EL IZQUIERDO ES INT
                    INT - INT : INT
                    INT - OTRO : ERROR
                 */

                if (varIz.getDataType == DataType.INTEGER)
                {
                    if (valDer.getDataType == DataType.INTEGER)
                    {
                        result = new Returned((int.Parse(varIz.Value.ToString()) - int.Parse(valDer.Value.ToString())), DataType.INTEGER);
                    }
                    else
                    {
                        var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                        //ErrorController.Instance.add(texto);
                    }

                }
                /*
                   SI EL IZQUIERDO ES REAL
                   REAL - REAL : REAL
                   REAL - OTRO : ERROR
                */

                if (varIz.getDataType == DataType.REAL)
                {
                    if (valDer.getDataType == DataType.REAL)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) - double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else
                    {
                        var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                        //ErrorController.Instance.add(texto);
                    }

                }
                else
                {
                    var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                    //ErrorController.Instance.add(texto);
                }


            }
            if (this.type.Equals("*"))
            {
                /*
                   SI EL IZQUIERDO ES INT
                   INT * INT : INT
                   INT * OTRO : ERROR
                */

                if (varIz.getDataType == DataType.INTEGER)
                {
                    if (valDer.getDataType == DataType.INTEGER)
                    {
                        result = new Returned((int.Parse(varIz.Value.ToString()) * int.Parse(valDer.Value.ToString())), DataType.INTEGER);
                    }
                    else
                    {
                        var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                        //ErrorController.Instance.add(texto);
                    }

                }
                /*
                   SI EL IZQUIERDO ES REAL
                   REAL * REAL : REAL
                   REAL * OTRO : ERROR
                */

                if (varIz.getDataType == DataType.REAL)
                {
                    if (valDer.getDataType == DataType.REAL)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) * double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else
                    {
                        var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                        //ErrorController.Instance.add(texto);
                    }

                }
                else
                {
                    var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                    //ErrorController.Instance.add(texto);
                }

            }
            if (this.type.Equals("/"))
            {
                /*
                   SI EL IZQUIERDO ES INT
                   INT / INT : INT
                   INT / OTRO : ERROR
                */

                if (varIz.getDataType == DataType.INTEGER)
                {
                    if (valDer.getDataType == DataType.INTEGER)
                    {
                        result = new Returned((int.Parse(varIz.Value.ToString()) / int.Parse(valDer.Value.ToString())), DataType.INTEGER);
                    }
                    else
                    {
                        var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                        //ErrorController.Instance.add(texto);
                    }

                }
                /*
                   SI EL IZQUIERDO ES REAL
                   REAL / REAL : REAL
                   REAL / OTRO : ERROR
                */

                if (varIz.getDataType == DataType.REAL)
                {
                    if (valDer.getDataType == DataType.REAL)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) / double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else
                    {
                        var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                        //ErrorController.Instance.add(texto);
                    }

                }
                else
                {
                    var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                    //ErrorController.Instance.add(texto);
                }

            }
            if (this.type.Equals("%"))
            {

                /*
                   SI EL IZQUIERDO ES INT
                   INT % INT : INT
                   INT % OTRO : ERROR
                */

                if (varIz.getDataType == DataType.INTEGER)
                {
                    if (valDer.getDataType == DataType.INTEGER)
                    {
                        result = new Returned((int.Parse(varIz.Value.ToString()) % int.Parse(valDer.Value.ToString())), DataType.INTEGER);
                    }
                    else
                    {
                        var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                        //ErrorController.Instance.add(texto);
                    }

                }
                /*
                   SI EL IZQUIERDO ES REAL
                   REAL % REAL : REAL
                   REAL % OTRO : ERROR
                */

                if (varIz.getDataType == DataType.REAL)
                {
                    if (valDer.getDataType == DataType.REAL)
                    {
                        result = new Returned((double.Parse(varIz.Value.ToString()) % double.Parse(valDer.Value.ToString())), DataType.REAL);
                    }
                    else
                    {
                        var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                        //ErrorController.Instance.add(texto);
                    }

                }
                else
                {
                    var texto = "Operador " + this.type + " NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType;
                    //ErrorController.Instance.add(texto);
                }
            }




            return result;
        }
        public string getOperator(int type)
        {

            if (type == 0)
            {
                return "+";

            }
            else if (type == 1)
            {
                return "-";
            }
            else if (type == 2)
            {
                return "*";
            }
            else if (type == 3)
            {
                return "/";
            }
            else if (type == 4)
            {
                return "^";
            }
            else if (type == 5)
            {
                return "%";
            }
            else if (type == 6)
            {
                return "++";
            }
            else if (type == 7)
            {
                return "--";
            }

            return "";
        }

    }
}
