using CompiPascal.grammar.expression;
using CompiPascal.grammar.identifier;
using CompiPascal.grammar.sentences;
using CompiPascal.Model;
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
        private ArrayList ambitos = new ArrayList();


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
            this.ambitos.Clear();
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


            print_image(path_startup, nombre, "",graphi);
        }


        public void getAmbitoGraficar(Ambit ambit, bool isgeneral)
        {

            if (ambitos.Count <= 20)
            {
                var ambito_name = ambit.Ambit_name.Replace("General_", "");

                var enlace = "";

                var texto =
                    ambito_name + "[label=<\n" +
                    "<table border='0' cellborder='1' cellspacing='0'>\n" +
                    "<tr>\n" +
                    "<td>" + ambito_name + "</td>" +
                    "</tr>\n" +
                    "<tr>\n" +
                    "<td cellpadding='4'>\n" +
                    "<table color='orange' cellspacing='0'>\n" +
                    "<tr>\n" +
                    "<td width='100'><i>Nombre</i></td>\n" +
                    "<td width='100'><i>Tipo</i></td>\n" +
                    "<td width='100'><i>Ambito</i></td>\n" +
                    "<td width='100'><i>Valor</i></td>\n" +
                    "<td width='100'><i>Tipo objeto</i></td>\n" +
                    "</tr>\n";


                if (ambit.anterior != null)
                {
                    var ambit_temp = ambit.anterior.Ambit_name.Replace("General_", "");
                    enlace = ambit_temp + "->" + ambito_name + ";\n";

                }

                var res_graphi = "";
                while (ambit != null)
                {
                    if (ambit.Variables.Count > 0)
                    {

                        foreach (var item in ambit.Variables)
                        {
                            Identifier id = (Identifier)item.Value;
                            ambito_name = ambit.Ambit_name.Replace("General_", "");
                            res_graphi = "\n<tr>\n\t<td height='25'>" + id.Id + "</td>\n\t<td height='25'>" + id.DataType + "</td>\n\t<td height='25'>" + ambito_name + "</td>\n\t<td height='25'>" + id.Value.ToString() + "</td>\n\t<td height='25'>" + id.Tipo_dato + "</td>\n</tr>" + res_graphi;
                        }

                    }


                    if (isgeneral)
                    {
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
                                res_graphi = "<tr>\n<td height='25'>" + func.Id + "</td>\n<td height='25'>" + type + "</td>\n<td height='25'>" + ambit.Ambit_name + "</td>\n<td height='25'>" + func.Retorno.ToString() + "</td>\n<td height='25'>" + isfunc + "</td>\n</tr>" + res_graphi;
                            }
                        }
                    }



                    var temp = ambit.anterior;
                    ambit = temp;
                }


                texto += res_graphi
                    + "</table>\n</td>\n</tr>\n </table>>];\n";

                ambitos.Add(new Simbolo(ambito_name, texto, enlace));
            }



        }


        public void getAmbitoGraficar_Trad(Ambit_Trad ambit, bool isgeneral)
        {

            var ambito_name = ambit.Ambit_name.Replace("General_", "");

            var enlace = "";

            var texto =
                ambito_name + "[label=<\n" +
                "<table border='0' cellborder='1' cellspacing='0'>\n" +
                "<tr>\n" +
                "<td>" + ambito_name + "</td>" +
                "</tr>\n" +
                "<tr>\n" +
                "<td cellpadding='4'>\n" +
                "<table color='orange' cellspacing='0'>\n" +
                "<tr>\n" +
                "<td width='100'><i>Nombre</i></td>\n" +
                "<td width='100'><i>Tipo</i></td>\n" +
                "<td width='100'><i>Ambito</i></td>\n" +
                "<td width='100'><i>Valor</i></td>\n" +
                "<td width='100'><i>Tipo objeto</i></td>\n" +
                "</tr>\n";


            if (ambit.Anterior != null)
            {
                var ambit_temp = ambit.Anterior.Ambit_name.Replace("General_", "");
                enlace = ambit_temp + "->" + ambito_name + ";\n";

            }

            var res_graphi = "";

            res_graphi = get_var_anterior(ambit, res_graphi);

            if (ambit.Functions.Count > 0)
            {
                var type = "void";
                var isfunc = "Procedure";

                foreach (var item in ambit.Functions)
                {
                    type = "void";
                    isfunc = "Procedure";

                    Function_Trad func = (Function_Trad)item.Value;
                    if (!func.IsProcedure)
                    {
                        isfunc = "Function";
                        type = func.Tipe.ToString();
                    }
                    res_graphi = "<tr>\n<td height='25'>" + func.Id + "</td>\n<td height='25'>" + type + "</td>\n<td height='25'>" + ambit.Ambit_name + "</td>\n<td height='25'>" + func.Retorno.ToString() + "</td>\n<td height='25'>" + isfunc + "</td>\n</tr>" + res_graphi;
                }
            }



            texto += res_graphi
                + "</table>\n</td>\n</tr>\n </table>>];\n";

            ambitos.Add(new Simbolo(ambito_name, texto, enlace));
        }


        public string get_var_anterior(Ambit_Trad ambit, string res_graphi)
        {
            if (ambit != null)
            {
                if (ambit.Variables.Count > 0)
                {
                    var ambito_name = ambit.Ambit_name.Replace("General_", "");

                    foreach (var item in ambit.Variables)
                    {
                        Identifier_Trad id = (Identifier_Trad)item.Value;
                        res_graphi = "\n<tr>\n\t<td height='25'>" + id.Id + "</td>\n\t<td height='25'>" + id.DataType + "</td>\n\t<td height='25'>" + ambito_name + "</td>\n\t<td height='25'>" + id.Value.ToString() + "</td>\n\t<td height='25'>" + id.Tipo_dato + "</td>\n</tr>" + res_graphi;
                    }

                }

                res_graphi = get_var_anterior(ambit.Anterior, res_graphi);
            }
            return res_graphi;
        }

        public void graficarTSGeneral()
        {


            

            if (ambitos.Count < 20)
            {

                var graphi = "digraph G{\n";
                graphi += "graph [pad=\"" + 0.5 + "\", nodesep=\"" + 0.5 + "\", ranksep=\"" + 2 + "\"]\nnode[shape = plain]\nrankdir = LR;\n";


                var texto = "";
                var enlaces = "";
                foreach (Simbolo simb in ambitos)
                {
                    texto += simb.Texto;
                    enlaces += simb.Apuntador;

                }

                graphi += texto + enlaces + "}";

                print_image(this.path, "tabla_simbolos", "total", graphi);
            }

            
            
        }


        

        public void graficarTSAmbit(Ambit ambit)
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


        public void graficarTS2(Ambit_Trad ambit)
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
                        Identifier_Trad id = (Identifier_Trad)item.Value;
                        ambito_name = ambit.Ambit_name.Replace("General_", "");
                        res_graphi = "\n<tr>\n\t<td height='25'>" + id.Id + "</td>\n\t<td height='25'>" + id.DataType + "</td>\n\t<td height='25'>" + ambito_name + "</td>\n\t<td height='25'>" + id.Value.ToString() + "</td>\n\t<td height='25'>" + id.Tipo_dato + "</td>\n</tr>" + res_graphi;
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

                        Function_Trad func = (Function_Trad)item.Value;
                        if (!func.IsProcedure)
                        {
                            isfunc = "Function";
                            type = func.Tipe.ToString();
                        }
                        res_graphi = "<tr>\n<td height='25'>" + func.Id + "</td>\n<td height='25'>" + type + "</td>\n<td height='25'>" + ambit.Ambit_name + "</td>\n<td height='25'>" + func.Retorno.ToString() + "</td>\n<td height='25'>" + isfunc + "</td>\n</tr>" + res_graphi;
                    }
                }
                var temp = ambit.Anterior;
                ambit = temp;
            }

            graphi += res_graphi + "\n</table>>];\n" + "}";


            print_image(this.path, "tabla_simbolos", ambito_name2, graphi);



        }

        public void print_image(string path_startup, string namedot, string namepng , string dot)
        {
            try
            {
                System.IO.File.WriteAllText(path_startup + "\\" + namedot + "_"+ namepng+".dot", dot);
                var command = "dot -Tpng \"" + path_startup + "\\" + namedot + "_" + namepng + ".dot\"  -o \"" + path_startup + "\\" + namedot + namepng + ".png\"   ";
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
