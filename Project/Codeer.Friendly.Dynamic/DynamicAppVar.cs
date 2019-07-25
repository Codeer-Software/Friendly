using System;
using System.Linq;
using System.Dynamic;
using Codeer.Friendly.Dynamic.Inside;
using System.Runtime.Serialization;
using Codeer.Friendly.Dynamic.Properties;
using System.Collections;
using System.Text;

namespace Codeer.Friendly.Dynamic
{
#if ENG
    /// <summary>
    /// Wrapper class to allow using the dynamic keyword to access objects'
    /// methods, properties and fields just like ordinary class members.
    /// One limitation is that you it is not possible to access members that have the same name as those
    /// defined in the DynamicObject class.
    /// And it is not possible to access member the name CodeerFriendlyAppVar.
    /// In the case that you need to access such members, please use the FriendlyObject APIs.
    /// </summary>
#else
    /// <summary>
    /// AppVarのFriendlyOperationをdynamicキーワードを使うことにより、
    /// 通常のメソッド、プロパティー、フィールド呼び出しのように扱うためのラッパークラスです。
    /// 注意点として、DynamicObjectクラスに定義されている操作と同名の操作を呼び出すことはできません。
    /// またCodeerFriendlyAppVarという名称の操作も呼び出すことはできません。
    /// どうしても必要な場合は、旧来のFriendlyOperationで実行してください。
    /// </summary>
#endif
    public class DynamicAppVar : DynamicObject, IAppVarSelf
    {
        readonly AppVar _appVar;

#if ENG
        /// <summary>
        /// AppVar to wrap.
        /// </summary>
#else
        /// <summary>
        /// 内部的に保持する対象アプリケーション内変数。
        /// </summary>
#endif
        public AppVar CodeerFriendlyAppVar { get { return _appVar; } }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="appVar">AppVar to wrap.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="appVar">ラップするappVar。</param>
#endif
        public DynamicAppVar(AppVar appVar)
        {
            _appVar = appVar;
        }

#if ENG
        /// <summary>
        /// Casts a DynamicAppVar to an AppVar.
        /// </summary>
        /// <param name="src">DynamicAppVar.</param>
        /// <returns>AppVar.</returns>
#else
        /// <summary>
        /// AppVarにキャストします。
        /// </summary>
        /// <param name="src">DynamicAppVar型変数。</param>
        /// <returns>AppVar型。</returns>
#endif
        public static implicit operator AppVar(DynamicAppVar src)
        {
            return src._appVar;
        }

#if ENG
        /// <summary>
        /// Casts an AppVar to a DynamicAppVar.
        /// </summary>
        /// <param name="src">AppVar.</param>
        /// <returns>DynamicAppVar.</returns>
#else
        /// <summary>
        /// DynamicAppVarにキャストします。
        /// </summary>
        /// <param name="src">AppVar型変数。</param>
        /// <returns>DynamicAppVar型。</returns>
#endif
        public static implicit operator DynamicAppVar(AppVar src)
        {
            return new DynamicAppVar(src);
        }

#if ENG
        /// <summary>
        /// Allows dynamic resolution for other types of casts.
        /// Serializes the value from the target application and retrieves it.
        /// Special cases
        /// • When casting to IDisposable, an internal AppVar is returned.
        /// · When casting to an IEnumerable, an IEnumerable＜dynamic＞ is returned. The dynamic object in this case is a DynamicAppVar.
        /// </summary>
        /// <param name="binder">Binder.</param>
        /// <param name="result">Cast result.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// その他のキャストの動的解決です。
        /// 対象アプリケーションから、値をシリアライズして取得します。        
        /// 特殊処理として、以下のものがあります。
        /// ・IDisposableにキャストすると、内部のAppVarが返ります。
        /// ・IEnumerableにキャストするとIEnumerable＜dynamic＞が返ります。dynamicはDynamicAppVarです。
        /// </summary>
        /// <param name="binder">バインダー。</param>
        /// <param name="result">キャスト結果。</param>
        /// <returns>成否。</returns>
#endif
        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (typeof(IDisposable) == binder.Type)
            {
                result = _appVar;
            }
            else if (typeof(IEnumerable) == binder.Type)
            {
                result = new Enumerate(_appVar).Select(e => e.Dynamic());
            }
            else
            {
                if (!binder.Type.IsSerializable && binder.Type.GetConstructor(new[] { typeof(AppVar) }) != null)
                {
                    //AppVarのみを渡すコンストラクタを持っている場合
                    if (_appVar.IsNull)
                    {
                        result = null;
                    }
                    else
                    {
                        result = Activator.CreateInstance(binder.Type, new object[] { _appVar });
                    }
                }
                else
                {
                    result = _appVar.Core;
                }
            }
            return true;
        }

#if ENG
        /// <summary>
        /// Provides dynamic resolution of get indexes.
        /// Cannot be used asynchronously.
        /// If you need asynchronous access, please cast the object to an AppVar and use
        /// a FriendlyOperation, or access the index via its method name (.get_Item() for
        /// object indexers and .Get() for array indices).    
        /// </summary>
        /// <param name="binder">Binder.</param>
        /// <param name="indexes">Index access arguments.</param>
        /// <param name="result">Result.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// インデックスアクセス(getter)の動的解決です。
        /// 非同期実行はできません。
        /// どうしても非同期実行が必要な場合はAppVarにキャストして、FriendlyOperationを使用するか、
        /// メソッド形式の呼び出しを実行してください。
        /// メソッド名称はインデックスアクセスの別名(配列はGet,オブジェクトでの定義はget_Item)を使用してください。
        /// </summary>
        /// <example>
        /// //非同期サンプル。
        /// dynamic array; //int[]のオブジェクトが格納されているとします。
        /// Async async = new Async();
        /// int value = array.Get(async, 1); //メソッド形式で呼び出すことが出来ます。Asyncオブジェクトを指定することが出来ます。
        /// </example>
        /// <param name="binder">バインダー。</param>
        /// <param name="indexes">インデックスアクセス引数。</param>
        /// <param name="result">取得。結果</param>
        /// <returns>成否。</returns>
#endif
        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            result = _appVar[GetIndexAccessOperation(indexes.Length)](indexes).Dynamic();
            return true;
        }

