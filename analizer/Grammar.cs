using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace CompiPascal.analizer
{
    class Grammar: Irony.Parsing.Grammar
    {

        public Grammar()
        {
            #region Lexical structure


            //COMENTARIOS
            CommentTerminal LINE_COMMENT = new CommentTerminal("LINE_COMMENT", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");
            CommentTerminal MULTI_LINE_COMMENT = new CommentTerminal("MULTI_LINE_COMMENT", "(*", "*)");
            CommentTerminal MULTI_LINE_COMMENT_LLAVE = new CommentTerminal("MULTI_LINE_COMMENT_LLAVE", "{", "}");

            NonGrammarTerminals.Add(LINE_COMMENT);
            NonGrammarTerminals.Add(MULTI_LINE_COMMENT);
            NonGrammarTerminals.Add(MULTI_LINE_COMMENT_LLAVE);


            #endregion



            #region ER
            var REAL = new RegexBasedTerminal("REAL", "[0-9]+[.][0-9]+");
            var NUMERO = new NumberLiteral("NUMERO");
            var IDENTIFIER = new IdentifierTerminal("IDENTIFIER", "[_a-zA-Z][_a-zA-Z0-9]");
            //var CADENA = new RegexBasedTerminal("CADENA", "\'[_a-zA-Z][_a-zA-Z0-9]\'");
            var CADENA = new StringLiteral("CADENA", "\'");
            #endregion

            #region Terminales
            var PUNTO_COMA = ToTerm(";", "TK_PUNTO_COMA");
            var PUNTO = ToTerm(".", "TK_PUNTO");
            var COMA = ToTerm(",", "TK_COMA");
            var DOS_PUNTOS = ToTerm(":", "TK_DOS_PUNTOS");
            var PAR_IZQ = ToTerm("(", "TK_PAR_IZQ");
            var PAR_DER = ToTerm(")", "TK_PAR_DER");
            var COR_IZQ = ToTerm("[", "TK_COR_IZQ");
            var COR_DER = ToTerm("]", "TK_COR_DER");
            //Aritmethic    
            var PLUS = ToTerm("+", "TK_PLUS");
            var MIN = ToTerm("-", "TK_MIN");
            var POR = ToTerm("*", "TK_POR");
            var DIVI = ToTerm("/", "TK_DIVI");
            var MODULE = ToTerm("%", "TK_MODULE");
            //Logic
            var AND = ToTerm("and", "TK_AND");
            var OR = ToTerm("or", "TK_OR");
            var NOT = ToTerm("not", "TK_NOT");
            //Relational
            var HIGHER = ToTerm(">", "TK_HIGHER");
            var LESS = ToTerm("<", "TK_LESS");
            var HIGHER_EQUAL = ToTerm(">=", "TK_HIGHER_EQUAL");
            var LESS_EQUAL = ToTerm("<=", "TK_LESS_EQUAL");
            var EQUALS = ToTerm("=", "TK_EQUALS");
            var DISCTINCT = ToTerm("<>", "TK_DISCTINCT");
            //Reservadas
            
            var RESERV_INT = ToTerm("integer", "RESERV_INT");
            var RESERV_STR = ToTerm("string", "RESERV_STR");
            var RESERV_REAL = ToTerm("real", "RESERV_REAL");
            var RESERV_BOL = ToTerm("boolean", "RESERV_BOL");
            var RESERV_VOID = ToTerm("void", "RESERV_VOID");
            var RESERV_TYPE = ToTerm("type", "RESERV_TYPE");
            var RESERV_OBJ = ToTerm("object", "RESERV_OBJ");
            var RESERV_PROGRAM = ToTerm("program", "RESERV_PROGRAM");
            var RESERV_VAR = ToTerm("var", "RESERV_VAR");
            var RESERV_BEGIN = ToTerm("begin", "RESERV_BEGIN");
            var RESERV_END = ToTerm("end", "RESERV_END");
            var RESERV_CONST = ToTerm("const", "RESERV_CONST");
            var RESERV_TRUE = ToTerm("true", "RESERV_TRUE");
            var RESERV_FALSE = ToTerm("false", "RESERV_FALSE");
            var RESERV_ARRAY = ToTerm("array", "RESERV_ARRAY");
            var RESERV_OF = ToTerm("of", "RESERV_OF");

            #region IF TERMINALES
            var RESERV_IF = ToTerm("if", "RESERV_IF");
            var RESERV_THEN = ToTerm("then", "RESERV_THEN");
            var RESERV_ELSE = ToTerm("else", "RESERV_ELSE");
            #endregion

            #region CASE TERMINALES
            var RESERV_CASE = ToTerm("case", "RESERV_CASE");
            #endregion

            #region WHILE TERMINALES
            var RESERV_WHILE = ToTerm("while", "RESERV_WHILE");
            var RESERV_DO = ToTerm("do", "RESERV_DO");
            #endregion

            #region REPEAT TERMINALES
            var RESERV_REPEAT = ToTerm("repeat", "RESERV_REPEAT");
            var RESERV_UNTIL = ToTerm("until", "RESERV_UNTIL");
            #endregion

            #region FOR TERMINALES
            var RESERV_FOR = ToTerm("for", "RESERV_FOR");
            var RESERV_TO = ToTerm("to", "RESERV_TO");
            var RESERV_DOWN = ToTerm("downto", "RESERV_DOWN");
            var RESERV_BREAK = ToTerm("break", "RESERV_BREAK");
            var RESERV_CONTINUE = ToTerm("continue", "RESERV_CONTINUE");

            #endregion

            #region FUNCTION Y PROCEDURE TERMINALES
            var RESERV_FUNCTION = ToTerm("function", "RESERV_FUNCTION");
            var RESERV_PROCEDURE = ToTerm("procedure", "RESERV_PROCEDURE");


            #endregion

            #region FUNCIONES NATIVAS TERMINALES
            var RESERV_WRITE = ToTerm("write", "RESERV_WRITE");
            var RESERV_WRITEN = ToTerm("writeln", "RESERV_WRITEN");
            var RESERV_EXIT = ToTerm("exit", "RESERV_EXIT");
            var RESERV_GRAF = ToTerm("graficar_ts", "RESERV_GRAFICAR");

            #endregion


            RegisterOperators(1, Associativity.Left, PLUS, MIN);
            RegisterOperators(2, Associativity.Left, POR, DIVI);
            RegisterOperators(3, Associativity.Left, MODULE);
            RegisterOperators(4, Associativity.Left, HIGHER_EQUAL, LESS_EQUAL, LESS, HIGHER);
            RegisterOperators(5, Associativity.Left, EQUALS, DISCTINCT);
            RegisterOperators(6, Associativity.Left, AND, OR, NOT);

            #endregion

            #region No Terminales
            NonTerminal init = new NonTerminal("init");
            NonTerminal INSTRUCTION = new NonTerminal("INSTRUCTION");
            NonTerminal INSTRUCTIONS = new NonTerminal("INSTRUCTIONS");
            NonTerminal INSTRUCTIONS_BODY = new NonTerminal("INSTRUCTIONS_BODY");
            NonTerminal PROGRAM_BODY = new NonTerminal("PROGRAM_BODY", "PROGRAM_BODY");




            NonTerminal LOGIC_EXPRESION = new NonTerminal("LOGIC_EXPRESION", "LOGIC_EXPRESION");
            /*NonTerminal LOGIC_EXPRESION_P = new NonTerminal("LOGIC_EXPRESION_P", "LOGIC_EXPRESION_P");
            NonTerminal RELATIONAL_EXPRESION = new NonTerminal("RELATIONAL_EXPRESION", "RELATIONAL_EXPRESION");
            NonTerminal RELATIONAL_EXPRESION_P = new NonTerminal("RELATIONAL_EXPRESION_P", "RELATIONAL_EXPRESION_P");
            NonTerminal EXPRESSION = new NonTerminal("EXPRESSION");
            NonTerminal EXPRESSION_P = new NonTerminal("EXPRESSION_P");
            NonTerminal TERM = new NonTerminal("TERM");
            NonTerminal TERM_P = new NonTerminal("TERM_P");
            NonTerminal FACTOR = new NonTerminal("FACTOR");*/


            NonTerminal start = new NonTerminal("start");
            NonTerminal DATA_TYPE = new NonTerminal("DATA_TYPE", "DATA_TYPE");


            #region VAR Y CONST 
            NonTerminal DECLARATION_LIST = new NonTerminal("DECLARATION_LIST", "DECLARATION_LIST");
            NonTerminal VAR_DECLARATION = new NonTerminal("VAR_DECLARATION", "VAR_DECLARATION");
            NonTerminal CONST_DECLARATION = new NonTerminal("CONST_DECLARATION", "CONST_DECLARATION");

            NonTerminal DECLARATION = new NonTerminal("DECLARATION", "DECLARATION");
            NonTerminal DECLARATION_BODY = new NonTerminal("DECLARATION_BODY", "DECLARATION_BODY");
            NonTerminal MORE_ID = new NonTerminal("MORE_ID", "MORE_ID");

            NonTerminal ASSIGNATION = new NonTerminal("ASSIGNATION", "ASSIGNATION");
            NonTerminal VAR_ASSIGNATE = new NonTerminal("VAR_ASSIGNATE", "VAR_ASSIGNATE");


            #endregion

            //NonTerminal ASSIGNATION_P = new NonTerminal("ASSIGNATION_P", "ASSIGNATION_P");
            //NonTerminal MORE_DECLARATION = new NonTerminal("MORE_DECLARATION", "MORE_DECLARATION");
            //NonTerminal MORE_DECLARATION_P = new NonTerminal("MORE_DECLARATION_P", "MORE_DECLARATION_P");
            //NonTerminal COMPARE = new NonTerminal("COMPARE", "COMPARE");
            //NonTerminal DIFFERENT = new NonTerminal("DIFFERENT", "DIFFERENT");


            NonTerminal TYPE = new NonTerminal("TYPE", "TYPE");
            NonTerminal TYPE_P = new NonTerminal("TYPE_P", "TYPE_P");
            NonTerminal ARRAY = new NonTerminal("ARRAY", "ARRAY");
            NonTerminal OBJECT = new NonTerminal("OBJECT", "OBJECT");

            #region IF-THEN NO TERMINALES
            NonTerminal IFTHEN = new NonTerminal("IF-THEN", "IF-THEN");
            NonTerminal IF_SENTENCE = new NonTerminal("IF_SENTENCE", "IF_SENTENCE");
            NonTerminal ELIF = new NonTerminal("ELIF", "ELIF");
            #endregion

            #region CASE NO TERMINALES
            NonTerminal SENTENCE_CASE = new NonTerminal("SENTENCE_CASE", "SENTENCE_CASE");
            NonTerminal CASE_ELSE = new NonTerminal("CASE_ELSE", "CASE_ELSE");
            NonTerminal CASES = new NonTerminal("CASES", "CASES");
            NonTerminal CASE = new NonTerminal("CASE", "CASE");

            #endregion

            #region WHILE DO
            NonTerminal WHILE = new NonTerminal("WHILE", "WHILE");
            #endregion

            #region REPEAT UNTIL
            NonTerminal REPEAT_UNTIL = new NonTerminal("REPEAT_UNTIL", "REPEAT_UNTIL");
            #endregion

            #region FOR
            NonTerminal FOR = new NonTerminal("FOR", "FOR");
            NonTerminal TODOWN = new NonTerminal("TODOWN", "TODOWN");
            NonTerminal TRANSFER = new NonTerminal("TRANSFER", "TRANSFER");

            #endregion

           

            #region  Funciones nativas NO TERMINALES
            NonTerminal WRITE = new NonTerminal("WRITE", "WRITE");
            NonTerminal WRHITE_PARAMETER = new NonTerminal("WRHITE_PARAMETER", "WRHITE_PARAMETER");
            NonTerminal MORE_WRHITE_PARAMETER = new NonTerminal("WRHITE_PARAMETER", "WRHITE_PARAMETER");


            NonTerminal EXIT = new NonTerminal("EXIT", "EXIT");
            NonTerminal GRAFICAR = new NonTerminal("GRAFICAR", "GRAFICAR");

            #endregion

            #region FUNCIONS NO TERMINALES
            //NonTerminal FUNCTION = new NonTerminal("FUNCTION", "FUNCTION");
            NonTerminal FUNCTION_LIST = new NonTerminal("FUNCTION_LIST", "FUNCTION_LIST");
            //NonTerminal PARAMETERS = new NonTerminal("PARAMETERS", "PARAMETERS");
            //NonTerminal ARGUMENTS = new NonTerminal("ARGUMENTS", "ARGUMENTS");
            //NonTerminal REFERENCIA_VALOR = new NonTerminal("REFERENCIA_VALOR", "REFERENCIA_VALOR");
            #endregion

            #endregion

            #region Gramatica
            init.Rule = start;

            start.Rule = RESERV_PROGRAM + IDENTIFIER + PUNTO_COMA + PROGRAM_BODY;

            PROGRAM_BODY.Rule
                = DECLARATION_LIST
                + FUNCTION_LIST
                + INSTRUCTIONS_BODY + PUNTO;

            INSTRUCTIONS_BODY.Rule 
                = RESERV_BEGIN + INSTRUCTIONS + RESERV_END
                ;


            INSTRUCTIONS.Rule = MakePlusRule(INSTRUCTIONS, INSTRUCTION);

            INSTRUCTION.Rule
                = VAR_ASSIGNATE
                | IFTHEN
                | SENTENCE_CASE
                | WHILE
                | REPEAT_UNTIL
                | FOR
                | TRANSFER
                //| FUNCTION
                | WRITE
                ;

            INSTRUCTION.ErrorRule
                = SyntaxError + PUNTO_COMA
                | SyntaxError + RESERV_END
                ;


            #region DECLARACION & ASIGNACION

            DECLARATION_LIST.Rule
               = RESERV_VAR + IDENTIFIER + DECLARATION_BODY + VAR_DECLARATION + DECLARATION_LIST
               | RESERV_CONST + IDENTIFIER + EQUALS + LOGIC_EXPRESION + PUNTO_COMA + CONST_DECLARATION + DECLARATION_LIST
               | Empty
               ;

            DECLARATION_LIST.ErrorRule
                = SyntaxError + PUNTO_COMA;


            VAR_DECLARATION.Rule = IDENTIFIER + DECLARATION_BODY + VAR_DECLARATION
                | Empty
                ;

            CONST_DECLARATION.Rule = IDENTIFIER + EQUALS + LOGIC_EXPRESION + PUNTO_COMA + CONST_DECLARATION
                | Empty
                ;

            DECLARATION_BODY.Rule
                = DOS_PUNTOS + DATA_TYPE + ASSIGNATION + PUNTO_COMA 
                | COMA + IDENTIFIER + MORE_ID + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA
                ;

            ASSIGNATION.Rule
                = EQUALS + LOGIC_EXPRESION
                | Empty;
                ;

            MORE_ID.Rule = COMA + IDENTIFIER + MORE_ID
                | Empty
                ;

          

            VAR_ASSIGNATE.Rule = IDENTIFIER + DOS_PUNTOS + EQUALS + LOGIC_EXPRESION + PUNTO_COMA;


            DATA_TYPE.Rule = RESERV_REAL
                | RESERV_STR
                | RESERV_TYPE
                | RESERV_INT
                | RESERV_BOL
                | IDENTIFIER
                ;

            #endregion






            #region TYPES Y ARRAY
            TYPE.Rule = RESERV_TYPE + IDENTIFIER + EQUALS + TYPE_P;

            TYPE_P.Rule = OBJECT
                | ARRAY
                ;

            OBJECT.Rule = RESERV_OBJ + DECLARATION + RESERV_END;

            ARRAY.Rule = RESERV_ARRAY + COR_IZQ + LOGIC_EXPRESION + COR_DER + PUNTO + PUNTO + LOGIC_EXPRESION + PAR_DER + RESERV_OF + DATA_TYPE;
            #endregion

            #region EXPRESSION

            LOGIC_EXPRESION.Rule
                = LOGIC_EXPRESION + PLUS + LOGIC_EXPRESION
                | LOGIC_EXPRESION + MIN + LOGIC_EXPRESION
                | LOGIC_EXPRESION + POR + LOGIC_EXPRESION
                | LOGIC_EXPRESION + DIVI + LOGIC_EXPRESION
                | LOGIC_EXPRESION + MODULE + LOGIC_EXPRESION
                | LOGIC_EXPRESION + LESS + LOGIC_EXPRESION
                | LOGIC_EXPRESION + HIGHER + LOGIC_EXPRESION
                | LOGIC_EXPRESION + LESS_EQUAL + LOGIC_EXPRESION
                | LOGIC_EXPRESION + HIGHER_EQUAL + LOGIC_EXPRESION
                | LOGIC_EXPRESION + EQUALS + LOGIC_EXPRESION
                | LOGIC_EXPRESION + DISCTINCT + LOGIC_EXPRESION
                | LOGIC_EXPRESION + AND + LOGIC_EXPRESION
                | LOGIC_EXPRESION + OR + LOGIC_EXPRESION
                | NOT + LOGIC_EXPRESION
                | MIN + LOGIC_EXPRESION
                | IDENTIFIER
                | NUMERO
                | CADENA
                | REAL
                | RESERV_TRUE
                | RESERV_FALSE
                | PAR_IZQ + LOGIC_EXPRESION + PAR_DER

                ;

            /*LOGIC_EXPRESION.Rule = RELATIONAL_EXPRESION + LOGIC_EXPRESION_P;

            LOGIC_EXPRESION_P.Rule = AND + RELATIONAL_EXPRESION + LOGIC_EXPRESION_P
                | OR + RELATIONAL_EXPRESION + LOGIC_EXPRESION_P
                | NOT + RELATIONAL_EXPRESION + LOGIC_EXPRESION_P
                | Empty
                ;

            RELATIONAL_EXPRESION.Rule = EXPRESSION + RELATIONAL_EXPRESION_P;

            RELATIONAL_EXPRESION_P.Rule = HIGHER + COMPARE
                | LESS + DIFFERENT
                | EQUALS + EXPRESSION
                | Empty
                ;

            COMPARE.Rule = EXPRESSION + RELATIONAL_EXPRESION_P
                | EQUALS + EXPRESSION + RELATIONAL_EXPRESION_P
                ;

            DIFFERENT.Rule = HIGHER + EXPRESSION + RELATIONAL_EXPRESION_P
                | COMPARE;


            EXPRESSION.Rule = TERM + EXPRESSION_P;

            EXPRESSION_P.Rule = PLUS + TERM + EXPRESSION_P
                | MIN + TERM + EXPRESSION_P
                | Empty
                ;

            TERM.Rule = FACTOR + TERM_P;

            TERM_P.Rule = POR + FACTOR + TERM_P
                | DIVI + FACTOR + TERM_P
                | MODULE + FACTOR + TERM_P
                | Empty
                ;

            FACTOR.Rule = 
                 CADENA
                | IDENTIFIER
                | REAL
                | NUMERO
                | RESERV_TRUE
                | RESERV_FALSE
                | PAR_IZQ + LOGIC_EXPRESION + PAR_DER;*/
            #endregion







            #endregion

            #region SENTENCIAS DE CONTROL

            #region IF-THEN
            IFTHEN.Rule
                = RESERV_IF + LOGIC_EXPRESION
                    + RESERV_THEN
                        + IF_SENTENCE
                    + ELIF;

            IF_SENTENCE.Rule = INSTRUCTIONS_BODY 
                | Empty
                ;

            ELIF.Rule
                = RESERV_ELSE + IF_SENTENCE //+ PUNTO_COMA
                | RESERV_ELSE + IFTHEN
                | Empty
                ;


            #endregion

            #region CASE
            SENTENCE_CASE.Rule = RESERV_CASE  + LOGIC_EXPRESION + RESERV_OF + CASES + CASE_ELSE + RESERV_END + PUNTO_COMA;

            CASES.Rule = MakePlusRule(CASES, CASE)
                | CASE
                ;
            CASE.Rule = LOGIC_EXPRESION + DOS_PUNTOS + INSTRUCTIONS;


            CASE_ELSE.Rule = RESERV_ELSE + INSTRUCTIONS
                | Empty
                ;
            #endregion

            #region WHILE DO
            WHILE.Rule = RESERV_WHILE + LOGIC_EXPRESION + RESERV_DO + INSTRUCTIONS_BODY + PUNTO_COMA;
            #endregion

            #region REPEAT UNTIL
            REPEAT_UNTIL.Rule = RESERV_REPEAT + INSTRUCTIONS + RESERV_UNTIL + LOGIC_EXPRESION + PUNTO_COMA;
            #endregion

            #region FOR
            FOR.Rule
                = RESERV_FOR + IDENTIFIER + DOS_PUNTOS + EQUALS + LOGIC_EXPRESION + TODOWN + LOGIC_EXPRESION
                    + RESERV_DO
                        + INSTRUCTIONS_BODY + PUNTO_COMA
                ;

            TODOWN.Rule 
                = RESERV_TO
                | RESERV_DOWN
                ;
            #endregion

            #endregion

            #region SENTENCIAS DE TRANSFERENCIA
            TRANSFER.Rule
               = RESERV_CONTINUE + PUNTO_COMA
               | RESERV_BREAK + PUNTO_COMA
               | Empty;
            #endregion

            #region FUNCIONES Y PROCEDIMIENTOS
            FUNCTION_LIST.Rule = Empty;
            #endregion

            #region FUNCIONES NATIVAS

            WRITE.Rule = RESERV_WRITE + PAR_IZQ + WRHITE_PARAMETER + PAR_DER + PUNTO_COMA
                | RESERV_WRITEN + PAR_IZQ + WRHITE_PARAMETER + PAR_DER + PUNTO_COMA
                ;
            
            WRHITE_PARAMETER.Rule
                = LOGIC_EXPRESION + MORE_WRHITE_PARAMETER
                | Empty
                ;
            MORE_WRHITE_PARAMETER.Rule
                = COMA + LOGIC_EXPRESION + MORE_WRHITE_PARAMETER
                | Empty
                ;

            EXIT.Rule = RESERV_EXIT;

            GRAFICAR.Rule = RESERV_GRAF;

            #endregion
            
            #region Preferencias
            this.Root = init;
            #endregion

        }


    }
}
