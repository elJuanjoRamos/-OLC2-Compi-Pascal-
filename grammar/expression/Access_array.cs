using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using CompiPascal.grammar.sentences;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.expression
{
    class Access_array : Expression
    {

        private string id;
        private Expression index;
        private int row;
        private int column;

        public string Id { get => id; set => id = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public Access_array(string id, Expression ind, int r, int c)
            : base(r, c, "Access")
        {
            this.Id = id;
            this.row = r;
            this.column = c;
            this.index = ind;
        }

        public override Returned Execute(Ambit ambit)
        {
            Arrays arr = ambit.getArray(Id);
            if (arr == null)
            {
                set_error("El arreglo '" + this.id + "' no ha sido declarado ", Row, column);
                return new Returned();
            }

            var linf = index.Execute(ambit);

            var larrayinf = arr.Inf.Execute(ambit);
            var larraysup = arr.Sup.Execute(ambit);

            var inferior = int.Parse(larrayinf.Value.ToString());
            var superior = int.Parse(larraysup.Value.ToString());
            var indice_actual = int.Parse(linf.Value.ToString());


            if (indice_actual < inferior || indice_actual > superior)
            {
                set_error("Indice fuera de rango al acceder al arreglo '" + this.id + "'", Row, column);
                return new Returned();
            }

            
            var value = arr.Elementos[indice_actual];

            return new Returned(value, arr.DataType);
        }
        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
