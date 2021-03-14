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

       

        public IF(Expression condition, Instruction sentences, Instruction elif, int ro, int co)
            : base(ro,co, "IF")
        {
            this.condition = condition;
            this.sentences = sentences;
            this.elif = elif;
            this.IsNull = false;
            this.row = ro;
            this.column = co;
            
        }

        public IF()
            : base(0,0, "IF")
        {
            this.IsNull = true;
        }
        public override object Execute(Ambit ambit)
        {
            
            var ifAmbit = new Ambit(ambit, ambit.Ambit_name, "If", false);

            //CONDICION
            var condition = this.condition.Execute(ifAmbit);
            //VERIFICA QUE LLA CONDICION SEA BOOLEANA
            if (condition.getDataType != DataType.BOOLEAN)
            {
                setError("Semantico - La condicion del If no es booleana", row, column);
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
                var elseAmbit = new Ambit(ambit, ambit.Ambit_name, "Else", false);
                return elif.Execute(elseAmbit);
            }

        } 
        public void setError(string text, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(text, row, column);
            ConsolaController.Instance.Add(text + " - Row: " + row + " - Col: " + column+"\n");
        }
        public bool IsNull { get => isNull; set => isNull = value; }
    }
}
