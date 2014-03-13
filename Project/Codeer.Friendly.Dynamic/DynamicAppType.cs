using System;
using System.Dynamic;
using Codeer.Friendly.Dynamic.Inside;
using Codeer.Friendly.Dynamic.Properties;
using System.Collections.Generic;

namespace Codeer.Friendly.Dynamic
{
#if ENG
    /// <summary>
    /// Allows accessing type-specific operations in the target application.
    /// Allows using the dynamic keyword to access static FriendlyOperations in a class just as you would access ordinary methods, properties, and fields.
    /// Also allows creating objects in the target application by using their class name.
    /// One limitation is that it is not possible to call static methods with the same name as those defined in the DynamicObject class.
    /// If you absolutely need to be able to do so, please use the FriendlyOperation APIs.
    /// </summary>
#else
    /// <summary>
    /// 対象アプリケーション内の型による操作に関するクラスです。
    /// static操作に対するFriendlyOperationをdynamicキーワードによって、
    /// 通常のメソッド、プロパティー、フィールド呼び出しのように扱うことができます。
    /// また、型名称を使って、対象アプリケーション内にオブジェクトを生成することもできます。
    /// 注意点として、DynamicObjectクラスに定義されている操作と同名のstatic操作を呼び出すことはできません。
    /// どうしても必要な場合は、旧来のFriendlyOperationで実行してください。
    /// </summary>
    /// <example>
    /// ・System.Windows.Forms.ControlクラスのFromHandleを呼び出します。
    ///   app.Type().System.Windows.Forms.Control.FromHandle(handle);
    ///   
    /// ・System.Windows.Forms.Controlクラスを生成します。
    ///   app.Type().System.Windows.Forms.Control();
    /// </example>
#endif
    public class DynamicAppType : DynamicObject, IDefinition
    {
        const string TypeCacheKey = "Codeer.Friendly.Dynamic.TypeCache";

        readonly AppFriend _app;
        readonly string _name;
        readonly bool _isType;

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">Application manipulation object.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
#endif
        public DynamicAppType(AppFriend app) : this(app, string.Empty) { }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">Application manipulation object.</param>
        /// <param name="name">Type full name.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="name">型名称。</param>
#endif
        public DynamicAppType(AppFriend app, string name)
        {
            _app = app;
            _name = name;
            _isType = IsTypeName(_app, name, false);
        }

#if ENG
        /// <summary>
        /// Dynamically resolves get access to properties and fields.
        /// Cannot be used asynchorously.
        /// If you need asynchronous access, please cast the object to an AppVar
        /// and use a FriendlyOperation, or call it in the form of a method by specifying a
        /// method name equal to the property name.
        /// </summary>
        /// <param name="binder">Binder.</param>
        /// <param name="result">Retrieved result.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// プロパティーへのアクセス(getter)の動的解決です。
        /// 非同期実行はできません。
        /// どうしても非同期実行が必要な場合はAppVarにキャストして、FriendlyOperationを使用するか、
        /// メソッド形式の呼び出しを実行してください。
        /// メソッド名称はプロパティー名称と同一です。
        /// </summary>
        /// <param name="binder">バインダー。</param>
        /// <param name="result">取得結果。</param>
        /// <returns>成否。</returns>
#endif
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string nextName = JointName(_name, binder.Name);

