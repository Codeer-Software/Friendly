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
    public class AppVarTest
    {
        WindowsAppFriend _app;
        dynamic _form;

        /// <summary>
        /// テスト初期化。
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _app = new WindowsAppFriend(Process.Start(TargetPath.TestTargetPath), "4.0");
            _form = WindowControl.FromZTop(_app).AppVar.Dynamic();
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
        /// プロパティーの設定、取得のテストです。
        /// </summary>
        [TestMethod]
        public void PropertyTest()
        {
            _form.Text = "abc";
            Assert.AreEqual("abc", (string)_form.Text);
        }

        /// <summary>
        /// プロパティーをメソッド形式で呼び出すテストです。
        /// </summary>
        [TestMethod]
        public void PropertyMethodCall()
        {
            _form.Text("abc");
            Assert.AreEqual("abc", (string)_form.Text());
        }

        /// <summary>
        /// プロパティーをメソッド形式で呼び出し、非同期実行させるテストです。
        /// </summary>
        [TestMethod]
        public void PropertyMethodCallAsync()
        {
            Async async = new Async();
            _form.Text("abc", async);
            async.WaitForCompletion();
            async = new Async();
            dynamic text = _form.Text(async);
            async.WaitForCompletion();
            Assert.AreEqual("abc", (string)text);
        }

        /// <summary>
        /// フィールドの設定取得のテストです。
        /// </summary>
        [TestMethod]
        public void FieldTest()
        {
            _form._field = 5;
            Assert.AreEqual(5, (int)_form._field);
        }

        /// <summary>
        /// メンバへの設定時にDynamicAppVarを使うテストです。
        /// </summary>
        [TestMethod]
        public void MemberSetDynamicAppVar()
        {
            _form._field = _app.Copy(5);
            Assert.AreEqual(5, (int)_form._field);
        }

        /// <summary>
        /// メンバへの設定時にAppVarを使うテストです。
        /// </summary>
        [TestMethod]
        public void MemberSetAppVar()
        {
            _form._field = _app.Dim(5);
            Assert.AreEqual(5, (int)_form._field);
        }

        /// <summary>
        /// 配列へのインデックスアクセスのテストです。
        /// </summary>
        [TestMethod]
        public void ArrayIndexAccess()
        {
            dynamic array = _app.Dim(new int[] { 0, 1, 2 }).Dynamic();
            array[1] = 100;
            Assert.AreEqual(100, (int)array[1]);
        }

        /// <summary>
        /// 配列へのインデックスアクセスのメソッド形式での非同期呼び出しのテストです。
        /// </summary>
        [TestMethod]
        public void ArrayIndexAccessMethodAsync()
        {
            dynamic array = _app.Dim(new int[] { 0, 1, 2 }).Dynamic();
            Async async = new Async();
            array.Set(1, 100, async);
            async.WaitForCompletion();
            async = new Async();
            dynamic value = array.Get(1, async);
            async.WaitForCompletion();
            Assert.AreEqual(100, (int)value);
        }

        /// <summary>
        /// 配列以外へのオブジェクトへのインデックスアクセスのテストです。
        /// </summary>
        [TestMethod]
        public void DictionaryIndexAccess()
        {
            dynamic dic = _app.Dim(new Dictionary<int, string>()).Dynamic();
            dic.Add(100, "百");
            dic[100] = "hundred";
            Assert.AreEqual("hundred", (string)dic[100]);
        }

        /// <summary>
        /// 配列以外へのオブジェクトへのインデックスアクセスのテストです。
        /// </summary>
        [TestMethod]
        public void DictionaryIndexAccessMethodAsync()
        {
            dynamic dic = _app.Dim(new Dictionary<int, string>()).Dynamic();
            dic.Add(100, "百");
            Async async = new Async();
            dic.set_Item(async, 100, "hundred");
            async.WaitForCompletion();
            async = new Async();
            dynamic value = dic.get_Item(async, 100);
            async.WaitForCompletion();
            Assert.AreEqual("hundred", (string)value);
        }

        /// <summary>
        /// インデックスアクセスでの設定で、DynamicAppVarを使うテストです。
        /// </summary>
        [TestMethod]
        public void IndexAccessSetDynamicAppVar()
        {
            dynamic array = _app.Dim(new int[] { 0, 1, 2 }).Dynamic();
            array[1] = _app.Copy(100);
            Assert.AreEqual(100, (int)array[1]);
        }

        /// <summary>
        /// インデックスアクセスでの設定で、AppVarを使うテストです。
        /// </summary>
        [TestMethod]
        public void IndexAccessSetAppVar()
        {
            dynamic array = _app.Dim(new int[] { 0, 1, 2 }).Dynamic();
            array[1] = _app.Dim(100);
            Assert.AreEqual(100, (int)array[1]);
        }

        /// <summary>
        /// 二次元配列の操作テストです。
        /// </summary>
        [TestMethod]
        public void TwoDimensionsArray()
        {
            dynamic array = _app.Dim(new int[3, 3]).Dynamic();
            array[1, 2] = 100;
            Assert.AreEqual(100, (int)array[1, 2]);
        }

        /// <summary>
        /// 二次元配列の操作テストです。
        /// </summary>
        [TestMethod]
        public void TwoDimensionsArrayMethodAccess()
        {
            dynamic array = _app.Dim(new int[3, 3]).Dynamic();
            array.Set(1, 2, 100);
            Assert.AreEqual(100, (int)array.Get(1, 2));
        }

        /// <summary>
        /// 配列以外へのオブジェクトでの二次元インデックスアクセスのテストです。
        /// </summary>
        [TestMethod]
        public void TwoIndexAccess()
        {
            _form["a", "b"] = 100;
            Assert.AreEqual(100, (int)_form["a", "b"]);
        }


        /// <summary>
        /// 配列以外へのオブジェクトでの二次元インデックスアクセスのテストです。
        /// </summary>
        [TestMethod]
        public void TwoIndexMethodAccess()
        {
            _form.set_Item("a", "b", 100);
            Assert.AreEqual(100, (int)_form.get_Item("a", "b"));
        }

        /// <summary>
        /// メソッド呼び出しのテストです。
        /// </summary>
        [TestMethod]
        public void MethodTest()
        {
            Assert.AreEqual(3, (int)_form.InstanceMethod("3"));
        }

        /// <summary>
        /// メソッド呼び出しで引数にDynamicAppVarを使うテストです。
        /// </summary>
        [TestMethod]
        public void MethodArgsDynamicAppVar()
        {
            Assert.AreEqual(3, (int)_form.InstanceMethod(_app.Copy("3")));
        }

        /// <summary>
        /// メソッド呼び出しで引数にAppVarを使うテストです。
        /// </summary>
        [TestMethod]
        public void MethodArgsAppVar()
        {
            Assert.AreEqual(3, (int)_form.InstanceMethod(_app.Dim("3")));
        }

        /// <summary>
        /// 非同期実行のテストです。
        /// </summary>
        [TestMethod]
        public void MethodAsync()
        {
            dynamic nextForm = _app.Dim(new NewInfo("TestTarget.MainForm")).Dynamic();
            WindowControl currentTop = WindowControl.FromZTop(_app);
            Async async = new Async();
            nextForm.ShowDialog(async);
            nextForm.Close();
            async.WaitForCompletion();
        }

        /// <summary>
        /// OperationTypeInfoを使うテストです。
        /// </summary>
        [TestMethod]
        public void MethodOperationTypeInfo()
        {
            OperationTypeInfo op = new OperationTypeInfo((string)_form.GetType().FullName, typeof(Control).FullName);
            string type = _form.OverLoadMethod(null, op);
            Assert.AreEqual(typeof(Control).FullName, type);
        }

        /// <summary>
        /// AppVarへのキャストテストです。
        /// </summary>
        [TestMethod]
        public void CastAppVar()
        {
            AppVar appVar = (AppVar)_form;
            appVar["Text"]("abc");
            Assert.AreEqual("abc", appVar["Text"]().Core);
        }

        /// <summary>
        /// IDisposableへのキャストテストです。
        /// </summary>
        [TestMethod]
        public void CastIDisposable()
        {
            IDisposable disposable = _form;
            disposable.Dispose();
            try
            {
                _form.Text = "abc";
                Assert.Fail();
            }
            catch { }
        }

        /// <summary>
        /// IEnumerableへのキャストのテストです。
        /// </summary>
        [TestMethod]
        public void CastIEnumerable()
        {
            int[] src = new int[] { 0, 1, 2 };
            List<int> list = new List<int>();
            foreach (dynamic element in _app.Copy(src))
            {
                list.Add((int)element);
            }
            int[] dst = list.ToArray();
            Assert.AreEqual(src.Length, dst.Length);
            Assert.AreEqual(src[0], dst[0]);
            Assert.AreEqual(src[1], dst[1]);
            Assert.AreEqual(src[2], dst[2]);
        }

        /// <summary>
        /// Equalsのテスト
        /// </summary>
        [TestMethod]
        public void EqualsTest()
        {
            dynamic form1 = _app.Type<Application>().OpenForms[0];
            dynamic form2 = _app.Type<Application>().OpenForms[0];
            Assert.IsTrue(form1.Equals(form2));
            Assert.AreEqual(form1, form2);
        }

        /// <summary>
        /// GetHashCodeのテスト
        /// </summary>
        [TestMethod]
        public void GetHashCodeTest()
        {
            dynamic val = _app.Copy(10);
            Assert.AreEqual(10, val.GetHashCode());
        }

        /// <summary>
        /// ToStringのテスト
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            dynamic val = _app.Copy(10);
            Assert.AreEqual("10", val.ToString());
        }

        /// <summary>
        /// GetTypeのテスト
        /// </summary>
        [TestMethod]
        public void GetTypeTest()
        {
            Assert.AreEqual("TestTarget.MainForm", _form.GetType().ToString());
        }

        /// <summary>
        /// Memberwiseのテスト
        /// </summary>
        [TestMethod]
        public void MemberwiseCloneTest()
        {
            dynamic obj = _app.Type().TestTarget.CloneTest();
            obj.Value = 1;
            dynamic clone = obj.MemberwiseClone();
            clone.Value = 2;
            Assert.AreEqual(1, (int)obj.Value);
            Assert.AreEqual(2, (int)clone.Value);
        }

        /// <summary>
        /// 旧インターフェイスとの互換性テスト
        /// </summary>
        [TestMethod]
        public void OldInterfaceCompatible()
        {
            AppVar appVar = _app.Type<Application>().OpenForms[0];
            appVar["Text"](_app.Copy("OldInterfaceCompatible"));
            Assert.AreEqual("OldInterfaceCompatible", (string)appVar.Dynamic().Text);
        }

        /// <summary>
        /// DynamicAppVarがさらにDynamic拡張されないことのテスト
        /// </summary>
        [TestMethod]
        public void IAppVarSelfNotExtensionsTest()
        {
            dynamic form = _app.Type<Application>().OpenForms[0];
            try
            {
                form.Dynamic();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("[型 : MainForm][操作 : Dynamic()]\r\n指定の操作が見つかりませんでした。",
                    e.Message);
            }
        }

        /// <summary>
        /// IAppVarOwnerを実装すると、Dynamic拡張が使えるテストのためのクラス
        /// </summary>
        class AppVarOwnerTest : IAppVarOwner
        {
            public AppVar AppVar { get; set; }
        }

        /// <summary>
        /// IAppVarOwnerClearlyを実装すると、Dynamic拡張が使えるテスト
        /// </summary>
        [TestMethod]
        public void IAppVarOwnerExtensionsTest()
        {
            AppVarOwnerTest v = new AppVarOwnerTest() { AppVar = _app.Type<Application>().OpenForms[0] };
            v.Dynamic().Text = "IAppVarOwnerExtensionsTest";
            Assert.AreEqual("IAppVarOwnerExtensionsTest", (string)v.Dynamic().Text);
        }

        /// <summary>
        /// WindowControlがDynamic()拡張できているテスト
        /// </summary>
        [TestMethod]
        public void WindowControlExtensionsTest()
        {
            WindowControl form = WindowControl.FromZTop(_app);
            form.Dynamic().Text = "WindowControlExtensionsTest";
            Assert.AreEqual("WindowControlExtensionsTest", (string)form.Dynamic().Text);
        }

        /// <summary>
        /// WindowControlを引数に渡せるようになっているテスト
        /// </summary>
        [TestMethod]
        public void WindowControlArgumentTest()
        {
            WindowControl form = WindowControl.FromZTop(_app);
            Assert.IsTrue((bool)_app.Type<object>().ReferenceEquals(form, _app.Type<Control>().FromHandle(form.Handle)));
        }
    }
}
