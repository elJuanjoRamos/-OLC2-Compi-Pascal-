using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class Assignation_arrayMultiple : Instruction
    {
        private string id;
        private Expression value;
        private DataType type;
        private int row;
        private int column;
        private Expression index;
        private ArrayList lista_indices;

        public string Id { get => id; set => id = value; }
        public Expression Value { get => value; set => this.value = value; }
        public DataType Type { get => type; set => type = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
        public Expression Index { get => index; set => index = value; }
        public ArrayList Lista_indices { get => lista_indices; set => lista_indices = value; }

        public Assignation_arrayMultiple(string id, Expression value, int row, int column, Expression index, ArrayList lista_indices)
        {
            this.id = id;
            this.value = value;
            this.row = row;
            this.column = column;
            this.index = index;
            this.lista_indices = lista_indices;
        }

        public override object Execute(Ambit ambit)
        {
            try
            {
                var val = this.value.Execute(ambit);
                
                if (val == null || val.getDataType == DataType.ERROR)
                {
                    return null;
                }
                //VERIFICO LA EXISTENCIA
                ArraysMultiple arr = ambit.getArrayMulti(id);

                if (arr != null)
                {
                    if (arr.Contador < lista_indices.Count +1 || arr.Contador > lista_indices.Count + 1)
                    {
                        setError("Cantidad de indicess fuera de rango al acceder al arreglo '" + this.id + "'", row, column);
                        return null;
                    }

                    if (arr.DataType == val.getDataType)
                    {
                        var ind = index.Execute(ambit);
                        var limiteinf = arr.Inf.Execute(ambit);
                        var larraysup = arr.Sup.Execute(ambit);

                        var inferior = int.Parse(limiteinf.Value.ToString());
                        var superior = int.Parse(larraysup.Value.ToString());
                        var indice = int.Parse(ind.Value.ToString());


                        if (indice < inferior || indice > superior)
                        {
                            setError("Indice fuera de rango al acceder al arreglo '" + this.id + "'", row, column);
                            return null;
                        }

                        var arreglo = arr.Elementos[indice];


                        var retorno = getElement((ArraysMultiple)arreglo, (Expression)lista_indices[0], 0, ambit, val);


                        if (retorno)
                        {
                            ambit.setArrayMulti(id, arr);
                            return val.Value;
                        }

                        return null;
                        

                    }
                    else
                    {
                        setError("El tipo " + val.getDataType + " no es asignable con " + arr.DataType, row, column);
                        return null;
                    }




                }
                else
                {
                    setError("El arreglo '" + id + "' no esta declaro", row, column);
                    return null;
                }
                }
            catch (Exception)
            {

                return null;
            }

            return null;
        }
        public bool getElement(ArraysMultiple arrays, Expression indice, int index, Ambit ambit, Returned valor)
        {
            var resul = indice.Execute(ambit);

            if (resul.getDataType != DataType.INTEGER)
            {
                setError("Tipo de dato incorrecto al acceder al arreglo '" + this.id + "'", Row, column);
            }
            var res = int.Parse(resul.Value.ToString());


            var limiteinf = arrays.Inf.Execute(ambit);
            var larraysup = arrays.Sup.Execute(ambit);

            var inferior = int.Parse(limiteinf.Value.ToString());
            var superior = int.Parse(larraysup.Value.ToString());



            if (res < inferior || res > superior)
            {
                setError("Indice fuera de rango al acceder al arreglo '" + this.id + "'", row, column);
                return false;
            }


            var elemento = arrays.Elementos[res];

            if (elemento is ArraysMultiple)
            {
                var exp = (Expression)lista_indices[index+1];
                return getElement((ArraysMultiple)elemento, exp, index + 1, ambit, valor);
            }


            try
            {
                switch (arrays.DataType)
                {
                    case DataType.INTEGER:
                        arrays.Elementos[res] = int.Parse(valor.Value.ToString());
                        return true;
                    case DataType.STRING:
                        arrays.Elementos[res] = valor.Value.ToString();
                        return true;
                    case DataType.BOOLEAN:
                        arrays.Elementos[res] = (bool)valor.Value;
                        return true;
                    case DataType.REAL:
                        arrays.Elementos[res] = double.Parse(valor.Value.ToString());
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception e)
            {

                ConsolaController.Instance.Add(e.Message + "\n");
            }




            return false;
        }
        public void setError(string texto, int row, int col)
        {
            ErrorController.Instance.SemantycErrors(texto, row, col);
            ConsolaController.Instance.Add(texto + " - Row:" + row + " - Col: " + col + "\n");
        }
    }
}
