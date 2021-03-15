using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    public class ArraysMultiple : Instruction
    {
        private string id;
        private Expression inf;
        private Expression sup;
        private ArraysMultiple arreglos;
        private ArrayList elementos;
        private int row, column;
        private DataType dataType;
        private DataType auxiliar;
        private int contador;

        public ArraysMultiple(string id, Expression inf, Expression sup, string dt, string dt2, ArraysMultiple elems, int row, int col, int cont):
            base("Array")
        {
            this.id = id;
            this.inf = inf;
            this.sup = sup;
            this.row = row;
            this.column = col;
            this.arreglos = elems;
            this.elementos = new ArrayList();
            this.dataType = (GetDataType(dt));
            this.auxiliar = (GetDataType(dt2));
            this.contador = cont;
        }
        public ArraysMultiple(string id, Expression inf, Expression sup, DataType dt, DataType dt2, ArraysMultiple elems, int row, int col, int cont):
            base("Array")
        {
            this.id = id;
            this.inf = inf;
            this.sup = sup;
            this.elementos = new ArrayList();
            this.arreglos = elems;
            this.dataType = (dt);
            this.row = row;
            this.column = col;
            this.auxiliar = dt;
            this.auxiliar = dt2;
            this.contador = cont;
        }
        public ArraysMultiple(string id, Expression inf, Expression sup, DataType dt, DataType dt2, ArraysMultiple elems, ArrayList datos, int row, int col, int cont) :
            base("Array")
        {
            this.id = id;
            this.inf = inf;
            this.sup = sup;
            this.elementos = datos;
            this.arreglos = elems;
            this.dataType = (dt);
            this.auxiliar = dt2;
            this.row = row;
            this.column = col;
            this.auxiliar = dt;
            this.contador = cont;
        }

        public DataType DataType { get => dataType; set => dataType = value; }
        public Expression Sup { get => sup; set => sup = value; }
        public Expression Inf { get => inf; set => inf = value; }
        public ArrayList Elementos { get => elementos; set => elementos = value; }
        internal ArraysMultiple Arreglos { get => arreglos; set => arreglos = value; }
        public int Column { get => column; set => column = value; }
        public int Row { get => row; set => row = value; }
        public DataType Auxiliar { get => auxiliar; set => auxiliar = value; }
        public int Contador { get => contador; set => contador = value; }

        public override object Execute(Ambit ambit)
        {
            var elem = insertData(ambit, this);

            if (elem == null)
            {
                return null;
            }

            ambit.saveArrayMultiple(id, elem);
            return 0;
        }

        public ArraysMultiple insertData(Ambit ambit, ArraysMultiple arraysMultiple)
        {

            var limite_inf = arraysMultiple.Inf.Execute(ambit);

            var limite_sup = arraysMultiple.Sup.Execute(ambit);

            if (limite_inf.getDataType == DataType.INTEGER && limite_sup.getDataType == DataType.INTEGER)
            {
                var res_sup = int.Parse(limite_sup.Value.ToString());

                var lf = res_sup - int.Parse(limite_inf.Value.ToString());

                if (lf < 0)
                {
                    ErrorController.Instance.SemantycErrors("El limite inferior no puede ser mayor al limite superior en el arreglo '" + id + "'", Row, Column);
                    ConsolaController.Instance.Add("El limite inferior no puede ser mayor al limite superior en el arreglo '" + id + "' - Row:" + Row + " - Col: " + Column + "\n");
                    return null;
                }

                if (arraysMultiple.Arreglos != null)
                {
                    var arreglos = insertData(ambit, arraysMultiple.Arreglos);

                    if (arreglos == null)
                    {
                        return null;
                    }

                    for (int i = 0; i <= res_sup; i++)
                    {
                        arraysMultiple.Elementos.Add(arreglos);
                    }

                }
                else
                {
                    switch (arraysMultiple.dataType)
                    {

                        case DataType.INTEGER:
                            for (int i = 0; i <= res_sup; i++)
                            {
                                arraysMultiple.Elementos.Add(0);
                            }
                            break;
                        case DataType.STRING:
                            for (int i = 0; i <= res_sup; i++)
                            {
                                arraysMultiple.Elementos.Add("");
                            }
                            break;
                        case DataType.BOOLEAN:
                            for (int i = 0; i <= res_sup; i++)
                            {
                                arraysMultiple.Elementos.Add(false);
                            }
                            break;
                        case DataType.REAL:
                            for (int i = 0; i <= res_sup; i++)
                            {
                                var a = double.Parse("0.0");
                                arraysMultiple.Elementos.Add(a);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                ErrorController.Instance.SemantycErrors("El tipo de datos en los limites del arreglo '" + id + "' debe ser numerico", Row, Column);
                ConsolaController.Instance.Add("El tipo de datos en los limites del arreglo '" + id + "' debe ser numerico - Row:" + Row + " - Col: " + Column + "\n");
                return null;
            }

            

            return arraysMultiple;

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
            else if (d.Equals("array"))
            {
                return DataType.ARRAY;
            }
            return DataType.STRING;

        }
    }
}
