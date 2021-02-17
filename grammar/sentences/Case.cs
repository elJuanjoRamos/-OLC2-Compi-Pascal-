using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class Case : Instruction
    {
        private Expression condition;
        private Sentence sentence;
        public int row;
        public int column;
        public bool isNull;
        public bool isElse;

        public Case(Expression condition, Sentence code)
            : base(0,0,"Case")
        {
            this.condition = condition;
            this.sentence = code;
            this.isNull = false;
            this.isElse = false;
        }
        public Case()
            : base(0, 0, "Case")
        {
            this.isElse = false;
            this.isNull = true;
        }
        //ESTE ES EL ELSE-CASE
        public Case(Sentence code)
            : base(0, 0, "Case")
        {
            this.sentence = code;
            this.isNull = false;
            this.isElse = true;
        }

        public override object Execute(Ambit ambit)
        {
            //VERIFICA QUE LAS SENTNECIAS NO VENGAN VACIAS
            if (!sentence.IsNull)
            {
                
                
                var element = sentence.Execute(ambit);

                if (element != null)
                {

                    if (element is Instruction)
                    {
                        var inst = (Instruction)element;
                        if (inst.Name.Equals("Break"))
                        {
                            return element;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return 0;
            }

            return 0;
        }


        public Returned getCaseCondition(Ambit ambit)
        {
            return this.condition.Execute(ambit);
        }
    }
}
