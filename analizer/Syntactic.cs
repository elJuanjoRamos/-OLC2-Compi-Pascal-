using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.expression;
using CompiPascal.grammar.identifier;
using CompiPascal.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
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
            Parser parser = new Parser(new LanguageData(grammar));
            ParseTree tree = parser.Parse(cadena);
            ParseTreeNode root = tree.Root;

            instructions(root.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2));

        }
        public void instructions(ParseTreeNode actual)
        {

            if (actual.ChildNodes.Count == 2)
            {
                instruccion(actual.ChildNodes.ElementAt(0));
                instructions(actual.ChildNodes.ElementAt(1));
            }
            else
            {
                instruccion(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
            }
        }

        public void instruccion(ParseTreeNode actual)
        {
            if (actual.Term.Name.Equals("assignation"))
            {

                assignation(actual);
            }
            //System.Diagnostics.Debug.WriteLine("El valor de la expresion es: " + expresion(actual.ChildNodes.ElementAt(2)));
        }

        public void assignation(ParseTreeNode actual)
        {

            var ex = expresion(actual.ChildNodes.ElementAt(5));

            Console.WriteLine(ex.Execute(new Ambit()));

            //var declaration = new Declaration(actual.ChildNodes.ElementAt(2), tipo, (new Expression(), 0, 0);

            Console.WriteLine(actual);
        }
        public Expression expresion(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 3)
            {
                var iz = new Literal(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Value, 0);
                var simb = actual.ChildNodes.ElementAt(1).Token.Text;
                var der = new Literal(actual.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Value, 0);
                return new Arithmetic(iz, der, simb);



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
                return null; // Double.Parse(actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0]);
            }
        }
    }

}
