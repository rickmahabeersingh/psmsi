﻿// Copyright (C) Microsoft Corporation. All rights reserved.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace Microsoft.Tools.WindowsInstaller.PowerShell.Commands
{
    /// <summary>
    /// Tests for the <see cref="ExportPatchXmlCommand"/> class.
    /// </summary>
    [TestClass]
    public sealed class ExportPatchXmlCommandTests : TestBase
    {
        [TestMethod]
        public void ExtractPatchFileXml()
        {
            using (var p = CreatePipeline(@"export-msipatchxml example.msp ""$TestRunDirectory\patchfilexml.xml"""))
            {
                p.Invoke();

                var path = Path.Combine(this.TestContext.TestRunDirectory, "patchfilexml.xml");
                Assert.IsTrue(File.Exists(path), "The output file does not exist.");

                // Check that the default encoding is correct.
                using (var file = File.OpenText(path))
                {
                    Assert.AreEqual<Encoding>(Encoding.UTF8, file.CurrentEncoding, "The encoding is incorreect.");
                }
            }
        }

        [TestMethod]
        public void ExtractPatchFileFormattedXml()
        {
            using (var p = CreatePipeline(@"export-msipatchxml example.msp ""$TestRunDirectory\patchfileformattedxml.xml"" -encoding Unicode -formatted"))
            {
                p.Invoke();

                var path = Path.Combine(this.TestContext.TestRunDirectory, "patchfileformattedxml.xml");
                Assert.IsTrue(File.Exists(path), "The output file does not exist.");

                // Make sure the file contains tabs.
                using (var file = File.OpenText(path))
                {
                    var xml = file.ReadToEnd();
                    StringAssert.Contains(xml, "\t", "The file does not contain tabs.");
                    Assert.AreEqual<Encoding>(Encoding.Unicode, file.CurrentEncoding, "The encoding is incorrect.");
                }
            }
        }
    }
}
