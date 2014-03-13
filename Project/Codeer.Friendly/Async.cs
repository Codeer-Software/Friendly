using System;
using System.Collections.Generic;
using Codeer.Friendly.Inside;
using Codeer.Friendly.Inside.Protocol;
using Codeer.Friendly.Properties;
using System.Threading;

namespace Codeer.Friendly
{
#if ENG
    /// <summary>
    /// Class for executing asynchronous operations in the target application of AppVar and AppFriends. 
    /// When the operation has a return value or ref or out arguments, the values for these are stored in this class' object after the operation has finished.
    /// </summary>
#else
    /// <summary>
    /// AppVar,AppFriendの対象アプリケーションへの操作を非同期で実行するためのクラスです。
    /// 操作に戻り値やref,outの引数がある場合、非同期実行完後、変数にそれぞれ値が格納されています。
    /// </summary>
#endif
    public class Async
	{
        OperationTalker _operationTalker;
		AppVar _completedResult;
        bool _completed;

#if ENG
        /// <summary>
        /// Indicates whether the operation has finished.
        /// </summary>
#else
        /// <summary>
        /// 操作が完了したかを取得します。
        /// </summary>
#endif
        public bool IsCompleted
		{
			get
			{
                if (_completed)
                {
                    return true;
                }
                if (_completedResult == null)
                {
                    return false;
                }
                return !(bool)_operationTalker.SendAndValueReceive(ProtocolType.IsEmptyVar, null, string.Empty, new object[] { _completedResult });
			}
		}

#if ENG
        /// <summary>
        /// Provides any exception that occurred during the execution. 
        /// Returns null if the operation has not yet completed or if there was no exception. 
        /// </summary>
#else
        /// <summary>
        /// 実行中例外が発生していれば、例外を取得します。
        /// 発生していない場合、もしくは操作がまだ完了していない場合はnullが返ります。
        /// </summary>
#endif
        public Exception ExecutingException
        {
            get
            {
                if (!IsCompleted)
                {
                    return null;
                }
                ReturnInfo info = (ReturnInfo)_completedResult.Core;
                if (info.Exception == null)
                {
                    return null;
                }
                return new FriendlyOperationException(info.Exception);
            }
        }

#if ENG
        /// <summary>
        /// Constractor.
        /// </summary>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
#endif
        public Async() { }

#if ENG
        /// <summary>
        /// Waits until the operation has finished.
        /// </summary>
#else
        /// <summary>
        /// 操作が完了するまで待ちます。
        /// </summary>
#endif
        public void WaitForCompletion()
        {
            while (!IsCompleted)
            {
                Thread.Sleep(10);
            }
        }

#if ENG
        /// <summary>
        /// Marks the operation as complete. 
        /// Should not typically be used. 
        /// Used by implementations of this class or by libraries. 
        /// </summary>
#else
        /// <summary>
        /// 完了を設定します。
        /// 通常は使用しません。
        /// ライブラリ実装者が使用します。
        /// </summary>
#endif
        public void SetCompleted()
        {
            //既に設定されている場合は呼び出してはならない。
            if (_completed)
            {
                throw new FriendlyOperationException(Resources.ErrorInvalidCompleted);
            }

            //既に通常使用されている場合は呼び出してはならない。
            if (_completedResult != null)
            {
                throw new FriendlyOperationException(Resources.ErrorInvalidCompleted);
            }
            _completed = true;
        }

        /// <summary>
        /// 初期化。
        /// </summary>
        /// <param name="operationTalker">操作通信クラス。</param>
        internal void Initialize(OperationTalker operationTalker)
		{
            //外部から完了をセットされた場合は呼び出し禁止。
            if (_completed)
            {
                throw new FriendlyOperationException(Resources.ErrorAsyncDuplicativeCall);
            }

            //再呼び出しは禁止。
            if (_completedResult != null)
            {
                throw new FriendlyOperationException(Resources.ErrorAsyncDuplicativeCall);
            }
            _operationTalker = operationTalker;
		}

        /// <summary>
        /// 非同期操作呼び出し。
        /// </summary>
        /// <param name="operationTypeInfo">操作タイプ情報。（オーバーロードの解決等に使用します。）</param>
        /// <param name="operation">操作。</param>
        /// <param name="arguments">引数。</param>
        /// <returns>戻り値。</returns>
        internal AppVar Invoke(OperationTypeInfo operationTypeInfo, string operation, object[] arguments)
		{
            //完了情報格納バッファ宣言
            _completedResult = _operationTalker.SendAndReceive(ProtocolType.AsyncResultVarInitialize, null, string.Empty, new object[] { null });

            //第一引数に入れて渡す。
			List<object> arg = new List<object>();
			arg.Add(_completedResult);
			arg.AddRange(arguments);

            //呼び出し。
            AppVar returnValue = _operationTalker.SendAndReceive(ProtocolType.AsyncOperation, operationTypeInfo, operation, arg.ToArray());
            GC.KeepAlive(_operationTalker);

            //戻り値格納変数を返す。
            return returnValue;
		}
	}
}
