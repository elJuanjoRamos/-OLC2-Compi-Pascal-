using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class Assignation_array : Instruction
    {
        private string id;
        private Expression value;
        private DataType type;
        private int row;
        private int column;
        private Expression index;


        public Assignation_array(string id, Expression value, int row, int column, Expression ex) :
            base(row, column, "Assignation")
        {
            this.id = id;
            this.value = value;
            this.row = row;
            this.column = column;
            this.index = ex;
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
                Arrays arr = ambit.getArray(id.ToLower());

                if (arr != null)
                {
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

                          

                        try
                        {
                            switch (arr.DataType)
                            {
                                case DataType.INTEGER:
                                    arr.Elementos[indice] = int.Parse(val.Value.ToString());
                                    break;
                                case DataType.STRING:
                                    arr.Elementos[indice] = val.Value.ToString();
                                    break;
                                case DataType.BOOLEAN:
                                    arr.Elementos[indice] = (bool)val.Value;
                                    break;
                                case DataType.REAL:
                                    arr.Elementos[indice] = double.Parse(val.Value.ToString());
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e);
                        }
                        

                        ambit.setArray(id.ToLower(), arr);
                        return val.Value;
                        
                    } else
                    {
                        setError("El tipo " + val.getDataType + " no es asignable con " + arr.DataType, row, column);
                        return null;
                    }
                   



                } else
                {
                    setError("El arreglo '" + id + "' no esta declaro", row, column);
                    return null;
                }

            }
            catch (Exception)
            {

                return null;
            }
        }
        public void setError(string texto, int row, int col)
        {
            ErrorController.Instance.SemantycErrors(texto, row, col);
            ConsolaController.Instance.Add(texto + " - Row:" + row + " - Col: " + col + "\n");
        }

    }
}
