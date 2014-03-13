using System.Collections.Generic;

namespace Codeer.Friendly.Dynamic.Inside
{
    /// <summary>
    /// タイプキャッシュ。
    /// </summary>
    class TypeChahe
    {
        AppVar _typeFinder;
        internal Dictionary<string, bool> NameSpace { get; private set; }
        internal Dictionary<string, bool> Type { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="app">アプリケーション操作クラス</param>
        internal TypeChahe(AppFriend app)
        {
            _typeFinder = app.Dim(new NewInfo("Codeer.Friendly.DotNetExecutor.TypeFinder"));
            NameSpace = new Dictionary<string, bool>();
            Type = new Dictionary<string, bool>();
        }

        /// <summary>
        /// タイプ名称か。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="name">名前。</param>
        /// <param name="cacheNotType">タイプ名称でなくてもキャッシュするか。</param>
        /// <returns>タイプ名称か。</returns>
        internal bool IsTypeName(AppFriend app, string name, bool cacheNotType)
        {
            if (NameSpace.ContainsKey(name))
            {
                return false;
            }
            bool isType = false;
            if (Type.TryGetValue(name, out isType))
            {
                return isType;
            }
            if ((bool)app[typeof(object), "ReferenceEquals"](_typeFinder["GetType"](name), null).Core)
            {
                if (cacheNotType)
                {
                    Type.Add(name, false);
                }
                return false;
            }
            Type.Add(name, true);
            int index = name.LastIndexOf('.');
            if (index != -1)
            {
                string nameSpace = name.Substring(0, index);
                NameSpace.Remove(nameSpace);
                NameSpace.Add(nameSpace, true);
            }
            return true;
        }
    }
}
