using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using CompiPascal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class IF : Instruction
    {
        private Expression condition;
        private Sentence sentences;
        private Sentence elif;
        private bool isNull;
        public  int row;
        public int column;

       

        public IF(Expression condition, Sentence sentences, Sentence elif)
            : base(0,0)
        {
            this.condition = condition;
            this.sentences = sentences;
            this.elif = elif;
            this.IsNull = false;
        }

        public IF()
            : base(0,0)
        {
            this.IsNull = true;
        }
        public override object Execute(Ambit ambit)
        {
            var ambitName = "Global_If";
            if (!ambit.IsNull)
            {
                ambitName = ambit.Ambit_name + "_If";
            }



            var ifAmbit = new Ambit(ambit, ambitName, false);

            var condition = this.condition.Execute(ifAmbit);

            if (condition.getDataType != DataType.BOOLEAN)
            {
                ConsolaController.Instance.Add("Semantico - La condicion del If no es booleana");
                throw new Exception();

            }

            if ((bool)condition.Value == true)
            {
                if (!sentences.IsNull)
                {
                    return this.sentences.Execute(ifAmbit);
                }
                else {
                    return 0;
                }
            }
            else
            {
                if (!elif.IsNull)
                {
                    var elseAmbit = new Ambit(ambit, ambitName, false);
                    return this.elif.Execute(elseAmbit);
                }
                else
                {
                    return 0;
                }
 
            }
        } 
        
        public bool IsNull { get => isNull; set => isNull = value; }
    }
}
