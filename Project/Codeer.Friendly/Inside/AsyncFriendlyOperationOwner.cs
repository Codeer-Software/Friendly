namespace Codeer.Friendly.Inside
{
    /// <summary>
    /// 非同期操作保持クラス。
    /// </summary>
    internal class AsyncFriendlyOperationOwner
    {
        Async _async;
        string _operation;
        OperationTypeInfo _operationTypeInfo;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="async">非同期実行クラス。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。（オーバーロードの解決等に使用します。）</param>
        /// <param name="operation">操作名称。</param>
        internal AsyncFriendlyOperationOwner(Async async, OperationTypeInfo operationTypeInfo, string operation)
        {
            _async = async;
            _operationTypeInfo = operationTypeInfo;
            _operation = operation;
        }

        /// <summary>
        /// 操作呼び出し。
        /// </summary>
        /// <param name="args">引数。</param>
        /// <returns>戻り値。</returns>
        internal AppVar FriendlyOperation(params object[] args)
        {
            if (args == null)
            {
                args = new object[] { null };
            }
            return _async.Invoke(_operationTypeInfo, _operation, args);
        }
    }
}

