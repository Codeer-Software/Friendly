using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using System.Windows.Forms;

namespace Codeer.Friendly.Dynamic.Test
{
    /// <summary>
    /// AppVarExtensionsとDynamicAppVarに関するテストです。
    /// </summary>
    [TestClass]
    public class ObjectMethodTest
    {
        WindowsAppFriend _app;
        dynamic type;

        /// <summary>
        /// テスト初期化。
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _app = new WindowsAppFriend(Process.Start(TargetPath.TestTargetPath), "4.0");
            type = _app.Type().TestTarget.ObjectMethod;
        }

        /// <summary>
        /// テスト終了処理。
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            Process process = Process.GetProcessById(_app.ProcessId);
            _app.Dispose();
            process.CloseMainWindow();
        }

        /// <summary>
        /// Equalsのテスト
        /// </summary>
        [TestMethod]
        public void EqualsTest()
        {
            type.Equals(1);
            Assert.AreEqual("Equals-1", (string)type.CallName);
        }

        /// <summary>
        /// Equals(static)のテスト
        /// </summary>
        [TestMethod]
        public void EqualsStaticTest()
        {
            type.Equals(1, 2);
            Assert.AreEqual("Equals-1-2", (string)type.CallName);
        }

        /// <summary>
        /// Equals(static)のテスト
        /// </summary>
        [TestMethod]
        public void EqualsStaticObjectTest()
        {
            dynamic form1 = _app.Type<Application>().OpenForms[0];
            dynamic form2 = _app.Type<Application>().OpenForms[0];
            dynamic form3 = _app.Type().TestTarget.MainForm();
            Assert.IsTrue((bool)_app.Type<object>().Equals(form1, form2));
            Assert.IsFalse((bool)_app.Type<object>().Equals(form1, form3));
        }

        /// <summary>
        /// GetHashCodeのテスト
        /// </summary>
        [TestMethod]
        public void GetHashCodeTest()
        {
            type.GetHashCode();
            Assert.AreEqual("GetHashCode", (string)type.CallName);
        }

        /// <summary>
        /// ToStringのテスト
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            type.ToString();
            Assert.AreEqual("ToString", (string)type.CallName);
        }

        /// <summary>
        /// GetTypeのテスト
        /// </summary>
        [TestMethod]
        public void GetTypeTest()
        {
            Assert.AreEqual(typeof(int).ToString(), type.GetType().ToString());
            Assert.AreEqual("GetType", (string)type.CallName);
        }

        /// <summary>
        /// Memberwiseのテスト
        /// </summary>
        [TestMethod]
        public void MemberwiseCloneTest()
        {
            type.MemberwiseClone();
            Assert.AreEqual("MemberwiseClone", (string)type.CallName);
        }

        /// <summary>
        /// ReferenceEqualsのテスト
        /// </summary>
        [TestMethod]
        public void ReferenceEqualsTest()
        {
            type.ReferenceEquals(1, 2);
            Assert.AreEqual("ReferenceEquals-1-2", (string)type.CallName);
        }

        /// <summary>
        /// ReferenceEqualsのテスト
        /// </summary>
        [TestMethod]
        public void ReferenceEqualsObjectTest()
        {
            dynamic form1 = _app.Type<Application>().OpenForms[0];
            dynamic form2 = _app.Type<Application>().OpenForms[0];
            dynamic form3 = _app.Type().TestTarget.MainForm();
            Assert.IsTrue((bool)_app.Type<object>().ReferenceEquals(form1, form2));
            Assert.IsFalse((bool)_app.Type<object>().ReferenceEquals(form1, form3));
        }
    }
}
