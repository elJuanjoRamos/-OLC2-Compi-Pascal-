using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    class While_Trad : Instruction_Trad
    {
        private Expresion_Trad condition;
        private Sentence_Trad sentences;
        private int cant_tabs;
        public While_Trad(Expresion_Trad condition, Sentence_Trad sentences, int ct)
           : base(0, 0, "While")
        {
            this.condition = condition;
            this.sentences = sentences;
            this.cant_tabs = ct;
        }


        public override string Execute(Ambit_Trad ambit)
        {
            var ambitName = "Global_While";
            if (!ambit.IsNull)
            {
                ambitName = ambit.Ambit_name + "_While";
            }

            var whileAmbit = new Ambit_Trad(ambit, ambitName, "While", false);

            //CONDICION
            var cond = condition.Execute(whileAmbit);

            //SENTENCIA
            var while_sentencia = sentences.Execute(whileAmbit);


            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + "  ";
            }

            return 
                tabs + "while " + cond + " do\n" +
                tabs + "begin\n" +
                    while_sentencia + "\n" +
                tabs + "end;\n";

        }
    }
}
