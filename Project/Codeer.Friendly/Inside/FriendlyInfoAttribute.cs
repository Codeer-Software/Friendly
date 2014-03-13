#define CODE_ANALYSIS
using System;
using System.Diagnostics.CodeAnalysis;

namespace Codeer.Friendly.Inside
{
	/// <summary>
	/// Friendly情報。
	/// </summary>
    [AttributeUsage(AttributeTargets.All)]
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
	public class FriendlyInfoAttribute : Attribute
	{
		private string _info;

		/// <summary>
		/// 情報。
		/// </summary>
		public string Info { get { return _info; } }
		
		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="info">情報。</param>
		public FriendlyInfoAttribute(string info) { _info = info; }
	}
}
