namespace Codeer.Friendly.Dynamic
{
#if ENG
    /// <summary>
    /// AppVar extension methods to provide dynamic functionality.
    /// </summary>
#else
    /// <summary>
    /// AppVarを拡張します。
    /// </summary>
#endif
    public static class AppVarExtensions
    {
#if ENG
        /// <summary>
        /// Helper method to convert an AppVar to a DynamicAppVar and convert it into a dynamic type.
        /// </summary>
        /// <param name="src">AppVar.</param>
        /// <returns>DynamicAppVar.</returns>
#else
        /// <summary>
        /// AppVarをDynamicAppVarに変換し、dynamic型に入れるためのヘルパメソッドです。
        /// </summary>
        /// <param name="src">AppVar型変数。</param>
        /// <returns>DynamicAppVar。</returns>
#endif
        public static dynamic Dynamic(this AppVar src)
        {
            return new DynamicAppVar(src);
        }

#if ENG
        /// <summary>
        /// Helper method to convert an AppVar to a DynamicAppVar and convert it into a dynamic type.
        /// </summary>
        /// <param name="src">AppVar Owner.</param>
        /// <returns>DynamicAppVar.</returns>
#else
        /// <summary>
        /// IAppVarOwnerClearlyをDynamicAppVarに変換し、dynamic型に入れるためのヘルパメソッドです。
        /// </summary>
        /// <param name="src">AppVar保持クラス。</param>
        /// <returns>DynamicAppVar。</returns>
#endif
        public static dynamic Dynamic(this IAppVarOwner src)
        {
            return new DynamicAppVar(src.AppVar);
        }
    }
}
