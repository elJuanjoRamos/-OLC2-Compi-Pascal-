using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    public class Procedure : Instruction
    {
        private string id;
        private LinkedList<Instruction> parametos;
        private Instruction sentences;
        private int row;
        private int column;

        public Procedure(string id, LinkedList<Instruction> parametos, Instruction sentences)
        : base(0, 0, "Procedure")
        {
            this.id = id;
            this.parametos = parametos;
            this.sentences = sentences;
        }

        public override object Execute(Ambit ambit)
        {
            ambit.saveProcedure(this.id, this);
            return 0;
        }
    }
}
