﻿using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class TypeAST
    {
        public TypeAST()
        {

        }


        public LinkedList<Instruction> TYPE_LIST(ParseTreeNode actual, LinkedList<Instruction> lista_actual)
        {

            /*
             TYPE_LIST.Rule
                = TYPE + TYPE_LIST
                | Empty
                ;             
             */
            if (actual.ChildNodes.Count > 0)
            {
                lista_actual = TYPE(actual.ChildNodes[0], lista_actual);
                lista_actual = TYPE_LIST(actual.ChildNodes[1], lista_actual);
            }


            return lista_actual;
        }

        public LinkedList<Instruction> TYPE(ParseTreeNode actual, LinkedList<Instruction> lista_actual)
        {
            /*
              TYPE.Rule = RESERV_TYPE + IDENTIFIER_ARRAY_TYPE + EQUALS + TYPE_P;
             */
            var identifier = actual.ChildNodes[1].Token.Text;
            var element = TYPE_P(identifier, actual.ChildNodes[3]);
            lista_actual.AddLast(element);

            return lista_actual;
        }

        public Instruction TYPE_P(string name, ParseTreeNode actual)
        {

            /*
             TYPE_P.Rule 
                = OBJECT
                | ARRAY
                ;
             */

            var element = actual.ChildNodes[0].ChildNodes[0].Token.Text;
            if (element.Equals("array"))
            {
                var result = ARRAYs(actual.ChildNodes[0], name);
                return result;
            } else
            {

            }
            return null;
        }

        public Instruction ARRAYs(ParseTreeNode actual, string name)
        {

            /*
              ARRAY.Rule =   
            RESERV_ARRAY  + COR_IZQ + EXPLOGICA + PUNTO + PUNTO + EXPLOGICA + COR_DER + RESERV_OF + MORE_ARRAY + PUNTO_COMA;
             */
            ExpressionAST expressionAST = new ExpressionAST();
            var limit_inf = expressionAST.getExpresion(actual.ChildNodes[2]);
            var limit_sup = expressionAST.getExpresion(actual.ChildNodes[5]);

            var row = actual.ChildNodes[0].Token.Location.Line;
            var col = actual.ChildNodes[0].Token.Location.Column;

            var resultado = MORE_ARRAY(actual.ChildNodes[8], name);

            if (resultado is string)
            {
                return new Arrays(name, limit_inf, limit_sup, resultado.ToString(), row, col);
            } else
            {
                var res = (ArraysMultiple)resultado;
                return new ArraysMultiple(name, limit_inf, limit_sup, res.DataType, res.Auxiliar, res, row, col, res.Contador+1);
            }
            
        }

        public object MORE_ARRAY(ParseTreeNode actual, string name)
        {
            /*
              MORE_ARRAY.Rule
                = DATA_TYPE
                | RESERV_ARRAY + COR_IZQ + EXPLOGICA + PUNTO + PUNTO + EXPLOGICA + COR_DER + RESERV_OF + MORE_ARRAY;
                ;
             */
            if (actual.ChildNodes.Count == 1)
            {
                return actual.ChildNodes[0].ChildNodes[0].Token.Text;
            } else
            {
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                ExpressionAST expressionAST = new ExpressionAST();
                var limit_inf = expressionAST.getExpresion(actual.ChildNodes[2]);
                var limit_sup = expressionAST.getExpresion(actual.ChildNodes[5]);

                var resultado = MORE_ARRAY(actual.ChildNodes[8], name);

                //Instruction exp = null; 
                if (resultado is string)
                {
                    return new ArraysMultiple(name, limit_inf, limit_sup, resultado.ToString(), resultado.ToString(), null,row, col,1);
                }
                else
                {
                    var exp = (ArraysMultiple)resultado;
                    return (new ArraysMultiple(name, limit_inf, limit_sup, exp.DataType, exp.Auxiliar, exp, row, col, exp.Contador+1));
                    
                }

            }
        }    
    }
}
