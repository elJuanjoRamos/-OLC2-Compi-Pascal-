using CompiPascal.Traduccion.grammar.abstracts;
using CompiPascal.Traduccion.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class IFTraduccion
    {
        //VARIABLES
        InstructionTraduccion instrucciones = new InstructionTraduccion();
        public IFTraduccion()
        {

        }


        #region IF



        public If_Trad IFTHEN(ParseTreeNode actual, int cant_tabs)
        {
            /*
              IFTHEN.Rule
                = RESERV_IF + EXPRESION
                    + RESERV_THEN
                        + IF_SENTENCE
                    + ELIF;
             */
            If_Trad ifs = new If_Trad();
            ExpressionTraduccion expressionAST = new ExpressionTraduccion();

            var LOGIC_EXPRESION = expressionAST.getExpresion(actual.ChildNodes[1]);
            var SENTENCES = IF_SENTENCE(actual.ChildNodes[3], cant_tabs+1);
            var ELSE = ELIF(actual.ChildNodes[4], cant_tabs+1);


            return new If_Trad(LOGIC_EXPRESION, SENTENCES, ELSE, cant_tabs);
        }

        public Sentence_Trad IF_SENTENCE(ParseTreeNode actual, int cant_tabs)
        {
            /*
               IF_SENTENCE.Rule = INSTRUCTIONS_BODY
                | Empty
                ;

             */
            Sentence_Trad sentence = new Sentence_Trad();
            if (actual.ChildNodes.Count > 0)
            {


                var lista_instrucciones = instrucciones.INSTRUCTIONS_BODY(actual.ChildNodes[0], cant_tabs+1);
                sentence = new Sentence_Trad(lista_instrucciones);
            }

            return sentence;
        }
        public Sentence_Trad ELIF(ParseTreeNode actual, int cant_tabs)
        {
            Sentence_Trad sentence = new Sentence_Trad();

            if (actual.ChildNodes.Count > 0)
            {
                LinkedList<Instruction_Trad> lista_instrucciones = new LinkedList<Instruction_Trad>();
                // ELSE 
                if (actual.ChildNodes[1].Term.ToString().Equals("IF_SENTENCE"))
                {
                    lista_instrucciones = instrucciones.INSTRUCTIONS_BODY(actual.ChildNodes[1].ChildNodes[0], cant_tabs+1);

                }
                // ELSE IF
                else
                {
                    var ifs = IFTHEN(actual.ChildNodes[1], cant_tabs);
                    lista_instrucciones.AddLast(ifs);
                }

                sentence = new Sentence_Trad(lista_instrucciones);

            }
            return sentence;
        }
        #endregion



    }
}
