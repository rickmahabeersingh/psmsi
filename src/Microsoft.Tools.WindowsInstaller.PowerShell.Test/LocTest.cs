﻿// Copyright (C) Microsoft Corporation. All rights reserved.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using Microsoft.Tools.WindowsInstaller.PowerShell;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Tools.WindowsInstaller
{
    /// <summary>
    /// Tests localization of the project using pseudo-localization files.
    /// </summary>
    /// <remarks>
    /// To enable this on Windows Vista and newer, see http://msdn.microsoft.com/en-us/library/aa366908.aspx.
    /// </remarks>
    [TestClass]
    public sealed class LocTest : TestBase
    {
        [TestMethod]
        public void PlocCmdletHelpTest()
        {
            using (var p = CreatePipeline(@"set-uiculture 'qps-ploc'; get-help get-msiproductinfo"))
            {
                var objs = p.Invoke();

                Assert.AreEqual<int>(1, objs.Count);
                Assert.AreEqual<string>(@"{Gééts próódüüçt ííñformââtioñ for registered produçts.}", objs[0].GetPropertyValue<string>("Synopsis"));
            }
        }

        [TestMethod]
        public void PlocFunctionHelpTest()
        {
            using (var p = CreatePipeline(@"set-uiculture 'qps-ploc'; get-help get-msisharedcomponentinfo"))
            {
                var objs = p.Invoke();

                Assert.AreEqual<int>(1, objs.Count);
                Assert.AreEqual<string>(@"{Gééts ííñfóórmââtioñ aboüüt shared çompoñeñts iñstalled or registered for the çurreñt user or the maçhiñe.}", objs[0].GetPropertyValue<string>("Synopsis"));
            }
        }
    }
}
