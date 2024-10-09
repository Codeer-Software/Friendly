#define CODE_ANALYSIS
using System;
using System.Diagnostics.CodeAnalysis;

namespace Codeer.Friendly
{
#if ENG
    /// <summary>
    /// Used to specify parameters for a FriendlyOperation when there is a need to resolve overloads or call an operation on a parent class.
    /// Please refer to samples.
    /// </summary>
#else
    /// <summary>
    /// FriendlyOperationの型を確定させるための情報です。
    /// オーバーロードの解決と、親クラスの操作呼び出しに使用します。
    /// </summary>
#endif
    [Serializable]
    public class OperationTypeInfo
    {
#if ENG
        /// <summary>
        /// Returns the full class name of the target type for the target operation.
        /// </summary>
#else
        /// <summary>
        /// 操作を保持する型フルネームです。
        /// </summary>
#endif
        public string Target { get; set; }

#if ENG
        /// <summary>
        /// Returns an array of the full class names of the target operation's parameters.
        /// </summary>
#else
        /// <summary>
        /// 引数の型のフルネーム配列。
        /// </summary>
#endif
        public string[] Arguments { get; set; }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
#endif
        public OperationTypeInfo() { }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="target">The full class name of the target type for the target operation.</param>
        /// <param name="arguments">The full class names of the the target operation's parameters.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="target">操作を保持する型フルネームです。</param>
        /// <param name="arguments">引数の型のフルネーム配列。</param>
#endif
        [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.ArgumentException.#ctor(System.String)")]
        public OperationTypeInfo(string target, params string[] arguments)
        {
            //引数チェック
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException("target");
            }
            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }
            for (int i = 0; i < arguments.Length; i++)
            {
                if (string.IsNullOrEmpty(arguments[i]))
                {
                    throw new ArgumentException("arguments is invalid");
                }
            }
            Target = target;
            Arguments = arguments;
        }
    }
}
