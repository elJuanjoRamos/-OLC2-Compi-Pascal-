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
        public Switch(Expression condicion, ArrayList cases, Case else_case, int ro, int col):
            base(ro,col, "Case")
        {
            this.condicion = condicion;
            this.cases = cases;
            this.else_case = else_case;
            this.row = ro;
            this.column = col;
        }

        public override object Execute(Ambit ambit)
        {
           

           

            //Condicion de switch
            var conSwitch = condicion.Execute(ambit);


            var numeroCaso = -1;


            var switchAmbit = new Ambit(ambit, ambit.Ambit_name+ "_Case", "Case", false);

            for (int i = 0; i < cases.Count; i++)
            {
                
                var condCase = ((Case)(cases[i])).getCaseCondition(ambit);

                if (conSwitch.Value.ToString() == condCase.Value.ToString())
                {
                    numeroCaso = i;

                    
                    var element = (Case)cases[i];
                    var resultado = element.Execute(switchAmbit);

                    if (resultado == null)
                    {
                        return null;
                    }
                    if (resultado is Instruction)
                    {
                        var inst = (Instruction)resultado;
                        if (inst.Name.Equals("Break") || inst.Name.Equals("Exit"))
                        {
                            return resultado;
                        } 
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
