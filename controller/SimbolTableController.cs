using CompiPascal.grammar.abstracts;
using CompiPascal.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.controller
{
    class SimbolTableController
    {
        private readonly static SimbolTableController _instance = new SimbolTableController();

        private SimbolTableController()
        {
        }

        public static SimbolTableController Instance
        {
            get
            {
                return _instance;
            }
        }


        ArrayList tablasimbolos = new ArrayList();
        public void add(string id, DataType tipo, string ambit, Expression value, bool variable, bool function)
        {
            if (!ExistVariable(id))
            {
                this.tablasimbolos.Add(new Tabla(id, tipo, ambit, value, variable, function));
            }
        }



        public bool ExistVariable(string name)
        {
            var flag = false;
            foreach (var item in this.tablasimbolos)
            {
                if (((Tabla)item).Name.Equals(name))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
    }
}
