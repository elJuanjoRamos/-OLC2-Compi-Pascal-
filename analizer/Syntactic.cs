using CompiPascal.controller;
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

namespace CompiPascal.analizer
{
    class Syntactic
    {
        //VARIABLES GLOBALES
        public Ambit general = new Ambit();
        public LinkedList<Instruction> lista_declaraciones = new LinkedList<Instruction>();

        public void analizer(String cadena)
        {
            Grammar grammar = new Grammar();
            LanguageData languageData = new LanguageData(grammar);
            ArrayList elemetos_heredados = new ArrayList();
            
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

            lista_declaraciones = LIST_DECLARATIONS(program_body.ChildNodes.ElementAt(0), lista_declaraciones, elemetos_heredados);


            LinkedList<Instruction> listaInstrucciones = instructions(program_body.ChildNodes.ElementAt(2));
            ejecutar(listaInstrucciones, lista_declaraciones);
            //instructions();

        }




        public LinkedList<Instruction> INSTRUCTIONS_BODY(ParseTreeNode actual)
        {
            LinkedList<Instruction> instructions = new LinkedList<Instruction>();
            var begind = actual.ChildNodes.ElementAt(0);

            var lista_instruciones = actual.ChildNodes.ElementAt(1);

            foreach (var item in lista_instruciones.ChildNodes)
            {
                instructions.AddLast(instruction(item.ChildNodes[0]));
            }


            var end = actual.ChildNodes.ElementAt(2);
            return instructions;
        }

