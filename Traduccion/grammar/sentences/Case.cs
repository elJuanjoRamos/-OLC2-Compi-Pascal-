using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    class Case_Trad : Instruction_Trad
    {

        private Expresion_Trad condition;
        private Sentence_Trad sentence;
        public bool isNull;
        public bool isElse;
        public int cant_tabs;
        public Case_Trad(Expresion_Trad condition, Sentence_Trad code, int ct)
           : base(0, 0, "Case")
        {
            this.condition = condition;
            this.sentence = code;
            this.isNull = false;
            this.isElse = false;
            this.cant_tabs = ct;
        }

        public Case_Trad()
            : base(0, 0, "Case")
        {
            this.isElse = false;
            this.isNull = true;
        }

        //ESTE ES EL ELSE-CASE
        public Case_Trad(Sentence_Trad code, int cant_tabs)
            : base(0, 0, "Case")
        {
            this.sentence = code;
            this.isNull = false;
            this.isElse = true;
            this.cant_tabs = cant_tabs;
        }

        public override string Execute(Ambit_Trad ambit)
        {
            if (!sentence.IsNull)
            {
                return sentence.Execute(ambit);
            } else
            {
                return "";
            }
        }

        public string getCaseCondition(Ambit_Trad ambit)
        {
            return this.condition.Execute(ambit);
        }
    }
}
