using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    class Repeat_Trad : Instruction_Trad
    {
        private Expresion_Trad condition;
        private Sentence_Trad sentences;
        private int cant_tabs;
        private int row;
        private int column;

        public Repeat_Trad(Expresion_Trad condition, Sentence_Trad sentences, int ct)
           : base(0, 0, "Repeat")
        {
            this.condition = condition;
            this.sentences = sentences;
            this.cant_tabs = ct;
        }
        public override string Execute(Ambit_Trad ambit)
        {
            var ambitName = "Global_Repeat";
            if (!ambit.IsNull)
            {
                ambitName = ambit.Ambit_name + "_Repeat";
            }
            var repeatAmbit = new Ambit_Trad(ambit, ambitName, "Repeat", false);

            //CONDICION
            var condicion = condition.Execute(repeatAmbit);
            //SENTENCIAS
            var repeat_senteces = sentences.Execute(repeatAmbit);


            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + "  ";
            }

            return
                tabs + "repeat \n" +
                    repeat_senteces + "\n" +
                tabs + "until " + condition + ";";
        }
    }
}
