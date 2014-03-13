namespace Codeer.Friendly
{
#if ENG
    /// <summary>
    /// A delegate to call operations within the target application.
    /// </summary>
    /// <param name="args">Parameters corresponding to the operation. Can be null, a serializable objecr, or an AppVar.</param>
    /// <returns>
    /// When the operation has a return value, the result is stored in a variable declared within the target class, and an object for manipulating that variable is returned. 
    /// When there is no return value, returns null.
    /// </returns>
#else
    /// <summary>
    /// アプリケーションに対する操作実行デリゲート。
	/// </summary>
    /// <param name="args">引数。</param>
    /// <returns>戻り値。</returns>
#endif
    public delegate AppVar FriendlyOperation(params object[] args);
}
