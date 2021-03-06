using CompiPascal.Traduccion.grammar.abstracts;
using CompiPascal.Traduccion.grammar.sentences;
using CompiPascal.Traduccion.sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.Traduccion
{
    class InstructionTraduccion
    {
        public InstructionTraduccion()
        {

        }


        public LinkedList<Instruction_Trad> INSTRUCTIONS_BODY(ParseTreeNode actual, int cant_tabs)
        {
            /*INSTRUCTIONS_BODY.Rule 
                = RESERV_BEGIN + INSTRUCTIONS + RESERV_END  + PUNTO
                ;*/
            var begind = actual.ChildNodes[0];

            LinkedList<Instruction_Trad> lista_instruciones = ISTRUCCIONES(actual.ChildNodes[1], cant_tabs);


            var end = actual.ChildNodes[2];
            return lista_instruciones;
        }

        //INSTRUCCIONES
        public LinkedList<Instruction_Trad> ISTRUCCIONES(ParseTreeNode actual, int cant_tabs)
        {
            LinkedList<Instruction_Trad> listaInstrucciones = new LinkedList<Instruction_Trad>();

            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {
                var inst = INSTRUCCION(nodo.ChildNodes[0], cant_tabs);
                listaInstrucciones.AddLast(inst);
            }
            return listaInstrucciones;
        }


        //INSTRUCCION
        public Instruction_Trad INSTRUCCION(ParseTreeNode actual, int cant_tabs)
        {
            if (actual.Term.ToString().Equals("WRITE"))
            {
                var writeAST = (new WriteTraduccion()).getWrite(actual, cant_tabs);
                return writeAST;

            }
            else if (actual.Term.ToString().Equals("IF-THEN"))
            {
                IFTraduccion iF_AST = new IFTraduccion();
                If_Trad _ifs = iF_AST.IFTHEN(actual, cant_tabs);
                return _ifs;
            }
            else if (actual.Term.ToString().Equals("WHILE"))
            {
                While_Trad _while = (new WhileTraduccion()).WHILE(actual, cant_tabs);
                return _while;
            }
            else if (actual.Term.ToString().Equals("VAR_ASSIGNATE"))
            {
                Assignation_Trad _assignation = (new AssignationTraduction()).VAR_ASSIGNATE(actual, cant_tabs);
                return _assignation;
            }
            else if (actual.Term.ToString().Equals("REPEAT_UNTIL"))
            {
                Repeat_Trad _repeat = (new RepeatTraduction()).REPEAT_UNTIL(actual, cant_tabs);
                return _repeat;
            }
            else if (actual.Term.ToString().Equals("FOR"))
            {
                For_Trad _for = ((new ForTraduccion())).SENCECIA_FOR(actual, cant_tabs);
                return _for;
            }
            else if (actual.Term.ToString().Equals("SENTENCE_CASE"))
            {
                Switch_Trad _SW = (new CaseTraduccion()).SENTENCE_CASE(actual, cant_tabs);
                return _SW;
            }
            else if (actual.Term.ToString().Equals("CONTINUE"))
            {
                return new Continue_Trad(cant_tabs);
            }
            else if (actual.Term.ToString().Equals("BREAK"))
            {
                return new Break_Trad(cant_tabs);
            }
            else if (actual.Term.ToString().Equals("CALL"))
            {
                return (new CallTraduccion()).CALL(actual, cant_tabs);
            }
            else if (actual.Term.ToString().Equals("EXIT"))
            {
                return (new ExitTraduccion()).getExit(actual.ChildNodes[2], cant_tabs);
            }

            return null;
        }

    }
}
