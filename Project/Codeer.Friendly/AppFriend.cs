using System;
using Codeer.Friendly.Inside;
using Codeer.Friendly.Inside.Protocol;
using Codeer.Friendly.Properties;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Codeer.Friendly
{
#if ENG
    /// <summary>
    /// It's a class for manipulating applications that have been Friendly enabled.
    /// Can declare variables and call static methods.
    /// Inherited classes are provided for each connection type.
    /// </summary>
#else
    /// <summary>
    /// Friendly接続可能なアプリケーションの操作クラスです。
    /// staticメソッド呼び出しと変数の宣言ができます。
    /// 抽象クラスで、接続先ごとに継承したクラスが用意されます。
	/// </summary>
#endif
    abstract public class AppFriend
    {
        readonly Dictionary<string, object> _systemCache = new Dictionary<string, object>();

#if ENG
        /// <summary>
        /// To Connect with Application.
        /// </summary>
#else
        /// <summary>
        /// アプリケーションとの接続クラスです。
		/// </summary>
#endif
        abstract protected IFriendlyConnector FriendlyConnector { get; }

#if ENG
        /// <summary>
        /// Acquires delegates for calling static operations in test target application.
        /// </summary>
        /// <param name="staticOperation">Selects by Namespace.Class.OperationName(Method, Property, Field)</param>
        /// <returns>A delegate for calling static operation in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内のstatic操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="staticOperation">ネームスペース.クラス名.操作名（メソッド、プロパティー、フィールド）の形式で指定します。</param>
        /// <returns>アプリケーション内のstatic操作を呼び出すdelegate。</returns>
#endif
        public FriendlyOperation this[string staticOperation]
        {
            get
            {
                return CreateFullNameFriendlyOperation(staticOperation, null, null);
            }
        }

#if ENG
        /// <summary>
        /// Acquires delegates for calling static operations in test target application.
        /// </summary>
        /// <param name="staticOperation">Selects by Namespace.Class.OperationName(Method, Property, Field)</param>
        /// <param name="async">An asynchronous execution.</param>
        /// <returns>A delegate for calling static operation in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内のstatic操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="staticOperation">ネームスペース.クラス名.操作名（メソッド、プロパティー、フィールド）の形式で指定します。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>アプリケーション内のstatic操作を呼び出すdelegate。</returns>
#endif
        public FriendlyOperation this[string staticOperation, Async async]
        {
            get
            {
                if (async == null)
                {
                    throw new ArgumentNullException("async");
                }
                return CreateFullNameFriendlyOperation(staticOperation, null, async);
            }
        }

#if ENG
        /// <summary>
        /// Acquires delegates for calling static operations in test target application.
        /// </summary>
        /// <param name="staticOperation">Selects by Namespace.Class.OperationName(Method, Property, Field)</param>
        /// <param name="operationTypeInfo">
        /// Operation type information.
        /// Used to call operation of the same name of a parent class when two or more overloads exist for the indicated operation.
        /// In many cases, an overload can be resolved using the parameters without specifying an OperationTypeInfo.
        /// </param>
        /// <returns>A delegate for calling static operation in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内のstatic操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="staticOperation">ネームスペース.クラス名.操作名（メソッド、プロパティー、フィールド）の形式で指定します。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。（オーバーロードの解決等に使用します。）</param>
        /// <returns>アプリケーション内のstatic操作を呼び出すdelegate。</returns>
#endif
        public FriendlyOperation this[string staticOperation, OperationTypeInfo operationTypeInfo]
        {
            get
            {
                if (operationTypeInfo == null)
                {
                    throw new ArgumentNullException("operationTypeInfo");
                }
                return CreateFullNameFriendlyOperation(staticOperation, operationTypeInfo, null);
            }
        }

#if ENG
        /// <summary>
        /// Acquires delegates for calling static operations in test target application.
        /// </summary>
        /// <param name="staticOperation">Selects by Namespace.Class.OperationName(Method, Property, Field)</param>
        /// <param name="operationTypeInfo">
        /// Operation type information.
        /// Used to call operation of the same name of a parent class when two or more overloads exist for the indicated operation.
        /// In many cases, an overload can be resolved using the parameters without specifying an OperationTypeInfo.
        /// </param>
        /// <param name="async">An asynchronous execution.</param>
        /// <returns>A delegate for calling static operation in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内のstatic操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="staticOperation">ネームスペース.クラス名.操作名（メソッド、プロパティー、フィールド）の形式で指定します。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。（オーバーロードの解決等に使用します。）</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>アプリケーション内のstatic操作を呼び出すdelegate。</returns>
#endif
        public FriendlyOperation this[string staticOperation, OperationTypeInfo operationTypeInfo, Async async]
        {
            get
            {
                if (operationTypeInfo == null)
                {
                    throw new ArgumentNullException("operationTypeInfo");
                }
                if (async == null)
                {
                    throw new ArgumentNullException("async");
                }
                return CreateFullNameFriendlyOperation(staticOperation, operationTypeInfo, async);
            }
        }

#if ENG
        /// <summary>
        /// Acquires delegates for calling static operations in test target application.
        /// </summary>
        /// <param name="type">Type information storing a static call.</param>
        /// <param name="operation">Name of the operation.</param>
        /// <returns>A delegate for calling static operation in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内のstatic操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="type">タイプ。</param>
        /// <param name="operation">操作名称。</param>
        /// <returns>アプリケーション内のstatic操作を呼び出すdelegate。</returns>
#endif
        public FriendlyOperation this[Type type, string operation]
        {
            get
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return new StaticOperationTalker(FriendlyConnector, type.FullName)[operation, null, null];
            }
        }

#if ENG
        /// <summary>
        /// Acquires delegates for calling static operations in test target application.
        /// </summary>
        /// <param name="type">Type information storing a static call.</param>
        /// <param name="operation">Name of the operation.</param>
        /// <param name="async">An asynchronous execution.</param>
        /// <returns>A delegate for calling static operation in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内のstatic操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="type">タイプ。</param>
        /// <param name="operation">操作名称。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>アプリケーション内のstatic操作を呼び出すdelegate。</returns>
#endif
        public FriendlyOperation this[Type type, string operation, Async async]
        {
            get
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return new StaticOperationTalker(FriendlyConnector, type.FullName)[operation, null, async];
            }
        }