            if (!_isType || //現在の名前が型でないのなら、つなげる
                IsTypeName(_app, nextName, _isType)) //現在の名前が型でも、つなげた名前が型になるなら、つなげる。
            {
                dynamic next = new DynamicAppType(_app, nextName);
                result = next;
            }
            else
            {
                //staticなgetter呼び出し。
                result = _app[nextName]().Dynamic();
            }
            return true;
        }

#if ENG
        /// <summary>
        /// Dynamically resolves set access to properties and fields.
        /// Cannot be used asynchronously.
        /// If you need asynchronous access, please cast the object to an AppVar
        /// and use a FriendlyOperation, or call it in the form of a method by specifying a
        /// method name equal to the property name.
        /// </summary>
        /// <param name="binder">Binder.</param>
        /// <param name="value">Value to set.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// プロパティーへのアクセス(setter)の動的解決です。
        /// 非同期実行はできません。
        /// どうしても非同期実行が必要な場合はAppVarにキャストして、FriendlyOperationを使用するか、
        /// メソッド形式の呼び出しを実行してください。
        /// メソッド名称はプロパティー名称と同一です。
        /// </summary>
        /// <param name="binder">バインダー。</param>
        /// <param name="value">設定値。</param>
        /// <returns>成否。</returns>
#endif
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _app[JointName(_name, binder.Name)](value);
            return true;
        }

#if ENG
        /// <summary>
        /// Dynamically resolves method calls
        /// To specify Async and/or OperationTypeInfo, please pass these as arguments.
        /// They can be included in any order.
        /// </summary>
        /// <param name="binder">Binder.</param>
        /// <param name="args">Arguments.</param>
        /// <param name="result">Return value.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// メソッド実行の動的解決です。
        /// AsyncとOperationTypeInfoを指定する場合は、引数に渡してください。
        /// その順番はどこでも構いません。
        /// </summary>
        /// <param name="binder">バインダー。</param>
        /// <param name="args">引数。</param>
        /// <param name="result">戻り値。</param>
        /// <returns>成否。</returns>
#endif
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = InvokeMember(binder.Name, args);
            return true;
        }

#if ENG
        /// <summary>
        /// Dynamically resolves delegate types.
        /// When called on this class, calls the constructor in the target application and creates a class instance.
        /// </summary>
        /// <param name="binder">Binder.</param>
        /// <param name="args">Arguments.</param>
        /// <param name="result">Return value.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// delegate型の実行の動的解決です。
        /// このクラスで呼ばれた場合、対象アプリケーション内で、その型のコンストラクタを呼び出して、インスタンスを生成します。
        /// </summary>
        /// <param name="binder">バインダー。</param>
        /// <param name="args">引数。</param>
        /// <param name="result">戻り値。</param>
        /// <returns>成否。</returns>
#endif
        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            result = NewAppVar(_app, _name, args);
            return true;
        }

#if ENG
        /// <summary>
        /// Calls the static Equals() method, if one is defined.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Returned as a DynamicAppVar.</returns>
#else
        /// <summary>
        /// Equalsという名前のstaticメソッドがあれば、それを実行します。
        /// </summary>
        /// <param name="obj">オブジェクト。</param>
        /// <returns>戻り値。DynamicAppVarで返ります。</returns>
#endif
        public new dynamic Equals(object obj)
        {
            return InvokeMember("Equals", new object[] { obj });
        }

#if ENG
        /// <summary>
        /// Calls the static GetHashCode() method, if it is defined.
        /// </summary>
        /// <returns>Returned as a DynamicAppVar.</returns>
#else
        /// <summary>
        /// GetHashCodeという名前のstaticメソッドがあれば、それを実行します。
        /// </summary>
        /// <returns>戻り値。DynamicAppVarで返ります。</returns>
#endif
        public new dynamic GetHashCode()
        {
            return InvokeMember("GetHashCode", new object[0]);
        }

#if ENG
        /// <summary>
        /// Calls the static ToString() method, if it is defined.
        /// </summary>
        /// <returns>Returned as a DynamicAppVar.</returns>
#else
        /// <summary>
        /// ToStringという名前のstaticメソッドがあれば、それを実行します。
        /// </summary>
        /// <returns>戻り値。DynamicAppVarで返ります。</returns>
#endif
        public new dynamic ToString()
        {
            return InvokeMember("ToString", new object[0]);
        }

#if ENG
        /// <summary>
        /// Calls the static GetType() method, if it is defined.
        /// </summary>
        /// <returns>Type object returned as a DynamicAppVar.</returns>
