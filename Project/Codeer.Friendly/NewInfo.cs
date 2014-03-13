#define CODE_ANALYSIS
using System;
using System.Diagnostics.CodeAnalysis;

namespace Codeer.Friendly
{
#if ENG
    /// <summary>
    /// Used to provide information when instantiating an object in an application.
    /// Stores generation information and constructor parameters.
    /// </summary>
#else
    /// <summary>
    /// アプリケーション内部でオブジェクトを生成するための情報を表すクラスです。
    /// 生成する型情報とコンストラクタ引数を保持します。
	/// </summary>
#endif
    public class NewInfo
	{
		string _typeFullName;
        object[] _arguments;

#if ENG
        /// <summary>
        /// Returns the type's full name.
        /// </summary>
#else
        /// <summary>
        /// タイプフルネーム。
		/// </summary>
#endif
        public string TypeFullName { get { return _typeFullName; } }

#if ENG
        /// <summary>
        /// Returns the set of constructor parameters.
        /// </summary>
#else
        /// <summary>
        /// 引数。
		/// </summary>
#endif
        public object[] Arguments { get { return _arguments; } }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="typeFullName">Fully qualified type name of the generated object.</param>
        /// <param name="arguments">
        /// Arguments to be passed to the object's constructor.
        /// Can be AppVars or serializable objects.
        /// </param>
#else
        /// <summary>
        /// コンストラクタ。
		/// </summary>
        /// <param name="typeFullName">生成する型のタイプフルネーム。</param>
        /// <param name="arguments">生成引数。</param>
#endif
        public NewInfo(string typeFullName, params object[] arguments)
		{
			_typeFullName = typeFullName;
            _arguments = arguments;
            if (_arguments == null)
            {
                _arguments = new object[] { null };
            }
		}

#if ENG
        /// <summary>
        /// Constractor.
        /// </summary>
        /// <param name="type">The type of the object to create.</param>
        /// <param name="args">
        /// Arguments to be passed to the object's constructor.
        /// Can be AppVars or serializable objects.
        /// </param>
#else
        /// <summary>
        /// コンストラクタ。
		/// </summary>
        /// <param name="type">タイプ。</param>
        /// <param name="args">生成引数。</param>
#endif
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public NewInfo(Type type, params object[] args) : this(type.FullName, args) { }
	}

#if ENG
    /// <summary>
    /// Used to provide information when instantiating an object in an application.
    /// Stores generation information and constructor parameters.
    /// </summary>
#else
    /// <summary>
    /// アプリケーション内部でオブジェクトを生成するための情報を表すクラスです。
    /// 生成する型情報とコンストラクタ引数を保持します。
    /// </summary>
#endif
    public class NewInfo<T> : NewInfo
	{
#if ENG
        /// <summary>
        /// Constructor for NewInfo<T>, not NewInfo.
        /// Provides one way of indicating the desired type.
        /// </summary>
        /// <param name="args">
        /// Arguments to be passed to the object's constructor.
        /// Can be AppVars or serializable objects.
        /// </param>
#else
        /// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="args">生成引数。</param>
#endif
        public NewInfo(params object[] args) : base(typeof(T), args) { }
	}
}
