using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Model
{
    class Simbolo
    {
        private string id;
        private string texto;
        private string apuntador;
        public Simbolo(string id, string texto, string ap)
        {
            this.id = id;
            this.texto = texto;
            this.apuntador = ap;
        }

        public string Id { get => id; set => id = value; }
        public string Texto { get => texto; set => texto = value; }
        public string Apuntador { get => apuntador; set => apuntador = value; }
    }
}
