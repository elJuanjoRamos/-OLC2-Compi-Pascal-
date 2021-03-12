using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.expression;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class While : Instruction
    {
        private Expression condition;
        private Sentence sentences;
        private int row;
        private int column;
        public While(Expression condition, Sentence sentences, int row, int col)
            : base(row,col, "While")
        {
            this.condition = condition;
            this.sentences = sentences;
            this.row = row;
            this.column = col;
        }

        public override object Execute(Ambit ambit)
        {
            

            var whileAmbit = new Ambit(ambit, "While", "While", false);

            //CONDICION
            var cond = condition.Execute(whileAmbit);

            if (cond.getDataType != DataType.BOOLEAN)
            {
                ErrorController.Instance.SemantycErrors("La condicion del While no es booleana",row, column);
                return null;
            }


            while ((bool)cond.Value == true)
            {
                //VERIFICA QUE LA SENTENCIA NO ESTE VACIA
                if (!sentences.IsNull)
                {
                    //EJECUTA LA SENTENCIA
                    var element = sentences.Execute(whileAmbit);

                    //VERIFICA QUE NO HAYA ERROR
                    if (element == null)
                    {
                        return null;
                    }
                    //VUELVE A EVALUAR LA CONDICION
                    cond = condition.Execute(whileAmbit);
                    if (cond.getDataType != DataType.BOOLEAN)
                    {

                        ErrorController.Instance.SemantycErrors("La condicion del While no es booleana", row, column);
                        return null;
                    }

                    if (element is Instruction)
                    {
                        Instruction ins = (Instruction)element;

                        //console.log(element);
                        if (ins.Name.Equals("Break"))
                        {
                            break;
                        }
                        else if (ins.Name.Equals("Continue"))
                        {
                            continue;
                        }
                        else if (ins.Name.Equals("Exit"))
                        {
                            return element;
                        }
                    }


                    


                }
                else
                {
                    break;
                }
            }

            return 0;
        }
    }
}
