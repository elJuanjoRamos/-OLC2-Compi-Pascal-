using CompiPascal.controller;
using CompiPascal.Traduccion;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.analizer
{
    class Syntactic_Trad
    {
        //AST
        InstructionTraduccion instructionAST = new InstructionTraduccion();
        string lista_instrucciones = "";
        string lista_declaraciones = "";
        string lista_funciones = "";
        public Syntactic_Trad()
        {

        }
        public void analizer(string cadena, string paths)
        {

            Grammar_Trad grammar = new Grammar_Trad();
            LanguageData languageData = new LanguageData(grammar);
            

            Parser parser = new Parser(new LanguageData(grammar));
            ParseTree tree = parser.Parse(cadena);
            ParseTreeNode root = tree.Root;
            /*var i = new LanguageData(grammar);
            foreach (var item in i.Errors)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }*/

            if (tree.ParserMessages.Count > 0)
            {
                foreach (var err in tree.ParserMessages)
                {
                    //Errores lexicos
                    if (err.Message.Contains("Invalid character"))
                    {
                        ErrorController.Instance.LexicalError(err.Message, err.Location.Line - 1, err.Location.Column);
                    }
                    //Errores sintacticos
                    else
                    {
                        ErrorController.Instance.SyntacticError(err.Message, err.Location.Line - 1, err.Location.Column);
                    }
                }
                return;
            }
            if (root == null)
            {
                return;
            }
            //Console.WriteLine(root);
            //GraphController.Instance.getGraph(root, paths);



            //PROGRAM BODY -> GRAMATICA
            var program_body = root.ChildNodes[0].ChildNodes[3];

            //LISTA DE DECLARCION DE VARIABLES
            ArrayList elementos_her = new ArrayList();
            lista_declaraciones = (new DeclarationTraduccion()).LIST_DECLARATIONS(program_body.ChildNodes[0], lista_declaraciones, elementos_her);

            //LISTA DE DECLARACION DE FUNCIONES
            elementos_her.Clear();
            lista_funciones = (new FuncionTraduccion()).FUNCTION_LIST(program_body.ChildNodes[1], lista_funciones, elementos_her);

            //LISTADO DE SENTENCIAS SENTENCIAS 
            lista_instrucciones = instructionAST.INSTRUCTIONS_BODY(program_body.ChildNodes[2], 0);


            //COMENZAR A EJECUTAR
        }

        public string get_Traduction()
        {
            return lista_declaraciones + "\n\n" + lista_funciones + "\n\n" + lista_instrucciones;
        }

    }
}
