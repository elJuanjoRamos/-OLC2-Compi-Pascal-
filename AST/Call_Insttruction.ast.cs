﻿using CompiPascal.grammar.sentences;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class Call_Instruction
    {
        public Call_Instruction()
        {

        }


        public Call CALL(ParseTreeNode actual)
        {
            // CALL.Rule = IDENTIFIER + PAR_IZQ + CALL_PARAMETERS + PAR_DER + PUNTO_COMA;
            ArrayList prametros_llamada = new ArrayList();

            prametros_llamada = ((new ParametersAST())).CALL_PARAMETERS(actual.ChildNodes[2], prametros_llamada);
            var row = actual.ChildNodes[0].Token.Location.Line;
            var column = actual.ChildNodes[0].Token.Location.Column;

            return new Call(actual.ChildNodes[0].Token.Text, prametros_llamada, row, column);
        }
    }
}
