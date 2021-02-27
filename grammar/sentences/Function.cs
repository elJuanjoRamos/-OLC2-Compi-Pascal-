using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    public class Function : Instruction
    {
        private string id;
        private LinkedList<Instruction> parametos;
        private DataType tipe;
        private Instruction sentences;
        private int row;
        private int column;
        private bool isProcedure;
        public Function(string id, LinkedList<Instruction> parametos, string tipe, Instruction sentences, bool isProcedure)
        : base(0,0,"Function")
        {
            this.id = id;
            this.parametos = parametos;
            this.tipe = GetDataType(tipe);
            this.sentences = sentences;
            this.isProcedure = isProcedure;
        }

        public override object Execute(Ambit ambit)
        {
            ambit.saveFuncion(this.id, this);
            return 0;
        }


        public DataType GetDataType(string d)
        {
            if (d.Equals("integer"))
            {
                return DataType.INTEGER;
            }
            else if (d.Equals("boolean"))
            {
                return DataType.BOOLEAN;
            }
            else if (d.Equals("real"))
            {
                return DataType.REAL;
            }
            else if (d.Equals("any"))
            {
                return DataType.ANY;
            }
            return DataType.STRING;

        }
        public Instruction getParameterAt(int i)
        {
            var cont = 0;
            foreach (var item in parametos)
            {
                if (cont == i)
                {
                    return item;
                }
                cont = cont + 1;

            }
            return null;
        }

        public LinkedList<Instruction> Parametos { get => parametos; set => parametos = value; }
        public Instruction Sentences { get => sentences; set => sentences = value; }
        public bool IsProcedure { get => isProcedure; set => isProcedure = value; }
        public DataType Tipe { get => tipe; set => tipe = value; }
    }
}
