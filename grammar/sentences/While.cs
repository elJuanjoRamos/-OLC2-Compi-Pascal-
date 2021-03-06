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

            var whileAmbit = new Ambit(ambit, ambitName, "While", false);

            //CONDICION
            var cond = condition.Execute(whileAmbit);

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
                    var element = sentences.Execute(whileAmbit);

                    //VERIFICA QUE NO HAYA ERROR
                    if (element == null)
                    {
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
                    }


                    cond = condition.Execute(whileAmbit);
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
