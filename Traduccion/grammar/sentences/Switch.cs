using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    class Switch_Trad : Instruction_Trad
    {
        private Expresion_Trad condicion;
        private ArrayList cases;
        private Case_Trad else_case;
        private int cant_tabs;
        public Switch_Trad(Expresion_Trad condicion, ArrayList cases, Case_Trad else_case, int ct) :
            base(0, 0, "Case")
        {
            this.condicion = condicion;
            this.cases = cases;
            this.else_case = else_case;
            this.cant_tabs = ct;
        }

        public override string Execute(Ambit_Trad ambit)
        {
            var ambitName = "Global_Case";
            if (ambit != null)
            {
                ambitName = ambit.Ambit_name + "_Case";
            }

            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + " ";
            }

            var tabs1 = "";
            for (int i = 0; i < cant_tabs+1; i++)
            {
                tabs1 = tabs1 + " ";
            }
            //Condicion de switch
            var conSwitch = condicion.Execute(ambit);

            var casos = "";
            for (int i = 0; i < cases.Count; i++)
            {
                
                var condCase = ((Case_Trad)(cases[i])).getCaseCondition(ambit);

                
                var switchAmbit = new Ambit_Trad(ambit, ambitName, "Case", false);

                var element = (Case_Trad)cases[i];
                var resultado = element.Execute(switchAmbit);


                casos = casos +  
                    tabs1 + condCase+":\n"+
                    resultado;
            }

            if (!else_case.IsNull)
            {
                var element = else_case.Execute(ambit);

                casos = casos +
                    tabs1 +"else\n" + element;
            }

            return
                tabs + "case (" + conSwitch + ")\n" +
                casos +
                tabs + "\nend;";

        }
    }
}
