using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using MiniJSON;

namespace granmonster
{
	public enum AppTypeEnum
	{
		ANDROID,
		IPHONE,
	}

	internal static class JsonTypeCast
	{
		public static KeyValuePair<System.Object, System.Object> Cast<K, V>(this KeyValuePair<K, V> keyValuePair)
		{
			return new KeyValuePair<System.Object, System.Object>(keyValuePair.Key, keyValuePair.Value);
		}

		/*
		public static KeyValuePair<T, V> CastFrom<T, V>(System.Object obj)
		{
			return (KeyValuePair<T, V>)obj;
		}
		*/

		public static KeyValuePair<System.Object, System.Object> CastFrom(System.Object obj)
		{
			var type = obj.GetType();
			if (type.IsGenericType &&
				type == typeof(KeyValuePair<,>))
			{
				var key = type.GetProperty("Key");
				var value = type.GetProperty("Value");
				var keyObj = key.GetValue(obj, null);
				var valueObj = value.GetValue(obj, null);
				return new KeyValuePair<System.Object, System.Object>(keyObj, valueObj);

			}

			throw new ArgumentException("매개변수는 반드시 KetValuePair<>이어야 합니다.");
		}
	}

	public class JsonBase<T> where T : new()
	{
		public static T CreateFromJson(string jsonString)
		{
			Dictionary<string, System.Object> originalJsonDatas = Json.Deserialize(jsonString) as Dictionary<string, System.Object>;
			Dictionary<string, System.Object> jsonDatas = new Dictionary<string, System.Object>(StringComparer.OrdinalIgnoreCase);

			foreach (KeyValuePair<string, System.Object> value in originalJsonDatas)
			{
				jsonDatas.Add(value.Key.ToString().ToLower(), value.Value);
			}

			T responseLogin = new T();

			Type type = typeof(T);
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
			PropertyInfo[] propertyInfos = type.GetProperties(bindingFlags);

			for (int i = 0; i < propertyInfos.Length; ++i)
			{
				if (jsonDatas.TryGetValue(propertyInfos[i].Name.ToLower(), out System.Object value))
				{
					if (propertyInfos[i].PropertyType.IsEnum)
					{
						propertyInfos[i].SetValue(responseLogin,
							Convert.ChangeType(Enum.Parse(propertyInfos[i].PropertyType, value.ToString()), propertyInfos[i].PropertyType), null);
					}
					else if (propertyInfos[i].PropertyType.IsGenericType)
					{
						bool isJsonBaseType = false;
						Type jsonBaseType = typeof(JsonBase<>);
						Type elementType = propertyInfos[i].PropertyType.GetGenericArguments()[0];

						while (elementType != null && elementType != typeof(System.Object))
						{
							var currentType = elementType.IsGenericType ? elementType.GetGenericTypeDefinition() : elementType;
							if (currentType == jsonBaseType)
							{
								isJsonBaseType = true;
								break;
							}

							elementType = elementType.BaseType;
						}

						if (isJsonBaseType)
						{
							StackTrace stackTrace = new StackTrace();
							StackFrame stackFrame = stackTrace.GetFrame(0);
							MethodBase methodBase = stackFrame.GetMethod();

							/*
							IEnumerable<MethodInfo> methodInfos = elementType.GetRuntimeMethods();
							foreach (MethodInfo methodInfo in methodInfos)
							{
								Utility.DebugLog(methodInfo.ToString());
							}
							*/

							if (value.GetType().IsGenericType &&
								value is IEnumerable<System.Object>)
							{
								IEnumerable<System.Object> enumerable = value as IEnumerable<System.Object>;
								System.Object listInstance = Activator.CreateInstance(propertyInfos[i].PropertyType);
								System.Collections.IList list = (System.Collections.IList)listInstance;

								foreach (System.Object subJson in enumerable)
								{
									MethodInfo methodInfo = elementType.GetRuntimeMethod(methodBase.Name, new Type[] { typeof(string) });
									list.Add(methodInfo.Invoke(responseLogin, new object[] { Json.Serialize(subJson) }));
								}

								propertyInfos[i].SetValue(responseLogin, list, null);
							}
						}
					}
					else
					{
						bool isJsonBaseType = false;
						Type jsonBaseType = typeof(JsonBase<>);
						Type propertyType = propertyInfos[i].PropertyType;

						while (propertyType != null && propertyType != jsonBaseType)
						{
							var currentType = propertyType.IsGenericType ? propertyType.GetGenericTypeDefinition() : propertyType;
							if (currentType == jsonBaseType)
							{
								isJsonBaseType = true;
								break;
							}

							propertyType = propertyType.BaseType;
						}

						if (isJsonBaseType)
						{
							// TODO: IEnumerator가 아닌 JsonBase<> 타입일 때의 처리
						}
						else
						{
							// 기본 타입에 대한 처리
							propertyInfos[i].SetValue(responseLogin,
								Convert.ChangeType(value, propertyInfos[i].PropertyType), null);
						}
					}
				}
			}

			return responseLogin;
		}
	}

