using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Model
{
    class ErrorSin: Exception
    {
        private int row, column;
        private string mensaje;
        private string tipo;

        public ErrorSin(int linea, int columna, string mensaje, string tipo)
        {
            this.row = linea;
            this.column = columna;
            this.mensaje = mensaje;
            this.tipo = tipo;
        }

        public override string ToString()
        {
            return "Se encontro un error: " + this.tipo + " - En la linea " + this.row + " - Mensaje: " + this.mensaje;
        }
    }
}
