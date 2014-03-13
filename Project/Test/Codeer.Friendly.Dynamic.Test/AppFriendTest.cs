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
    /// AppFriendExtensionsとDynamicAppTypeに関するテスト。
    /// </summary>
    [TestClass]
    public class AppFriendTest
    {
        WindowsAppFriend _app;

        /// <summary>
        /// テスト初期化。
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _app = new WindowsAppFriend(Process.Start(TargetPath.TestTargetPath), "4.0");
        }

        /// <summary>
        /// テスト終了処理。
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            WindowsAppFriend windowsAppFriend = (WindowsAppFriend)_app;
            Process process = Process.GetProcessById(windowsAppFriend.ProcessId);
            windowsAppFriend.Dispose();
            process.CloseMainWindow();
        }

        /// <summary>
        /// nullの変数を対象アプリケーションに宣言するテスト。
        /// </summary>
        [TestMethod]
        public void Null()
        {
            dynamic appVar = _app.Null();
            Assert.IsNull(((AppVar)appVar).Core);
        }

        /// <summary>
        /// オブジェクトを対象アプリケーションにコピーするテスト。
        /// </summary>
        [TestMethod]
        public void Copy()
        {
            dynamic appVar = _app.Copy(3);
            Assert.AreEqual(3, (int)appVar);
        }

        /// <summary>
        /// オブジェクトを対象アプリケーションにコピーするテスト。
        /// </summary>
        [TestMethod]
        public void CopyAppVar()
        {
            dynamic appVar = _app.Copy((AppVar)_app.Copy(4));
            Assert.AreEqual(4, (int)appVar);
        }

        /// <summary>
        /// オブジェクトを対象アプリケーションにコピーするテスト。
        /// </summary>
        [TestMethod]
        public void CopyDynamicAppVar()
        {
            dynamic appVar = _app.Copy((DynamicAppVar)_app.Copy(5));
            Assert.AreEqual(5, (int)appVar);
        }

        /// <summary>
        /// 変数を対象アプリケーションに生成するテスト。
        /// </summary>
        [TestMethod]
        public void New()
        {
            dynamic appVar = _app.Type().System.Windows.Forms.Form();
            appVar.Text = "abc";
            Assert.AreEqual("abc", (string)appVar.Text);
        }

        /// <summary>
        /// 変数を対象アプリケーションに生成するテスト。
        /// </summary>
        [TestMethod]
        public void NewGeneric()
        {
            dynamic appVar = _app.Type<Form>()();
            appVar.Text = "abc";
            Assert.AreEqual("abc", (string)appVar.Text);
        }

        /// <summary>
        /// 変数を対象アプリケーションに生成するテスト。
        /// </summary>
        [TestMethod]
        public void NewTypeFullName()
        {
            dynamic appVar = _app.Type("System.Windows.Forms.Form")();
            appVar.Text = "abc";
            Assert.AreEqual("abc", (string)appVar.Text);
        }

        /// <summary>
        /// 変数を対象アプリケーションに生成するテスト。
        /// </summary>
        [TestMethod]
        public void NewTypeType()
        {
            dynamic appVar = _app.Type(typeof(Form))();
            appVar.Text = "abc";
            Assert.AreEqual("abc", (string)appVar.Text);
        }

        /// <summary>
        /// 変数を対象アプリケーションに生成するテスト。
        /// </summary>
        [TestMethod]
        public void NewNameSpaceAndName()
        {
            dynamic appVar = _app.Type("System.Windows.Forms").Form();
            appVar.Text = "abc";
            Assert.AreEqual("abc", (string)appVar.Text);
        }

        /// <summary>
        /// タイプ情報を使ってインスタンスを生成するテスト。
        /// </summary>
        [TestMethod]
        public void NewWithOperationInfo()
        {
            dynamic appVar = _app.Type().TestTarget.GuiData(null, new OperationTypeInfo("TestTarget.GuiData", "System.Windows.Forms.Form"));
            Assert.AreEqual("TestTarget.GuiData", appVar.GetType().ToString());
        }

        /// <summary>
        /// タイプ情報を使ってインスタンスを生成するテスト。
        /// </summary>
        [TestMethod]
        public void NewWithOperationInfoGeneric()
        {
            dynamic appVar = _app.Type<TestTarget.GuiData>()(null, new OperationTypeInfo("TestTarget.GuiData", "System.Windows.Forms.Form"));
            Assert.AreEqual("TestTarget.GuiData", appVar.GetType().ToString());
        }

        /// <summary>
        /// staticメソッド。
        /// </summary>
        [TestMethod]
        public void MethodTest()
        {
            Assert.AreEqual(3, (int)_app.Type().TestTarget.MainForm.StaticMethod("3"));
        }

        /// <summary>
        /// staticプロパティー。
        /// </summary>
        [TestMethod]
        public void PropertyTest()
        {
            dynamic sMainForm = _app.Type().TestTarget.MainForm;
            sMainForm.StaticProperty = 3;
            Assert.AreEqual(3, (int)sMainForm.StaticProperty);
        }

        /// <summary>
        /// staticフィールド。
        /// </summary>
        [TestMethod]
        public void FieldTest()
        {
            dynamic sMainForm = _app.Type().TestTarget.MainForm;
            sMainForm._staticField = 5;
            Assert.AreEqual(5, (int)sMainForm._staticField);
        }

        /// <summary>
        /// 非同期実行のテストです。
        /// </summary>
        [TestMethod]
        public void MethodAsync()
        {
            const int BM_CLICK = 0x00F5;
            WindowControl currentTop = WindowControl.FromZTop(_app);
            Async async = new Async();
            _app.Type<MessageBox>().Show("text", async);
            currentTop.WaitForNextModal().IdentifyFromWindowText("OK").SendMessage(BM_CLICK, IntPtr.Zero, IntPtr.Zero);
            async.WaitForCompletion();
        }

        /// <summary>
        /// OperationTypeInfoを使うテストです。
        /// </summary>
        [TestMethod]
        public void MethodOperationTypeInfo()
        {
            dynamic form = WindowControl.FromZTop(_app).AppVar.Dynamic();
            OperationTypeInfo op = new OperationTypeInfo((string)form.GetType().FullName, typeof(Control).FullName);
            string type = _app.Type().TestTarget.MainForm.OverLoadMethodStatic(null, op);
            Assert.AreEqual(typeof(Control).FullName, type);
        }


        /// <summary>
        /// OperationTypeInfoとAsyncを使うテストです。
        /// </summary>
        [TestMethod]
        public void MethodOperationTypeInfoAsync()
        {
            dynamic form = WindowControl.FromZTop(_app).AppVar.Dynamic();
            OperationTypeInfo op = new OperationTypeInfo((string)form.GetType().FullName, typeof(Control).FullName);

            Async async = new Async();
            dynamic type = _app.Type().TestTarget.MainForm.OverLoadMethodStatic(null, op, async);
            async.WaitForCompletion();

            Assert.AreEqual(typeof(Control).FullName, (string)type);
        }

        /// <summary>
        /// コンストラクタに非同期実行引数を渡すと例外が発生するテストです。
        /// </summary>
        [TestMethod]
        public void ConstructorAsync()
        {
            string text = string.Empty;
            try
            {
                _app.Type<Form>()(new Async());
            }
            catch (FriendlyOperationException e)
            {
                text = e.Message;
            }
            Assert.AreEqual("インスタンスの生成時にはAsyncクラスを指定することはできません。", text);
        }

        /// <summary>
        /// Asyncを2つ渡すと例外が発生するテストです。
        /// </summary>
        [TestMethod]
        public void MethodAsync2()
        {
            string text = string.Empty;
            try
            {
                _app.Type<Control>().FromHandle(IntPtr.Zero, new Async(), new Async());
            }
            catch (FriendlyOperationException e)
            {
                text = e.Message;
            }
            Assert.AreEqual("引数に複数のAsyncが見つかりました。" + Environment.NewLine + 
                            "Asyncは一度の呼び出しで一つしか指定することができません。", 
                            text);
        }

        /// <summary>
        /// OperationTypeInfoを二つ渡すと例外が発生するテストです。
        /// </summary>
        [TestMethod]
        public void MethodOperationTypeInfo2()
        {
            string text = string.Empty;
            try
            {
                dynamic form = WindowControl.FromZTop(_app).AppVar.Dynamic();
                OperationTypeInfo op = new OperationTypeInfo((string)form.GetType().FullName, typeof(Control).FullName);
                string type = _app.Type().TestTarget.MainForm.OverLoadMethodStatic(null, op, op);
            }
            catch (FriendlyOperationException e)
            {
                text = e.Message;
            }
            Assert.AreEqual("引数に複数のOperationTypeInfoが見つかりました。" + Environment.NewLine +
                       "OperationTypeInfoは一度の呼び出しで一つしか指定することができません。",
                       text);
        }

        /// <summary>
        /// ネームスペースを引数として渡した場合のエラー文言
        /// </summary>
        [TestMethod]
        public void TestDynamicAppTypeArgumentNameSpace()
        {
            try
            {
                _app.Type<Control>().FromHandle(_app.Type().System.Windows.Forms);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("第1引数が不正です。\r\n「ネームスペース」もしくは「クラス名称」と推測されます。引数を生成した構文を確認してください。",
                    e.Message);
            }
        }

        /// <summary>
        /// クラス名称を引数としえ渡した場合のエラー文言
        /// </summary>
        [TestMethod]
        public void TestDynamicAppTypeArgumentClassName()
        {
            try
            {
                _app.Type<Control>().FromHandle(_app.Type().System.Windows.Forms.Control);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("第1引数が不正です。\r\n「ネームスペース」もしくは「クラス名称」と推測されます。引数を生成した構文を確認してください。",
                    e.Message);
            }
        }

        /// <summary>
        /// 不正な名前のネームスペースとして推測されるものを引数として渡した場合のエラー文言
        /// </summary>
        [TestMethod]
        public void TestDynamicAppTypeArgumentInvalidName()
        {
            try
            {
                _app.Type<Control>().FromHandle(IntPtr.Zero, _app.Type().SystemX);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("第2引数が不正です。\r\n「ネームスペース」もしくは「クラス名称」と推測されます。引数を生成した構文を確認してください。",
                    e.Message);
            }
        }
    }
}
