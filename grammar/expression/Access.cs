﻿using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.expression
{
    public class Access : Expression
    {
        private string id;
        private int row;
        private int column;

        public string Id { get => id; set => id = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public Access(string id, int r, int c)
            : base(r, c,"Access")
        {
            this.Id = id;
            this.row = r;
            this.column = c;
        }

        public override Returned Execute(Ambit ambit)
        {
            Identifier value = ambit.getVariable(this.id.ToLower());
            if (value.IsNull)
            {
                ErrorController.Instance.SemantycErrors("La variable '" + this.id + "' no ha sido declarada", Row, column);
            }
            return new Returned(value.Value, value.DataType);
        }
    }
}
