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

        public Assignation(string id, Expression value):
            base(0,0)
        {
            this.id = id;
            this.value = value;
        }

        public override object Execute(Ambit ambit)
        {
            try
            {
                var val = this.value.Execute(ambit);

                if (val.getDataType == DataType.ERROR)
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
                        ErrorController.Instance.SyntacticError("No se puede asignar a una constante", 0,0);
                        return null;
                    } else
                    {
                        /**
                        * VALIDAR VALOR: VERIFICA SI EL TIPO DE LA VARIABLE ES IGUAL AL DEL VALOR A ASIGNAR
                        */
                        if (variable.DataType == val.getDataType)
                        {
                            ambit.setVariable(id, val.Value, val.getDataType);
                        } else
                        {
                            ConsolaController.Instance.Add("El tipo " + val.getDataType + " no es asignable con " + variable.DataType);
                            ErrorController.Instance.SyntacticError("El tipo " + val.getDataType + " no es asignable con " + variable.DataType, 0, 0);
                            return null;
                        }

                        /*switch (val.getDataType)
                        {
                            case DataType.INTEGER:

                                if (variable.DataType == DataType.INTEGER)
                                {
                                    ambit.setVariable(id, val.Value, val.getDataType);
                                } else
                                {
                                    ErrorController.Instance.SyntacticError("El tipo " + val.getDataType + " no es asignable con " + variable.DataType, 0, 0);
                                    return new Error("El tipo " + val.getDataType + " no es asignable con " + variable.DataType, 0, 0);
                                } 

                                
                                break;
                            case DataType.STRING:
                                if (variable.DataType == DataType.STRING)
                                {
                                    ambit.setVariable(id, val.Value, val.getDataType);
                                }
                                else
                                {
                                    
                                }



                                break;
                            case DataType.BOOLEAN:
                                break;
                            case DataType.REAL:
                                break;
                            case DataType.TYPE:
                                break;
                            case DataType.ARRAY:
                                break;
                             default:
                                break;
                        }*/
                    }

                } else
                {
                    ConsolaController.Instance.Add("La variable '" + id + "' no esta declara");
                    ErrorController.Instance.SyntacticError("La variable '" + id + "' no esta declara", 0, 0);
                    return null;

                }


            }
            catch (Exception)
            {

                return new Error("Error Irrecuperable", 0, 0);
            }
            return 0;
        } 
    }
}