	///<summary>
	///그랑몬스터 서버에 무엇인가를 요청할 때 필요한 데이터들을 모아놓은 클래스들의 집합입니다.
	///이 프로토콜들은 ServerConnection 싱글톤 객체를 사용하여 함수를 호출할 때 매개변수로 사용됩니다.
	///</summary>
	public class RequestProtocol
	{
		[Serializable]
		internal struct Login
		{
			public string userKey;
			public string appKey;
			public string userEmail;
			public AppTypeEnum appTypeEnum;
		}

		[Serializable]
		internal struct AppList
		{
			public string userKey;
			public string appKey;
			public AppTypeEnum appTypeEnum;
			public Orientation orientation;
		}

		[Serializable]
		internal struct Event
		{
			public string userKey;
			public string appKey;
			public string eventKey;
			public AppTypeEnum appTypeEnum;
		}

		[Serializable]
		internal struct EventReward
		{
			public string userKey;
			public string appKey;
			public string eventKey;
			public AppTypeEnum appTypeEnum;
		}

		[Serializable]
		public struct Payload
		{
			public string userKey;
			public string appKey;
			public AppTypeEnum appTypeEnum;
		}

		[Serializable]
		public struct Exhaust
		{
			public string userKey;
			public string appKey;
			public int coin;
			public string payload;
			public AppTypeEnum appTypeEnum;
		}

		[Serializable]
		public struct Purchase
		{
			public string userKey;
			public string appKey;
			public int coin;
			public int price;
			public string payload;
			public string productId;
			public string purchaseToken;
			public AppTypeEnum appTypeEnum;
		}

		[Serializable]
		internal struct ExchangeList
		{
			public string userKey;
			public string appKey;
			public Orientation orientation;
			public AppTypeEnum appTypeEnum;
		}

		[Serializable]
		internal struct InvokeInstall
		{
			public string userKey;
			public string appKey;
			public string desAppKey;
			public AppTypeEnum appTypeEnum;
		}

		[Serializable]
		internal struct InvokePlay
		{
			public string userKey;
			public string appKey;
			public string desAppKey;
			public AppTypeEnum appTypeEnum;
		}

		[Serializable]
		internal struct WidgetVisible
		{
			public string userKey;
			public string appKey;
			public AppTypeEnum appTypeEnum;
		}

		[Serializable]
		internal struct GrancoinShopVisible
		{
			public string userKey;
			public string appKey;
			public AppTypeEnum appTypeEnum;
		}
	}
		
	public class ResponseProtocol
	{
		public class ResponseLogin : JsonBase<ResponseLogin>
		{
			public enum StatusCode
			{
				UNKNOWN,
				NOT_EXIST_APPKEY,
				USER_KEY_INVALID,
				USER_ALREADY_JOIN_APP,
				SUCCESS
			}

			public StatusCode State { get; set; }
			public string UserEmail { get; set; }
			public int UserCoin { get; set; }
			public bool FirstLogin { get; set; }
		}

