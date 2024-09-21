using System;

namespace Codeer.Friendly.Inside.Protocol
{
	/// <summary>
	/// 戻り値情報。
	/// </summary>
	[Serializable]
	public class ReturnInfo
	{
		/// <summary>
		/// 戻り値。
		/// </summary>
		public object ReturnValue { get; set; }

		/// <summary>
		/// 例外。
		/// </summary>
		public ExceptionInfo Exception { get; set; }

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
			ReturnValue = returnValue;
		}

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="exception">例外情報。</param>
        public ReturnInfo(ExceptionInfo exception)
		{
            Exception = exception;
		}
	}
}