        public LinkedList<Instruction> instructions(ParseTreeNode actual)
        {

            LinkedList<Instruction> listaInstrucciones = new LinkedList<Instruction>();

            var instructions = actual.ChildNodes[1];
            foreach (ParseTreeNode nodo in instructions.ChildNodes)
            {
                var inst = instruction(nodo.ChildNodes[0]);
                listaInstrucciones.AddLast(inst);
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
            else if (actual.Term.ToString().Equals("IF-THEN"))
            {
                IF ifs = IFTHEN(actual);
                return ifs;
            }
            else if (true)
            {

            }

            return null;
        }


        #region DECLARACION Y ASIGNACION

        public LinkedList<Instruction> LIST_DECLARATIONS(ParseTreeNode actual, LinkedList<Instruction> lista_actual, ArrayList elementos_her)
        {
            
            /*
             DECLARATION_LIST.Rule
               = RESERV_VAR + IDENTIFIER + DECLARATION_BODY + VAR_DECLARATION + DECLARATION_LIST
               | RESERV_CONST + IDENTIFIER + EQUALS + LOGIC_EXPRESION + PUNTO_COMA + CONST_DECLARATION + DECLARATION_LIST
               | Empty
               ;
             */

            if (actual.ChildNodes.Count != 0)
            {
                

                //VERIFICA SI ES VAR O CONST
                var tipo = actual.ChildNodes.ElementAt(0);

                //ES CONST
                if (tipo.Term.ToString().Equals("RESERV_CONST"))
                {
                    var identifier = actual.ChildNodes[1].Token.Text;
                    lista_actual.AddLast(new Declaration(identifier, expresion(actual.ChildNodes[3]), 0,0, true));
                    lista_actual = CONST_DECLARATION(actual.ChildNodes[5], lista_actual, elementos_her);
                    lista_actual = LIST_DECLARATIONS(actual.ChildNodes[6], lista_actual, elementos_her);
                }
                //ES VAR
                else
                {
                    var identifier = actual.ChildNodes[1].Token.Text;
                    elementos_her.Add(identifier);

                    lista_actual = DECLARATION_BODY(actual.ChildNodes[2], lista_actual, elementos_her);
                    lista_actual = VAR_DECLARATION(actual.ChildNodes[3], lista_actual, elementos_her);
                    lista_actual = LIST_DECLARATIONS(actual.ChildNodes[4], lista_actual, elementos_her);

                }






                return lista_actual;


            }


            return lista_declaraciones;
        }

        public LinkedList<Instruction> DECLARATION_BODY(ParseTreeNode actual, LinkedList<Instruction> lista_actual, ArrayList elementos_her)
        {
            var element = actual.ChildNodes[0];
            // SI VIENE VARIOS IDES 
            if (element.Term.ToString().ToLower().Equals("tk_coma"))
            {
                //OBTENGO EL IDENTIFICADOR
                var identifier = actual.ChildNodes[1].Token.Text;
                elementos_her.Add(identifier);
                //OBTENGO LOS DEMAS IDENTIFICADORES
                elementos_her = MORE_ID_DECLARATION(actual.ChildNodes[2], elementos_her);
                //OBTENGO EL TIPO
                var datatype = actual.ChildNodes[4].ChildNodes[0].Token.Text;

                foreach (var item in elementos_her)
                {
                    lista_actual.AddLast(GetDeclarationValue(item.ToString(), datatype));
                }
                elementos_her.Clear();

            } 
            //SI VIENE UN SOLO ID
            else
            {
                var datatype = actual.ChildNodes[1].ChildNodes[0].Token.Text;
                elementos_her.Add(datatype);
                lista_actual = ASSIGNATION_VARIABLE(actual.ChildNodes[2], lista_actual, elementos_her);
            }
            return lista_actual;
        }
        public LinkedList<Instruction> VAR_DECLARATION(ParseTreeNode actual, LinkedList<Instruction> lista_actual, ArrayList elementos_her)
        {
            if (actual.ChildNodes.Count > 0)
            {
                var identifier = actual.ChildNodes[0].Token.Text;
                elementos_her.Add(identifier);
                lista_actual = DECLARATION_BODY(actual.ChildNodes[1], lista_actual, elementos_her);
                lista_actual = VAR_DECLARATION(actual.ChildNodes[2], lista_actual, elementos_her);

            }

            return lista_actual;
        }
        public LinkedList<Instruction> CONST_DECLARATION(ParseTreeNode actual, LinkedList<Instruction> lista_actual, ArrayList elementos_her)
        {
            /*
             *  CONST_DECLARATION.Rule = IDENTIFIER + EQUALS + LOGIC_EXPRESION + PUNTO_COMA + CONST_DECLARATION
                | Empty
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                var identifier = actual.ChildNodes[0].Token.Text;
                lista_actual.AddLast(new Declaration(identifier, expresion(actual.ChildNodes[2]),0,0,true));
                lista_actual = CONST_DECLARATION(actual.ChildNodes[4], lista_actual, elementos_her);
            }
            return lista_actual;
        }

        public LinkedList<Instruction> ASSIGNATION_VARIABLE(ParseTreeNode actual, LinkedList<Instruction> lista_actual, ArrayList elementos_her)
        {
            //VAR A: TIPO = EXP;
            if (actual.ChildNodes.Count > 0)
            {
                var exp = expresion(actual.ChildNodes[1]);
                lista_actual.AddLast(new Declaration(elementos_her[0].ToString(), elementos_her[1].ToString(), exp, 0,0));
                elementos_her.Clear();
            } 
            // VAR A:TIPO;
            else
            {
                lista_actual.AddLast(GetDeclarationValue(elementos_her[0].ToString(), elementos_her[1].ToString()));
                elementos_her.Clear();
            }
            return lista_actual;
        }

        public ArrayList MORE_ID_DECLARATION(ParseTreeNode actual, ArrayList elementos_her)
        {
            if (actual.ChildNodes.Count > 0)
            {
                var identifier = actual.ChildNodes[1].Token.Text;
                elementos_her.Add(identifier);
                elementos_her = MORE_ID_DECLARATION(actual.ChildNodes[2], elementos_her);
            }
            return elementos_her;
        }
        public Declaration GetDeclarationValue(string identifier, string datatype)
        {
            if (datatype.Equals("integer"))
            {
                return new Declaration(identifier.ToString(), datatype, new Literal(0, 0), 0, 0);
            }
            else if (datatype.Equals("real"))
            {
                return new Declaration(identifier.ToString(), datatype, new Literal(0, 4), 0, 0);
            }
            else if (datatype.Equals("string"))
            {
                return new Declaration(identifier.ToString(), datatype, new Literal("", 2), 0, 0);
            }
            else if (datatype.Equals("boolean"))
            {
                return new Declaration(identifier.ToString(), datatype, new Literal(false, 3), 0, 0);
            }
            return null;
        }
        #endregion

        #region FUNCIONES NATIVAS 


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

        #endregion

        #region SENTENCIAS DE CONTROL

        #region IF

        public IF IFTHEN(ParseTreeNode actual)
        {
            IF ifs = new IF();
            var LOGIC_EXPRESION = expresion(actual.ChildNodes[1]);

            var SENTENCES = IF_SENTENCE(actual.ChildNodes[3]);
            var ELSE = ELIF(actual.ChildNodes[4]);


            return new IF(LOGIC_EXPRESION, SENTENCES, ELSE);
        }

        public Sentence IF_SENTENCE(ParseTreeNode actual)
        {
            Sentence sentence = new Sentence(); 
            if (actual.ChildNodes.Count > 0)
            {
                var lista_instrucciones = INSTRUCTIONS_BODY(actual.ChildNodes[0]);
                sentence = new Sentence(lista_instrucciones);
            }

            return sentence;
        }
        public Sentence ELIF(ParseTreeNode actual)
        {
            Sentence sentence = new Sentence();
            
            if (actual.ChildNodes.Count > 0)
            {
                LinkedList<Instruction> lista_instrucciones = new LinkedList<Instruction>();
                // ELSE 
                if (actual.ChildNodes[1].Term.ToString().Equals("IF_SENTENCE"))
                {
                    lista_instrucciones = INSTRUCTIONS_BODY(actual.ChildNodes[1].ChildNodes[0]);

                } 
                 // ELSE IF
                else
                {
                    var ifs = IFTHEN(actual.ChildNodes[1]);
                    lista_instrucciones.AddLast(ifs);
                }

                sentence = new Sentence(lista_instrucciones);

            }
            return sentence;
        }
        #endregion

        #region WHILE


        #endregion

        #endregion




        #region EXPRESIONES
        public Expression expresion(ParseTreeNode actual)
        {
            
            if (actual.ChildNodes.Count == 3)
            {
                // VERIFICA SI EL LADO IZQUIERDO ES PARENTESIS OSEA ( EXPRESION )
                var izq = actual.ChildNodes.ElementAt(0);


                if (izq.Term.ToString().ToLower().Contains("tk"))
                {
                    return expresion(actual.ChildNodes.ElementAt(1));
                }
                //SI NO LO ES, ES PORQUE VIENE UNA EXPRESION COMUN
                else
                {
                    var simb = actual.ChildNodes.ElementAt(1).Token.Text;
                    var opcion = 0;


                    if (simb.Equals("+") || simb.Equals("-") || simb.Equals("*") || simb.Equals("/") || simb.Equals("%"))
                    {
                        opcion = 1;
                    }
                    else if (simb.Equals("<") || simb.Equals("<=") || simb.Equals(">") || simb.Equals(">="))
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
                }

                
            }
            else if (actual.ChildNodes.Count == 2)
            {
                var simb = actual.ChildNodes.ElementAt(0).Token.Text;
                var iz = GetLiteral(actual.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0));

                return new Logical(iz, null, simb, 0, 0);

            }
            else
            {
                 return GetLiteral(actual.ChildNodes.ElementAt(0));
            }

        }

        public Expression GetLiteral(ParseTreeNode node)
        {
            Expression result = null;

            if (node.Term.ToString().ToString().Equals("NUMERO"))
            {
                result = new Literal(node.Token.Value, 1);
            }
            else if (node.Term.ToString().Equals("CADENA"))
            {
                result = new Literal(node.Token.Value, 2);
            }
            else if (node.Term.ToString().Equals("RESERV_TRUE") || node.Term.ToString().Equals("RESERV_FALSE"))
            {
                result = new Literal(node.Token.Value, 3);
            }
            else if (node.Term.ToString().Equals("REAL"))
            {
                result = new Literal(node.Token.Value, 4);
            }
            else if (node.Term.ToString().Equals("TYPE"))
            {
                result = new Literal(node.Token.Value, 5);
            }
            else if (node.Term.ToString().Equals("ARRAY"))
            {
                result = new Literal(node.Token.Value, 6);
            }
            else if (node.Term.ToString().Equals("IDENTIFIER"))
            {
                result = new Access(node.Token.Value.ToString());
            }

            return result;
        }
        #endregion


        public void ejecutar(LinkedList<Instruction> actual, LinkedList<Instruction> lista_declaraciones)
        {
            //GUARDAR VARIABLES

            foreach (var item in lista_declaraciones)
            {
                try
                {
                    var result = item.Execute(general);
                    if (result == null)
                    {
                        ConsolaController.Instance.Add("");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            foreach (var item in actual)
            {
                try
                {
                   var result = item.Execute(general);
                    if (result == null)
                    {
                        ConsolaController.Instance.Add("");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                
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
