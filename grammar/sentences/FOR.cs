using CompiPascal.controller;
using CompiPascal.grammar.abstracts;
using CompiPascal.grammar.expression;
using CompiPascal.grammar.identifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.grammar.sentences
{
    class FOR : Instruction
    {
        private string initId;
        private Expression inicializacion;
        private Expression actualizacion;
        private Instruction sentence;
        private string direccion;
        private int row;
        private int column;
        public FOR(string initId, Expression inicializacion, Expression actualizacion, Instruction sentence, string dir, int ro, int col)
            : base(ro, col, "For")
        {
            this.initId = initId;
            this.inicializacion = inicializacion;
            this.actualizacion = actualizacion;
            this.sentence = sentence;
            this.direccion = dir;
            this.row = ro;
            this.column = col;
        }

        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public override object Execute(Ambit ambit)
        {
            var forAmbit = new Ambit(ambit,  ambit.Ambit_name, "For", false);


            //SE HACE LA ASIGNACION INICIAL
            Assignation assignation = new Assignation(initId, inicializacion, Row, Column);


            Identifier identifier = ambit.getVariable(initId);

            //VERIFICA QUE EL IDENTIFICADOR EXISTA  
            if (!identifier.IsNull)
            {
                //VERIFICA QUE LA VARIABLE NO HAYA SIDO ASIGNADA EN LA DECLARACION
                if (!identifier.IsAssiged)
                {



                    var simb = "";

                    if (direccion.ToLower().Equals("to"))
                    {
                        simb = "<=";
                    } else
                    {
                        simb = ">=";
                    }

                    Relational cond_temp = new Relational(inicializacion, actualizacion, simb, Row, Column);

                    var condicion = cond_temp.Execute(forAmbit);


                    if (condicion.getDataType != DataType.BOOLEAN)
                    {
                        set_error("La condicion del for no es booleana", Row, Column);
                        return null;
                    }


                    if ((bool)condicion.Value == true)
                    {

                        assignation.Execute(ambit);


                        Relational cond = new Relational(new Access(initId,Row, column), actualizacion, simb, row, column);


                        //EJECUCION
                        while ((bool)condicion.Value == true)
                        {


                            //VERIFICA QUE LA SENTENCIAS NO ESTEN VACIAS
                            if (!sentence.IsNull)
                            {
                                var element = sentence.Execute(forAmbit);


                                //SE HACE UPDATE DEL VALOR
                                var s = "+";
                                if (!direccion.ToLower().Equals("to"))
                                {
                                    s = "-";
                                }

                                var arit = new Arithmetic(new Access(initId, Row, column), new Literal("1", 1, Row, column), s, Row, column);
                                var val = arit.Execute(forAmbit);


                                cond = new Relational(arit, actualizacion, simb, Row, Column);
                                condicion = cond.Execute(forAmbit);



                                if (condicion.getDataType != DataType.BOOLEAN)
                                {
                                    set_error("La condicion del for no es booleana", Row, Column);
                                    return null;
                                }

                                if ((bool)condicion.Value)
                                {
                                    forAmbit.setVariable(initId, val.Value, val.getDataType, false, "Variable");
                                }

                                if (element != null)
                                {
                                    if (element is Instruction)
                                    {
                                        Instruction ins = (Instruction)element;

                                        //console.log(element);
                                        if (ins.Name.Equals("Break"))
                                        {
                                            break;
                                        }
                                        else if (ins.Name.Equals("Continue"))
                                        {
                                            continue;
                                        }
                                        else if (ins.Name.Equals("Exit"))
                                        {
                                            return element;
                                        }
                                    }
                                }
                            }

                            else
                            {
                                break;
                            }
                        }
                    }

                } else
                {
                    set_error("Variable de contador ilegal: La variable '" + initId + "' no debe estar asignada al momento de su declaracion",Row, Column);
                    return null;
                }

            }
            else
            {
                set_error("La variable '" + initId + "' no esta declarada.", Row, Column);
                return null;
            }



            return 0;

        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
