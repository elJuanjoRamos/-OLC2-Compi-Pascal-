using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    public class Sentence_Trad : Instruction_Trad
    {
        private LinkedList<Instruction_Trad> list;
        private int row;
        private int column;
        private bool isNull;

        public Sentence_Trad(LinkedList<Instruction_Trad> list)
           : base(0, 0, "Sentence")
        {
            this.list = list;
            this.row = 0;
            this.column = 0;
            this.isNull = false;
        }
        public Sentence_Trad()
            : base(0, 0, "Sentence")
        {
            this.isNull = true;
        }

        public override string Execute(Ambit_Trad ambit)
        {
            var newAmbit = new Ambit_Trad(ambit, ambit.Ambit_name, ambit.Ambit_name_inmediato, false);

            if (isNull)
            {
                return "";
            }

            var response = "";

            foreach (var inst in list)
            {
                var res = inst.Execute(newAmbit);
                response = response + res;
            }

            return response;
        }
    }
}
