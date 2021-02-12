using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.expression;
using CompiPascal.grammar.identifier;
using CompiPascal.grammar.instruction;
using CompiPascal.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CompiPascal.analizer
{
    class Syntactic
    {
        public int root = 0;

        public void analizer(String cadena)
        {
            Grammar grammar = new Grammar();
            LanguageData languageData = new LanguageData(grammar);

            /*foreach (var item in languageData.Errors)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }*/
            Parser parser = new Parser(new LanguageData(grammar));
            ParseTree tree = parser.Parse(cadena.ToLower());
            ParseTreeNode root = tree.Root;
            if (root == null)
            {
                return;
            }

            /*if (tree.ParserMessages.Count > 0)
            {
                foreach (var err in tree.ParserMessages)
                {
                    //Errores lexicos
                    if (err.Message.Contains("Invalid character"))
                    {
                        ErrorController.Instance.LexicalError(err.Message, err.Location.Line, err.Location.Column);
                    } 
                    //Errores sintacticos
                    else
                    {
                        ErrorController.Instance.SyntacticError(err.Message, err.Location.Line, err.Location.Column);
                    }
                }
            }*/


            getGraph(root);
            //instructions(root.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2));
            
        }
        public /*LinkedList<Instruction>*/ void instructions(ParseTreeNode actual)
        {
            LinkedList<Instruction> instruction_list = new LinkedList<Instruction>();

            /*foreach (ParseTreeNode node in actual.ChildNodes)
            {
                var exp = expresion(node);
                var a = exp.Execute(new Ambit());

                System.Diagnostics.Debug.WriteLine(a.Value);
                instruction_list.AddLast(new Evaluar(exp));
            }*/
            if (actual.ChildNodes.Count == 2)
            {
                instruccion(actual.ChildNodes.ElementAt(0));
                instructions(actual.ChildNodes.ElementAt(1));
            }
            else
            {
                instruccion(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
            }

            //return instruction_list;
        }

        public void instruccion(ParseTreeNode actual)
        {
            if (actual.Term.Name.Equals("DECLARATION"))
            {

                declaration(actual);
            }
        }

        public void declaration(ParseTreeNode actual)
        {


            var declaration_list = actual.ChildNodes.ElementAt(1);

            var identifier = declaration_list.ChildNodes.ElementAt(0).Token.Text;


            assignation(declaration_list.ChildNodes.ElementAt(1), identifier);

   
        }

        public void declaration_prima(ParseTreeNode actual)
        {
            /*LISTA_ DECLARACION_PRIMA => 
                 LISTA_DECLARACION
                | epsilon
            */

            if (actual.ChildNodes.Count != 0)
            {
                declaration(actual.ChildNodes.ElementAt(0));
            }
            else
            {
                //EPSILON
            }
        }


        public void assignation(ParseTreeNode actual, string identificador)
        {

            if (actual.ChildNodes.ElementAt(0).Token.Text.Equals(":"))
            {

                var type = data_type(actual.ChildNodes.ElementAt(1));
                assignation_prima(actual.ChildNodes.ElementAt(2), identificador, type);
                declaration_prima(actual.ChildNodes.ElementAt(3));


            }
            else
            {
                //SE OBTIENE EL TIPO
                //type = assignation.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text;

            }
        }
        public void assignation_prima(ParseTreeNode actual, string identificador, string type)
        {
            if (actual.ChildNodes.Count != 0)
            {

                var exp = expresion(actual.ChildNodes.ElementAt(1));

                var a = exp.Execute(new Ambit());
                System.Diagnostics.Debug.WriteLine(a.Value);

                Declaration declaration = new Declaration(identificador, type, exp, 0, 0);
        

            }
            
        }
        public string data_type(ParseTreeNode actual)
        {
            return actual.ChildNodes.ElementAt(0).Token.Text;

        }

        

        #region EXPRESIONES
        public Expression expresion(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 3)
            {



               

                var simb = actual.ChildNodes.ElementAt(1).Token.Text;
                var opcion = 0;


                if (simb.Equals("+") || simb.Equals("-") || simb.Equals("*") || simb.Equals("/") || simb.Equals("%"))
                {
                    opcion = 1;
                } else if (simb.Equals("<") || simb.Equals("<=") || simb.Equals(">") || simb.Equals(">="))
                {
                    opcion = 2;
                }
                else if (simb.Equals("and") || simb.Equals("or") || simb.Equals("not"))
                {
                    opcion = 3;
                }
                var iz = GetLiteral(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
                var der = GetLiteral(actual.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0));

                switch (opcion)
                {
                    case 1: //OPERACIONES ARITMETICAS
                        return new Arithmetic(iz, der, simb);
                    case 2: //OPERACIONES RELACIONALES
                        return new Relational(iz, der, simb, 0, 0);
                    case 3: //OPREACIONES LOGICAS
                        return new Logical(iz, der, simb, 0, 0);
                    default:
                        break;
                }

                return null;


                /*string tokenOperador = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                switch (tokenOperador)
                {
                    case "+":
                        return expresion(actual.ChildNodes.ElementAt(0)) + expresion(actual.ChildNodes.ElementAt(2));
                    case "-":
                        return expresion(actual.ChildNodes.ElementAt(0)) - expresion(actual.ChildNodes.ElementAt(2));
                    case "*":
                        return expresion(actual.ChildNodes.ElementAt(0)) * expresion(actual.ChildNodes.ElementAt(2));
                    case "/":
                        return expresion(actual.ChildNodes.ElementAt(0)) / expresion(actual.ChildNodes.ElementAt(2));
                    default:
                        return expresion(actual.ChildNodes.ElementAt(1));
                }*/

            }
            else if (actual.ChildNodes.Count == 2)
            {
                return null; //-1 * expresion(actual.ChildNodes.ElementAt(1));
            }
            else
            {
                 return new Literal(actual.ChildNodes.ElementAt(0).Token.Text, 0);
            }
        }

        public Literal GetLiteral(ParseTreeNode node)
        {
            var result = new Literal();

            if (node.Term.ToString().ToString().Equals("NUMERO"))
            {
                result = new Literal(node.Token.Value, 1);
            }
            else if (node.Term.ToString().Equals("REAL"))
            {
                result = new Literal(node.Token.Value, 2);
            }
            else if (node.Term.ToString().Equals("IDENTIFIER"))
            {
                result = new Literal(node.Token.Value, 3);
            }
            else if (node.Term.ToString().Equals("CADENA"))
            {
                result = new Literal(node.Token.Value, 4);
            }
            else if (node.Term.ToString().Equals("boolean"))
            {
                result = new Literal(node.Token.Value, 5);
            }

            return result;
        }
        #endregion


        

        public void getGraph(ParseTreeNode root)
        {
            GraphController graph = new GraphController();
            string dot = graph.getDot(root);
            var path = "ast.txt";
            try
            {
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(dot);
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("ERROR AL GENERAR EL DOT");
            }
        }
    }

}
