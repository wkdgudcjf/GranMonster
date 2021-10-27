using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace granmonster
{
	///<summary>
	///REST API를 활용하여 그랑몬스터 서버와 통신하는 클래스입니다.
	///싱글톤으로 사용되며 수동으로 초기화를 할 수 있습니다.
	///초기화를 하지 않고 별도의 메소드 호출 시에는 내부적으로 초기화 과정을 거치도록 되어있습니다.
	///</summary>
	public class ServerConnection
	{
		private static ServerConnection instance = null;
		private System.Net.WebClient WebClient { get; set; }

		private ServerConnection() { }

		///<summary>
		///현재 개체를 나타내는 문자열을 반환합니다.
		///</summary>
		public override string ToString() { return "ServerConnection"; }

		///<summary>
		///ServerConnection 클래스의 싱글톤 객체를 초기화합니다.
		///</summary>
		public static bool Initialize
		{
			get
			{
				try
				{
					if (IsInitialized())
					{
						Utility.DebugLog("이미 " + instance.ToString() + " 싱글톤 객체가 생성되어 있습니다.");
						return true;
					}

					instance = new ServerConnection
					{
						WebClient = new System.Net.WebClient
						{
							BaseAddress = "http://13.124.56.249/"
							//BaseAddress = "http://192.168.200.170:8080/"
						}
					};

					return true;
				}
				catch
				{
					Utility.DebugLog("알 수 없는 이유로 " + instance.ToString() + " 싱글톤 객체 생성에 실패하였습니다.");
					return false;
				}
			}
		}

		///<summary>
		///ServerConnection 클래스의 싱글톤 객체가 초기화되었는지 확인합니다.
		///</summary>
		public static bool IsInitialized()
		{
			return (instance != null &&
				instance.WebClient != null);
		}

		///<summary>
		///그랑몬스터 서버에 로그인 요청을 합니다.
		///회원가입이 되어있지 않은 경우 회원가입 후 로그인을 진행합니다.
		///</summary>
		public static ServerResponse Login(string userKey, string appKey, string userEmail = null)
		{
			RequestProtocol.Login requestLogin = new RequestProtocol.Login
			{
				userKey = userKey,
				appKey = appKey,
				userEmail = userEmail,
				appTypeEnum = Utility.GetRuntimePlatform(),
			};

			ServerResponse serverResponse = POST("api/login", JsonUtility.ToJson(requestLogin));
			if (RestApiCallStatusMethods.Error(serverResponse.Status))
			{
				Utility.DebugLog(instance.ToString() + "/Login/'api/login' failed.");
				return serverResponse;
			}

			ResponseProtocol.ResponseLogin responseLogin = ResponseProtocol.ResponseLogin.CreateFromJson(serverResponse.Data);
			if (responseLogin.State != ResponseProtocol.ResponseLogin.StatusCode.SUCCESS)
			{
				Utility.DebugLog(instance.ToString() + "/Login/Error Code : " + responseLogin.State);
				return serverResponse;
			}

			// 서버로부터 로그인 성공 응답을 받았을 경우
			if (User.Initialize)
			{
				User.OnLoginSuccess(userKey, appKey, responseLogin.UserEmail, responseLogin.UserCoin);
			}

			return serverResponse;
		}

		///<summary>
		///그랑몬스터 서버에 등록된 게임들의 목록을 가져옵니다.
		///</summary>
		public static ServerResponse RequestAppList(string data)
		{
			return POST("api/applist", data);
		}

		///<summary>
		///그랑코인을 지급할 것을 그랑몬스터 서버에 요청합니다.
		///</summary>
		public static ServerResponse Purchase(string data)
		{
			ServerResponse serverResponse = POST("api/purchase", data);
			if (RestApiCallStatusMethods.Error(serverResponse.Status))
			{
				// 클라이언트에서 실패에 대한 처리를 할 수 있도록 위임한다.
				return serverResponse;
			}

			ResponseProtocol.ResponsePurchase responsePurchase =
				ResponseProtocol.ResponsePurchase.CreateFromJson(serverResponse.Data);
			if (responsePurchase.State == ResponseProtocol.ResponsePurchase.StatusCode.SUCCESS)
			{
				User.SetCoin(responsePurchase.AfterCoin);
			}

			return serverResponse;
		}

		internal static bool GrancoinShopVisible()
		{
			// 버튼이 눌려졌을 때 서버로부터 그랑코인 샵 오픈 허가를 받아야 한다.
			RequestProtocol.GrancoinShopVisible requestGrancoinShopVisible = new RequestProtocol.GrancoinShopVisible
			{
				userKey = User.GetUserKey(),
				appKey = User.GetAppKey(),
				appTypeEnum = Utility.GetRuntimePlatform(),
			};

			ServerResponse responseVisible = GrancoinShopVisible(JsonUtility.ToJson(requestGrancoinShopVisible));
			if (RestApiCallStatusMethods.Error(responseVisible.Status))
			{
				// 비정상적인 에러이다.
				Utility.DebugLog("ServerConnection/Initialize/GrancoinShopVisible protocol failed.");
				return false;
			}

			ResponseProtocol.ResponseGrancoinShopVisible responseGrancoinShopVisible =
				ResponseProtocol.ResponseGrancoinShopVisible.CreateFromJson(responseVisible.Data);
			if (responseGrancoinShopVisible.State != ResponseProtocol.ResponseGrancoinShopVisible.StatusCode.VISIBLE)
			{
				// 서버가 특정 이유로 위젯 오픈을 거부하였다.
				Utility.DebugLog("ServerConnection/Initialize/GrancoinShopVisible false by reason : " + responseGrancoinShopVisible.State);
				return false;
			}

			return true;
		}

		internal static bool WidgetVisible()
		{
			// 버튼이 눌려졌을 때 서버로부터 위젯 오픈 허가를 받아야 한다.
			RequestProtocol.WidgetVisible requestWidgetVisible = new RequestProtocol.WidgetVisible
			{
				userKey = User.GetUserKey(),
				appKey = User.GetAppKey(),
				appTypeEnum = Utility.GetRuntimePlatform(),
			};

			ServerResponse serverResponse = WidgetVisible(JsonUtility.ToJson(requestWidgetVisible));
			if (RestApiCallStatusMethods.Error(serverResponse.Status))
			{
				// 비정상적인 에러이다.
				Utility.DebugLog("ServerConnection/Initialize/WidgetVisible protocol failed.");
				return false;
			}

			ResponseProtocol.ResponseWidgetVisible responseWidgetVisible =
				ResponseProtocol.ResponseWidgetVisible.CreateFromJson(serverResponse.Data);
			if (responseWidgetVisible.State != ResponseProtocol.ResponseWidgetVisible.StatusCode.VISIBLE)
			{
				// 서버가 특정 이유로 위젯 오픈을 거부하였다.
				Utility.DebugLog("ServerConnection/Initialize/WidgetVisible false by reason : " + responseWidgetVisible.State);
				return false;
			}

			return true;
		}

		public static ServerResponse Payload(string data)
		{
			return POST("api/payload", data);
		}

		public static ServerResponse Exhaust(string data)
		{
			return POST("api/exhaust", data);
		}

		public static ServerResponse Event(string data)
		{
			return POST("api/event", data);
		}

		public static ServerResponse EventReward(string data)
		{
			return POST("api/eventreward", data);
		}

		internal static ServerResponse RequestExchangeList(string data)
		{
			return POST("api/exchange", data);
		}

		internal static ServerResponse InvokeInstall(string data)
		{
			return POST("api/install", data);
		}

		internal static ServerResponse InvokePlay(string data)
		{
			return POST("api/play", data);
		}

		internal static ServerResponse WidgetVisible(string data)
		{
			return POST("api/widgetVisible", data);
		}

		internal static ServerResponse GrancoinShopVisible(string data)
		{
			return POST("api/billingVisible", data);
		}

		internal static string GetAppIconImageResourceUrl()
		{
			if (IsInitialized() == false)
			{
				Utility.DebugLog("ServerConnection/GetAppImageResourceUrl/Singleton instance is not initialized.");
				return string.Empty;
			}

			return instance.WebClient.BaseAddress + "image/appIcon/";
		}

		internal static string GetAppBannerImageResourceUrl(Orientation orientation)
		{
			if (IsInitialized() == false)
			{
				Utility.DebugLog("ServerConnection/GetAppImageResourceUrl/Singleton instance is not initialized.");
				return string.Empty;
			}

			// TODO: 일단은 썸네일 무조건 처음만 보여주도록 함. 시간이 없다.
			switch (orientation.GetValue())
			{
				case Orientation.Type.Vertical: return instance.WebClient.BaseAddress + "image/";
				case Orientation.Type.Horizontal: return instance.WebClient.BaseAddress + "image/";
				default:
					Utility.DebugLog("ServerConnection/GetAppImageResourceUrl/Invalid orientation.");
					return string.Empty;
			}
		}

		internal static string GetProductIconResourceUrl(Orientation orientation)
		{
			if (IsInitialized() == false)
			{
				Utility.DebugLog("ServerConnection/GetProductIconResourceUrl/Singleton instance is not initialized.");
				return string.Empty;
			}

			switch (orientation.GetValue())
			{
				case Orientation.Type.Vertical: return instance.WebClient.BaseAddress + "image/VExchange/";
				case Orientation.Type.Horizontal: return instance.WebClient.BaseAddress + "image/HExchange/";
				default:
					Utility.DebugLog("ServerConnection/GetProductIconResourceUrl/Invalid orientation.");
					return string.Empty;
			}
		}

#region REST API Basic Methods
		private static ServerResponse GET(string url)
		{
			try
			{
				if (IsInitialized() == false)
				{
					if (Initialize == false)
					{
						Utility.DebugLog("GET/Singleton instance is not initialized");
						return new ServerResponse(RestApiCallStatus.InitializeFailed, "Singleton instance is not initialized");
					}
				}

				Stream stream = instance.WebClient.OpenRead(instance.WebClient.BaseAddress + url);
				string data = new StreamReader(stream).ReadToEnd();

				return new ServerResponse(RestApiCallStatus.Success, data);
			}
			catch (System.Net.WebException webException)
			{
				System.Net.WebExceptionStatus status = webException.Status;
				string responseData = new StreamReader(webException.Response.GetResponseStream()).ReadToEnd();
				string errorMessage = "[WebExceptionStatus : " + webException.Status + "] [Description : " + responseData + "]";

				return new ServerResponse(RestApiCallStatus.HttpError, errorMessage);
			}
			catch
			{
				return new ServerResponse(RestApiCallStatus.UnknownError,
					RestApiCallStatusMethods.StatusDescription(RestApiCallStatus.UnknownError));
			}
		}

		private static ServerResponse POST(string url, string data)
		{
			try
			{
				if (IsInitialized() == false)
				{
					if (Initialize == false)
					{
						Utility.DebugLog("POST/Singleton instance is not initialized");
						return new ServerResponse(RestApiCallStatus.InitializeFailed, "Singleton instance is not initialized");
					}
				}

				instance.WebClient.Encoding = System.Text.Encoding.UTF8;
				instance.WebClient.Headers.Add("Content-Type", "application/json");

				return new ServerResponse(RestApiCallStatus.Success,
					instance.WebClient.UploadString(instance.WebClient.BaseAddress + url, data));
			}
			catch (System.Net.WebException webException)
			{
				try
				{
					System.Net.WebExceptionStatus status = webException.Status;
					string errorMessage = "[WebExceptionStatus : " + webException.Status + "]";

					StreamReader streamReader = new StreamReader(webException.Response.GetResponseStream());
					errorMessage += " [Description : " + streamReader.ReadToEnd() + "]";

					return new ServerResponse(RestApiCallStatus.HttpError, errorMessage);
				}
				catch
				{
					return new ServerResponse(RestApiCallStatus.HttpError, webException.Message);
				}
			}
			catch
			{
				return new ServerResponse(RestApiCallStatus.UnknownError,
					RestApiCallStatusMethods.StatusDescription(RestApiCallStatus.UnknownError));
			}
		}

		private static ServerResponse PUT(string url)
		{
			try
			{
				if (IsInitialized() == false)
				{
					if (Initialize == false)
					{
						Utility.DebugLog("POST/Singleton instance is not initialized");
						return new ServerResponse(RestApiCallStatus.InitializeFailed, "Singleton instance is not initialized");
					}
				}

				return null; // TODO: PUT 함수 구현
			}
			catch (System.Net.WebException webException)
			{
				System.Net.WebExceptionStatus status = webException.Status;
				string responseData = new StreamReader(webException.Response.GetResponseStream()).ReadToEnd();
				string errorMessage = "[WebExceptionStatus : " + webException.Status + "] [Description : " + responseData + "]";

				return new ServerResponse(RestApiCallStatus.HttpError, errorMessage);
			}
			catch
			{
				return new ServerResponse(RestApiCallStatus.UnknownError,
					RestApiCallStatusMethods.StatusDescription(RestApiCallStatus.UnknownError));
			}
		}

		private static ServerResponse DELETE(string url)
		{
			try
			{
				if (IsInitialized() == false)
				{
					if (Initialize == false)
					{
						Utility.DebugLog("POST/Singleton instance is not initialized");
						return new ServerResponse(RestApiCallStatus.InitializeFailed, "Singleton instance is not initialized");
					}
				}

				return null; // TODO: DELETE 함수 구현
			}
			catch (System.Net.WebException webException)
			{
				System.Net.WebExceptionStatus status = webException.Status;
				string responseData = new StreamReader(webException.Response.GetResponseStream()).ReadToEnd();
				string errorMessage = "[WebExceptionStatus : " + webException.Status + "] [Description : " + responseData + "]";

				return new ServerResponse(RestApiCallStatus.HttpError, errorMessage);
			}
			catch
			{
				return new ServerResponse(RestApiCallStatus.UnknownError,
					RestApiCallStatusMethods.StatusDescription(RestApiCallStatus.UnknownError));
			}
		}
#endregion
	}

	///<summary>
	///REST API 호출 후 서버의 리스폰스 정보를 담고 있는 클래스입니다.
	///</summary>
	public class ServerResponse
	{
		///<summary>
		///서버의 응답 상태를 나타내는 enum 값입니다.
		///성공일 경우 0 또는 양수를, 실패일 경우 음수를 반환합니다.
		///각 코드에 대한 내용은 RestApiCallStatus enum 클래스를 참조하십시오.
		///</summary>
		public RestApiCallStatus Status { get; }

		///<summary>
		///로직 수행에 성공하였을 경우 서버가 보내준 데이터를 나타냅니다.
		///로직 수행에 실패하였을 경우 에러 메시지를 나타냅니다.
		///</summary>
		public string Data { get; }

		///<summary>
		///현재 개체를 나타내는 문자열을 반환합니다.
		///</summary>
		public override string ToString()
		{
			return Status + " : " + Data;
		}

		///<summary>
		///</summary>
		public ServerResponse()
		{
			Status = RestApiCallStatus.UnknownError;
			Data = "";
		}

		///<summary>
		///</summary>
		public ServerResponse(RestApiCallStatus restApiCallStatus, string errorMessage)
		{
			Status = restApiCallStatus;
			Data = errorMessage;
		}
	}
}