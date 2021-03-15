using CompiPascal.Traduccion.grammar.abstracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion.grammar.sentences
{
    public class Arrays_Trad : Instruction_Trad
    {

        private string id;
        private Expresion_Trad inf;
        private Expresion_Trad sup;
        private ArrayList elementos;
        private string dataType;


        public Arrays_Trad(string id, Expresion_Trad inf, Expresion_Trad sup, string dt)
        {
            this.id = id;
            this.inf = inf;
            this.sup = sup;
            this.elementos = new ArrayList();
            this.dataType = (dt);
        }
        public Arrays_Trad(string id, Expresion_Trad inf, Expresion_Trad sup, string dt, ArrayList elems)
        {
            this.id = id;
            this.inf = inf;
            this.sup = sup;
            this.elementos = elems;
            this.dataType = (dt);
        }

        public Expresion_Trad Inf { get => inf; set => inf = value; }
        public Expresion_Trad Sup { get => sup; set => sup = value; }
        public ArrayList Elementos { get => elementos; set => elementos = value; }
        public string DataType { get => dataType; set => dataType = value; }



        public override string Execute(Ambit_Trad ambit)
        {
            ambit.saveArray(id, this);

            var texto = "type \n\t";



            return texto;
        }
    }
}