#if ENG
        /// <summary>
        /// Provides dynamic resolution of set indexes.
        /// Cannot be used asynchronously.
        /// By casting AppVar, you can use the FriendlyOperation If you need asynchronous execution by all means,
        /// Please execute the method call format.
        /// Please use the (set_Item definition Set, in the object array aliases) index access method name.
        /// </summary>
        /// <param name="binder">Binder.</param>
        /// <param name="indexes">Index access argument.</param>
        /// <param name="value">Value to be set.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// インデックスアクセス(setter)の動的解決です。       
        /// 非同期実行はできません。
        /// どうしても非同期実行が必要な場合はAppVarにキャストして、FriendlyOperationを使用するか、
        /// メソッド形式の呼び出しを実行してください。
        /// メソッド名称はインデックスアクセスの別名(配列はSet,オブジェクトでの定義はset_Item)を使用してください。
        /// </summary>
        /// <example>
        /// //非同期サンプル。
        /// dynamic array; //int[]のオブジェクトが格納されているとします。
        /// Async async = new Async();
        /// array.Set(async, 1, 100); //メソッド形式で呼び出すことが出来ます。Asyncオブジェクトを指定することが出来ます。
        /// </example>
        /// <param name="binder">バインダー。</param>
        /// <param name="indexes">インデックスアクセス引数</param>
        /// <param name="value">設定する値</param>
        /// <returns>成否。</returns>
#endif
        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            object[] args = DynamicFriendlyOperationUtility.AddSetterValue(indexes, value);
            _appVar[GetIndexAccessOperation(indexes.Length)](args);
            return true;
        }

#if ENG
        /// <summary>
        /// Provides dynamic resolution of properties and fields.
        /// Cannot be used asynchronously.
        /// If you need asynchronous access, please cast the object to an AppVar
        /// and use a FriendlyOperation, or call it in the form of a method by specifying a
        /// method name equal to the property name.
        /// </summary>
        /// <param name="binder">Binder.</param>
        /// <param name="result">Return value.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// プロパティーへのアクセス(getter)の動的解決です。
        /// 非同期実行はできません。
        /// どうしても非同期実行が必要な場合はAppVarにキャストして、FriendlyOperationを使用するか、
        /// メソッド形式の呼び出しを実行してください。
        /// メソッド名称はプロパティー名称と同一です。
        /// </summary>
        /// <example>
        /// dynamic form; //System.Windows.Forms.Formのオブジェクトが格納されているとします。
        /// Async async = new Async();
        /// dynamic text = form.Text(async); //メソッド形式で呼び出すことが出来ます。Asyncオブジェクトを指定することが出来ます。
        /// </example>
        /// <param name="binder">バインダー。</param>
        /// <param name="result">取得結果。</param>
        /// <returns>成否。</returns>
#endif
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = _appVar[binder.Name]().Dynamic();
            return true;
        }

