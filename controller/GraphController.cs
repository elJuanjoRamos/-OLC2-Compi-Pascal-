using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CompiPascal.controller
{
    class GraphController
    {
        private static int cont;
        private static string graph;


        private readonly static GraphController _instance = new GraphController();

        private GraphController()
        {
        }

        public static GraphController Instance
        {
            get
            {
                return _instance;
            }
        }

        public void getGraph(ParseTreeNode root)
        {
            string dot = getDot(root);
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
            if (name.Contains("reserv"))
            {
                return true;
            }
            if (name.Contains("tk_"))
            {
                return true;
            }
            else if (name.Equals("identifier"))
            {
                return true;
            }
            else if (name.Equals("numero"))
            {
                return true;
            }
            else if (name.Equals("real"))
            {
                return true;
            }
            else if (name.Equals("boolean"))
            {
                return true;
            }
            else if (name.Equals("cadena"))
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
