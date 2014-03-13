using Codeer.Friendly.Inside.Protocol;

namespace Codeer.Friendly.Inside
{
    /// <summary>
    /// 操作通信
    /// </summary>
    public abstract class OperationTalker
    {
        /// <summary>
        /// アプリケーションとの接続者。
        /// </summary>
        internal abstract IFriendlyConnector FriendlyConnector { get; }

        /// <summary>
        /// 戻り値をAppVarで取得する通信。
        /// </summary>
        /// <param name="protocolType">通信タイプ。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。</param>
        /// <param name="operation">操作名称。</param>
        /// <param name="arguments">引数。</param>
        /// <returns>変数。</returns>
        internal abstract AppVar SendAndReceive(ProtocolType protocolType, OperationTypeInfo operationTypeInfo, string operation, object[] arguments);

        /// <summary>
        /// 戻り値を値で取得する通信処理。
        /// </summary>
        /// <param name="protocolType">通信タイプ。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。</param>
        /// <param name="operation">操作名称。</param>
        /// <param name="arguments">引数。</param>
        /// <returns>値。</returns>
        internal abstract object SendAndValueReceive(ProtocolType protocolType, OperationTypeInfo operationTypeInfo, string operation, object[] arguments);
    }
}
