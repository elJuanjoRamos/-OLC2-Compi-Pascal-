using CompiPascal.Traduccion.grammar.abstracts;
using CompiPascal.Traduccion.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class RepeatTraduction
    {
        public RepeatTraduction()
        {

        }

        #region REPEAT UTIL
        public Repeat_Trad REPEAT_UNTIL(ParseTreeNode actual, int cant_tabs)
        {
            //REPEAT_UNTIL.Rule = RESERV_REPEAT + INSTRUCTIONS + RESERV_UNTIL + LOGIC_EXPRESION + PUNTO_COMA;

            //SE OBTIENEN LOS VALORES
            var instrucciones = actual.ChildNodes[1];
            var condicion = (new ExpressionTraduccion()).getExpresion(actual.ChildNodes[3]);

            InstructionTraduccion instructionAST = new InstructionTraduccion();

            //OBTENGO LA LISTA DE INSTRUCCIONES
            LinkedList<Instruction_Trad> lista_instrucciones = instructionAST.ISTRUCCIONES(instrucciones, cant_tabs+1);

            //RETORNO EL NUEVO REPEAT-UTIL
            return new Repeat_Trad(condicion, new Sentence_Trad(lista_instrucciones), cant_tabs);

        }
        #endregion
    }
}
