using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.controller
{
    class GraphController
    {
        private static int cont;
        private static string graph;

        public GraphController() { }


        public string getDot(ParseTreeNode root)
        {
            graph = "digraph G{\n";
            graph += "nodo0[ label=\"" + escape(root.ToString()) + "\"];\n";
            cont = 1;
            getBody("nodo0", root);
            graph += "}";
            return graph;
        }

        public void getBody(string father, ParseTreeNode root)
        {
            
                foreach (ParseTreeNode child in root.ChildNodes)
                {
                    var childName = "nodo" + cont.ToString();
                    if (child.ChildNodes.Count != 0 || validName(child.Term.Name.ToLower()))
                    {
                        graph += childName + "[ label =\"" + escape(child.ToString()) + "\"]\n";
                        graph += father + "->" + childName + "; \n";
                        cont++;

                    }
                    getBody(childName, child);
                }
            
            
        }
        public bool validName(string name)
        {
            if (name.Equals("leaf"))
            {
                return true;
            }
            if (name.Equals("identifier"))
            {
                return true;
            }
            if (name.Equals("numero"))
            {
                return true;
            }
            if (name.Equals("real"))
            {
                return true;
            }
            if (name.Equals("boolean"))
            {
                return true;
            }
            if (name.Equals("cadena"))
            {
                return true;
            }
            return false;
        }
        public string escape(String cadena)
        {
            cadena = cadena.Replace("\\", "\\\\");
            cadena = cadena.Replace("\"", "\\\"");
            return cadena;
        }
    }
}
