using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using CompiPascal.Model;
using CompiPascal.grammar.expression;

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
        ArrayList semantycErrors = new ArrayList();



        public void SyntacticError(string message, int row, int col)
        {
            syntacticErrors.Add(new Error(message, row, col));
        }

        public void LexicalError(string message, int row, int col)
        {
            lexicalErrors.Add(new Error(message, row, col));
        }
        public void SemantycErrors(string message, int row, int col)
        {
            semantycErrors.Add(new Error(message, row, col));
        }

        public void Clean()
        {
            semantycErrors.Clear();
            syntacticErrors.Clear();
            lexicalErrors.Clear();
        }


        public string getLexicalError()
        {
            return getText(lexicalErrors);
        }
        public string getSemantycError()
        {
            return getText(semantycErrors);
        }

        public string getSintactycError()
        {
            return getText(syntacticErrors);
        }


        public string getText(ArrayList ar)
        {
            var text = "";
            if (ar.Count > 0)
            {
                foreach (var item in ar)
                {
                    var err = (Error)item;
                    text = text + err.toString() + "\n";
                }
                text = "\n" + text;
            }
            return text;

        }
    }
}
