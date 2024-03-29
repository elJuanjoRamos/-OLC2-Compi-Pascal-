﻿using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.expression;
using CompiPascal.grammar.identifier;
using CompiPascal.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CompiPascal.AST;

namespace CompiPascal.analizer
{
    class Syntactic
    {
        //VARIABLES GLOBALES
        public Ambit general = new Ambit();
        public LinkedList<Instruction> lista_types = new LinkedList<Instruction>();
        public LinkedList<Instruction> lista_declaraciones = new LinkedList<Instruction>();
        public LinkedList<Instruction> lista_funciones = new LinkedList<Instruction>();

        //AST
        InstructionAST instructionAST = new InstructionAST();

        public void analizer(String cadena, string paths)
        {
            Grammar grammar = new Grammar();
            LanguageData languageData = new LanguageData(grammar);
            ArrayList elemetos_heredados = new ArrayList();
            

            var i = new LanguageData(grammar);
            foreach (var item in i.Errors)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }


            Parser parser = new Parser(new LanguageData(grammar));
            ParseTree tree = parser.Parse(cadena);
            ParseTreeNode root = tree.Root;

            

            if (tree.ParserMessages.Count > 0)
            {
                foreach (var err in tree.ParserMessages)
                {
                    //Errores lexicos
                    if (err.Message.Contains("Invalid character"))
                    {
                        ErrorController.Instance.LexicalError(err.Message, err.Location.Line+1, err.Location.Column);
                    } 
                    //Errores sintacticos
                    else
                    {
                        ErrorController.Instance.SyntacticError(err.Message, err.Location.Line+1, err.Location.Column);
                    }
                }
                return;
            }
            if (root == null)
            {
                return;
            }
            //SE MANDA A GRAFICAR
            GraphController.Instance.getGraph(root, paths);

            //PROGRAM BODY -> GRAMATICA
            var program_body = root.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3);

            //LISTA DE DECLARACION DE TYPES Y ARRAYS
            lista_types = (new TypeAST()).TYPE_LIST(program_body.ChildNodes.ElementAt(0), lista_types);


            //LISTA DE DECLARCION DE VARIABLES
            
            lista_declaraciones = (new DeclarationAST()).LIST_DECLARATIONS(program_body.ChildNodes.ElementAt(1), lista_declaraciones, elemetos_heredados);
            
            //LISTA DE DECLARACION DE FUNCIONES
            elemetos_heredados.Clear();
            lista_funciones =  (new FunctionAST()).FUNCTION_LIST(program_body.ChildNodes.ElementAt(2), lista_funciones, elemetos_heredados);

            //LISTADO DE SENTENCIAS SENTENCIAS 
            LinkedList<Instruction> listaInstrucciones = instructionAST.INSTRUCTIONS_BODY(program_body.ChildNodes.ElementAt(3));
            
            //COMENZAR A EJECUTAR
            ejecutar(listaInstrucciones, lista_declaraciones, lista_funciones, lista_types, paths);

        }


        //EMPIEZA LA EJECUCION
        #region EJECUCION


        public void ejecutar(LinkedList<Instruction> actual, 
            LinkedList<Instruction> lista_declaraciones, LinkedList<Instruction> lista_funciones, 
            LinkedList<Instruction> lista_types, string path)
        {
            //GUARDAR VARIABLES
            var error_type = false;
            var error_variable = false;
            var error_funcion = false;

            GraphController.Instance.setPath(path);

            foreach (var item in lista_types)
            {
                if (item != null)
                {
                    try
                    {
                        var result = item.Execute(general);
                        if (result == null)
                        {
                            error_type = true;
                            break;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }


            foreach (var item in lista_declaraciones)
            {
                try
                {
                    var result = item.Execute(general);
                    if (result == null)
                    {
                        error_variable = true;
                        break;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            if (!error_variable)
            {
                foreach (var item in lista_funciones)
                {
                    try
                    {
                        var res = item.Execute(general);
                        if (res == null)
                        {
                            error_funcion = true;
                            break;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }



            if (!error_variable && !error_funcion)
            {
                //GRAFICAR TS
                //GraphController.Instance.setPath(path);
                //GraphController.Instance.graficarTS(general.getGeneral());


                foreach (var item in actual)
                {
                    try
                    {
                        var result = item.Execute(general);
                        if (result == null)
                        {
                            continue;
                        }
                        else if (result is Instruction)
                        {
                            if (result is Break)
                            {
                                var r = (Break)result;
                                ErrorController.Instance.SyntacticError("La sentencia Break solo puede aparece en ciclos o en la sentencia CASE", r.Row, r.Column);
                            }
                            else if (result is Continue)
                            {
                                var r = (Continue)result;
                                ErrorController.Instance.SyntacticError("La sentencia Continue solo puede aparece en ciclos", r.Row, r.Column);
                            }
                            else if (result is Exit)
                            {
                                var r = (Exit)result;
                                ErrorController.Instance.SyntacticError("La sentencia Exit solo puede aparece en funciones", r.Row, r.Column);
                            }
                        }
                    }
                    catch (Exception)
                    {

                        break;
                    }

                }

                
                GraphController.Instance.getAmbitoGraficar(general.getGeneral(), true);
                GraphController.Instance.graficarTSGeneral();
            }

        }
        #endregion
    }

}
