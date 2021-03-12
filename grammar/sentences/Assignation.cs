﻿using CompiPascal.controller;
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

                Identifier variable = ambit.getVariable(id.ToLower());

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
                        ErrorController.Instance.SemantycErrors("No se puede cambiar el valor a una constante",0,0);
                        return null;
                    } else
                    {
                        /**
                        * VALIDAR VALOR: VERIFICA SI EL TIPO DE LA VARIABLE ES IGUAL AL DEL VALOR A ASIGNAR
                        */
                        if (variable.DataType == val.getDataType)
                        {
                            ambit.setVariable(id.ToLower(), val.Value, val.getDataType, false, "Variable");
                            return variable.Value;
                        } else
                        {
                            ErrorController.Instance.SemantycErrors("El tipo " + val.getDataType + " no es asignable con " + variable.DataType, 0, 0);
                            return null;
                        }
                    }

                } 
                else
                {
                    Function function = ambit.getFuncion(id.ToLower());

                    if (function != null)
                    {

                        if (function.IsProcedure)
                        {
                            ErrorController.Instance.SemantycErrors("No puede asignarse ningun valor al procedimiento '" + id+ "' ", 0, 0);
                            return null;
                        }
                        
                        /**
                       * VALIDAR VALOR: VERIFICA SI EL TIPO DE LA VARIABLE ES IGUAL AL DEL VALOR A ASIGNAR
                       */
                        if (function.Tipe == val.getDataType)
                        {
                            function.Retorno = val.Value.ToString();
                            ambit.setFunction(Id.ToLower(), function);
                            return new Returned(function.Retorno, function.Tipe);
                        }
                        else
                        {
                            ErrorController.Instance.SemantycErrors("El tipo " + val.getDataType + " no es asignable con " + variable.DataType, 0, 0);
                            return null;
                        }

                    } else
                    {
                        ErrorController.Instance.SemantycErrors("La variable '" + id + "' no esta declara", 0, 0);
                        return null;

                    }
                }


            }
            catch (Exception)
            {

                ConsolaController.Instance.Add("Error Irrecuperable");
                return null;
            }
            
        }



        public string Id { get => id; set => id = value; }
        public Expression Value { get => value; set => this.value = value; }


    }
}
