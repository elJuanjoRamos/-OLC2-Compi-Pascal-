using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.expression;
using CompiPascal.grammar.identifier;

namespace CompiPascal.grammar.sentences
{
    public class Declaration : Instruction
    {
        private string id;
        private DataType type;
        private Expression value;
        public int row;
        public int column;
        public bool isConst;
        public bool isAssigned;
        public bool perteneceFuncion;
        private bool referencia;
        //CONSTRUCTOR PARA VARIABLES
        public Declaration(string id, String dataType, Expression ex, int r, int c, bool isAs, bool refe)
            : base("Declaration")
        {
            this.id = id;
            this.type = GetDataType(dataType);
            this.value = ex;
            this.row = r;
            this.column = c;
            this.isConst = false;
            this.isAssigned = isAs;
            this.referencia = refe;
        }
        //CONSTRUCTOR PARA CONSTANTES
        public Declaration(string i, Expression e, int r, int c, bool isc)
            : base("Declaration")
        {
            this.id = i;
            this.type = DataType.CONST;
            this.value = e;
            this.row = r;
            this.column = c;
            this.isConst = isc;
            this.isAssigned = true;
        }

        public override object Execute(Ambit ambit)
        {
            Identifier buscar = new Identifier();
            //BUSCA LA VARIABLE SI NO HA SIDO DECLARADA
            if (!ambit.Ambit_name_inmediato.Equals("Function") && !ambit.Ambit_name_inmediato.Equals("Procedure"))
            {
                buscar = ambit.getVariable(id);
            } 
            //SIGFINICA QUE ES UNA DECLARACION EN FUNCION
            else
            {
                buscar = ambit.getVariableFunctionInAmbit(id);
            }

            if (buscar.IsNull)
            {
                try
                {
                    Returned val = this.value.Execute(ambit);

                    //VERIFICA QUE NO HAYA ERROR
                    if (val.getDataType == DataType.ERROR)
                    {
                        return null;
                    }


                    if (this.type == DataType.CONST)
                    {
                        ambit.save(this.id, val.Value, val.getDataType, true, true, "Constante");
                        return val.Value;

                    }
                    else
                    {
                        if (val.getDataType == this.type)
                        {
                            ambit.save(this.id, val.Value, val.getDataType, false, isAssigned, "Variable");
                            return val.Value;
                        }
                        else
                        {
                            set_error("El tipo " + val.getDataType + " no es asignable con " + this.type.ToString(), row, column);
                            return null;
                        }
                    }

                }
                catch (Exception)
                {

                }

            }
            else
            {
                set_error("La variable '" + id + "' ya fue declarada", row, column);
                return null;
            }
            return 0;
        }


        public DataType GetDataType(string d)
        {
            if (d.Equals("integer"))
            {
                return DataType.INTEGER;
            }
            else if (d.Equals("boolean"))
            {
                return DataType.BOOLEAN;
            }
            else if (d.Equals("real"))
            {
                return DataType.REAL;
            }
            return DataType.STRING;

        }



        public string Id { get => id; set => id = value; }
        public DataType getDataType
        {
            get { return type; }
            set { type = value; }
        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
        public bool Referencia { get => referencia; set => referencia = value; }
    }
}
