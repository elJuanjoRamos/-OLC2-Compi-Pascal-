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

        public Repeat(Expression condition, Sentence sentences)
            : base(0,0, "Repeat")
        {
            this.condition = condition;
            this.sentences = sentences;
        }

        public override object Execute(Ambit ambit)
        {

            var ambitName = "Global_Repeat";
            if (!ambit.IsNull)
            {
                ambitName = ambit.Ambit_name + "_Repeat";
            }
            var repeatAmbit = new Ambit(ambit, ambitName, "Repeat", false);


            //CONDICION
            var condicion = condition.Execute(repeatAmbit);

            //VERIFICA QUE SEA BOOL
            if (condicion.getDataType != DataType.BOOLEAN)
            {
                ConsolaController.Instance.Add("Semantico - La condicion del If no es booleana");
                return null;
            }




            if (!sentences.IsNull)
            {
                do
                {

                    var element = sentences.Execute(repeatAmbit);
                    if (element == null)
                    {
                        break;
                    }
                    else
                    {

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
                        }
                        
                    }
                    condicion = this.condition.Execute(repeatAmbit);
                    if (condicion.getDataType != DataType.BOOLEAN)
                    {
                        ConsolaController.Instance.Add("La condicion no es booleana");
                        return null;
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
