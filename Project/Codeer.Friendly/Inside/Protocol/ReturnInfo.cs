using System;

namespace Codeer.Friendly.Inside.Protocol
{
	/// <summary>
	/// 戻り値情報。
	/// </summary>
	[Serializable]
	public class ReturnInfo
	{
		object _returnValue;
		ExceptionInfo _exception;

		/// <summary>
		/// 戻り値。
		/// </summary>
		public object ReturnValue { get { return _returnValue; } }

		/// <summary>
		/// 例外。
		/// </summary>
		public ExceptionInfo Exception { get { return _exception; } }

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public ReturnInfo() { }

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="returnValue">戻り値。</param>
		public ReturnInfo(object returnValue)
		{
			_returnValue = returnValue;
		}

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="exception">例外情報。</param>
        public ReturnInfo(ExceptionInfo exception)
		{
            _exception = exception;
		}
	}
}
