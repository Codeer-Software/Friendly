using System;
using System.Collections.Generic;
using Codeer.Friendly.Dynamic.Properties;

namespace Codeer.Friendly.Dynamic.Inside
{
    /// <summary>
    /// DynamicでFriendlyOperationを実行するためのユーティリティー。
    /// </summary>
    static class DynamicFriendlyOperationUtility
    {
        /// <summary>
        /// FriendlyOperationを取得。
        /// </summary>
        /// <param name="target">対象。</param>
        /// <param name="name">操作名称。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <param name="typeInfo">操作タイプ情報。</param>
        /// <returns>FriendlyOperation。</returns>
        internal static FriendlyOperation GetFriendlyOperation(AppVar target, string name, Async async, OperationTypeInfo typeInfo)
        {
            if (async != null && typeInfo != null)
            {
                return target[name, typeInfo, async];
            }
            else if (async != null)
            {
                return target[name, async];
            }
            else if (typeInfo != null)
            {
                return target[name, typeInfo];
            }
            return target[name];
        }

        /// <summary>
        /// FriendlyOperationを取得。
        /// </summary>
        /// <param name="target">対象。</param>
        /// <param name="name">操作名称。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <param name="typeInfo">操作タイプ情報。</param>
        /// <returns>FriendlyOperation。</returns>
        internal static FriendlyOperation GetFriendlyOperation(AppFriend target, string name, Async async, OperationTypeInfo typeInfo)
        {
            if (async != null && typeInfo != null)
            {
                return target[name, typeInfo, async];
            }
            else if (async != null)
            {
                return target[name, async];
            }
            else if (typeInfo != null)
            {
                return target[name, typeInfo];
            }
            return target[name];
        }

        /// <summary>
        /// 引数を解決する。
        /// </summary>
        /// <param name="srcArgs">元引数。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <param name="typeInfo">操作タイプ情報。</param>
        /// <returns>解決した後の引数。</returns>
        internal static object[] ResolveArguments(object[] srcArgs, out Async async, out OperationTypeInfo typeInfo)
        {
            return ResolveAsyncAndTypeInfo(srcArgs, out async, out typeInfo);
        }

        /// <summary>
        /// setter時に引数に値を加える。
        /// </summary>
        /// <param name="argsSrc">元引数。</param>
        /// <param name="value">setする値。</param>
        /// <returns>加えた後の引数。</returns>
        internal static object[] AddSetterValue(object[] argsSrc, object value)
        {
            object[] args = new object[argsSrc.Length + 1];
            Array.Copy(argsSrc, args, argsSrc.Length);
            args[argsSrc.Length] = value;
            return args;
        }

        /// <summary>
        /// 非同期実行オブジェクトと操作タイプ情報に関して引数を解決する。
        /// </summary>
        /// <param name="srcArgs">元引数。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <param name="typeInfo">操作タイプ情報。</param>
        /// <returns>解決した後の引数。</returns>
        private static object[] ResolveAsyncAndTypeInfo(object[] srcArgs, out Async async, out OperationTypeInfo typeInfo)
        {
            List<object> list = new List<object>();
            async = null;
            typeInfo = null;
            foreach(object element in srcArgs)
            {
                Async checkAsync = element as Async;
                if (checkAsync != null)
                {
                    if (async != null)
                    {
                        throw new FriendlyOperationException(Resources.ErrorAsyncArgument);
                    }
                    async = checkAsync;
                    continue;
                }

                OperationTypeInfo checkInfo = element as OperationTypeInfo;
                if (checkInfo != null)
                {
                    if (typeInfo != null)
                    {
                        throw new FriendlyOperationException(Resources.ErrorOperationTypeInfoArgument);
                    }
                    typeInfo = checkInfo;
                    continue;
                }

                list.Add(element);
            }
            return list.ToArray();
        }
    }
}
