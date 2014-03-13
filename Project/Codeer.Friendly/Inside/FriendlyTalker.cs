using System;
using System.Threading;
using System.Collections.Generic;
using Codeer.Friendly.Inside.Protocol;
using Codeer.Friendly.Properties;
using System.Globalization;

namespace Codeer.Friendly.Inside
{
	/// <summary>
	/// コミュニケーター。
	/// </summary>
	static class FriendlyTalker
	{
		/// <summary>
		/// 戻り値をAppVarで取得する通信。
		/// </summary>
		/// <param name="invoker">呼び出し元。</param>
        /// <param name="friendlyConnector">アプリケーションとの接続者。</param>
        /// <param name="protocolType">通信タイプ。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。</param>
        /// <param name="varAddress">変数アドレス。</param>
		/// <param name="typeFullName">タイプフルネーム。</param>
        /// <param name="operation">操作名称。</param>
        /// <param name="arguments">引数。</param>
		/// <returns>変数。</returns>
        internal static AppVar SendAndVarReceive(object invoker, IFriendlyConnector friendlyConnector, ProtocolType protocolType,
                                    OperationTypeInfo operationTypeInfo, VarAddress varAddress, string typeFullName, string operation, object[] arguments)
		{
            object value = SendAndValueReceive(invoker, friendlyConnector, protocolType, operationTypeInfo, varAddress, typeFullName, operation, arguments);
			VarAddress retHandle = value as VarAddress;
            return (retHandle == null) ? (null) : (new AppVar(friendlyConnector, retHandle));
		}

		/// <summary>
        /// 戻り値を値で取得する通信処理。
        /// 通信基本形。
		/// </summary>
		/// <param name="invoker">呼び出し元。</param>
        /// <param name="friendlyConnector">アプリケーションとの接続者。</param>
        /// <param name="protocolType">通信タイプ。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。</param>
        /// <param name="varAddress">変数アドレス。</param>
		/// <param name="typeFullName">タイプフルネーム。</param>
		/// <param name="operation">操作名称。</param>
        /// <param name="arguments">引数。</param>
		/// <returns>値。</returns>
        internal static object SendAndValueReceive(object invoker, IFriendlyConnector friendlyConnector, ProtocolType protocolType,
                                    OperationTypeInfo operationTypeInfo, VarAddress varAddress, string typeFullName, string operation, object[] arguments)
		{
            //配列の場合の調整
            arguments = AdjustArrayArgs(arguments);

            ReturnInfo ret = friendlyConnector.SendAndReceive(new ProtocolInfo(protocolType, operationTypeInfo, varAddress, typeFullName, operation, ConvertAppVar(friendlyConnector, arguments)));
			GC.KeepAlive(invoker);
            GC.KeepAlive(friendlyConnector);
            for (int i = 0; i < arguments.Length; i++)
			{
                if (arguments[i] != null)
				{
                    GC.KeepAlive(arguments[i]);
				}
			}
			if (ret.Exception != null)
			{
				throw new FriendlyOperationException(ret.Exception);
			}
			return ret.ReturnValue;
		}

        /// <summary>
        /// object[]以外の場合はobject[]でくるんでやる
        /// </summary>
        /// <param name="arguments">引数</param>
        /// <returns>調整した引数</returns>
        private static object[] AdjustArrayArgs(object[] arguments)
        {
            if (arguments.GetType() != typeof(object[]))
            {
                return new object[] { arguments };
            }
            return arguments;
        }

		/// <summary>
		/// AppVarがあれば、Varハンドルに変換。
		/// </summary>
        /// <param name="friendlyConnector">アプリケーションとの接続者。</param>
        /// <param name="arguments">引数。</param>
		/// <returns>変換結果。</returns>
        static object[] ConvertAppVar(IFriendlyConnector friendlyConnector, object[] arguments)
		{
            object[] argsTmp = new object[arguments.Length];
            for (int i = 0; i < arguments.Length; i++)
			{
                argsTmp[i] = ConvertAppVar(friendlyConnector, arguments[i], i);
			}
			return argsTmp;
		}

		/// <summary>
		/// AppVarであれば、Varハンドルに変換。
		/// </summary>
        /// <param name="friendlyConnector">アプリケーションとの接続者。</param>
        /// <param name="index">引数のインデックス。</param>
        /// <param name="arg">引数。</param>
		/// <returns>変換結果。</returns>
		static object ConvertAppVar(IFriendlyConnector friendlyConnector, object arg, int index)
        {
            AppVar appVar = null;
            {
                IAppVarSelf selft = arg as IAppVarSelf;
                if (selft != null)
                {
                    appVar = selft.CodeerFriendlyAppVar;
                }
            }
            if (appVar == null)
            {
                IAppVarOwner owner = arg as IAppVarOwner;
                if (owner != null)
                {
                    appVar = owner.AppVar;
                }
            }
            if (appVar == null)
            {
                appVar = arg as AppVar;
            }
            if (appVar == null)
            {
                if ((arg as IDefinition) != null)
                {
                    throw new FriendlyOperationException(string.Format(CultureInfo.CurrentCulture, Resources.ErrorDefinitionArgument, index + 1));
                }
                return arg;
            }

            //違う変数プールに属する変数は使用できない
            if (!object.ReferenceEquals(friendlyConnector.Identity, appVar.FriendlyConnector.Identity))
            {
                throw new FriendlyOperationException(string.Format(CultureInfo.CurrentCulture, Resources.ErrorDifferentAppFriendVar, index + 1));
            }
            return appVar.VarAddress;
        }
    }
}