#if ENG
        /// <summary>
        /// Provides dynamic resolution of properties and fields.
        /// Cannot be used asynchronously.
        /// If you need asynchronous access, please cast the object to an AppVar
        /// and use a FriendlyOperation, or call it in the form of a method by specifying a
        /// method name equal to the property name.
        /// </summary>
        /// <param name="binder">Binder.</param>
        /// <param name="value">Value to set.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// プロパティーへのアクセス(setter)の動的解決です。
        /// 非同期実行はできません。
        /// どうしても非同期実行が必要な場合はAppVarにキャストして、FriendlyOperationを使用するか、
        /// メソッド形式の呼び出しを実行してください。
        /// メソッド名称はプロパティー名称と同一です。
        /// </summary>
        /// <example>
        /// dynamic form; //System.Windows.Forms.Formのオブジェクトが格納されているとします。
        /// Async async = new Async();
        /// form.Text(async, "text"); //メソッド形式で呼び出すことが出来ます。Asyncオブジェクトを指定することが出来ます。
        /// </example>
        /// <param name="binder">バインダー。</param>
        /// <param name="value">設定値。</param>
        /// <returns>成否。</returns>
#endif
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _appVar[binder.Name](value);
            return true;
        }

#if ENG
        /// <summary>
        /// Provides dynamic resolution of method calls.
        /// To specify Async and/or OperationTypeInfo, please pass these as arguments.
        /// They can be included in any order.
        /// </summary>
        /// <param name="binder">Binder.</param>
        /// <param name="args">Arguments.</param>
        /// <param name="result">Return value.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// メソッド実行の自動解決です。
        /// AsyncとOperationTypeInfoを指定する場合は、引数に渡してください。
        /// その順番はどこでも構いません。
        /// </summary>
        /// <param name="binder">バインダー。</param>
        /// <param name="args">引数。</param>
        /// <param name="result">戻り値。</param>
        /// <returns>成否。</returns>
#endif
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            Async async;
            OperationTypeInfo typeInfo;
            args = DynamicFriendlyOperationUtility.ResolveArguments(args, out async, out typeInfo);
            result = DynamicFriendlyOperationUtility.GetFriendlyOperation(_appVar, binder.Name, async, typeInfo)(args).Dynamic();
            return true;
        }

#if ENG
        /// <summary>
        /// Equality. Returns the result of carrying out the equality operation inside the target application.
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns>Comparison result.</returns>
#else
        /// <summary>
        /// 等価比較。 操作対象アプリケーション内部で実施した結果を返します。
        /// </summary>
        /// <param name="obj">オブジェクト。</param>
        /// <returns>比較結果。</returns>
#endif
        public override bool Equals(object obj)
        {
            return _appVar.Equals(obj);
        }

#if ENG
        /// <summary>
        /// Hash code access. Returns the result of the GetHashCode() operation within the target application.
        /// </summary>
        /// <returns>Hash code.</returns>
#else
        /// <summary>
        /// ハッシュコード取得。 操作対象アプリケーション内部で実施した結果を返します。
        /// </summary>
        /// <returns>ハッシュコード。</returns>
#endif
        public override int GetHashCode()
        {
            return _appVar.GetHashCode();
        }

#if ENG
        /// <summary>
        /// String conversion. Returns the result of the ToString() operation carried out within the target application.
        /// </summary>
        /// <returns>String.</returns>
#else
        /// <summary>
        /// 文字列変換。 操作対象アプリケーション内部で実施した結果を返します。
        /// </summary>
        /// <returns>文字列。</returns>
#endif
        public override string ToString()
        {
            return _appVar.ToString();
        }

#if ENG
        /// <summary>
        /// Type retrieval. Returns the result of the GetType() operation carried out within the target application.
        /// </summary>
        /// <returns>Type object wrapped in a DynamicAppVar.</returns>
#else
        /// <summary>
        /// タイプを取得。操作対象アプリケーション内部で実施した結果を返します。
        /// </summary>
        /// <returns>タイプ。DynamicAppVarで返ります。</returns>
#endif
        public new dynamic GetType()
        {
            return _appVar["GetType"]().Dynamic();
        }

#if ENG
        /// <summary>
        /// Creates a shallow copy. Returns the result of the MemberwiseClone() carried out within the target application.
        /// </summary>
        /// <returns>Shallow copy wrapped in a DynamicAppVar.</returns>
#else
        /// <summary>
        /// 簡易コピーの作成。操作対象アプリケーション内部で実施した結果を返します。
        /// </summary>
        /// <returns>簡易コピー。DynamicAppVarで返ります。</returns>
#endif
        public new dynamic MemberwiseClone()
        {
            return _appVar["MemberwiseClone"]().Dynamic();
        }

        /// <summary>
        /// インデックス操作文字列取得。
        /// </summary>
        /// <param name="indexCount">インデックス数</param>
        /// <returns>インデックス操作文字列。</returns>
        private static string GetIndexAccessOperation(int indexCount)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            for (int i = 0; i < indexCount - 1; i++)
            {
                builder.Append(",");
            }
            builder.Append("]");
            return builder.ToString();
        }
    }
}
