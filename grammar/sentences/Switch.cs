using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class Switch : Instruction
    {
        private Expression condicion;
        private ArrayList cases;
        private Case else_case;
        private int row;
        private int column;

        public Switch(Expression condicion, ArrayList cases, Case else_case):
            base(0,0, "Case")
        {
            this.condicion = condicion;
            this.cases = cases;
            this.else_case = else_case;
        }

        public override object Execute(Ambit ambit)
        {
            var ambitName = "Global_Case";
            if (ambit != null)
            {
                ambitName = ambit.Ambit_name + "_Case";
            }

           

            //Condicion de switch
            var conSwitch = condicion.Execute(ambit);


            var numeroCaso = -1;


            
            for (int i = 0; i < cases.Count; i++)
            {
                
                var condCase = ((Case)(cases[i])).getCaseCondition(ambit);

                if (conSwitch.Value.ToString() == condCase.Value.ToString())
                {
                    numeroCaso = i;

                    var switchAmbit = new Ambit(ambit, ambitName, "Case", false);
                    var element = (Case)cases[i];
                    var resultado = element.Execute(switchAmbit);

                    if (resultado == null)
                    {
                        return null;
                    }
                    break;
                }
            }

            if (numeroCaso == -1)
            {
                if (!else_case.IsNull)
                {
                    var element = else_case.Execute(ambit);
                    if (element == null)
                    {
                        return null;
                    }
                    return element;
                }
            }


            return 0;

        }
    }
}
