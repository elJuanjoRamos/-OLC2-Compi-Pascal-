using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class Declaration_Array : Instruction
    {
        private string id;
        private string array;
        private int row;
        private int column;

        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public Declaration_Array(string id, String dataType, int r, int c)
            : base("Declaration")
        {
            this.id = id;
            this.array = dataType;
            this.row = r;
            this.column = c;
        }
        public override object Execute(Ambit ambit)
        {
            Arrays ar = ambit.getArray(array);

            if (ar != null)
            {
                var rest = ambit.getArray(id);
                if (rest == null)
                {
                    ambit.saveArray(id, new Arrays(id, ar.Inf, ar.Sup, ar.DataType, ar.Elementos, Row, Column));
                } else
                {
                    set_error("El arreglo '" + id + "' ya fue declarado", row, column);
                }

            } 
            else
            {
                ArraysMultiple arraysMultiple = ambit.getArrayMulti(array);

                if (arraysMultiple != null)
                {
                    var rest = ambit.getArrayMulti(id);
                    if (rest == null)
                    {
                        ambit.saveArrayMultiple(id, new ArraysMultiple(id, arraysMultiple.Inf, arraysMultiple.Sup, arraysMultiple.DataType, arraysMultiple.Auxiliar, arraysMultiple.Arreglos, arraysMultiple.Elementos, Row, Column, arraysMultiple.Contador));
                    }
                    else
                    {
                        set_error("El arreglo '" + id + "' ya fue declarado", row, column);
                    }

                } else
                {
                    set_error("El arreglo '" + array + "' no ha sido declarado", row, column);
                    return null;

                }
            }

            return 0;
        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