		public class ResponseAppList : JsonBase<ResponseAppList>
		{
			public enum StatusCode
			{
				UNKNOWN,
				NOT_EXIST_APPKEY,
				NOT_EXIST_USERKEY,
				SUCCESS
			}

			public StatusCode State { get; set; }
			public List<AppList> AppList { get; set; }
			public int UserCoin { get; set; }

			public ResponseAppList()
			{
				State = StatusCode.UNKNOWN;
				AppList = null;
				UserCoin = 0;
			}
		}

		public class AppList : JsonBase<AppList>
		{
			public string AppKey { get; set; }
			public string AppName { get; set; }
			public string AppUrl { get; set; }
			public string AppImageIconPath { get; set; }
			public string AppImageVBannerPath1 { get; set; }
			public string AppImageVBannerPath2 { get; set; }
			public string AppImageVBannerPath3 { get; set; }
			public string AppImageHBannerPath1 { get; set; }
			public string AppImageHBannerPath2 { get; set; }
			public string AppImageHBannerPath3 { get; set; }
			public string AppPackage { get; set; }
			public List<AppEventList> AppEventList { get; set; }
			public bool AppInstall { get; set; }

			public AppList()
			{
				AppKey = string.Empty;
				AppName = string.Empty;
				AppUrl = string.Empty;
				AppImageIconPath = string.Empty;
				AppImageVBannerPath1 = string.Empty;
				AppImageVBannerPath2 = string.Empty;
				AppImageVBannerPath3 = string.Empty;
				AppImageHBannerPath1 = string.Empty;
				AppImageHBannerPath2 = string.Empty;
				AppImageHBannerPath3 = string.Empty;
				AppPackage = string.Empty;
				AppEventList = null;
				AppInstall = false;
			}
		}

		public class AppEventList : JsonBase<AppEventList>
		{
			public int AppEventId { get; set; }
			public int AppId { get; set; }
			public string AppEventContent { get; set; }
			public string AppEventKey { get; set; }
			public int AppEventCoin { get; set; }
			public bool AppEventRewardEnable { get; set; }
			public bool AppEventSuccessEnable { get; set; }

			public AppEventList()
			{
				AppEventId = 0;
				AppId = 0;
				AppEventContent = string.Empty;
				AppEventKey = string.Empty;
				AppEventCoin = 0;
				AppEventRewardEnable = false;
				AppEventSuccessEnable = false;
			}

		}

		public class ResponseEvent : JsonBase<ResponseEvent>
		{
			public enum StatusCode
			{
				UNKNOWN,
				NOT_EXIST_APPKEY,
				NOT_REGIST_EVENT,
				ALREADY_EVENT_END,
				NOT_ENABLE_EVENT,
				ALREADY_SUCCESS_EVENT,
				INVALID_EVENT,
				SUCCESS,
			}

			public StatusCode State { get; set; }

			public ResponseEvent()
			{
				State = StatusCode.UNKNOWN;
			}
		}

		public class ResponseEventReward : JsonBase<ResponseEventReward>
		{
			public enum StatusCode
			{
				UNKNOWN,
				NOT_EXIST_APPKEY,
				NOT_EXIST_EVENT,
				ALREADY_EVENT_END,
				ALREADY_EVENT_LIMIT_COUNT,
				NOT_ACHIEVE_EVENT,
				NOT_ENABLE_EVENT,
				ALREADY_REWARD_EVENT,
				INVALID_USER,
				INVALID_BILLING,
				INVALID_EVENT_COUNT,
				SUCCESS
			}

			public StatusCode State { get; set; }
			public int Coin { get; set; }

			public ResponseEventReward()
			{
				State = StatusCode.UNKNOWN;
				Coin = 0;
			}
		}

		public class ResponsePayLoad : JsonBase<ResponsePayLoad>
		{
			public enum StatusCode
			{
				UNKNOWN,
				NOT_EXIST_APPKEY,
				USER_KEY_INVALID,
				SUCCESS
			}

			public StatusCode State { get; set; }
			public string UserPayload { get; set; }

