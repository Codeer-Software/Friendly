using System;

namespace Codeer.Friendly.Inside.Protocol
{
	/// <summary>
	/// 変数アドレス。
	/// </summary>
	[Serializable]
	public class VarAddress
	{
		/// <summary>
		/// コア。
		/// </summary>
		public int Core { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public VarAddress() { }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="core">コア。</param>
        public VarAddress(int core)
		{
			Core = core;
		}
	}
}
