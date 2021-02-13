using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.expression;
using CompiPascal.grammar.identifier;
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
        public Ambit general = new Ambit();

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

            if (tree.ParserMessages.Count > 0)
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
            }


            getGraph(root);

            var program_body = root.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3);

            //declarations(program_body.ChildNodes.ElementAt(0));

            LinkedList<Instruction> listaInstrucciones = instructions(program_body.ChildNodes.ElementAt(2));
            ejecutar(listaInstrucciones);
            //instructions();

        }

        public void declarations(ParseTreeNode actual)
        {

            if (actual.ChildNodes.Count !=0)
            {

                //VERIFICA SI ES VAR O CONST
                var tipo = actual.ChildNodes.ElementAt(0);
                //ES CONST
                if (tipo.Term.Equals("RESERV_CONST"))
                {

                } 
                //ES VAR
                else
                {

                }




                if (true)
                {

                }

                foreach (var item in actual.ChildNodes)
                {
                    if (item.Term.Equals("DECLARATION_LIST"))
                    {
                        declarations(item);
                    }
                }
                var declaration_list = actual.ChildNodes.ElementAt(1);

                var identifier = declaration_list.ChildNodes.ElementAt(0).Token.Text;






            }


        }




        public LinkedList<Instruction> instructions(ParseTreeNode actual)
        {

            LinkedList<Instruction> listaInstrucciones = new LinkedList<Instruction>();

            var instructions = actual.ChildNodes[1];
            foreach (ParseTreeNode nodo in instructions.ChildNodes)
            {
                var inst = instruction(nodo.ChildNodes[0]);
                listaInstrucciones.AddLast(inst);

                //System.Diagnostics.Debug.WriteLine(nodo);
                //Console.WriteLine(expresion(nodo.ChildNodes[2]));
                //listaInstrucciones.AddLast();
            }
            return listaInstrucciones;
        }
        public Instruction instruction(ParseTreeNode actual)
        {
            if (actual.Term.ToString().Equals("WRITE"))
            {
                var WRHITE_PARAMETER = actual.ChildNodes[2];

                var isln = false;
                LinkedList<Expression> list = new LinkedList<Expression>();
                
                list = WRITES(WRHITE_PARAMETER);
                
                if (actual.ChildNodes[0].Term.ToString().Equals("RESERV_WRITEN"))
                {
                    isln = true;
                }
                return new Write(list, isln);

            }
            else if (actual.Term.ToString().Equals("IFTHEN"))
            {

            }

            return null;
        }


        #region WRITE
        public LinkedList<Expression> WRITES(ParseTreeNode actual)
        {
            LinkedList<Expression> list = new LinkedList<Expression>();
            if (actual.ChildNodes.Count > 0)
            {
                var exp = expresion(actual.ChildNodes.ElementAt(0));
                list.AddLast(exp);
                list = WRHITE_PARAMETER(actual.ChildNodes.ElementAt(1), list);

            }
            return list;
        }
        public LinkedList<Expression> WRHITE_PARAMETER(ParseTreeNode actual, LinkedList<Expression> list)
        {
            if (actual.ChildNodes.Count > 0)
            {
                var exp = expresion(actual.ChildNodes.ElementAt(1));
                list.AddLast(exp);
                list = WRHITE_PARAMETER(actual.ChildNodes.ElementAt(2), list);

            }
            return list;
        }
        #endregion





        

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
                 return new Literal(actual.ChildNodes.ElementAt(0).Token.Text, 2);
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


        public void ejecutar(LinkedList<Instruction> actual)
        {
            foreach (var item in actual)
            {
               item.Execute(general);
                
            }
        }

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
