using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Model
{
    class SimboloReferencia
    {
        private string actual;
        private string padre;

        public SimboloReferencia(string actual, string padre)
        {
            this.actual = actual;
            this.padre = padre;
        }

        public string Actual { get => actual; set => actual = value; }
        public string Padre { get => padre; set => padre = value; }
    }
}
