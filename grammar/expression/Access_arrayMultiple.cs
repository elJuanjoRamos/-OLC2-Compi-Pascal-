using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using CompiPascal.grammar.sentences;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.expression
{
    class Access_arrayMultiple : Expression
    {
        private string id;
        private Expression index;
        private ArrayList lista_index;
        private int row;
        private int column;

        public string Id { get => id; set => id = value; }
        public Expression Index { get => index; set => index = value; }
        public ArrayList Lista_index { get => lista_index; set => lista_index = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public Access_arrayMultiple(string id, Expression ind, ArrayList lista, int r, int c)
            : base(r, c, "Access")
        {
            this.Id = id;
            this.row = r;
            this.column = c;
            this.index = ind;
            this.lista_index = lista;
        }


        public override Returned Execute(Ambit ambit)
        {
            ArraysMultiple arr = ambit.getArrayMulti(Id);
            if (arr == null)
            {
                set_error("El arreglo '" + this.id + "' no ha sido declarado ", Row, column);
                return new Returned();
            }

            if (arr.Contador < lista_index.Count + 1 || arr.Contador > lista_index.Count + 1)
            {
                set_error("Indice fuera de rango al acceder al arreglo '" + this.id + "'", row, column);
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


            var arreglo = arr.Elementos[indice_actual];



            var retorno = getElement((ArraysMultiple)arreglo, (Expression)lista_index[0], 0, ambit);

            return new Returned(retorno, arr.DataType);
        }


        public object getElement(ArraysMultiple arrays, Expression indice, int index, Ambit ambit)
        {
            var resul = indice.Execute(ambit);

            if (resul.getDataType != DataType.INTEGER)
            {
                set_error("Tipo de dato incorrecto al acceder al arreglo '" + this.id + "'", Row, column);
            }
            var res = int.Parse(resul.Value.ToString());


            var limiteinf = arrays.Inf.Execute(ambit);
            var larraysup = arrays.Sup.Execute(ambit);

            var inferior = int.Parse(limiteinf.Value.ToString());
            var superior = int.Parse(larraysup.Value.ToString());



            if (res < inferior || res > superior)
            {
                set_error("Indice fuera de rango al acceder al arreglo '" + this.id + "'", row, column);
                return false;
            }


            var elemento = arrays.Elementos[res];

            if (elemento is ArraysMultiple)
            {
                var exp = (Expression)lista_index[index + 1];
                return getElement((ArraysMultiple)elemento, exp, index + 1, ambit);
            }

            return arrays.Elementos[res];
        }
        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
