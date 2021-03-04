using CompiPascal.grammar.expression;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void getGraph(ParseTreeNode root, string path_startup)
        {
            string dot = getDot(root);

            try
            {
                System.IO.File.WriteAllText(path_startup + "\\"  + "ast.dot", dot);
                var command = "dot -Tpng \"" + path_startup + "\\" +  "ast.dot\"  -o \"" + path_startup + "\\" + "ast.png\"   ";
                var procStarInfo = new ProcessStartInfo("cmd", "/C" + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStarInfo;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception)
            {

                System.Diagnostics.Debug.WriteLine("ERROR AL GENERAR EL DOT");
            }
            /*try
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
            }*/
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

        public void printLexicalError(ArrayList errors, string path_startup)
        {
            var graphi = "digraph G{\n";

            graphi += "graph [pad=\"" + 0.5 + "\", nodesep=\"" + 0.5 + "\", ranksep=\"" + 2 + "\"]\nnode[shape = plain]\nrankdir = LR;\nBaz [label=<";


            graphi += "\n<table border=\"" + 0 + "\" cellborder=\"" + 1 + "\" cellspacing=\"" + 0 + "\">";

            graphi += "<tr>\n<td width='100'><i>Tipo</i></td>\n<td width='100'><i>Mensaje</i></td>\n<td width='100'><i>Linea</i></td>\n<td><i width='100'>Columna</i></td> </tr>\n";


            for (int i = 0; i < errors.Count; i++)
            {
                var err = (Error)errors[i];

                graphi += "<tr>\n<td height='25'>" + "Lexico"+ "</td>\n<td height='25'>" + err.Message + "</td>\n<td height='25'>" + err.Row + "</td>\n<td height='25'>" + err.Column + "</td>\n</tr>";

            }
            graphi += "\n</table>>];}";


            print_image(path_startup, "error_lexico", graphi);
        }

        public void print_image(string path_startup, string name, string dot)
        {
            try
            {
                System.IO.File.WriteAllText(path_startup + "\\" + name+".dot", dot);
                var command = "dot -Tpng \"" + path_startup + "\\" + name+".dot\"  -o \"" + path_startup + "\\" + name+".png\"   ";
                var procStarInfo = new ProcessStartInfo("cmd", "/C" + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStarInfo;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception)
            {

                System.Diagnostics.Debug.WriteLine("ERROR AL GENERAR EL DOT");
            }
        }
    }
}
