﻿using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.grammar.abstracts;

namespace CompiPascal.grammar.identifier
{
    public class Identifier
    {
        private object _value;
        private string id;
        private DataType type;
        private bool esconsante;
        private string tipo_dato;
        private bool isNull;
        private bool isAssiged;
        private bool perteneceFucion;

        public Identifier(object v, string i, DataType t, bool ec, bool isa, bool perteneceFuc, string ti)
        {
            this._value = v;
            this.id = i;
            this.type = t;
            this.esconsante = ec;
            this.isNull = false;
            this.isAssiged = isa;
            this.perteneceFucion = perteneceFuc;
            this.tipo_dato = ti;
        }
        public Identifier()
        {
            this.isNull = true;
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public DataType DataType
        {
            get { return type; }
            set { type = value; }
        }

        public bool Esconstante
        {
            get { return esconsante; }
            set { esconsante = value; }
        }
        public bool IsNull
        {
            get { return isNull; }
            set { isNull = value; }
        }

        public bool IsAssiged { get => isAssiged; set => isAssiged = value; }
        public bool PerteneceFucion { get => perteneceFucion; set => perteneceFucion = value; }
        public string Tipo_dato { get => tipo_dato; set => tipo_dato = value; }
    }
}
