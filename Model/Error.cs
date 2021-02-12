using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Model
{
    class Error
    {
        private string message;
        private int row;
        private int column;

        public Error(string message, int row, int column)
        {
            this.message = message;
            this.row = row;
            this.column = column;
        }

        public string Message { get => message; set => message = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
    }
}
