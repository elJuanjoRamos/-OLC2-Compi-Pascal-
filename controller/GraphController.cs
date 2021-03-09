using CompiPascal.grammar.expression;
using CompiPascal.grammar.identifier;
using CompiPascal.grammar.sentences;
using CompiPascal.Traduccion.grammar;
using CompiPascal.Traduccion.grammar.identifier;
using CompiPascal.Traduccion.grammar.sentences;
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
        private string path = "";

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
        public void setPath(string path)
        {
            this.path = path;
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

        public void printError(ArrayList errors, string path_startup, string tipo, string nombre)
        {

            var graphi = "digraph G{\n";

            graphi += "graph [pad=\"" + 0.5 + "\", nodesep=\"" + 0.5 + "\", ranksep=\"" + 2 + "\"]\nnode[shape = plain]\nrankdir = LR;\nBaz [label=<";


            graphi += "\n<table border=\"" + 0 + "\" cellborder=\"" + 1 + "\" cellspacing=\"" + 0 + "\">";

            graphi += "<tr>\n<td width='100'><i>Tipo</i></td>\n<td width='100'><i>Mensaje</i></td>\n<td width='100'><i>Linea</i></td>\n<td><i width='100'>Columna</i></td> </tr>\n";


            for (int i = 0; i < errors.Count; i++)
            {
                var err = (Error)errors[i];

                graphi += "<tr>\n<td height='25'>" + tipo+ "</td>\n<td height='25'>" + err.Message + "</td>\n<td height='25'>" + err.Row + "</td>\n<td height='25'>" + err.Column + "</td>\n</tr>";

            }
            graphi += "\n</table>>];}";


            print_image(path_startup, nombre, nombre,graphi);
        }


        public void graficarTS(Ambit ambit)
        {
            var res_graphi = "";
            var ambito_name = "";
            var ambito_name2 = ambit.Ambit_name.Replace("General_", "");
            var graphi = "digraph G{\n";

            graphi += "label = \"" + "Ambito Graficado: " + ambito_name2 + "\"\n";

            graphi += "graph [pad=\"" + 0.5 + "\", nodesep=\"" + 0.5 + "\", ranksep=\"" + 2 + "\"]\nnode[shape = plain]\nrankdir = LR;\nBaz [label=<";


            graphi += "\n<table border=\"" + 0 + "\" cellborder=\"" + 1 + "\" cellspacing=\"" + 0 + "\">";


            graphi += "\n<tr>\n\t<td width='100'><i>Nombre</i></td>\n\t<td width='100'><i>Tipo</i></td>\n\t<td width='100'><i>Ambito</i></td>\n\t<td><i width='100'>Valor</i></td>\n\t<td><i width='100'>Tipo Objeto</i></td>\n</tr>\n";



            while (ambit != null)
            {
                if (ambit.Variables.Count > 0)
                {

                    foreach (var item in ambit.Variables)
                    {
                        Identifier id = (Identifier)item.Value;
                        ambito_name = ambit.Ambit_name.Replace("General_", "");
                        res_graphi = "\n<tr>\n\t<td height='25'>" + id.Id + "</td>\n\t<td height='25'>" + id.DataType + "</td>\n\t<td height='25'>" + ambito_name + "</td>\n\t<td height='25'>" + id.Value.ToString() + "</td>\n\t<td height='25'>" + id.Tipo_dato+"</td>\n</tr>" + res_graphi;
                    }

                }

                if (ambit.Functions.Count > 0)
                {
                    var type = "void";
                    var isfunc = "Procedure";
                    
                    foreach (var item in ambit.Functions)
                    {
                        type = "void";
                        isfunc = "Procedure";

                        Function func = (Function)item.Value;
                        if (!func.IsProcedure)
                        {
                            isfunc = "Function";
                            type = func.Tipe.ToString();
                        }
                        res_graphi = "<tr>\n<td height='25'>" + func.Id + "</td>\n<td height='25'>" + type + "</td>\n<td height='25'>" + ambit.Ambit_name + "</td>\n<td height='25'>" + func.Retorno.ToString() + "</td>\n<td height='25'>"+isfunc+"</td>\n</tr>" + res_graphi;
                    }
                }
                var temp = ambit.anterior;
                ambit = temp;
            }

            graphi += res_graphi + "\n</table>>];\n" +"}";


            print_image(this.path, "tabla_simbolos", ambito_name2, graphi);



        }

   
        public void print_image(string path_startup, string namedot, string namepng , string dot)
        {
            try
            {
                System.IO.File.WriteAllText(path_startup + "\\" + namedot+".dot", dot);
                var command = "dot -Tpng \"" + path_startup + "\\" + namedot+".dot\"  -o \"" + path_startup + "\\" + namepng+ ".png\"   ";
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
