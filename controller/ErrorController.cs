using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CompiPascal.controller
{
    class ErrorController
    {

        private readonly static ErrorController _instance = new ErrorController();

        private ErrorController()
        {
        }

        public static ErrorController Instance
        {
            get
            {
                return _instance;
            }
        }


        ArrayList error = new ArrayList();



        public void add(string texto)
        {
            this.error.Add(texto);
        }

    }
}
