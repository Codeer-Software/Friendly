using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using Codeer.Friendly.Windows.Grasp;
using System.Collections.Generic;

namespace Codeer.Friendly.Dynamic.Test
{
    [TestClass]
    public class Demo
    {
        WindowsAppFriend _app;
        Process _process;

        [TestInitialize]
        public void TestInitialize()
        {
            _app = new WindowsAppFriend(Process.Start(TargetPath.TestTargetPath), "4.0");
            _process = Process.GetProcessById(_app.ProcessId);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _app.Dispose();
            _process.CloseMainWindow();
        }

        [TestMethod]
        public void Old()
        {
            //フォームの取得
            AppVar form = _app["System.Windows.Forms.Control.FromHandle"](_process.MainWindowHandle);
            
            //テキストの変更と取得
            form["Text"]("abc");
            string text = (string)form["Text"]().Core;
            Assert.AreEqual("abc", text);

            //リストを生成して、それの操作
            AppVar list = _app.Dim(new List<int>());
            list["Add"](1);
            list["[]"](0, 100);
            int value = (int)list["[]"](0).Core;
            Assert.AreEqual(100, value);
        }

        [TestMethod]
        public void New()
        {
            //フォームの取得
            dynamic form = _app.Type().System.Windows.Forms.Control.FromHandle(_process.MainWindowHandle);

            //テキストの変更と取得
            form.Text = "abc";
            string text = form.Text;
            Assert.AreEqual("abc", text);

            //リストを生成して、それの操作
            dynamic list = _app.Copy(new List<int>());
            list.Add(1);
            list[0] = 100;
            int value = list[0];
            Assert.AreEqual(100, value);
        }
    }
}