#if ENG
        /// <summary>
        /// Acquires delegates for calling static operations in test target application.
        /// </summary>
        /// <param name="type">Type information storing a static call.</param>
        /// <param name="operation">Name of the operation.</param>
        /// <param name="operationTypeInfo">
        /// Operation type information.
        /// Used to call operation of the same name of a parent class when two or more overloads exist for the indicated operation.
        /// In many cases, an overload can be resolved using the parameters without specifying an OperationTypeInfo.
        /// </param>
        /// <returns>A delegate for calling static operation in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内のstatic操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="type">タイプ。</param>
        /// <param name="operation">操作名称。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。（オーバーロードの解決等に使用します。）</param>
        /// <returns>アプリケーション内のstatic操作を呼び出すdelegate。</returns>
#endif
        public FriendlyOperation this[Type type, string operation, OperationTypeInfo operationTypeInfo]
        {
            get
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return new StaticOperationTalker(FriendlyConnector, type.FullName)[operation, operationTypeInfo, null];
            }
        }

#if ENG
        /// <summary>
        /// Acquires delegates for calling static operations in test target application.
        /// </summary>
        /// <param name="type">Type information storing a static call.</param>
        /// <param name="operation">Name of the operation.</param>
        /// <param name="operationTypeInfo">
        /// Operation type information.
        /// Used to call operation of the same name of a parent class when two or more overloads exist for the indicated operation.
        /// In many cases, an overload can be resolved using the parameters without specifying an OperationTypeInfo.
        /// </param>
        /// <param name="async">An asynchronous execution.</param>
        /// <returns>A delegate for calling static operation in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内のstatic操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="type">タイプ。</param>
        /// <param name="operation">操作名称。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。（オーバーロードの解決等に使用します。）</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>アプリケーション内のstatic操作を呼び出すdelegate。</returns>
#endif
        public FriendlyOperation this[Type type, string operation, OperationTypeInfo operationTypeInfo, Async async]
        {
            get
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return new StaticOperationTalker(FriendlyConnector, type.FullName)[operation, operationTypeInfo, async];
            }
        }

#if ENG
        /// <summary>
        /// Declares null variables in test target applications.
        /// </summary>
        /// <returns>Variable in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内にnullの変数を宣言します。
		/// </summary>
        /// <returns>アプリケーション内変数。</returns>
#endif
        public AppVar Dim()
		{
            return Invoke(ProtocolType.VarInitialize, null, string.Empty, new object[] { null });
		}

#if ENG
        /// <summary>
        /// Declares a variable initialized with the specified object in the application under test.
        /// The reference of the selected object is serialized, transmitted, and deserialized, stored in the variable declared as an initial value, rather than being declaring a reference to the specified object within the application.
        /// </summary>
        /// <param name="obj">
        /// The initial value of the variable to be declared inside the application.
        /// It is necessary to be a type which can be serialized, or null.
        /// </param>
        /// <returns>Variable in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内に指定のオブジェクトで初期化された変数を宣言します。
        /// 指定のオブジェクトの参照がアプリケーション内に宣言されるのではなく、シリアライズされ、転送され、デシリアライズされたオブジェクトが初期値として宣言された変数に格納されます。
		/// </summary>
        /// <param name="obj">初期化オブジェクト（シリアライズ可能であること）。</param>
        /// <returns>アプリケーション内変数。</returns>
#endif
        public AppVar Dim(object obj)
		{
            if (obj == null)
            {
                return Dim();//特殊処理。空の変数宣言 Dim(NewInfo newInfo)と混同しないための処理。
            }
            return Invoke(ProtocolType.VarInitialize, null, string.Empty, new object[] { obj });
		}

