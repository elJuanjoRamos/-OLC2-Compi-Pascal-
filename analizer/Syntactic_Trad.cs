using CompiPascal.controller;
using CompiPascal.Traduccion;
using CompiPascal.Traduccion.grammar;
using CompiPascal.Traduccion.grammar.abstracts;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.analizer
{
    class Syntactic_Trad
    {
        string texto_traduccion = "";
        //AST
        public Ambit_Trad general = new Ambit_Trad();
        InstructionTraduccion instructionAST = new InstructionTraduccion();
        LinkedList<Instruction_Trad> lista_instrucciones = new LinkedList<Instruction_Trad>();
        LinkedList<Instruction_Trad> lista_declaraciones = new LinkedList<Instruction_Trad>();
        LinkedList<Instruction_Trad> lista_funciones = new LinkedList<Instruction_Trad>();
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
            lista_declaraciones = (new DeclarationTraduccion()).LIST_DECLARATIONS(program_body.ChildNodes[0], lista_declaraciones, elementos_her, 0);

            //LISTA DE DECLARACION DE FUNCIONES
            elementos_her.Clear();
            lista_funciones = (new FuncionTraduccion()).FUNCTION_LIST(program_body.ChildNodes[1], lista_funciones, elementos_her, 0);

            //LISTADO DE SENTENCIAS SENTENCIAS 
            lista_instrucciones = instructionAST.INSTRUCTIONS_BODY(program_body.ChildNodes[2], 0);


            //COMENZAR A EJECUTAR
            ejecutar(lista_instrucciones, lista_declaraciones, lista_funciones, paths);
        }


        //EMPIEZA LA EJECUCION
        #region EJECUCION


        public void ejecutar(LinkedList<Instruction_Trad> actual,
            LinkedList<Instruction_Trad> lista_declaraciones, LinkedList<Instruction_Trad> lista_funciones,
            string path)
        {
            //GUARDAR VARIABLES


            foreach (var item in lista_declaraciones)
            {
                try
                {
                    texto_traduccion += item.Execute(general);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            //FUNCIONES
            foreach (var item in lista_funciones)
            {
                try
                {
                    texto_traduccion += "\n" + item.Execute(general);
                }
                catch (Exception)
                {

                    throw;
                }
            }


            //INSTRUCCIONES
            texto_traduccion += "\n\nbegin";

            //GRAFICAR TS
            //GraphController.Instance.setPath(path);
            //GraphController.Instance.graficarTS2(general.getGeneral());


            foreach (var item in actual)
            {
                try
                {
                    texto_traduccion +=  "\n" + item.Execute(general);
                    
                }
                catch (Exception)
                {

                    break;
                }

            }

            texto_traduccion += "\n\nend.";

        }
        #endregion

        public string get_Traduction()
        {
            return texto_traduccion;
        }

    }
}
