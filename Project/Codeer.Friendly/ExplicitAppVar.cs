namespace Codeer.Friendly
{
#if ENG
    /// <summary>
    /// Explicit AppVar.
    /// If you use dynamic to argument, return value becomes dynamic.
    /// This class prevents it.
    /// </summary>
#else
    /// <summary>
    /// アプリケーション内部の変数を明示するときに使用する
    /// dynamicを引数に使うとメソッドの戻り値もdynamicになるのでそれを防ぐ
    /// </summary>
#endif
    public class ExplicitAppVar : IAppVarOwner
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
        public AppVar AppVar { get; private set; }

#if ENG
        /// <summary>
        /// Constractor.
        /// </summary>
        /// <param name="appVar">Variable in the application.</param>
#else
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="appVar">アプリケーション内部の変数</param>
#endif
        public ExplicitAppVar(AppVar appVar)
        {
            AppVar = appVar;
        }
    }
}
