namespace Codeer.Friendly
{
#if ENG
    /// <summary>
    /// Demonstrating interface keeping the AppVar therein.
    /// </summary>
#else
    /// <summary>
    /// 内部にAppVarを保持することを明示するインターフェイス。
    /// </summary>
#endif
    public interface IAppVarOwner 
    {
#if ENG
        /// <summary>
        /// Variable in the application.
        /// </summary>
#else
        /// <summary>
        /// 内部的に保持する対象アプリケーション内変数。
        /// </summary>
#endif
        AppVar AppVar { get; }
    }
}
