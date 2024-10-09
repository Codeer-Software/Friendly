using System;

namespace Codeer.Friendly.Inside.Protocol
{
    /// <summary>
    /// 例外情報。
    /// 例外クラスは場合によっては、シリアライズ不可能なので、必要なデータのみ抜粋し、保持する。
    /// </summary>
    [Serializable]
    public class ExceptionInfo
    {
        /// <summary>
        /// 例外のタイプ文字列。
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// 例外に関連付けられているヘルプ ファイルへのリンク。
        /// </summary>
        public string HelpLink { get; set; }

        /// <summary>
        /// 現在の例外を説明するメッセージ。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// エラーの原因となったアプリケーションまたはオブジェクトの名前。
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 現在の例外がスローされたときにコール スタック。
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ExceptionInfo() { }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="exception">例外。</param>
        public ExceptionInfo(Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            //アプリ内部でFriendly系の処理によって発生した想定内の例外はメッセージのみ返す。
            if (exception is InformationException)
            {
                Message = exception.Message;
                return;
            }

            //InternalErrorがあれば、それを利用する
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
            Message = exception.Message;
            ExceptionType = exception.GetType().FullName;
            HelpLink = exception.HelpLink;
            Source = exception.Source;
            StackTrace = exception.StackTrace;
        }
    }
}
