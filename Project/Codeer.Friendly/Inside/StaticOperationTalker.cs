using System;
using Codeer.Friendly.Inside.Protocol;
using Codeer.Friendly.Properties;

namespace Codeer.Friendly.Inside
{
    /// <summary>
    /// Static操作通信
    /// </summary>
    class StaticOperationTalker : OperationTalker
    {
        readonly IFriendlyConnector _friendlyConnector;
        readonly string _staticOperationTypeFullName;

        /// <summary>
        /// アプリケーションとの接続者。
        /// </summary>
        internal override IFriendlyConnector FriendlyConnector { get { return _friendlyConnector; } }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="friendlyConnector">操作送信者。</param>
        /// <param name="typeFullName">タイプフルネーム。</param>
        internal StaticOperationTalker(IFriendlyConnector friendlyConnector, string typeFullName)
        {
            _friendlyConnector = friendlyConnector;
            _staticOperationTypeFullName = typeFullName;
        }

        /// <summary>
        /// 操作取得。
        /// </summary>
        /// <param name="operation">操作名称。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>操作</returns>
        internal FriendlyOperation this[string operation, OperationTypeInfo operationTypeInfo, Async async]
        {
            get
            {
                if (async == null)
                {
                    return new FriendlyOperationOwner(this, operationTypeInfo, operation).FriendlyOperation;
                }
                else
                {
                    async.Initialize(this);
                    return new AsyncFriendlyOperationOwner(async, operationTypeInfo, operation).FriendlyOperation;
                }
            }
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
            return FriendlyTalker.SendAndVarReceive(this, _friendlyConnector, protocolType, operationTypeInfo, null, _staticOperationTypeFullName, operation, arguments);
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
            return FriendlyTalker.SendAndValueReceive(this, _friendlyConnector, protocolType, operationTypeInfo, null, _staticOperationTypeFullName, operation, arguments);
        }
    }
}
