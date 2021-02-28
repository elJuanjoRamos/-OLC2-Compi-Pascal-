using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class IF_AST
    {

        //VARIABLES
        InstructionAST instructionAST = new InstructionAST();

        public IF_AST()
        {

        }

        #region IF

        /*
         
        

            IF_SENTENCE.Rule = INSTRUCTIONS_BODY
                | Empty
                ;

            ELIF.Rule
                = RESERV_ELSE + IF_SENTENCE //+ PUNTO_COMA
                | RESERV_ELSE + IFTHEN
                | Empty
                ;
         */

        public IF IFTHEN(ParseTreeNode actual)
        {
            /*
              IFTHEN.Rule
                = RESERV_IF + EXPRESION
                    + RESERV_THEN
                        + IF_SENTENCE
                    + ELIF;
             */
            IF ifs = new IF();
            ExpressionAST expressionAST = new ExpressionAST();
            
            var LOGIC_EXPRESION = expressionAST.getExpresion(actual.ChildNodes[1]);
            var SENTENCES = IF_SENTENCE(actual.ChildNodes[3]);
            var ELSE = ELIF(actual.ChildNodes[4]);


            return new IF(LOGIC_EXPRESION, SENTENCES, ELSE);
        }

        public Sentence IF_SENTENCE(ParseTreeNode actual)
        {
            Sentence sentence = new Sentence();
            if (actual.ChildNodes.Count > 0)
            {
                

                var lista_instrucciones = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[0]);
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
                    lista_instrucciones = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[1].ChildNodes[0]);

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

    }
}
