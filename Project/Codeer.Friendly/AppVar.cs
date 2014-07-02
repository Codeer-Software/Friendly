#define CODE_ANALYSIS
using System;
using Codeer.Friendly.Inside;
using Codeer.Friendly.Inside.Protocol;
using System.Diagnostics.CodeAnalysis;
using Codeer.Friendly.Properties;

namespace Codeer.Friendly
{
#if ENG
    /// <summary>
    /// Represents variables in the target application.
    /// Operates properies, fields, methods.
    /// Can acquire variable values from the test process, and set values to the test process.
    /// When the variables in application support iterative processes (In case of .Net, it inherits IEnumerator）,
    /// can use foreach to iterate through them.
    /// As long as an instance of this class remains in the test process, the underlying object in the target application will not be marked for garbage collection.
    /// Timing of the garbage collection within the test process is not affected by the memory state of the product process.
    /// If needed, use methods such as System.GC's Collect() or WaitForPendingFinalizers() to trigger garbage collection and delete instances from the test process.
    /// You can also use Dispose() to release a resource immediately.
    /// </summary>
#else
    /// <summary>
    /// アプリケーション内部の変数クラスです。
    /// プロパティー、フィールド、メソッドの操作ができます。
    /// また、変数の中の値をテストプロセス側へ取得したりテストプロセス側の値を設定したりもできます。
    /// さらに、アプリケーション内の変数が繰り返し処理に対応している場合（.NetならIEnumeratorを継承している場合）foreachが使用できます。
    /// このクラスのインスタンスがテストプロセスに残っている限り、対象プロセス内部のインスタンスもガベージコレクションの対象になりません。
    /// テストプロセスのガベージコレクション実施のタイミングはプロダクトプロセスのメモリ状態とは無関係です。
    /// 必要があれば、System.GCクラスのCollectメソッドやWaitForPendingFinalizersのような
    /// ガベージコレクションを促進するメソッドを使用してテストプロセスからこのインスタンスを削除してください。
    /// また、直ちに解放したい場合はDisposeメソッドを使用してください。
	/// </summary>
#endif
    public class AppVar : OperationTalker, IDisposable
	{
        readonly VarAddress _varAddress;
        readonly IFriendlyConnector _friendlyConnector;
        bool _disposed;

        /// <summary>
        /// 変数アドレス。
        /// </summary>
        internal VarAddress VarAddress { get { return _varAddress; } }

        /// <summary>
        /// アプリケーションとの接続クラス。
        /// </summary>
        internal override IFriendlyConnector FriendlyConnector { get { return _friendlyConnector; } }

#if ENG
        /// <summary>
        /// Returns the associated application manipulation object.
        /// </summary>
#else
        /// <summary>
        /// アプリケーション操作クラスを取得します。
        /// </summary>
#endif
        public AppFriend App { get { return _friendlyConnector.App; } }

#if ENG
        /// <summary>
        /// Get that variables in the target application is null.
        /// </summary>
#else
        /// <summary>
        /// アプリケーション内変数がnullであるかを取得します。
        /// </summary>
#endif
        public bool IsNull { get { return (bool)SendAndValueReceive(ProtocolType.IsEmptyVar, null, string.Empty, new object[] { this }); } }

		/// <summary>
        /// コンストラクタ。
		/// </summary>
        /// <param name="friendlyConnector">アプリケーションとの接続クラス。</param>
        /// <param name="varAddress">変数アドレス</param>
        internal AppVar(IFriendlyConnector friendlyConnector, VarAddress varAddress)
		{
            _friendlyConnector = friendlyConnector;
            _varAddress = varAddress;
		}
		
