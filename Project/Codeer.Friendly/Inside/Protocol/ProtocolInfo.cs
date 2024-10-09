using System;

namespace Codeer.Friendly.Inside.Protocol
{
	/// <summary>
	/// 通信情報。
	/// </summary>
	[Serializable]
	public class ProtocolInfo
	{
        /// <summary>
        /// 通信タイプ。
        /// </summary>
        public ProtocolType ProtocolType { get; set; }

        /// <summary>
        /// 操作タイプ情報。
        /// </summary>
        public OperationTypeInfo OperationTypeInfo { get; set; }

        /// <summary>
        /// 変数アドレス。
        /// </summary>
        public VarAddress VarAddress { get; set; }

        /// <summary>
        /// タイプフルネーム。
        /// </summary>
        public string TypeFullName { get; set; }

        /// <summary>
        /// 操作名称。
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 引数。
        /// </summary>
        public object[] Arguments { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ProtocolInfo() { }

        /// <summary>
		/// コンストラクタ。
		/// </summary>
        /// <param name="protocolType">通信タイプ。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。</param>
        /// <param name="varAddress">変数アドレス。</param>
		/// <param name="typeFullName">タイプフルネーム。</param>
		/// <param name="operation">操作名称。</param>
		/// <param name="arguments">引数。</param>
        public ProtocolInfo(ProtocolType protocolType, OperationTypeInfo operationTypeInfo, VarAddress varAddress, string typeFullName, string operation, object[] arguments)
		{
            ProtocolType = protocolType;
            OperationTypeInfo = operationTypeInfo;
            VarAddress = varAddress;
			TypeFullName = typeFullName;
			Operation = operation;
			Arguments = arguments;
		}
	}
}