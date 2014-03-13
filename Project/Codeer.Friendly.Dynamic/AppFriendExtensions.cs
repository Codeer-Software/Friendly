using Codeer.Friendly.Dynamic.Inside;
using System;

namespace Codeer.Friendly.Dynamic
{
#if ENG
    /// <summary>
    /// AppFriend extension methods to provide dynamic functionality.
    /// </summary>
#else
    /// <summary>
    /// AppFriendを拡張します。
    /// </summary>
#endif
    public static class AppFriendExtensions
    {
#if ENG
        /// <summary>
        /// Helper method for generating a DynamicAppType for the specified type.
        /// </summary>
        /// <example>
        /// dynamic controlType = app.Type＜Control＞();
        /// </example>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="app">Application manipulation object.</param>
        /// <returns>DynamicAppType.</returns>
#else
        /// <summary>
        /// 指定のタイプのDynamicAppTypeを生成するためのヘルパメソッドです。
        /// </summary>
        /// <example>
        /// ・System.Windows.Forms.ControlクラスのDynamicAppTypeを作成する場合
        /// 　dynamic controlType = app.Type＜Control＞();
        /// </example>
        /// <typeparam name="T">タイプ。</typeparam>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <returns>DynamicAppType。</returns>
#endif
        public static dynamic Type<T>(this AppFriend app)
        {
            return new DynamicAppType(app, typeof(T).FullName);
        }

#if ENG
        /// <summary>
        /// Helper method for generating a DynamicAppType for the specified type.
        /// </summary>
        /// <example>
        /// dynamic controlType = app.Type(typeof(Control));
        /// </example>
        /// <param name="app">Application manipulation object.</param>
        /// <param name="type">Type.</param>
        /// <returns>DynamicAppType.</returns>
#else
        /// <summary>
        /// 指定の名前のDynamicAppTypeを生成するヘルパメソッドです。
        /// </summary>
        /// <example>
        /// ・System.Windows.Forms.ControlクラスのDynamicAppTypeを作成する場合
        /// 　dynamic controlType = app.Type(typeof(Control));
        /// </example>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="type">タイプ。</param>
        /// <returns>DynamicAppType。</returns>
#endif
        public static dynamic Type(this AppFriend app, Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return new DynamicAppType(app, type.FullName);
        }

#if ENG
        /// <summary>
        /// Helper method for generating a DynamicAppType for the specified type.
        /// </summary>
        /// <example>
        /// dynamic controlType = app.Type("System.Windows.Forms.Control");
        /// </example>
        /// <param name="app">Application manipulation object.</param>
        /// <param name="name">Name space or TypeFullName.</param>
        /// <returns>DynamicAppType.</returns>
#else
        /// <summary>
        /// 指定の名前のDynamicAppTypeを生成するヘルパメソッドです。
        /// </summary>
        /// <example>
        /// ・System.Windows.Forms.ControlクラスのDynamicAppTypeを作成する場合
        /// 　dynamic controlType = app.Type("System.Windows.Forms.Control");
        /// </example>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="name">ネームスペースもしくはタイプフルネーム。</param>
        /// <returns>DynamicAppType。</returns>
#endif
        public static dynamic Type(this AppFriend app, string name)
        {
            return new DynamicAppType(app, name);
        }

#if ENG
        /// <summary>
        /// Helper method to generate an empty DynamicAppType.
        /// You should follow a call to Type() with the fully qualified namespace and classname of the type you want to access.
        /// </summary>
        /// <example>
        /// app.Type().System.Windows.Forms.Control.FromHandle(handle);
        /// </example>      
        /// <param name="app">Application manipulation object.</param>
        /// <returns>DynamicAppType。</returns>
#else
        /// <summary>
        /// 空のDynamicAppTypeを生成するヘルパメソッドです。
        /// この後にネームスペースとタイプを続けてください。
        /// </summary>
        /// <example>
        /// ・System.Windows.Forms.ControlクラスのDynamicAppTypeを作成する場合
        /// 　dynamic controlType = app.Type().System.Windows.Forms.Control;
        /// </example>      
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <returns>DynamicAppType。</returns>
#endif
        public static dynamic Type(this AppFriend app)
        {
            return new DynamicAppType(app);
        }

#if ENG
        /// <summary>
        /// Copies indicated object into the target application and returns a DynamicAppVar to access it.
        /// </summary>
        /// <param name="app">Application manipulation object.</param>
        /// <param name="obj">Object to be sent (must be serializable, AppVar, or DynamicAppVar).</param>
        /// <returns>DynamicAppVar.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内に指定のオブジェクトをコピーし、その変数を操作するDynamicAppVarを返します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="obj">初期化オブジェクト（シリアライズ可能なオブジェクトもしくはAppVar、DynamicAppVarであること）。</param>
        /// <returns>DynamicAppVar。</returns>
#endif
        public static dynamic Copy(this AppFriend app, object obj)
        {
            return app.Dim(obj).Dynamic();
        }

#if ENG
        /// <summary>
        /// Declares a null variable in the target application and returns a DynamicAppVar to access it.
        /// </summary>
        /// <param name="app">Application manipulation object.</param>
        /// <returns>DynamicAppVar.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内にnullの変数を宣言し、その変数を操作するDynamicAppVarを返します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <returns>DynamicAppVar。</returns>
#endif
        public static dynamic Null(this AppFriend app)
        {
            return app.Dim().Dynamic();
        }
    }
}
