namespace Codeer.Friendly.Inside.Protocol
{
	/// <summary>
	/// 通信タイプ。
	/// </summary>
	public enum ProtocolType
	{
		/// <summary>
		/// 変数初期化。
		/// </summary>
		VarInitialize,

		/// <summary>
		/// 変数生成。
		/// </summary>
		VarNew,

		/// <summary>
		/// 変数を捨てる。
		/// </summary>
		BinOff,

		/// <summary>
		/// 値を設定。
		/// </summary>
		GetValue,

		/// <summary>
		/// 値を取得。
		/// </summary>
		SetValue,

		/// <summary>
		/// 要素取得。
		/// </summary>
		GetElements,

		/// <summary>
		/// 操作。
		/// </summary>
		Operation,

        /// <summary>
        /// 空変数であるかチェック。
        /// 非同期実行中に使用されるので可能な限り高速に処理を戻すこと。
        /// </summary>
        IsEmptyVar,

        /// <summary>
        /// 非同期結果格納バッファ初期化。
        /// </summary>
        AsyncResultVarInitialize,

		/// <summary>
		/// 非同期操作。
		/// </summary>
		AsyncOperation,
	}
}
