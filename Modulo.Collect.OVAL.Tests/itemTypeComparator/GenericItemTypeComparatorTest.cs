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
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modulo.Collect.OVAL.SystemCharacteristics;
using Modulo.Collect.OVAL.Tests.helpers;
using Modulo.Collect.OVAL.SystemCharacteristics.comparators;
using Modulo.Collect.OVAL.SystemCharacteristics.Unix;
using Modulo.Collect.OVAL.SystemCharacteristics.Linux;

namespace Modulo.Collect.OVAL.Tests.itemTypeComparator
{
    /// <summary>
    /// Summary description for GenericItemTypeComparatorTest
    /// </summary>
    [TestClass]
    public class GenericItemTypeComparatorTest
    {
        public GenericItemTypeComparatorTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod, Owner("lcosta")]
        public void Should_be_possible_to_compare_two_itemTypes()
        {
            oval_system_characteristics systemCharacteristics = new OvalDocumentLoader().GetFakeOvalSystemCharacteristics("system_characteristics_with_local_variable.xml");

            ItemType itemType = systemCharacteristics.GetSystemDataByReferenceId("3");
            ItemType otherItemType = systemCharacteristics.GetSystemDataByReferenceId("3");

            GenericItemTypeComparator itemTypeComparator = new GenericItemTypeComparator();
            bool resultComparator = itemTypeComparator.IsEquals(itemType, otherItemType);
            
            Assert.IsTrue(resultComparator, "the item types are not equals");

            otherItemType = systemCharacteristics.GetSystemDataByReferenceId("1");
            resultComparator = itemTypeComparator.IsEquals(itemType, otherItemType);
            Assert.IsFalse(resultComparator, "the item types are not equals");         

        }

        [TestMethod, Owner("lcosta")]
        public void should_be_possible_to_compare_two_itemTypes_with_multiples_values()
        {
            oval_system_characteristics systemCharacteristics = new OvalDocumentLoader().GetFakeOvalSystemCharacteristics("system_characteristics_with_sets.xml");

            ItemType itemType = systemCharacteristics.GetSystemDataByReferenceId("3");
            ItemType otherItemType = systemCharacteristics.GetSystemDataByReferenceId("3");

            GenericItemTypeComparator itemTypeComparator = new GenericItemTypeComparator();
            bool resultComparator = itemTypeComparator.IsEquals(itemType, otherItemType);
            Assert.IsTrue(resultComparator, "the item types are not equals");

            otherItemType = systemCharacteristics.GetSystemDataByReferenceId("2");
            resultComparator = itemTypeComparator.IsEquals(itemType, otherItemType);
            Assert.IsFalse(resultComparator, "the item types are not equals");
        }

        [TestMethod, Owner("lcosta")]
        public void Should_be_possible_to_compare_two_itemTypes_with_entities_with_null_values()
        {
            oval_system_characteristics systemCharacteristics = new OvalDocumentLoader().GetFakeOvalSystemCharacteristics("system_characteristics_with_sets.xml");

            ItemType itemType = systemCharacteristics.GetSystemDataByReferenceId("5");
            ItemType otherItemType = systemCharacteristics.GetSystemDataByReferenceId("5");

            GenericItemTypeComparator itemTypeComparator = new GenericItemTypeComparator();            
            bool resultComparator = itemTypeComparator.IsEquals(itemType, otherItemType);
            Assert.IsTrue(resultComparator, "the item types are not equals");

            otherItemType = systemCharacteristics.GetSystemDataByReferenceId("4");
            resultComparator = itemTypeComparator.IsEquals(itemType, otherItemType);
            Assert.IsFalse(resultComparator, "the item types are not equals");
        }

        [TestMethod, Owner("lfernandes")]
        public void Should_be_possible_to_compare_two_unix_itemTypes()
        {
            var systemCharacteristics = 
                new OvalDocumentLoader()
                    .GetFakeOvalSystemCharacteristics("system_characteristics_with_sets.xml");
            var itemType = systemCharacteristics.GetSystemDataByReferenceId("6");
            var otherItemType = systemCharacteristics.GetSystemDataByReferenceId("7");
            var itemTypeComparator = new GenericItemTypeComparator();
            
            var resultComparator = itemTypeComparator.IsEquals(itemType, otherItemType);

            Assert.IsFalse(resultComparator, "the item types are not equals");
            otherItemType = systemCharacteristics.GetSystemDataByReferenceId("1");
            resultComparator = itemTypeComparator.IsEquals(itemType, otherItemType);
            Assert.IsFalse(resultComparator, "the item types are not equals");
        }



        [TestMethod, Owner("lfernandes")]
        public void Should_be_possible_to_compare_two_windows_itemTypes_when_they_were_created_manually()
        {
            ItemType firstItemType = new registry_item() { name = new EntityItemStringType() { Value = "Modulo" } };
            ItemType secondItemType = new registry_item() { name = new EntityItemStringType() { Value = "Microsoft" } };
            var itemTypeComparator = new GenericItemTypeComparator();

            var comparisionResult = itemTypeComparator.IsEquals(firstItemType, secondItemType);

            Assert.IsFalse(comparisionResult, "The compared items are different");
        }
    }
}
