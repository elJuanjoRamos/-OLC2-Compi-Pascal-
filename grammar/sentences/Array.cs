using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    public class Arrays : Instruction
    {
        private string id;
        private Expression inf;
        private Expression sup;
        private ArrayList elementos;
        private DataType dataType;
        private int row;
        private int column;
        public Arrays(string id, Expression inf, Expression sup, string dt, int row, int col)
            :base("Arreglo")
        {
            this.id = id;
            this.inf = inf;
            this.sup = sup;
            this.elementos = new ArrayList();
            this.dataType = GetDataType(dt);
            this.row = row;
            this.column = col;
        }
        public Arrays(string id, Expression inf, Expression sup, DataType dt, ArrayList elems, int row, int col)
            : base("Array")
        {
            this.id = id;
            this.inf = inf;
            this.sup = sup;
            this.elementos = elems;
            this.dataType = (dt);
            this.row = row;
            this.column = col;
        }
        public Arrays(string id, Expression inf, Expression sup, DataType dt, int row, int col)
        {
            this.id = id;
            this.inf = inf;
            this.sup = sup;
            this.elementos = new ArrayList();
            this.dataType = (dt);
            this.row = row;
            this.column = col;
        }
        public Expression Inf { get => inf; set => inf = value; }
        public Expression Sup { get => sup; set => sup = value; }
        public ArrayList Elementos { get => elementos; set => elementos = value; }
        public DataType DataType { get => dataType; set => dataType = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public override object Execute(Ambit ambit)
        {
            var limite_inf = inf.Execute(ambit);

            var limite_sup = sup.Execute(ambit);

            if (limite_inf.getDataType == DataType.INTEGER && limite_sup.getDataType == DataType.INTEGER)
            {

                var result_sup = int.Parse(limite_sup.Value.ToString());

                var lf = result_sup - int.Parse(limite_inf.Value.ToString());

                if (lf < 0)
                {
                    ErrorController.Instance.SemantycErrors("El limite inferior no puede ser mayor al limite superior en el arreglo '" + id + "'", Row, Column);
                    ConsolaController.Instance.Add("El limite inferior no puede ser mayor al limite superior en el arreglo '" + id + "' - Row:" + Row + " - Col: " + Column + "\n");
                    return null;
                }

                switch (dataType)
                {
                    case DataType.INTEGER:
                        for (int i = 0; i <= result_sup; i++)
                        {
                            elementos.Add(0);
                        }
                        break;
                    case DataType.STRING:
                        for (int i = 0; i <= result_sup; i++)
                        {
                            elementos.Add("");
                        }
                        break;
                    case DataType.BOOLEAN:
                        for (int i = 0; i <= result_sup; i++)
                        {
                            elementos.Add(false);
                        }
                        break;
                    case DataType.REAL:
                        for (int i = 0; i <= result_sup; i++)
                        {
                            var a = double.Parse("0.0");
                            elementos.Add(a);
                        }
                        break;
                    default:
                        break;
                }

                ambit.saveArray(id, this);



            } else
            {
                ErrorController.Instance.SemantycErrors("El tipo de datos en los limites del arreglo '" + id + "' debe ser numerico", Row, Column);
                ConsolaController.Instance.Add("El tipo de datos en los limites del arreglo '" + id + "' debe ser numerico - Row:" + Row + " - Col: " + Column + "\n");
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

    }
}
