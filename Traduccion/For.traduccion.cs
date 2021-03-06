using CompiPascal.Traduccion.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class ForTraduccion
    {
        //VARIABLES
        InstructionTraduccion instructionAST = new InstructionTraduccion();
        ExpressionTraduccion expressionAST = new ExpressionTraduccion();

        public ForTraduccion()
        {

        }

        #region FOR
        public For_Trad SENCECIA_FOR(ParseTreeNode actual, int cant_tabs)
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
            var lista_instrucciones = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[8], cant_tabs+1);
            return new For_Trad(ident, inicio, fin, new Sentence_Trad(lista_instrucciones), direccion, cant_tabs);



        }
        #endregion
    }
}
