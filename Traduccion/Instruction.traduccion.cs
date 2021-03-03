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


        public string INSTRUCTIONS_BODY(ParseTreeNode actual, int cant_tabs)
        {
            /*INSTRUCTIONS_BODY.Rule 
                = RESERV_BEGIN + INSTRUCTIONS + RESERV_END  + PUNTO
                ;*/


            //tabulaciones
            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + " ";
            }
            
            
            var begind = actual.ChildNodes[0].Token.Text;

            string  lista_instruciones = ISTRUCCIONES(actual.ChildNodes[1], cant_tabs+1);


            var end = actual.ChildNodes[2].Token.Text;
            return  
                tabs +begind  
                + "\n" + lista_instruciones + "\n" 
                + tabs +end;

        }

        //INSTRUCCIONES
        public string ISTRUCCIONES(ParseTreeNode actual, int cant_tabs)
        {
            string listaInstrucciones = "";

            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {
                var inst = INSTRUCCION(nodo.ChildNodes[0], cant_tabs);
                listaInstrucciones = listaInstrucciones + inst;
            }
            return listaInstrucciones;
        }


        //INSTRUCCION
        public string INSTRUCCION(ParseTreeNode actual, int cant_tabs)
        {
            var tabs = "";
            for (int i = 0; i < cant_tabs; i++)
            {
                tabs = tabs + " ";
            }

            var retorno = "";

            if (actual.Term.ToString().Equals("WRITE"))
            {
                var WRHITE_PARAMETER = actual.ChildNodes[2];
                var write_instruction = actual.ChildNodes[0].Token.Text + "(";
                retorno = tabs + write_instruction + (new WriteTraduccion()).WRITES(WRHITE_PARAMETER)  + ");\n";
            }
            else if (actual.Term.ToString().Equals("IF-THEN"))
            {
                IFTraduccion iF_AST = new IFTraduccion();
                var _ifs = iF_AST.IFTHEN(actual, (cant_tabs + 1));
                retorno = tabs+ _ifs;
            }
            else if (actual.Term.ToString().Equals("WHILE"))
            {
                var _while = (new WhileTraduccion()).WHILE(actual,cant_tabs+1);
                retorno = tabs + _while;
            }
            else if (actual.Term.ToString().Equals("VAR_ASSIGNATE"))
            {
                var _assignation = (new AssignationTraduction()).VAR_ASSIGNATE(actual);
                retorno = tabs + _assignation;
            }
            else if (actual.Term.ToString().Equals("REPEAT_UNTIL"))
            {
                var _repeat = (new RepeatTraduction()).REPEAT_UNTIL(actual, cant_tabs);
                retorno = tabs + _repeat;
            }
            else if (actual.Term.ToString().Equals("FOR"))
            {
                var _for = ((new ForTraduccion())).SENCECIA_FOR(actual, cant_tabs);
                retorno = tabs + _for;
            }
            else if (actual.Term.ToString().Equals("SENTENCE_CASE"))
            {
                var _SW = (new CaseTraduccion()).SENTENCE_CASE(actual, cant_tabs);
                retorno = tabs + _SW;
            }
            else if (actual.Term.ToString().Equals("CONTINUE"))
            {
                retorno = tabs + actual.Token.Text;
            }
            else if (actual.Term.ToString().Equals("BREAK"))
            {
                retorno = tabs + actual.Token.Text;
            }
            else if (actual.Term.ToString().Equals("CALL"))
            {

                return (new CallTraduccion()).CALL(actual);
            }
            else if (actual.Term.ToString().Equals("EXIT"))
            {
                return (new ExitTraduccion().getExit(actual));
            }

            return retorno;
        }
    }
}