#if ENG
        /// <summary>
        /// Declares a variable within the application under test using the specified information.
        /// </summary>
        /// <param name="newInfo">Object generation information</param>
        /// <returns>Variable in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内に指定の生成情報で生成されたオブジェクトを格納する変数を宣言します。
        /// </summary>
        /// <param name="newInfo">生成情報。</param>
        /// <returns>アプリケーション内変数。</returns>
#endif
        public AppVar Dim(NewInfo newInfo)
        {
            if (newInfo == null)
            {
                return Dim();//特殊処理。空の変数宣言 Dim(object obj)と混同しないための処理。
            }
            return Invoke(ProtocolType.VarNew, null, newInfo.TypeFullName, newInfo.Arguments);
        }

#if ENG
        /// <summary>
        /// Declares a variable within the application under test using the specified information.
        /// </summary>
        /// <param name="newInfo">Variable in the application</param>
        /// <param name="operationTypeInfo">
        /// Operation type information.
        /// Used to call operation of the same name of a parent class when two or more overloads exist for the indicated operation.
        /// In many cases, an overload can be resolved using the parameters without specifying an OperationTypeInfo.
        /// </param>
        /// <returns>Variable in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内に指定の生成情報で生成されたオブジェクトを格納する変数を宣言します。
        /// </summary>
        /// <param name="newInfo">生成情報。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。（オーバーロードの解決等に使用します。）</param>
        /// <returns>アプリケーション内変数。</returns>
#endif
        public AppVar Dim(NewInfo newInfo, OperationTypeInfo operationTypeInfo)
        {
            if (newInfo == null)
            {
                throw new ArgumentNullException("newInfo");
            }
            return Invoke(ProtocolType.VarNew, operationTypeInfo, newInfo.TypeFullName, newInfo.Arguments);
        }

#if ENG
        /// <summary>
        /// Add Application Control Infomation.
        /// Should not typically be used. 
        /// Used by implementations of this class or by libraries. 
        /// This Method is Thread Safe.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
#else
        /// <summary>
        /// アプリケーション制御情報追加。
        /// 通常利用しません。
        /// 主にライブラリを拡張する際に利用します。
        /// このメソッドはスレッドセーフです。
        /// </summary>
        /// <param name="key">キー。</param>
        /// <param name="value">値。</param>
#endif
        public void AddAppControlInfo(string key, object value)
        {
            lock (_systemCache)
            {
                _systemCache.Remove(key);
                _systemCache.Add(key, value);
            }
        }

#if ENG
        /// <summary>
        /// Get Application Control Infomation.
        /// Should not typically be used. 
        /// Used by implementations of this class or by libraries. 
        /// This Method is Thread Safe.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// アプリケーション制御情報取得。
        /// 通常利用しません。
        /// 主にライブラリを拡張する際に利用します。
        /// このメソッドはスレッドセーフです。
        /// </summary>
        /// <param name="key">キー。</param>
        /// <param name="value">値。</param>
        /// <returns>取得に成功したか。</returns>
#endif
        [SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate")]
        public bool TryGetAppControlInfo(string key, out object value)
        {
            lock (_systemCache)
            {
                return _systemCache.TryGetValue(key, out value);
            }
        }

        /// <summary>
        /// 実行。
		/// </summary>
        /// <param name="protocolType">プロトコルタイプ。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。（オーバーロードの解決等に使用します。）</param>
        /// <param name="typeFullName">タイプフルネーム。</param>
        /// <param name="args">引数。</param>
        /// <returns>変数。</returns>
        private AppVar Invoke(ProtocolType protocolType, OperationTypeInfo operationTypeInfo, string typeFullName, object[] args)
		{
            return FriendlyTalker.SendAndVarReceive(this, FriendlyConnector, protocolType, operationTypeInfo, null, typeFullName, string.Empty, args);
		}

        /// <summary>
        /// static呼び出しで型名称から操作名称までフルネームのFriendlyOperation生成。
        /// </summary>
        /// <param name="staticOperation">スタティック操作。</param>
        /// <param name="operationTypeInfo">操作タイプ確定情報。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>FriendlyOperation</returns>
        private FriendlyOperation CreateFullNameFriendlyOperation(string staticOperation, OperationTypeInfo operationTypeInfo, Async async)
        {
            if (staticOperation == null)
            {
                throw new ArgumentNullException("staticOperation");
            }
            int index = staticOperation.LastIndexOf('.');
            if (index == -1)
            {
                throw new FriendlyOperationException(Resources.ErrorInvalidStaticCall);
            }
            string typeFullName = staticOperation.Substring(0, index);
            string operation = staticOperation.Substring(index + 1);
            return new StaticOperationTalker(FriendlyConnector, typeFullName)[operation, operationTypeInfo, async];
        }
	}
}
