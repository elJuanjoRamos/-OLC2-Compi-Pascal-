using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.expression;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class Assignation : Instruction
    {
        private string id;
        private Expression value;
        private DataType type;
        private int row;
        private int column;

        public Assignation(string id, Expression value, int row, int column):
            base(row,column, "Assignation")
        {
            this.id = id;
            this.value = value;
            this.row = row;
            this.column = column;

        }

        public override object Execute(Ambit ambit)
        {
            try
            {
                var val = this.value.Execute(ambit);

                if (val == null || val.getDataType == DataType.ERROR ) 
                {
                    return null;
                }

                Identifier variable = ambit.getVariable(id);

                /**
                * VALIDAR EXISTENCIA
                */
                if (!variable.IsNull)
                {
                    /**
                    * VERIFICA QUE NO SEA CONSTABTE
                    */
                    if (variable.Esconstante)
                    {
                        setError("No se puede cambiar el valor a una constante",row,column);
                        return null;
                    } else
                    {
                        /**
                        * VALIDAR VALOR: VERIFICA SI EL TIPO DE LA VARIABLE ES IGUAL AL DEL VALOR A ASIGNAR
                        */
                        if (variable.DataType == val.getDataType)
                        {
                            ambit.setVariable(id, val.Value, val.getDataType, false, "Variable");
                            return variable.Value;
                        } else
                        {
                            setError("El tipo " + val.getDataType + " no es asignable con " + variable.DataType, row, column);
                            return null;
                        }
                    }

                } 
                else
                {
                    Function function = ambit.getFuncion(id);

                    if (function != null)
                    {

                        if (function.IsProcedure)
                        {
                            setError("No puede asignarse ningun valor al procedimiento '" + id+ "' ", row, column);
                            return null;
                        }
                        
                        /**
                       * VALIDAR VALOR: VERIFICA SI EL TIPO DE LA VARIABLE ES IGUAL AL DEL VALOR A ASIGNAR
                       */
                        if (function.Tipe == val.getDataType)
                        {
                            function.Retorno = val.Value.ToString();
                            ambit.setFunction(Id, function);
                            return new Returned(function.Retorno, function.Tipe);
                        }
                        else
                        {
                            setError("El tipo " + val.getDataType + " no es asignable con " + variable.DataType, row, column);
                            return null;
                        }

                    } else
                    {
                        setError("La variable '" + id + "' no esta declara", row, column);
                        return null;

                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ConsolaController.Instance.Add("Recuperado de error, Row:" + row + " Col: " + column);
                return null;
            }
            
        }

        public void setError(string texto, int row, int col)
        {
            ErrorController.Instance.SemantycErrors(texto, row, col);
            ConsolaController.Instance.Add(texto +" - Row:" + row + " - Col: " + col + "\n");
        }



        public string Id { get => id; set => id = value; }
        public Expression Value { get => value; set => this.value = value; }


    }
}
