﻿using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class Sentence : Instruction
    {
        private LinkedList<Instruction> list;
        private int row;
        private int column;
        private bool isNull;


        public Sentence(LinkedList<Instruction> list)
            : base("Sentence")
        {
            this.list = list;
            this.row = 0;
            this.column = 0;
            this.isNull = false;
        }
        public Sentence()
            : base("Sentence")
        {
            this.isNull = true;
        }

        public override object Execute(Ambit ambit)
        {
            var newAmbit = new Ambit(ambit, ambit.Ambit_name, ambit.Ambit_name_inmediato, false);

            if (isNull)
            {
                return 0;
            }

            foreach (var inst in this.list)
            {
                try
                {
                    //EJECUTA LAS INSTRUCCIONES
                    var element = inst.Execute(newAmbit);

                    if (element == null)
                    {
                        return null;
                    }
                    if (element is Instruction)
                    {
                        return element;
                    }
                }
                catch (Exception ex)
                {

                    return null;
                }

            }

            return 0;
        }

        public string obtenerTipo(DataType tipo) {

            if (tipo == DataType.INTEGER)
            {
                return "integer";
            }
            else if (tipo == DataType.REAL)
            {
                return "real";
            }
            else if (tipo == DataType.STRING)
            {
                return "string";
            }
            else if (tipo == DataType.TYPE)
            {
                return "type";
            }
            else if (tipo == DataType.BOOLEAN)
            {
                return "boolean";
            }
            return "array";
        }

        public bool IsNull { get => isNull; set => isNull = value; }
        public LinkedList<Instruction> List { get => list; set => list = value; }
    }
}
