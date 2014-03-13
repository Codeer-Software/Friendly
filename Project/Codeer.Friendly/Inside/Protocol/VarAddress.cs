using System;

namespace Codeer.Friendly.Inside.Protocol
{
	/// <summary>
	/// 変数アドレス。
	/// </summary>
	[Serializable]
	public class VarAddress
	{
		int _core;

		/// <summary>
		/// コア。
		/// </summary>
		public int Core { get { return _core; } }

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="core">コア。</param>
		public VarAddress(int core)
		{
			_core = core;
		}
	}
}
