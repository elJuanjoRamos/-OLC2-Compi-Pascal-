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
        private Sentence sentence;
        private string direccion;

        public FOR(string initId, Expression inicializacion, Expression actualizacion, Sentence sentence, string dir)
            : base(0,0, "For")
        {
            this.initId = initId;
            this.inicializacion = inicializacion;
            this.actualizacion = actualizacion;
            this.sentence = sentence;
            this.direccion = dir;
        }

        public override object Execute(Ambit ambit)
        {
            var ambitName = "Global_For";
            if (!ambit.IsNull)
            {
                ambitName = ambit.Ambit_name + "_For";
            }
            var forAmbit = new Ambit(ambit, ambitName, false);


            //SE HACE LA ASIGNACION INICIAL
            Assignation assignation = new Assignation(initId, inicializacion);


            Identifier identifier = ambit.getVariable(initId);

            //VERIFICA QUE EL IDENTIFICADOR EXISTA  
            if (!identifier.IsNull)
            {
                //VERIFICA QUE LA VARIABLE NO HAYA SIDO ASIGNADA EN LA DECLARACION
                if (!identifier.IsAssiged)
                {

                    assignation.Execute(ambit);
                    



                    //INCREMENTAL
                    if (direccion.Equals("to"))
                    {

                        Relational cond = new Relational(new Access(initId), actualizacion, "<=", 0,0);
                        var condicion = cond.Execute(forAmbit);


                        if (condicion.getDataType != DataType.BOOLEAN)
                        {
                            ConsolaController.Instance.Add("La condicion del for no es boleana");

                            return null;
                        }

                        

                        //EJECUCION
                        while ((bool)condicion.Value == true)
                        {

                            //VERIFICA QUE LA SENTENCIAS NO ESTEN VACIAS
                            if (!sentence.IsNull)
                            {
                                var element = sentence.Execute(forAmbit);
                                
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
                                        else if (ins.Name.Equals("Return"))
                                        {

                                        }
                                        //return ins.;
                                    }
                                }

                                //SE HACE UPDATE DEL VALOR
                                var arit = new Arithmetic(new Access(initId), new Literal("1", 1), "+");
                                var val = arit.Execute(forAmbit);


                                cond = new Relational(arit, actualizacion, "<=", 0, 0);
                                condicion = cond.Execute(forAmbit);

                                

                                if (condicion.getDataType != DataType.BOOLEAN)
                                {
                                    ConsolaController.Instance.Add("La condicion no es booleana");
                                    return null;
                                }

                                if ((bool)condicion.Value)
                                {
                                    forAmbit.setVariable(initId, val.Value, val.getDataType, false);
                                }

                            }

                            else
                            {
                                break;
                            }
                        }
                    }
                    //DECREMENTAL
                    else
                    {
                        Relational cond = new Relational(new Access(initId), actualizacion, ">=", 0, 0);
                        var condicion = cond.Execute(forAmbit);


                        if (condicion.getDataType != DataType.BOOLEAN)
                        {
                            ConsolaController.Instance.Add("La condicion no es boleana");
                            return null;
                        }



                        //EJECUCION
                        while ((bool)condicion.Value == true)
                        {

                            //VERIFICA QUE LA SENTENCIAS NO ESTEN VACIAS
                            if (!sentence.IsNull)
                            {
                                var element = sentence.Execute(forAmbit);

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
                                        else if (ins.Name.Equals("Return"))
                                        {

                                        }
                                        //return ins.;
                                    }
                                }

                                //SE HACE UPDATE DEL VALOR
                                var arit = new Arithmetic(new Access(initId), new Literal("1", 1), "-");
                                var val = arit.Execute(forAmbit);


                                cond = new Relational(arit, actualizacion, ">=", 0, 0);
                                condicion = cond.Execute(forAmbit);



                                if (condicion.getDataType != DataType.BOOLEAN)
                                {
                                    ConsolaController.Instance.Add("La condicion no es booleana");
                                    return null;
                                }

                                if ((bool)condicion.Value)
                                {
                                    forAmbit.setVariable(initId, val.Value, val.getDataType, false);
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
                    ConsolaController.Instance.Add("Variable de contador ilegal: La variable '" + initId + "' no debe estar asignada al momento de su declaracion");
                    ErrorController.Instance.SyntacticError("Variable de contador ilegal: La variable '" + initId + "' no debe estar asignada al momento de su declaracion",0,0);

                    return null;
                }

            }
            else
            {
                ConsolaController.Instance.Add("La variable '" + initId + "' no esta declarada.");
                ErrorController.Instance.SyntacticError("La variable '" + initId + "' no esta declarada.", 0, 0);
                return null;
            }



            return 0;

        }
    }
}
