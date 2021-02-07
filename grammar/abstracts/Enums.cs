using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.abstracts
{
    public enum DataType
    {
        INTEGER = 0,
        STRING = 1,
        BOOLEAN = 2,
        REAL = 3,
        TYPE = 4,
    }

    public enum OpRelational
    {
        EQUALS,
        DISCTINCT,
        LESS,
        LESS_EQUALS,
        HIGHER,
        HIGHER_EQUALS
    }

    public enum OpLogical
    {
        AND,
        OR,
        NOT
    }

    public enum OpArithmetic
    {
        SUM,
        SUBTRACTION,
        MULTIPLICATION,
        DIVISION,
        MODULE
    }
    public enum Transfer
    {
        BREAK,
        CONTINUE,
        RETURN,
        RETURNDATA
    }

    class Enums
    {
        public Enums()
        {
        }
    }
}