		/// <summary>
        /// ファイナライザ。
		/// </summary>
        ~AppVar()
        {
            Dispose(false);
        }

#if ENG
        /// <summary>
        /// Releases the application's variable from management. 
        /// After calling this method, the variable of this AppVar cannot be accessed. 
        /// In many cases, it is not necessary to call this explicitly.
        /// If needed, please use methods such as System.GC's Collect() or WaitForPendingFinalizers()
        /// to trigger garbage collection. 
        /// Please use this, for example, when the variable inside the application uses a lot of memory.
        /// </summary>
#else
        /// <summary>
        /// アプリケーション内部の変数を管理から削除します。
        /// このメソッドを呼び出した後、そのAppVarの変数にはアクセスできません。
        /// このメソッドはファイナライザからも実施されます。
        /// 多くの場合、これを明示的に呼び出す必要はありません。
        /// 必要があれば、System.GCクラスのCollectメソッドやWaitForPendingFinalizersのような
        /// ガベージコレクションを促進するメソッドを利用してください。
        /// アプリケーション内部の変数が特別に大きなメモリの場合など、特殊な場合に利用してください。
        /// </summary>
#endif
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

#if ENG
        /// <summary>
        /// Despose.
        /// </summary>
        /// <param name="disposing">flag.</param>
#else
        /// <summary>
        /// 破棄。
        /// </summary>
        /// <param name="disposing">破棄フラグ。</param>
#endif
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            //ファイナライザのタイミングによっては相手アプリケーションが存在しないことは十分にありうる。
            try
            {
                SendAndReceive(ProtocolType.BinOff, null, string.Empty, new object[0]);
            }
            catch { }
            _disposed = true;
        }

#if ENG
        /// <summary>
        /// Acquires a delegate which can call operations on variables in the test target application.
        /// </summary>
        /// <param name="operation">Operation name.</param>
        /// <returns>Delegate for executing operations.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内の変数の操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="operation">操作名称。</param>
        /// <returns>操作実行delegate。</returns>
#endif
        public FriendlyOperation this[string operation]
        {
            get
            { 
                return (new FriendlyOperationOwner(this, null, operation)).FriendlyOperation;
            }
        }

#if ENG
        /// <summary>
        /// Acquires a delegate which can call operations on variables in the test target application.
        /// </summary>
        /// <param name="operation">Operation name.</param>
        /// <param name="async">Object for asynchronous execution. </param>
        /// <returns>Delegate for executing operations.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内の変数の操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="operation">操作名称。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>操作実行delegate。</returns>
#endif
        public FriendlyOperation this[string operation, Async async] 
        {
            get
            {
                if (async == null)
                {
                    throw new ArgumentNullException("async");
                }
                async.Initialize(this);
                return (new AsyncFriendlyOperationOwner(async, null, operation)).FriendlyOperation;
            } 
        }

#if ENG
        /// <summary>
        /// Acquires a delegate which can call operations on variables in the test target application.
        /// </summary>
        /// <param name="operation">Operation name.</param>
        /// <param name="operationTypeInfo">
        /// Operation type information.
        /// Used to differentiate between multiple overloads or to target an operation with the same name within a parent class. 
        /// Overloads can often be resolved by their parameters without specifying an OperationTypeInfo.
        /// </param>
        /// <returns>Delegate for executing operations.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内の変数の操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="operation">操作名称。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。（オーバーロードの解決等に使用します。）</param>
        /// <returns>操作実行delegate。</returns>
#endif
        public FriendlyOperation this[string operation, OperationTypeInfo operationTypeInfo] 
        {
            get
            {
                return (new FriendlyOperationOwner(this, operationTypeInfo, operation)).FriendlyOperation; 
            }
        }

#if ENG
        /// <summary>
        /// Acquires a delegate which can call operations on variables in the test target application.
        /// </summary>
        /// <param name="operation">Operation name.</param>
        /// <param name="operationTypeInfo">
        /// Operation type information.
        /// Used to differentiate between multiple overloads or to target an operation with the same name within a parent class. 
        /// Overloads can often be resolved by their parameters without specifying an OperationTypeInfo.
        /// </param>
        /// <param name="async">Object for asynchronous execution. </param>
        /// <returns>Delegate for executing operations.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内の変数の操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="operation">操作名称。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。（オーバーロードの解決等に使用します。）</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>操作実行delegate。</returns>
#endif
        public FriendlyOperation this[string operation, OperationTypeInfo operationTypeInfo, Async async] 
        {
            get
            {
                if (async == null)
                {
                    throw new ArgumentNullException("async");
                }
                async.Initialize(this);
                return (new AsyncFriendlyOperationOwner(async, operationTypeInfo, operation)).FriendlyOperation; 
            }
        }

#if ENG
        /// <summary>
        /// Serializes a variable and passes it to the application under test or sends a value to it. 
        /// Can only be used when the object can be serialized. 
        /// AppVar can also be assigned to the setter.
        /// </summary>
#else
        /// <summary>
        /// 変数をシリアライズして、アプリケーションからテストプロセスへ取得もしくは、
        /// テストプロセスからアプリケーションへ設定します。
        /// このプロパティー指定するオブジェクトがシリアライズ可能な場合のみ使用可能です。
        /// setterにはAppVarも指定可能です。
		/// </summary>
#endif
        public object Core
		{
			get
			{
				return SendAndValueReceive(ProtocolType.GetValue, null, string.Empty, new object[0]);
			}
			set
			{
                SendAndReceive(ProtocolType.SetValue, null, string.Empty, new object[] { value });
			}
		}

#if ENG
        /// <summary>
        /// Equivalence comparison. 
        /// Carries out an equivalence comparison against the variable in the application.
        /// </summary>
        /// <param name="obj">
        /// A candidate for the comparison. 
        /// AppVar can also be specified. 
        /// Please refer to sample code. 
        /// </param>
        /// <returns>true is returned when the values are equal. </returns>
#else
        /// <summary>
        /// 等価比較。
        /// 操作対象アプリケーション内部で実施した結果を返します。
		/// </summary>
        /// <param name="obj">オブジェクト。</param>
        /// <returns>比較結果。</returns>
#endif
        public override bool Equals(object obj)
		{
			return (bool)this["Equals"](obj).Core;
		}

#if ENG
        /// <summary>
        /// Acquires a hash code of the variable in the application.
        /// </summary>
        /// <returns>Hash code of the variable in the application.</returns>
#else
        /// <summary>
        /// ハッシュコード取得。
        /// 操作対象アプリケーション内部で実施した結果を返します。
		/// </summary>
		/// <returns>ハッシュコード。</returns>
#endif
        public override int GetHashCode()
		{
			return (int)this["GetHashCode"]().Core;
		}

#if ENG
        /// <summary>
        /// Produces a string value for the object in the application.
        /// </summary>
        /// <returns>String representing the object in the application.</returns>
#else
        /// <summary>
        /// 文字列変換。
        /// 操作対象アプリケーション内部で実施した結果を返します。
		/// </summary>
        /// <returns>文字列。</returns>
#endif
        public override string ToString()
		{
			return (string)this["ToString"]().Core;
		}

