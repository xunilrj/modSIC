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
using Modulo.Collect.OVAL.Variables;
using Modulo.Collect.OVAL.Common;

namespace Modulo.Collect.OVAL.Tests.helpers
{
    public class OvalVariablesBuilder
    {
        private GeneratorType Generator;
        private List<VariableType> VariableTypes;

        public OvalVariablesBuilder()
        {
            this.Generator = GetDefaultGenerator();
            this.VariableTypes = new List<VariableType>();
        }

        public OvalVariablesBuilder AddVariable(string id, string comment, SimpleDatatypeEnumeration datatype, string value)
        {
            this.VariableTypes.Add(new VariableType(datatype, id, value) { comment = comment });
            return this;
        }

        public oval_variables Build()
        {
            return new oval_variables()
            {
                generator = GetDefaultGenerator(),
                variables = this.VariableTypes
            };
        }

        private GeneratorType GetDefaultGenerator()
        {
            return new GeneratorType()
            {
                product_name = "modSIC",
                product_version = "1.0",
                schema_version = new decimal(5.6),
                timestamp = DateTime.Now
            };
        }
    }
}
