using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.identifier;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    public class Function : Instruction
    {
        private string id;
        private LinkedList<Instruction> parametos;
        private LinkedList<Instruction> declaraciones;
        private ArrayList parametros_referencia;
        private DataType tipe;
        private Instruction sentences;
        private int row;
        private int column;
        private bool isProcedure;
        private string retorno;
        public Function(string id, LinkedList<Instruction> parametos, LinkedList<Instruction> declas, string tipe, Instruction sentences, bool isProcedure, int row, int col)
        : base(row ,col,"Function")
        {
            this.retorno = "-";
            this.id = id;
            this.parametos = parametos;
            this.declaraciones = declas;
            this.tipe = GetDataType(tipe);
            this.sentences = sentences;
            this.isProcedure = isProcedure;
            this.parametros_referencia = new ArrayList();
            this.row = row;
            this.column = col;
        }

        public override object Execute(Ambit ambit)
        {
            ambit.saveFuncion(this.id, this);
            return 0;
        }


        public DataType GetDataType(string d)
        {
            if (d.Equals("integer"))
            {
                this.retorno = "0";
                return DataType.INTEGER;
            }
            else if (d.Equals("boolean"))
            {
                this.retorno = "false";
                return DataType.BOOLEAN;
            }
            else if (d.Equals("real"))
            {
                this.retorno = "0";
                return DataType.REAL;
            }
            else if (d.Equals("any"))
            {
                return DataType.ANY;
            }
            this.retorno = "-";
            return DataType.STRING;

        }
        public Instruction getParameterAt(int i)
        {
            var cont = 0;
            foreach (var item in parametos)
            {
                if (cont == i)
                {
                    return item;
                }
                cont = cont + 1;

            }
            return null;
        }

        public LinkedList<Instruction> Parametos { get => parametos; set => parametos = value; }
        public Instruction Sentences { get => sentences; set => sentences = value; }
        public bool IsProcedure { get => isProcedure; set => isProcedure = value; }
        public DataType Tipe { get => tipe; set => tipe = value; }
        public string Retorno { get => retorno; set => retorno = value; }
        public string Id { get => id; set => id = value; }
        public LinkedList<Instruction> Declaraciones { get => declaraciones; set => declaraciones = value; }
        public ArrayList Parametros_referencia { get => parametros_referencia; set => parametros_referencia = value; }
    }
}
