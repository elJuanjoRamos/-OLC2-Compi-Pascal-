using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    class If_Trad : Instruction_Trad
    {
        private Expresion_Trad condition;
        private Instruction_Trad sentences;
        private Instruction_Trad elif;
        private bool isNull;
        private int cant_tabs;

        public If_Trad(Expresion_Trad condition, Instruction_Trad sentences, Instruction_Trad elif, int ct)
            : base(0, 0, "IF")
        {
            this.condition = condition;
            this.sentences = sentences;
            this.elif = elif;
            this.IsNull = false;
            this.cant_tabs = ct;
        }

        public If_Trad()
           : base(0, 0, "IF")
        {
            this.IsNull = true;
        }


        public override string Execute(Ambit_Trad ambit)
        {

            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + "  ";
            }


            var ambitName = "Global_If";
            if (!ambit.IsNull)
            {
                ambitName = ambit.Ambit_name + "_If";
            }

            var ifAmbit = new Ambit_Trad(ambit, ambitName, "If", false);

            //CONDICION
            var condition = this.condition.Execute(ifAmbit);
            
            //SENTENCIAS
            var if_sentencias = this.sentences.Execute(ifAmbit);

            var if_total = 
                tabs + "if " + condition + " then\n" +
                tabs + "begin\n" +
                if_sentencias+ 
                tabs + "end\n";

            if (elif.IsNull)
            {
                return if_total;
            }
            var elseAmbit = new Ambit_Trad(ambit, ambitName, "Else", false);
            var else_sentence = elif.Execute(elseAmbit);

            if_total += tabs + "else \n"
                + tabs + "begin \n"
                + else_sentence
                + tabs + "end;\n";

            return if_total;

        }
    }
}
