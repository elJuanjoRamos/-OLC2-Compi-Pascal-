using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    public class Types : Instruction
    {
        private string id;
        private LinkedList<Declaration> lista_variables;
        private int row;
        private int column;

        public Types(string id, LinkedList<Declaration> lista_variables, int row, int column)
        {
            this.id = id;
            this.lista_variables = lista_variables;
            this.row = row;
            this.column = column;
        }

        public string Id { get => id; set => id = value; }
        public LinkedList<Declaration> Lista_variables { get => lista_variables; set => lista_variables = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public override object Execute(Ambit ambit)
        {
            throw new NotImplementedException();
        }
    }
}
