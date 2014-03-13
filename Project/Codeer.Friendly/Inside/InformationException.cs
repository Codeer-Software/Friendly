using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Codeer.Friendly.Inside
{
    /// <summary>
    /// 情報通知用例外。
    /// </summary>
    [Serializable]
    public class InformationException : Exception 
    {
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public InformationException(){}

		/// <summary>
        /// コンストラクタ。
		/// </summary>
        /// <param name="message">メッセージ。</param>
		public InformationException(string message) : base(message) { }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="innerException">内部例外。</param>
        public InformationException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="info">シリアライズ情報。</param>
        /// <param name="context">コンテキスト。</param>
        protected InformationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// シリアライズ。
        /// </summary>
        /// <param name="info">シリアライズ情報。</param>
        /// <param name="context">コンテキスト。</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
