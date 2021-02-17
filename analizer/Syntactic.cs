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
            ParseTree tree = parser.Parse(cadena);
            ParseTreeNode root = tree.Root;
            

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
                return;
            }
            if (root == null)
            {
                return;
            }
            //SE MANDA A GRAFICAR
            GraphController.Instance.getGraph(root);


            var program_body = root.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3);

            lista_declaraciones = LIST_DECLARATIONS(program_body.ChildNodes.ElementAt(0), lista_declaraciones, elemetos_heredados);


            LinkedList<Instruction> listaInstrucciones = INSTRUCTIONS_BODY(program_body.ChildNodes.ElementAt(2));
            ejecutar(listaInstrucciones, lista_declaraciones);
            
        }


        #region EJECUCION


        public void ejecutar(LinkedList<Instruction> actual, LinkedList<Instruction> lista_declaraciones)
        {
            //GUARDAR VARIABLES

            var error_variable = false;


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
                foreach (var item in actual)
                {
                    try
                    {
                        var result = item.Execute(general);
                        if (result == null)
                        {
                            break;
                        }
                    }
                    catch (Exception)
                    {

                        break;
                    }

                }
            }

        }

        

        #endregion


        //EMPIEZA LA EJECUCION


        public LinkedList<Instruction> INSTRUCTIONS_BODY(ParseTreeNode actual)
        {
            /*INSTRUCTIONS_BODY.Rule 
                = RESERV_BEGIN + INSTRUCTIONS + RESERV_END  + PUNTO
                ;*/
            var begind = actual.ChildNodes.ElementAt(0);

            LinkedList<Instruction> lista_instruciones = ISTRUCCIONES(actual.ChildNodes.ElementAt(1));


            var end = actual.ChildNodes.ElementAt(2);
            return lista_instruciones;
        }

        public LinkedList<Instruction> ISTRUCCIONES(ParseTreeNode actual)
        {

            LinkedList<Instruction> listaInstrucciones = new LinkedList<Instruction>();

            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {
                var inst = INSTRUCCION(nodo.ChildNodes[0]);
                listaInstrucciones.AddLast(inst);
            }
            return listaInstrucciones;
        }
        public Instruction INSTRUCCION(ParseTreeNode actual)
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
                IF _ifs = IFTHEN(actual);
                return _ifs;
            }
            else if (actual.Term.ToString().Equals("WHILE"))
            {
                While _while = WHILE(actual);
                return _while;
            }
            else if (actual.Term.ToString().Equals("VAR_ASSIGNATE"))
            {
                Assignation _assignation = VAR_ASSIGNATE(actual);
                return _assignation;
            }
            else if (actual.Term.ToString().Equals("REPEAT_UNTIL"))
            {
                Repeat _repeat = REPEAT_UNTIL(actual);
                return _repeat;
            }
            else if (actual.Term.ToString().Equals("FOR"))
            {
                FOR _for = SENCECIA_FOR(actual);
                return _for;
            }
            else if (actual.Term.ToString().Equals("SENTENCE_CASE"))
            {
                Switch _SW = SENTENCE_CASE(actual);
                return _SW;
            }

            return null;
        }


        #region DECLARACION Y ASIGNACION

        #region DECLARACION

        
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
                lista_actual.AddLast(new Declaration(elementos_her[0].ToString(), elementos_her[1].ToString(), exp, 0,0, true));
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
                return new Declaration(identifier.ToString(), datatype, new Literal(0, 1), 0, 0, false);
            }
            else if (datatype.Equals("real"))
            {
                return new Declaration(identifier.ToString(), datatype, new Literal(0, 4), 0, 0, false);
            }
            else if (datatype.Equals("string"))
            {
                return new Declaration(identifier.ToString(), datatype, new Literal("", 2), 0, 0, false);
            }
            else if (datatype.Equals("boolean"))
            {
                return new Declaration(identifier.ToString(), datatype, new Literal(false, 3), 0, 0, false);
            }
            return null;
        }

        #endregion

        #region ASIGNACION
        public Assignation VAR_ASSIGNATE(ParseTreeNode actual)
        {
            //VAR_ASSIGNATE.Rule = IDENTIFIER + DOS_PUNTOS + EQUALS + LOGIC_EXPRESION + PUNTO_COMA;
            var identifier = actual.ChildNodes[0].Token.Text;
            var exp = expresion(actual.ChildNodes[3]);
            return new Assignation(identifier, exp);
        }

        #endregion

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
        public While WHILE(ParseTreeNode actual)
        {
            //WHILE.Rule = RESERV_WHILE + LOGIC_EXPRESION + RESERV_DO + INSTRUCTIONS_BODY;

            var condition = expresion(actual.ChildNodes[1]);

            LinkedList<Instruction> lista_instrucciones = INSTRUCTIONS_BODY(actual.ChildNodes[3]);

            return new While(condition, new Sentence(lista_instrucciones));

        }

        #endregion

        #region REPEAT UTIL
        public Repeat REPEAT_UNTIL(ParseTreeNode actual)
        {
            //REPEAT_UNTIL.Rule = RESERV_REPEAT + INSTRUCTIONS + RESERV_UNTIL + LOGIC_EXPRESION + PUNTO_COMA;
            
            //SE OBTIENEN LOS VALORES
            var instrucciones = actual.ChildNodes[1];
            var condicion = expresion(actual.ChildNodes[3]);
            
            //OBTENGO LA LISTA DE INSTRUCCIONES
            LinkedList<Instruction> lista_instrucciones = ISTRUCCIONES(instrucciones);
            

            //RETORNO EL NUEVO REPEAT-UTIL
            return new Repeat(condicion, new Sentence(lista_instrucciones));

        }
        #endregion

        #region FOR
        public FOR SENCECIA_FOR(ParseTreeNode actual)
        {
            /*
             FOR.Rule
                = RESERV_FOR + IDENTIFIER + DOS_PUNTOS + EQUALS + LOGIC_EXPRESION + TODOWN + LOGIC_EXPRESION
                    + RESERV_DO
                        + INSTRUCTIONS_BODY //+ PUNTO_COMA
                ;

            TODOWN.Rule 
                = RESERV_TO
                | RESERV_DOWN + RESERV_TO
                ;
             */
            var ident = actual.ChildNodes[1].Token.Text;
            var inicio = expresion(actual.ChildNodes[4]);
            var direccion = actual.ChildNodes[5].ChildNodes.ElementAt(0).Token.Text;
            var fin = expresion(actual.ChildNodes[6]);
            var lista_instrucciones = INSTRUCTIONS_BODY(actual.ChildNodes[8]);
            return new FOR(ident,inicio, fin, new Sentence(lista_instrucciones), direccion);



        }
        #endregion

        #region CASE
        public Switch SENTENCE_CASE(ParseTreeNode actual)
        {
            /*
             *  SENTENCE_CASE.Rule = RESERV_CASE  + LOGIC_EXPRESION + RESERV_OF + CASES + CASE_ELSE + RESERV_END + PUNTO_COMA;

          

            CASE_ELSE.Rule = RESERV_ELSE + INSTRUCTIONS
                | Empty
                ;
             */

            var condicion = expresion(actual.ChildNodes[1]);
            
            var lista_cases = CASES(actual.ChildNodes[3]);

            var else_case = CASE_ELSE(actual.ChildNodes[4]);
            return new Switch(condicion, lista_cases, else_case);
        }

        public ArrayList CASES(ParseTreeNode actual)
        {
            /*
               CASES.Rule = MakePlusRule(CASES, CASE)
                | CASE
                ;
             */
            ArrayList lista_cases = new ArrayList();
            if (actual.ChildNodes.Count> 0)
            {
                foreach (var item in actual.ChildNodes)
                {
                    lista_cases.Add(CaseSing(item));
                }
            }

            return lista_cases;
        }

        public Case CaseSing(ParseTreeNode actual)
        {
            /* CASE.Rule = LOGIC_EXPRESION + DOS_PUNTOS + INSTRUCTIONS;*/
            var condicion = expresion(actual.ChildNodes[0]);
            var lista_instrucciones = ISTRUCCIONES(actual.ChildNodes[2]);

            return new Case(condicion, new Sentence(lista_instrucciones));
            
        }

        public Case CASE_ELSE(ParseTreeNode actual)
        {
            /*
             CASE_ELSE.Rule = RESERV_ELSE + INSTRUCTIONS
                | Empty
                ;
             */
            Case _case = new Case();
            if (actual.ChildNodes.Count > 0)
            {
                var lista_declaraciones = ISTRUCCIONES(actual.ChildNodes[1]);
                _case = new Case(new Sentence(lista_declaraciones));
            }

            return _case;
        }
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
                    else if (simb.Equals("<") || simb.Equals("<=") || simb.Equals(">") || simb.Equals(">=") || simb.Equals("=") || simb.Equals("<>"))
                    {
                        opcion = 2;
                    }
                    else if (simb.Equals("and") || simb.Equals("or") || simb.Equals("not"))
                    {
                        opcion = 3;
                    }
                    var iz = expresion(actual.ChildNodes.ElementAt(0));
                    var der = expresion(actual.ChildNodes.ElementAt(2));

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


                if (simb.Equals("-"))
                {
                    var iz = expresion(actual.ChildNodes[1]);
                    return new Arithmetic(iz, new Literal("-1", 1), "*");
                } else
                {
                    var iz = GetLiteral(actual.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0));
                    return new Logical(iz, null, simb, 0, 0);
                }



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



    

    }

}
