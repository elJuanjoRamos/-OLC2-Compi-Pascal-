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
        private Instruction sentences;
        private Instruction elif;
        private bool isNull;
        public  int row;
        public int column;

       

        public IF(Expression condition, Instruction sentences, Instruction elif)
            : base(0,0, "IF")
        {
            this.condition = condition;
            this.sentences = sentences;
            this.elif = elif;
            this.IsNull = false;
            
        }

        public IF()
            : base(0,0, "IF")
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



            var ifAmbit = new Ambit(ambit, ambitName, "If", false);

            //CONDICION
            var condition = this.condition.Execute(ifAmbit);
            //VERIFICA QUE LLA CONDICION SEA BOOLEANA
            if (condition.getDataType != DataType.BOOLEAN)
            {
                ErrorController.Instance.SemantycErrors("Semantico - La condicion del If no es booleana", 0, 0);
                return null;
            }

            if ((bool)condition.Value == true)
            {
                if (sentences.IsNull)
                {
                    return 0;
                }
                return this.sentences.Execute(ifAmbit);
            }
            else
            {
                if (elif.IsNull)
                {
                    return 0;
                }
                var elseAmbit = new Ambit(ambit, ambitName, "Else", false);
                return elif.Execute(elseAmbit);
            }

        } 
        
        public bool IsNull { get => isNull; set => isNull = value; }
    }
}
