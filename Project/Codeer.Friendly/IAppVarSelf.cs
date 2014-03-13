namespace Codeer.Friendly
{
#if ENG
    /// <summary>
    /// Show that it is equivalent to him.
    /// </summary>
#else
    /// <summary>
    /// AppVarとほぼ等価な存在を表すインターフェイス。
    /// ライブラリ内で使います。
    /// </summary>
#endif
    public interface IAppVarSelf
    {
#if ENG
        /// <summary>
        /// Variable in the application
        /// </summary>
#else
        /// <summary>
        /// 内部的に保持する対象アプリケーション内変数。
        /// </summary>
#endif
        AppVar CodeerFriendlyAppVar { get; }
    }
}
