using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    public class Arrays : Instruction
    {
        private string id;
        private Expression tipo;
        private DataType dataType;

        public Arrays(string id, Expression tipo, DataType dt)
        {
            this.id = id;
            this.tipo = tipo;
            this.dataType = dt;
        }

        public override object Execute(Ambit ambit)
        {
            ambit.saveArray(id, this);
            return 0;
        }
    }
}
