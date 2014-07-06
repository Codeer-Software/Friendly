using System;

namespace Codeer.Friendly
{
#if ENG
    /// <summary>
    /// Helper methos of AppVar.
    /// </summary>
#else
    /// <summary>
    /// AppVarのヘルパメソッドです。
    /// </summary>
#endif
    public static class AppVarHelper
    {
#if ENG
        /// <summary>
        /// Determines whether the specified Object instances in target application are the same instance.
        /// </summary>
        /// <param name="lhs">The first object to compare. </param>
        /// <param name="rhs">The second object to compare. </param>
        /// <returns>is same.</returns>
#else
        /// <summary>
        /// 対象プロセス内のインスタンスが同一であるかを判断します。
        /// </summary>
        /// <param name="lhs">比較対象1。</param>
        /// <param name="rhs">比較対象2。</param>
        /// <returns>インスタンスが同一であるか。</returns>
#endif
        public static bool ReferenceEquals(AppVar lhs, AppVar rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException("lhs");
            }
            if (rhs == null)
            {
                throw new ArgumentNullException("rhs");
            }
            return (bool)lhs.App[typeof(object), "ReferenceEquals"](lhs, rhs).Core;
        }

#if ENG
        /// <summary>
        /// Get that variables in the target application is null.
        /// </summary>
        /// <param name="appVar">Variable in the target application.</param>
        /// <returns>is null.</returns>
#else
        /// <summary>
        /// アプリケーション内変数がnullであるかを取得します。
        /// </summary>
        /// <param name="appVar">アプリケーション内変数。</param>
        /// <returns>nullであるか。</returns>
#endif
        public static bool IsNull(AppVar appVar)
        {
            if (appVar == null)
            {
                throw new ArgumentNullException("appVar");
            }
            return appVar.IsNull;
        }
    }
}
