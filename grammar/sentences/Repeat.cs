using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class Repeat : Instruction
    {
        private Expression condition;
        private Sentence sentences;
        private int row;
        private int column;

        public Repeat(Expression condition, Sentence sentences, int ro, int col)
            : base(ro,col, "Repeat")
        {
            this.condition = condition;
            this.sentences = sentences;
            this.row = ro;
            this.column = col;
        }

        public override object Execute(Ambit ambit)
        {

            var repeatAmbit = new Ambit(ambit, "Repeat", "Repeat", false);


            //CONDICION
            var condicion = condition.Execute(repeatAmbit);

            //VERIFICA QUE SEA BOOL
            if (condicion.getDataType != DataType.BOOLEAN)
            {
                ErrorController.Instance.SemantycErrors("La condicion del repeat no es booleana", row, column);
                return null;
            }




            if (!sentences.IsNull)
            {
                do
                {

                    var element = sentences.Execute(repeatAmbit);

                    ////VERIFICA QUE NO HAYA ERROR
                    if (element == null)
                    {
                        return null;
                    }

                    //VUELVE A EVALUAR LA CONDICION
                    condicion = this.condition.Execute(repeatAmbit);
                    if (condicion.getDataType != DataType.BOOLEAN)
                    {
                        ErrorController.Instance.SemantycErrors("La condicion del repeat no es booleana", row, column);
                        return null;
                    }

                    //EVALUA LO QUE VIENE
                    if (element is Instruction)
                    {
                        Instruction inst = (Instruction)element;
                        if (inst.Name.Equals("Break"))
                        {
                            break;
                        }
                        else if (inst.Name.Equals("Continue"))
                        {
                            continue;
                        }
                        else if (inst.Name.Equals("Exit"))
                        {
                            return element;
                        }
                    }
                    

                } while ((bool)condicion.Value == false);

            } else
            {
                return 0;
            }

            return 0;
        }
    }
}
