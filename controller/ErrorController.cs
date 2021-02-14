using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using CompiPascal.Model;

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


        ArrayList syntacticErrors = new ArrayList();
        ArrayList lexicalErrors = new ArrayList();



        public void SyntacticError(string message, int row, int col)
        {
            syntacticErrors.Add(new Error(message, row, col));
        }

        public void LexicalError(string message, int row, int col)
        {
            lexicalErrors.Add(new Error(message, row, col));
        }

    }
}
