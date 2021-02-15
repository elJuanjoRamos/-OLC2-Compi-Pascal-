using CompiPascal.controller;
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
            : base(0,0, "Sentence")
        {
            this.list = list;
            this.row = 0;
            this.column = 0;
            this.isNull = false;
        }
        public Sentence()
            : base(0, 0, "Sentence")
        {
            this.isNull = true;
        }

        public override object Execute(Ambit ambit)
        {
            //console.error("SENTENCIA")
            var newAmbit = new Ambit(ambit, ambit.Ambit_name, false);
            object retorno = null;

            foreach (var inst in this.list)
            {
                try
                {
                    var element = inst.Execute(newAmbit);
                    if (element != null)
                    {
                        retorno = element;
                    } else
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    return null;
                }

            }
            return retorno;
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

    }
}
