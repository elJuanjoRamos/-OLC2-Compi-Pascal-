﻿using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.sentences;
using Irony.Parsing;

namespace CompiPascal.AST
{
    class InstructionAST
    {

        public InstructionAST()
        {

        }


        public LinkedList<Instruction> INSTRUCTIONS_BODY(ParseTreeNode actual)
        {
            /*INSTRUCTIONS_BODY.Rule 
                = RESERV_BEGIN + INSTRUCTIONS + RESERV_END  + PUNTO
                ;*/
            var begind = actual.ChildNodes[0];

            LinkedList<Instruction> lista_instruciones = ISTRUCCIONES(actual.ChildNodes[1]);


            var end = actual.ChildNodes[2];
            return lista_instruciones;
        }

        //INSTRUCCIONES
        public LinkedList<Instruction> ISTRUCCIONES(ParseTreeNode actual)
        {
            LinkedList<Instruction> listaInstrucciones = new LinkedList<Instruction>();

            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {
                var inst = INSTRUCCION(nodo.ChildNodes[0]);
                listaInstrucciones.AddLast(inst);
            }
            return listaInstrucciones;
        }


        //INSTRUCCION
        public Instruction INSTRUCCION(ParseTreeNode actual)
        {
            if (actual.Term.ToString().Equals("WRITE"))
            {
                var WRHITE_PARAMETER = actual.ChildNodes[2];

                var isln = false;
                LinkedList<Expression> list = new LinkedList<Expression>();
                WriteAST writeAST = new WriteAST();

                list = writeAST.WRITES(WRHITE_PARAMETER);

                if (actual.ChildNodes[0].Term.ToString().Equals("RESERV_WRITEN"))
                {
                    isln = true;
                }
                return new Write(list, isln);

            }
            else if (actual.Term.ToString().Equals("IF-THEN"))
            {
                IF_AST iF_AST = new IF_AST();
                IF _ifs = iF_AST.IFTHEN(actual);
                return _ifs;
            }
            else if (actual.Term.ToString().Equals("WHILE"))
            {
                While _while = (new WhileAST()).WHILE(actual);
                return _while;
            }
            else if (actual.Term.ToString().Equals("VAR_ASSIGNATE"))
            {
                var _assignation = (new AssignationAST()).VAR_ASSIGNATE(actual);

                if (_assignation is Assignation)
                {
                    return (Assignation)_assignation;
                }
                else if (_assignation is Assignation_array)
                {
                    return (Assignation_array)_assignation;
                }
                else if (_assignation is Assignation_arrayMultiple)
                {
                    return (Assignation_arrayMultiple)_assignation;
                }
            }
            else if (actual.Term.ToString().Equals("REPEAT_UNTIL"))
            {
                Repeat _repeat = (new RepeatAST()).REPEAT_UNTIL(actual);
                return _repeat;
            }
            else if (actual.Term.ToString().Equals("FOR"))
            {
                FOR _for = ((new ForAST())).SENCECIA_FOR(actual);
                return _for;
            }
            else if (actual.Term.ToString().Equals("SENTENCE_CASE"))
            {
                Switch _SW = (new CaseAST()).SENTENCE_CASE(actual);
                return _SW;
            }
            else if (actual.Term.ToString().Equals("CONTINUE"))
            {
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                return new Continue(row, col);
            }
            else if (actual.Term.ToString().Equals("BREAK"))
            {
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                return new Break(row, col);
            }
            else if (actual.Term.ToString().Equals("CALL"))
            {
                return (new Call_Instruction()).CALL(actual);
            }
            else if (actual.Term.ToString().Equals("EXIT"))
            {
                return (new ExitAst()).getExit(actual.ChildNodes[2]);
            }

            return null;
        }

    }
}