#else
        /// <summary>
        /// GetTypeという名前のstaticメソッドがあれば、それを実行します。
        /// </summary>
        /// <returns>タイプ。DynamicAppVarで返ります。</returns>
#endif
        public new dynamic GetType()
        {
            return InvokeMember("GetType", new object[0]);
        }

#if ENG
        /// <summary>
        /// Calls the static MemberwiseClone() method, if it is defined.
        /// </summary>
        /// <returns>Returned as a DynamicAppVar.</returns>
#else
        /// <summary>
        /// MemberwiseCloneという名前のstaticメソッドがあれば、それを実行します。
        /// </summary>
        /// <returns>戻り値。DynamicAppVarで返ります。</returns>
#endif
        public new dynamic MemberwiseClone()
        {
            return InvokeMember("MemberwiseClone", new object[0]);
        }

        /// <summary>
        /// メソッド実行の動的解決です。
        /// AsyncとOperationTypeInfoを指定する場合は、引数に渡してください。
        /// その順番はどこでも構いません。
        /// </summary>
        /// <param name="name">名前。</param>
        /// <param name="args">引数。</param>
        /// <returns>戻り値。</returns>
        private dynamic InvokeMember(string name, object[] args)
        {
            string nextTypeName = JointName(_name, name);
            if (IsTypeName(_app, nextTypeName, _isType))
            {
                //型名称になれば、生成する。
                return NewAppVar(_app, nextTypeName, args);
            }
            else
            {
                //staticメソッドの呼び出し。
                Async async;
                OperationTypeInfo typeInfo;
                args = DynamicFriendlyOperationUtility.ResolveArguments(args, out async, out typeInfo);
                return DynamicFriendlyOperationUtility.GetFriendlyOperation(_app, nextTypeName, async, typeInfo)(args).Dynamic();
            }
        }

        /// <summary>
        /// 対象アプリケーション内にインスタンス生成。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="typeFullName">生成する型のフルネーム。</param>
        /// <param name="args">引数。</param>
        /// <returns>生成したインスタンスを操作するためのDynamicAppVar。</returns>
        private static dynamic NewAppVar(AppFriend app, string typeFullName, object[] args)
        {
            Async async;
            OperationTypeInfo typeInfo;
            args = DynamicFriendlyOperationUtility.ResolveArguments(args, out async, out typeInfo);
            if (async != null)
            {
                throw new FriendlyOperationException(Resources.ErrorInstanceCreateCantUseAsync);
            }
            return (typeInfo == null) ? app.Dim(new NewInfo(typeFullName, args)).Dynamic() : 
                                            app.Dim(new NewInfo(typeFullName, args), typeInfo).Dynamic();
        }

        /// <summary>
        /// 名前の結合。
        /// </summary>
        /// <param name="name1">名前1。</param>
        /// <param name="name2">名前2。</param>
        /// <returns>結合された名前。</returns>
        private static string JointName(string name1, string name2)
        {
            return string.IsNullOrEmpty(name1) ? name2 : name1 + "." + name2;
        }

        /// <summary>
        /// タイプの名前であるか。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="name">名前。</param>
        /// <param name="cacheNotType">タイプでなくてもキャッシュする。</param>
        /// <returns>タイプの名前であるか。</returns>
        private static bool IsTypeName(AppFriend app, string name, bool cacheNotType)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            //初回登録時にマルチスレッドでアクセスされた場合
            //初回のキャッシュ登録が無駄になるが、許容する
            object typeCache;
            if (!app.TryGetAppControlInfo(TypeCacheKey, out typeCache))
            {
                typeCache = new TypeChahe(app);
                app.AddAppControlInfo(TypeCacheKey, typeCache);
            }
            lock (typeCache)
            {
                return ((TypeChahe)typeCache).IsTypeName(app, name, cacheNotType);
            }
        }
    }
}