			public ResponsePayLoad()
			{
				State = StatusCode.UNKNOWN;
				UserPayload = string.Empty;
			}
		}

		public class ResponseExhaust : JsonBase<ResponseExhaust>
		{
			public enum StatusCode
			{
				UNKNOWN,
				NOT_EXIST_APPKEY,
				NOT_EQUAL_PAYLOAD,
				NOT_ENOUGH_COIN,
				INVALID_BILLING,
				SUCCESS
			}

			public StatusCode State { get; set; }
			public int AfterCoin { get; set; }

			public ResponseExhaust()
			{
				State = StatusCode.UNKNOWN;
				AfterCoin = 0;
			}
		}

		internal class ResponseExchangeList : JsonBase<ResponseExchangeList>
		{
			public enum StatusCode
			{
				UNKNOWN,
				NOT_EXIST_APPKEY,
				SUCCESS
			}

			public StatusCode State { get; set; }
			public List<ResponseExchange> ExchangeList { get; set; }

			public ResponseExchangeList()
			{
				State = StatusCode.UNKNOWN;
				ExchangeList = null;
			}
		}

		internal class ResponseExchange : JsonBase<ResponseExchange>
		{
			public string ExchangeKey { get; set; }
			public int ExchangeMoney { get; set; }
			public int ExchangeCoin { get; set; }
			public string ExchangeName { get; set; }
			public string ExchangeVImagePath { get; set; }
			public string ExchangeHImagePath { get; set; }

			public ResponseExchange()
			{
				ExchangeKey = string.Empty;
				ExchangeMoney = 0;
				ExchangeCoin = 0;
				ExchangeName = string.Empty;
				ExchangeVImagePath = string.Empty;
				ExchangeHImagePath = string.Empty;
			}
		}

		internal class ResponsePurchase : JsonBase<ResponsePurchase>
		{
			public enum StatusCode
			{
				UNKNOWN,
				NOT_EXIST_APPKEY,
				NOT_EQUAL_PAYLOAD,
				INVALID_BILLING,
				SUCCESS
			}

			public StatusCode State { get; set; }
			public int AfterCoin { get; set; }

			public ResponsePurchase()
			{
				State = StatusCode.UNKNOWN;
				AfterCoin = 0;
			}
		}

		internal class ResponseInvokeInstall : JsonBase<ResponseInvokeInstall>
		{
			public enum StatusCode
			{
				UNKNOWN,
				NOT_EXIST_APPKEY,
				NOT_EXIST_USERKEY,
				SUCCESS,
			}

			public StatusCode State { get; set; }

			public ResponseInvokeInstall()
			{
				State = StatusCode.UNKNOWN;
			}
		}

		internal class ResponseInvokePlay : JsonBase<ResponseInvokePlay>
		{
			public enum StatusCode
			{
				UNKNOWN,
				NOT_EXIST_APPKEY,
				NOT_EXIST_USERKEY,
				SUCCESS,
			}

			public StatusCode State { get; set; }

			public ResponseInvokePlay()
			{
				State = StatusCode.UNKNOWN;
			}
		}

		internal class ResponseWidgetVisible : JsonBase<ResponseWidgetVisible>
		{
			public enum StatusCode
			{
				UNKNOWN,
				VISIBLE,
				INVISIBLE,
				NOT_EXIST_APPKEY,
				NOT_EXIST_USERKEY,
			}

			public StatusCode State { get; set; }

			public ResponseWidgetVisible()
			{
				State = StatusCode.UNKNOWN;
			}
		}

		internal class ResponseGrancoinShopVisible : JsonBase<ResponseGrancoinShopVisible>
		{
			public enum StatusCode
			{
				UNKNOWN,
				VISIBLE,
				INVISIBLE,
				NOT_EXIST_APPKEY,
				NOT_EXIST_USERKEY,
			}

			public StatusCode State { get; set; }

			public ResponseGrancoinShopVisible()
			{
				State = StatusCode.UNKNOWN;
			}
		}
	}
}