        /// <summary>
        /// 戻り値をAppVarで取得する通信。
        /// </summary>
        /// <param name="protocolType">通信タイプ。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。</param>
        /// <param name="operation">操作名称。</param>
        /// <param name="arguments">引数。</param>
        /// <returns>変数。</returns>
        internal override AppVar SendAndReceive(ProtocolType protocolType, OperationTypeInfo operationTypeInfo, string operation, object[] arguments)
        {
            if (_disposed)
            {
                throw new FriendlyOperationException(Resources.ErrorDisposedObject);
            }
            return FriendlyTalker.SendAndVarReceive(this, _friendlyConnector, protocolType, operationTypeInfo, _varAddress, string.Empty, operation, arguments);
        }

        /// <summary>
        /// 戻り値を値で取得する通信処理。
        /// </summary>
        /// <param name="protocolType">通信タイプ。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。</param>
        /// <param name="operation">操作名称。</param>
        /// <param name="arguments">引数。</param>
        /// <returns>値。</returns>
        internal override object SendAndValueReceive(ProtocolType protocolType, OperationTypeInfo operationTypeInfo, string operation, object[] arguments)
        {
            if (_disposed)
            {
                throw new FriendlyOperationException(Resources.ErrorDisposedObject);
            }
            return FriendlyTalker.SendAndValueReceive(this, _friendlyConnector, protocolType, operationTypeInfo, _varAddress, string.Empty, operation, arguments);
        }
	}
}
