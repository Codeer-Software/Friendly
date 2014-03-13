using System;
using Codeer.Friendly.Properties;
using System.Security.Permissions;
using System.Runtime.Serialization;

namespace Codeer.Friendly.Inside
{
    /// <summary>
    /// 内部例外。
    /// </summary>
    [Serializable]
    public class InternalException : Exception 
    {
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public InternalException(){}

		/// <summary>
        /// コンストラクタ。
		/// </summary>
        /// <param name="message">メッセージ。</param>
		public InternalException(string message) : base(message) { }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="innerException">内部例外。</param>
        public InternalException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="info">シリアライズ情報。</param>
        /// <param name="context">コンテキスト。</param>
        protected InternalException(SerializationInfo info, StreamingContext context)
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
