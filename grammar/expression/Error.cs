using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.expression
{
    class Error: Expression
    {
        private string message;
        private int row;
        private int column;
        private DataType type;
        private bool isNull;
        public Error(string message, int row, int column)
        {
            this.message = message;
            this.row = row;
            this.column = column;
            this.type = DataType.ERROR;
            this.isNull = false;
        }

        public string Message { get => message; set => message = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public override Returned Execute(Ambit ambit)
        {
            return new Returned(message, type);
        }
    }
}
