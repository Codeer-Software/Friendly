using System.Collections.Generic;
using System.Collections;
using Codeer.Friendly.Inside.Protocol;

namespace Codeer.Friendly
{
#if ENG
    /// <summary>
    /// When the variable in application corresponds to repetition processing, repetitive processing by foreach can be executed. (in .Net, when IEnumerator is inherited).
    /// </summary>
#else
    /// <summary>
    /// アプリケーション内の変数が繰り返し処理に対応している場合（.NetならIEnumeratorを継承している場合）foreachによる反復処理を実行できます。
    /// </summary>
#endif
    public class Enumerate : IEnumerable<AppVar>
    {
        AppVar _enumerable;

#if ENG
        /// <summary>
        /// Constractor.
        /// </summary>
        /// <param name="enumerable">An enumerable variable in the application.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="enumerable">反復処理可能なアプリケーション内変数。</param>
#endif
        public Enumerate(AppVar enumerable)
        {
            _enumerable = enumerable;
        }

#if ENG
        /// <summary>
        /// Produces an enumerator for the provided variable. 
        /// </summary>
        /// <returns>An enumerator that allows iterative processing on the variable.</returns>
#else
        /// <summary>
        /// コレクションを反復処理する列挙子を返します。
        /// </summary>
        /// <returns>コレクションを反復処理するために使用できる列挙子。</returns>
#endif
        public IEnumerator<AppVar> GetEnumerator()
        {
            VarAddress[] core = (VarAddress[])_enumerable.SendAndValueReceive(ProtocolType.GetElements, null, string.Empty, new object[0]);
            List<AppVar> elements = new List<AppVar>();
            for (int i = 0; i < core.Length; i++)
            {
                elements.Add(new AppVar(_enumerable.FriendlyConnector, core[i]));
            }
            return elements.GetEnumerator();
        }

#if ENG
        /// <summary>
        /// Produces an enumerator for the provided variable. 
        /// </summary>
        /// <returns>An enumerator that allows iterative processing on the variable.</returns>
#else
        /// <summary>
        /// コレクションを反復処理する列挙子を返します。
        /// </summary>
        /// <returns>コレクションを反復処理するために使用できる列挙子。</returns>
#endif
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
