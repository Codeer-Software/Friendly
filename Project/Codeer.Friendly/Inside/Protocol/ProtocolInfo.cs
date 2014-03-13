using System;

namespace Codeer.Friendly.Inside.Protocol
{
	/// <summary>
	/// 通信情報。
	/// </summary>
	[Serializable]
	public class ProtocolInfo
	{
        ProtocolType _protocolType;
        OperationTypeInfo _operationTypeInfo;
        VarAddress _varAddress;
		string _typeFullName;
		string _operation;
		object[] _arguments;

        /// <summary>
        /// 通信タイプ。
        /// </summary>
        public ProtocolType ProtocolType { get { return _protocolType; } }

        /// <summary>
        /// 操作タイプ情報。
        /// </summary>
        public OperationTypeInfo OperationTypeInfo { get { return _operationTypeInfo; } }
        
        /// <summary>
		/// 変数アドレス。
		/// </summary>
		public VarAddress VarAddress { get { return _varAddress; } }

		/// <summary>
		/// タイプフルネーム。
		/// </summary>
		public string TypeFullName { get { return _typeFullName; } }

		/// <summary>
		/// 操作名称。
		/// </summary>
		public string Operation { get { return _operation; } }

        /// <summary>
        /// 引数。
        /// </summary>
        public object[] Arguments { get { return _arguments; } }

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
            _protocolType = protocolType;
            _operationTypeInfo = operationTypeInfo;
            _varAddress = varAddress;
			_typeFullName = typeFullName;
			_operation = operation;
			_arguments = arguments;
		}
	}
}