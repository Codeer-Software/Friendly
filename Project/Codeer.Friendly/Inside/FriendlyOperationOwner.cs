using Codeer.Friendly.Inside.Protocol;

namespace Codeer.Friendly.Inside
{
    /// <summary>
    /// 操作保持クラス。
    /// </summary>
    class FriendlyOperationOwner
    {
        OperationTalker _operationTalker;
        OperationTypeInfo _operationTypeInfo;
        string _operation;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="talker">操作通信社</param>
        /// <param name="operationTypeInfo">操作タイプ情報。</param>
        /// <param name="operation">操作名称。</param>
        internal FriendlyOperationOwner(OperationTalker talker, OperationTypeInfo operationTypeInfo, string operation)
        {
            _operationTalker = talker;
            _operationTypeInfo = operationTypeInfo;
            _operation = operation;
        }

        /// <summary>
        /// 操作実行。
        /// </summary>
        /// <param name="arguments">引数。</param>
        /// <returns>戻り値。</returns>
        internal AppVar FriendlyOperation(params object[] arguments)
        {
            if (arguments == null)
            {
                arguments = new object[] { null };
            }
            return _operationTalker.SendAndReceive(ProtocolType.Operation, _operationTypeInfo, _operation, arguments);
        }
    }
}
