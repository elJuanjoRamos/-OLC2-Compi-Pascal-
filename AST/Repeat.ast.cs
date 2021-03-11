using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class RepeatAST
    {
        public RepeatAST()
        {

           
        }

        #region REPEAT UTIL
        public Repeat REPEAT_UNTIL(ParseTreeNode actual)
        {
            //REPEAT_UNTIL.Rule = RESERV_REPEAT + INSTRUCTIONS + RESERV_UNTIL + LOGIC_EXPRESION + PUNTO_COMA;

            //SE OBTIENEN LOS VALORES
            var instrucciones = actual.ChildNodes[1];
            var condicion = (new ExpressionAST()).getExpresion(actual.ChildNodes[3]);

            var row = actual.ChildNodes[0].Token.Location.Line;
            var col = actual.ChildNodes[0].Token.Location.Column;

            InstructionAST instructionAST = new InstructionAST();

            //OBTENGO LA LISTA DE INSTRUCCIONES
            LinkedList<Instruction> lista_instrucciones = instructionAST.ISTRUCCIONES(instrucciones);

            //RETORNO EL NUEVO REPEAT-UTIL
            return new Repeat(condicion, new Sentence(lista_instrucciones), row, col);

        }
        #endregion

    }
}
