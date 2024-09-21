using System;
using Codeer.Friendly.Inside.Protocol;
using Codeer.Friendly.Properties;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Codeer.Friendly
{
#if ENG
    /// <summary>
    /// Inherits the Exception class
    /// An exception thrown while a FriendlyOperation executes within the target application.
    /// </summary>
#else
    /// <summary>
    /// Exceptionクラスを継承します。
    /// テスト対象アプリケーションとの通信中、FriendlyOperation実行中に発生する例外です。
	/// </summary>
#endif
    [Serializable]
	public class FriendlyOperationException : Exception
	{
#if ENG
        /// <summary>
        /// Infomation.
        /// </summary>
#else
        /// <summary>
        /// 例外情報。
        /// </summary>
#endif
        public ExceptionInfo ExceptionInfo { get; set; }

#if ENG
        /// <summary>
        /// Constractor.
        /// </summary>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
#endif
        public FriendlyOperationException(){}

#if ENG
        /// <summary>
        /// Constractor.
        /// </summary>
        /// <param name="message">Message.</param>
#else
        /// <summary>
        /// コンストラクタ。
		/// </summary>
        /// <param name="message">メッセージ。</param>
#endif
        public FriendlyOperationException(string message) : base(message) { }

#if ENG
        /// <summary>
        /// Constractor.
        /// </summary>
        /// <param name="message">Constractor.</param>
        /// <param name="innerException">Internal Exception.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="innerException">内部例外。</param>
#endif
        public FriendlyOperationException(string message, Exception innerException) : base(message, innerException) { }

#if ENG
        /// <summary>
        /// Constractor.。
        /// </summary>
        /// <param name="info">Infomation</param>
#else
        /// <summary>
        /// コンストラクタ。
		/// </summary>
        /// <param name="info">例外情報。</param>
#endif
        public FriendlyOperationException(ExceptionInfo info) : base(ExceptionInfoMessageFormat(info))
        {
            ExceptionInfo = info;
        }

#if ENG
        /// <summary>
        /// Constractor.
        /// </summary>
        /// <param name="info">Serialize Infomation.</param>
        /// <param name="context">Serialize Context.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="info">シリアライズ情報。</param>
        /// <param name="context">コンテキスト。</param>
#endif
        protected FriendlyOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            ExceptionInfo = (ExceptionInfo)info.GetValue("_exceptionInfo", typeof(ExceptionInfo));
        }

#if ENG
        /// <summary>
        /// Serialize.
        /// </summary>
        /// <param name="info">Serialize Infomation.</param>
        /// <param name="context">Serialize Context.</param>
#else
        /// <summary>
        /// シリアライズ。
        /// </summary>
        /// <param name="info">シリアライズ情報。</param>
        /// <param name="context">コンテキスト。</param>
#endif
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("_exceptionInfo", ExceptionInfo);
            base.GetObjectData(info, context);
        }

		/// <summary>
        /// 例外情報をメッセージ文字列にするフォーマット。
		/// </summary>
        /// <param name="info">例外情報。</param>
        /// <returns>メッセージ文字列。</returns>
		static string ExceptionInfoMessageFormat(ExceptionInfo info)
		{
            if (string.IsNullOrEmpty(info.StackTrace))
            {
                return info.Message;
            }
            return string.Format(CultureInfo.CurrentCulture, Resources.ExceptionInfoFormat,
				info.Message,
				info.ExceptionType,
				info.Source,
				info.StackTrace,
				info.HelpLink);
		}
	}
}
