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

            IdentifierTerminal identifier = new IdentifierTerminal("identifier", "[_a-zA-Z][_a-zA-Z0-9]");

            //COMENTARIOS
            CommentTerminal LINE_COMMENT = new CommentTerminal("LINE_COMMENT", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");
            CommentTerminal MULTI_LINE_COMMENT = new CommentTerminal("MULTI_LINE_COMMENT", "(*", "*)");
            CommentTerminal MULTI_LINE_COMMENT_LLAVE = new CommentTerminal("MULTI_LINE_COMMENT_LLAVE", "{", "}");

            NonGrammarTerminals.Add(LINE_COMMENT);
            NonGrammarTerminals.Add(MULTI_LINE_COMMENT);
            NonGrammarTerminals.Add(MULTI_LINE_COMMENT_LLAVE);


            #endregion



            #region ER
            var NUMERO = new NumberLiteral("Numero");

            #endregion

            #region Terminales
            var REVALUAR = ToTerm("Evaluar");
            var PUNTO_COMA = ToTerm(";");
            var DOS_PUNTOS = ToTerm(":");
            var PAR_IZQ = ToTerm("(");
            var PAR_DER = ToTerm(")");
            var COR_IZQ = ToTerm("[");
            var COR_DER = ToTerm("]");
            //Aritmethic    
            var PLUS = ToTerm("+");
            var MIN = ToTerm("-");
            var POR = ToTerm("*");
            var DIVI = ToTerm("/");
            var MODULE = ToTerm("%");
            //Logic
            var AND = ToTerm("&&");
            var OR = ToTerm("||");
            var NOT = ToTerm("!");
            //Relational
            var HIGHER = ToTerm(">");
            var LESS = ToTerm("<");
            var HIGHER_EQUAL = ToTerm(">=");
            var LESS_EQUAL = ToTerm("<=");
            var EQUALS = ToTerm("=");
            var DISCTINCT = ToTerm("<>");
            //Reservadas
            var RESERV_INT = ToTerm("integer");
            var RESERV_STR = ToTerm("string");
            var RESERV_REAL = ToTerm("real");
            var RESERV_BOL = ToTerm("boolean");
            var RESERV_VOID = ToTerm("void");
            var RESERV_TYPE = ToTerm("type");
            var RESERV_OBJ = ToTerm("object");
            var RESERV_PROGRAM = ToTerm("program");
            var RESERV_VAR = ToTerm("var");
            var RESERV_FUN = ToTerm("function");
            var RESERV_PROC = ToTerm("procedure");
            var RESERV_BEGIN = ToTerm("begin");
            var RESERV_END = ToTerm("end");





            RegisterOperators(1, OR);
            RegisterOperators(2, AND);
            RegisterOperators(3, EQUALS, DISCTINCT);
            RegisterOperators(4, HIGHER_EQUAL, LESS_EQUAL, LESS, HIGHER);
            RegisterOperators(5, PLUS, MIN);
            RegisterOperators(6, POR, DIVI);
            RegisterOperators(7, MODULE);

            #endregion

            #region No Terminales
            NonTerminal init = new NonTerminal("init");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal expresion = new NonTerminal("expresion");
            NonTerminal start = new NonTerminal("start");
            NonTerminal tipo = new NonTerminal("tipo", "tipo");


            NonTerminal declaration = new NonTerminal("declaration", "declaration");
            NonTerminal declaration_list = new NonTerminal("declaration_list", "declaration_list");



            #endregion

            #region Gramatica
            init.Rule = start;

            start.Rule = RESERV_PROGRAM + identifier + instrucciones;

            instrucciones.Rule = instrucciones + instruccion
                | instruccion;

            instruccion.Rule = declaration;




            declaration.Rule = RESERV_VAR + identifier + DOS_PUNTOS + tipo + PUNTO_COMA;

            tipo.Rule = RESERV_REAL
                | RESERV_STR
                | RESERV_TYPE
                | RESERV_INT;



            expresion.Rule = MIN + expresion
                | expresion + PLUS + expresion
                | expresion + MIN + expresion
                | expresion + POR + expresion
                | expresion + DIVI + expresion
                | NUMERO
                | PAR_IZQ + expresion + PAR_DER;

            #endregion

            #region Preferencias
            this.Root = init;
            #endregion

        }


    }
}
