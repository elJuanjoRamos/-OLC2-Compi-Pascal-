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
            var CADENA = new RegexBasedTerminal("CADENA", "\'" + "[_a-zA-Z][_a-zA-Z0-9]" + "\'");

            #endregion

            #region Terminales
            var PUNTO_COMA = ToTerm(";", "leaf");
            var PUNTO = ToTerm(".", "leaf");
            var COMA = ToTerm(",", "leaf");
            var DOS_PUNTOS = ToTerm(":", "leaf");
            var PAR_IZQ = ToTerm("(", "leaf");
            var PAR_DER = ToTerm(")", "leaf");
            var COR_IZQ = ToTerm("[", "leaf");
            var COR_DER = ToTerm("]", "leaf");
            //Aritmethic    
            var PLUS = ToTerm("+", "leaf");
            var MIN = ToTerm("-", "leaf");
            var POR = ToTerm("*", "leaf");
            var DIVI = ToTerm("/", "leaf");
            var MODULE = ToTerm("%", "leaf");
            //Logic
            var AND = ToTerm("and", "leaf");
            var OR = ToTerm("or", "leaf");
            var NOT = ToTerm("not", "leaf");
            //Relational
            var HIGHER = ToTerm(">", "leaf");
            var LESS = ToTerm("<", "leaf");
            var HIGHER_EQUAL = ToTerm(">=", "leaf");
            var LESS_EQUAL = ToTerm("<=", "leaf");
            var EQUALS = ToTerm("=", "leaf");
            var DISCTINCT = ToTerm("<>", "leaf");
            //Reservadas
            var RESERV_INT = ToTerm("integer", "leaf");
            var RESERV_STR = ToTerm("string", "leaf");
            var RESERV_REAL = ToTerm("real", "leaf");
            var RESERV_BOL = ToTerm("boolean", "leaf");
            var RESERV_VOID = ToTerm("void", "leaf");
            var RESERV_TYPE = ToTerm("type", "leaf");
            var RESERV_OBJ = ToTerm("object", "leaf");
            var RESERV_PROGRAM = ToTerm("program", "leaf");
            var RESERV_VAR = ToTerm("var", "leaf");
            var RESERV_FUN = ToTerm("function", "leaf");
            var RESERV_PROC = ToTerm("procedure", "leaf");
            var RESERV_BEGIN = ToTerm("begin", "leaf");
            var RESERV_END = ToTerm("end", "leaf");
            var RESERV_CONST = ToTerm("const", "leaf");
            var RESERV_TRUE = ToTerm("true", "boolean");
            var RESERV_FALSE = ToTerm("false", "boolean");
            var RESERV_ARRAY = ToTerm("array", "leaf");
            var RESERV_OF = ToTerm("of", "leaf");

            #region IF TERMINALES
            var RESERV_IF = ToTerm("if", "leaf");
            var RESERV_THEN = ToTerm("then", "leaf");
            var RESERV_ELSE = ToTerm("else", "leaf");
            #endregion

            #region CASE TERMINALES
            var RESERV_CASE = ToTerm("case", "leaf");
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
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal instrucciones = new NonTerminal("instrucciones");

            NonTerminal LOGIC_EXPRESION = new NonTerminal("LOGIC_EXPRESION", "LOGIC_EXPRESION");
            NonTerminal LOGIC_EXPRESION_P = new NonTerminal("LOGIC_EXPRESION_P", "LOGIC_EXPRESION_P");
            NonTerminal RELATIONAL_EXPRESION = new NonTerminal("RELATIONAL_EXPRESION", "RELATIONAL_EXPRESION");
            NonTerminal RELATIONAL_EXPRESION_P = new NonTerminal("RELATIONAL_EXPRESION_P", "RELATIONAL_EXPRESION_P");
            NonTerminal EXPRESSION = new NonTerminal("EXPRESSION");
            NonTerminal EXPRESSION_P = new NonTerminal("EXPRESSION_P");
            NonTerminal TERM = new NonTerminal("TERM");
            NonTerminal TERM_P = new NonTerminal("TERM_P");
            NonTerminal FACTOR = new NonTerminal("FACTOR");


            NonTerminal start = new NonTerminal("start");
            NonTerminal DATA_TYPE = new NonTerminal("DATA_TYPE", "DATA_TYPE");


            NonTerminal DECLARATION = new NonTerminal("DECLARATION", "DECLARATION");
            NonTerminal DECLARATION_BODY = new NonTerminal("DECLARATION_BODY", "DECLARATION_BODY");
            NonTerminal MORE_ID = new NonTerminal("MORE_ID", "MORE_ID");

            NonTerminal ASSIGNATION = new NonTerminal("ASSIGNATION", "ASSIGNATION");
            //NonTerminal DECLARATION_LIST_P = new NonTerminal("DECLARATION_LIST_P", "DECLARATION_LIST_P");
            //NonTerminal ASSIGNATION_P = new NonTerminal("ASSIGNATION_P", "ASSIGNATION_P");
            //NonTerminal MORE_DECLARATION = new NonTerminal("MORE_DECLARATION", "MORE_DECLARATION");
            //NonTerminal MORE_DECLARATION_P = new NonTerminal("MORE_DECLARATION_P", "MORE_DECLARATION_P");
            //NonTerminal COMPARE = new NonTerminal("COMPARE", "COMPARE");
            //NonTerminal DIFFERENT = new NonTerminal("DIFFERENT", "DIFFERENT");
            NonTerminal VAR_ASSIGNATE = new NonTerminal("VAR_ASSIGNATE", "VAR_ASSIGNATE");


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

            #endregion
            NonTerminal SENTENCE_CASE = new NonTerminal("SENTENCE_CASE", "SENTENCE_CASE");
            NonTerminal CASE_ELSE = new NonTerminal("CASE_ELSE", "CASE_ELSE");
            NonTerminal CASES = new NonTerminal("CASES", "CASES");
            NonTerminal CASE = new NonTerminal("CASE", "CASE");

            #endregion

            #region Gramatica
            init.Rule = start;

            start.Rule = RESERV_PROGRAM + IDENTIFIER + instrucciones;

            instrucciones.Rule = MakePlusRule(instrucciones, instruccion);

            instruccion.Rule
                = DECLARATION
                | VAR_ASSIGNATE
                | IFTHEN
                | SENTENCE_CASE
                ;



            #region DECLARACION & ASIGNACION

            DECLARATION.Rule
                = RESERV_VAR + IDENTIFIER + DECLARATION_BODY
                | RESERV_CONST + IDENTIFIER + EQUALS + LOGIC_EXPRESION + PUNTO_COMA
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

            /*DECLARATION.Rule = RESERV_VAR + DECLARATION_LIST
                                | RESERV_CONST + IDENTIFIER + EQUALS + LOGIC_EXPRESION + PUNTO_COMA
                                ;


            DECLARATION_LIST.Rule = IDENTIFIER + ASSIGNATION;

            DECLARATION_LIST_P.Rule = DECLARATION_LIST
                | Empty
                ;

            ASSIGNATION.Rule = DOS_PUNTOS + DATA_TYPE + ASSIGNATION_P + PUNTO_COMA + DECLARATION_LIST_P
                | MORE_DECLARATION + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA + DECLARATION_LIST_P
                ;

            ASSIGNATION_P.Rule = EQUALS + LOGIC_EXPRESION
                | Empty
                ;


            MORE_DECLARATION.Rule = COMA + IDENTIFIER + MORE_DECLARATION_P;

            MORE_DECLARATION_P.Rule = MORE_DECLARATION
                | Empty
                ;
            */
            // a: = 5;

            VAR_ASSIGNATE.Rule = IDENTIFIER + DOS_PUNTOS + EQUALS + LOGIC_EXPRESION + PUNTO_COMA;
            #endregion



            DATA_TYPE.Rule = RESERV_REAL
                | RESERV_STR
                | RESERV_TYPE
                | RESERV_INT
                | RESERV_BOL
                | IDENTIFIER
                ;




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
            IFTHEN.Rule = RESERV_IF + PAR_IZQ + LOGIC_EXPRESION + PAR_DER + RESERV_THEN + IF_SENTENCE + ELIF;
            
            IF_SENTENCE.Rule = instrucciones
                | Empty
                ;

            ELIF.Rule
                = RESERV_ELSE + IF_SENTENCE
                | RESERV_ELSE + IFTHEN
                | Empty
                ;
            

            #endregion

            #region CASE
            SENTENCE_CASE.Rule = RESERV_CASE  + PAR_IZQ + LOGIC_EXPRESION + PAR_DER + RESERV_OF+ CASES + CASE_ELSE + RESERV_END + PUNTO_COMA;

            CASES.Rule = MakePlusRule(CASES, CASE)
                | CASE
                ;
            CASE.Rule = LOGIC_EXPRESION + DOS_PUNTOS + instrucciones;


            CASE_ELSE.Rule = RESERV_ELSE +  instrucciones
                | Empty
                ;
            #endregion

            #endregion

            #region Preferencias
            this.Root = init;
            #endregion

        }


    }
}
