﻿// Copyright (C) Microsoft Corporation. All rights reserved.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Microsoft.Tools.WindowsInstaller.PowerShell.Commands
{
    /// <summary>
    /// Tests for the <see cref="InstallCommandActionData"/> class.
    /// </summary>
    [TestClass]
    public sealed class InstallCommandActionDataTests : TestBase
    {
        [TestMethod]
        public void TestNullCommandLineArgs()
        {
            var data = new InstallCommandActionData();
            data.ParseCommandLine(null);

            Assert.IsNull(data.CommandLine, "The command line should be null.");
        }

        [TestMethod]
        public void TestEmptyCommandLineArgs()
        {
            var data = new InstallCommandActionData();
            data.ParseCommandLine(new string[] { });

            Assert.IsNull(data.CommandLine, "The command line should be null.");
        }

        [TestMethod]
        public void TestCommandLineArgs()
        {
            var data = new InstallCommandActionData();
            data.ParseCommandLine(new string[] { "PROPERTY=VALUE",  "REINSTALL=ALL" });

            Assert.AreEqual("PROPERTY=VALUE REINSTALL=ALL", data.CommandLine, "The command line is incorrect.");
        }

        [TestMethod]
        public void ExtractPathFromPSPath()
        {
            using (var p = CreatePipeline("get-item example.msi"))
            {
                var file = p.Invoke().SingleOrDefault();
                Assert.IsNotNull(file, "The file item is null.");

                var data = InstallCommandActionData.CreateActionData<InstallCommandActionData>(TestRunspace.SessionStateProxy.Path, file);
                string path = Path.Combine(this.TestContext.DeploymentDirectory, "example.msi");
                Assert.AreEqual(path, data.Path, "The paths are not the same.");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullResolver()
        {
            var data = InstallCommandActionData.CreateActionData<InstallCommandActionData>(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullFile()
        {
            var data = InstallCommandActionData.CreateActionData<InstallCommandActionData>(TestRunspace.SessionStateProxy.Path, null);
        }
    }
}
