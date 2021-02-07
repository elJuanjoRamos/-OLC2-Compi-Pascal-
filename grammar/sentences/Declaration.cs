using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;

namespace CompiPascal.grammar.sentences
{
    public class Declaration : Instruction
    {
        private string id;
        private DataType type;
        private Expression value;
        public int row;
        public int column;


        public Declaration(string i, DataType d, Expression e, int r, int c)
            : base(r, c)
        {
            this.id = i;
            this.type = d;
            this.value = e;
            this.row = r;
            this.column = c;
        }

        public override void Execute(Ambit ambit)
        {
            try
            {
                var val = this.value.Execute(ambit);


                ambit.save(this.id, val.Value, val.DataType, false);

            }
            catch (Exception)
            {

            }

        }

    }
}
