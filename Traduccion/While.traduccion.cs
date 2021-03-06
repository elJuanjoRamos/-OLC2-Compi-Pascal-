using CompiPascal.Traduccion.grammar.abstracts;
using CompiPascal.Traduccion.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class WhileTraduccion
    {
        public WhileTraduccion()
        {

        }

        #region WHILE
        public While_Trad WHILE(ParseTreeNode actual, int cant_tabs)
        {
            //WHILE.Rule = RESERV_WHILE + LOGIC_EXPRESION + RESERV_DO + INSTRUCTIONS_BODY;

            var condition = (new ExpressionTraduccion()).getExpresion(actual.ChildNodes[1]);

            InstructionTraduccion instructionAST = new InstructionTraduccion();

            LinkedList<Instruction_Trad> lista_instrucciones = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[3], cant_tabs+1);

            return new While_Trad(condition, new Sentence_Trad(lista_instrucciones), cant_tabs);

        }

        #endregion
    }
}
