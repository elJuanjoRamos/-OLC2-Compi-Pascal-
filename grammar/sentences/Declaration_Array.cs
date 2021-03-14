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
        public int row;
        public int column;

        public Declaration_Array(string id, String dataType, int r, int c)
            : base(r, c, "Declaration")
        {
            this.id = id;
            this.array = dataType;
        }
        public override object Execute(Ambit ambit)
        {
            Arrays ar = ambit.getArray(array.ToLower());

            if (ar != null)
            {
                var rest = ambit.getArray(id);
                if (rest == null)
                {
                    ambit.saveArray(id.ToLower(), new Arrays(id.ToLower(), ar.Inf, ar.Sup, ar.DataType, ar.Elementos));
                } else
                {
                    set_error("El arreglo '" + id + "' ya fue declarado", row, column);
                }

            } else
            {
                set_error("El arreglo '" + array + "' ya no ha sido declarado", row, column);
                return null;
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
