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

        public While(Expression condition, Sentence sentences)
            : base(0,0, "While")
        {
            this.condition = condition;
            this.sentences = sentences;
        }

        public override object Execute(Ambit ambit)
        {
            var ambitName = "Global_While";
            if (!ambit.IsNull)
            {
                ambitName = ambit.Ambit_name + "_While";
            }


            var cond = condition.Execute(ambit);

            if (cond.getDataType != DataType.BOOLEAN)
            {
                ConsolaController.Instance.Add("La condicion del While no es booleana");
                return null;
            }


            while ((bool)cond.Value == true)
            {
                //VERIFICA QUE LA SENTENCIA NO ESTE VACIA
                if (!sentences.IsNull)
                {
                    //EJECUTA LA SENTENCIA
                    var element = sentences.Execute(ambit);

                    //VERIFICA QUE NO HAYA ERROR
                    if (element == null)
                    {
                        return null;
                    }

                    cond = condition.Execute(ambit);
                    if (cond.getDataType != DataType.BOOLEAN)
                    {

                        ConsolaController.Instance.Add("La condicion del While no es booleana");
                        return null;
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
