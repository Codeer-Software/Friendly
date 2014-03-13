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
        string _message = string.Empty;
        string _exceptionType = string.Empty;
        string _helpLink = string.Empty;
        string _source = string.Empty;
        string _stackTrace = string.Empty;

        /// <summary>
        /// 例外のタイプ文字列。
        /// </summary>
        public string ExceptionType { get { return _exceptionType; } }

        /// <summary>
        /// 例外に関連付けられているヘルプ ファイルへのリンク。
        /// </summary>
        public string HelpLink { get { return _helpLink; } }

        /// <summary>
        /// 現在の例外を説明するメッセージ。
        /// </summary>
        public string Message { get { return _message; } }
        
        /// <summary>
        /// エラーの原因となったアプリケーションまたはオブジェクトの名前。
        /// </summary>
        public string Source { get { return _source; } }

        /// <summary>
        /// 現在の例外がスローされたときにコール スタック。
        /// </summary>
        public string StackTrace { get { return _stackTrace; } }

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
                _message = exception.Message;
                return;
            }

            //InternalErrorがあれば、それを利用する
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
            _message = exception.Message;
            _exceptionType = exception.GetType().FullName;
            _helpLink = exception.HelpLink;
            _source = exception.Source;
            _stackTrace = exception.StackTrace;
        }
    }
}
