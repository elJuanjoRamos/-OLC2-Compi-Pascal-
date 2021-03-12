using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class WhileAST
    {
        public WhileAST()
        {

        }

        #region WHILE
        public While WHILE(ParseTreeNode actual)
        {
            //WHILE.Rule = RESERV_WHILE + LOGIC_EXPRESION + RESERV_DO + INSTRUCTIONS_BODY;

            var condition = (new ExpressionAST()).getExpresion(actual.ChildNodes[1]);

            int row = actual.ChildNodes[0].Token.Location.Line;
            int col = actual.ChildNodes[0].Token.Location.Column;


            InstructionAST instructionAST = new InstructionAST();

            LinkedList<Instruction> lista_instrucciones = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[3]);

            return new While(condition, new Sentence(lista_instrucciones), row, col);

        }

        #endregion

    }
}
