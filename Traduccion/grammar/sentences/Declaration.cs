using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    class Declaration_Trad : Instruction_Trad
    {
        private string id;
        private string type;
        private Expresion_Trad value;
        private bool isConst;
        private int cant_tabs;
        private bool referencia;
        //CONSTRUCTOR PARA VARIABLES
        public Declaration_Trad(string id, String dataType, Expresion_Trad ex, bool ic, int ct, bool refe)
            : base(0, 0, "Declaration")
        {
            this.id = id;
            this.type = dataType;
            this.value = ex;
            this.isConst = ic;
            this.cant_tabs = ct;
            this.referencia = refe;
        }

        public string Id { get => id; set => id = value; }
        public string Type { get => type; set => type = value; }
        public bool Referencia { get => referencia; set => referencia = value; }

        public override string Execute(Ambit_Trad ambit)
        {
            
            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + "  ";
            }

            var val = this.value.Execute(ambit);
            ambit.save(Id, val, Type, "Variable");

            if (this.isConst)
            {
                return tabs+ "const " + this.id + "= " + val + ";\n";
            } else
            {
                return tabs+ "var " + this.id + ":" + this.type + "= " + val + ";\n";
            }

        }
    }
}
