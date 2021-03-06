﻿/*
 * Modulo Open Distributed SCAP Infrastructure Collector (modSIC)
 * 
 * Copyright (c) 2011-2015, Modulo Solutions for GRC.
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 * 
 * - Redistributions of source code must retain the above copyright notice,
 *   this list of conditions and the following disclaimer.
 *   
 * - Redistributions in binary form must reproduce the above copyright 
 *   notice, this list of conditions and the following disclaimer in the
 *   documentation and/or other materials provided with the distribution.
 *   
 * - Neither the name of Modulo Security, LLC nor the names of its
 *   contributors may be used to endorse or promote products derived from
 *   this software without specific  prior written permission.
 *   
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 * */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modulo.Collect.OVAL.Definitions;
using Modulo.Collect.OVAL.SystemCharacteristics;
using Modulo.Collect.OVAL.Definitions.VariableEvaluators.Evaluators;
using Modulo.Collect.OVAL.Variables;

namespace Modulo.Collect.OVAL.Definitions.VariableEvaluators
{
    public class VariableEvaluator
    {
        private IEnumerable<VariableType> variablesOfDefinitions;
        private EvaluatorFactory evaluatorFactory;
        private oval_system_characteristics systemCharacteristics;
        oval_variables externalVariables;

        public VariableEvaluator(IEnumerable<VariableType> variables, oval_system_characteristics systemCharacteristics, oval_variables externalVariables)
        {
            this.variablesOfDefinitions = variables;
            this.evaluatorFactory = new EvaluatorFactory();
            this.systemCharacteristics = systemCharacteristics;
            this.externalVariables = externalVariables;
        }

        /// <summary>
        /// This method evaluates variables specified in entityType. 
        /// If is not exists any variables, the return value is the same value defined in entityType.        
        /// </summary>
        /// <param name="entityType">EntityType.</param>
        /// <returns></returns>
        public IEnumerable<string> Evaluate(EntitySimpleBaseType entityType)
        {
            if (this.ExistsVariableDefined(entityType))
            {
                return EvaluateVariable(entityType.var_ref);
            }
            else
            {
                return this.ReturnTheSameValue(entityType);
            }
        }

        /// <summary>
        /// This method evalutes a variable specified by his id.
        /// </summary>
        /// <param name="variableId">The variable id.</param>
        /// <returns></returns>
        public IEnumerable<string> EvaluateVariable(string variableId)
        {
            VariableType variableOfDefinitions = this.GetVariableByIdFromDefinitions(variableId);
            Evaluator probeVariable = this.evaluatorFactory.CreateVariable(variableOfDefinitions, systemCharacteristics, variablesOfDefinitions, externalVariables);
            return probeVariable.GetValue();
        }

        private IEnumerable<string> ReturnTheSameValue(EntitySimpleBaseType entityType)
        {
            List<String> values = new List<string>();
            values.Add(entityType.Value);
            return values;
        }

        private bool ExistsVariableDefined(EntitySimpleBaseType entityType)
        {
            return (entityType.var_ref != null);
        }

        

        private VariableType GetVariableByIdFromDefinitions(string id)
        {
            VariableType variableOfDefinitions = variablesOfDefinitions.Where<VariableType>(v => v.id == id).SingleOrDefault();
            return variableOfDefinitions;
        }
    }
}
