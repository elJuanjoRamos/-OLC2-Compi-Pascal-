using CompiPascal.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class ForAST
    {
        //VARIABLES
        InstructionAST instructionAST = new InstructionAST();
        ExpressionAST expressionAST = new ExpressionAST();

        public ForAST()
        {

        }



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
            var inicio = expressionAST.getExpresion(actual.ChildNodes[4]);
            var direccion = actual.ChildNodes[5].ChildNodes[0].Token.Text;
            var fin = expressionAST.getExpresion(actual.ChildNodes[6]);
            var lista_instrucciones = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[8]);
            return new FOR(ident, inicio, fin, new Sentence(lista_instrucciones), direccion);



        }
        #endregion

    }
}
