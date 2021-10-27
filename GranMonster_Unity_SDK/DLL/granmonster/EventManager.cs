using UnityEngine;

namespace granmonster
{
	public class EventManager
	{
		private static EventManager Instance { get; set; }

		private EventManager() { }

		///<summary>
		///현재 개체를 나타내는 문자열을 반환합니다.
		///</summary>
		public override string ToString() { return "EventManager"; }

		///<summary>
		///EventManager 클래스의 싱글톤 객체를 초기화합니다.
		///</summary>
		public static bool Initialize
		{
			get
			{
				try
				{
					if (IsInitialized())
					{
						Utility.DebugLog("이미 " + Instance.ToString() + " 싱글톤 객체가 생성되어 있습니다.");
						return true;
					}

					Instance = new EventManager();
					return true;
				}
				catch
				{
					Utility.DebugLog("알 수 없는 이유로 " + Instance.ToString() + " 싱글톤 객체 생성에 실패하였습니다.");
					return false;
				}
		}
		}

		///<summary>
		///EventManager 클래스의 싱글톤 객체가 초기화되었는지 확인합니다.
		///</summary>
		public static bool IsInitialized()
		{
			return Instance != null;
		}

		///<summary>
		///특정 이벤트 완료 시 서버에게 정보를 전달합니다.
		///</summary>
		public static ResponseProtocol.ResponseEvent EventComplete(string appKey, string eventKey)
		{
			if (IsInitialized() == false)
			{
				if (Initialize == false)
				{
					Utility.DebugLog(Instance.ToString() + "/EventComplete/Singleton instance is not initialized.");
					return null;
				}
			}

			if (User.IsInitialized() == false)
			{
				Utility.DebugLog(Instance.ToString() + "/EventComplete/User not loggined.");
				return null;
			}

			RequestProtocol.Event requestEvent = new RequestProtocol.Event
			{
				userKey = User.GetUserKey(),
				appKey = appKey,
				eventKey = eventKey,
				appTypeEnum = Utility.GetRuntimePlatform(),
			};

			ServerResponse serverResponse = ServerConnection.Event(JsonUtility.ToJson(requestEvent));
			if (RestApiCallStatusMethods.Error(serverResponse.Status))
			{
				Utility.DebugLog(Instance.ToString() + "/LoadWidgetObject/" + serverResponse.Status + ", " + serverResponse.Data);
				return null;
			}

			ResponseProtocol.ResponseEvent responseEvent = ResponseProtocol.ResponseEvent.CreateFromJson(serverResponse.Data);
			if (responseEvent.State != ResponseProtocol.ResponseEvent.StatusCode.SUCCESS)
			{
				Utility.DebugLog(Instance.ToString() + "/EventComplete/Error Code : " + responseEvent.State);
			}

			return responseEvent;
		}

		///<summary>
		///</summary>
		public static ResponseProtocol.ResponseEventReward EventReward(string appKey, string eventKey)
		{
			if (IsInitialized() == false)
			{
				if (Initialize == false)
				{
					Utility.DebugLog(Instance.ToString() + "/EventReward/Singleton instance is not initialized.");
					return null;
				}
			}

			if (User.IsInitialized() == false)
			{
				Utility.DebugLog(Instance.ToString() + "/EventReward/User not loggined.");
				return null;
			}

			RequestProtocol.EventReward requestEventReward = new RequestProtocol.EventReward
			{
				userKey = User.GetUserKey(),
				appKey = appKey,
				eventKey = eventKey,
				appTypeEnum = Utility.GetRuntimePlatform(),
			};

			ServerResponse serverResponse = ServerConnection.EventReward(JsonUtility.ToJson(requestEventReward));
			if (RestApiCallStatusMethods.Error(serverResponse.Status))
			{
				Utility.DebugLog(Instance.ToString() + "/LoadWidgetObject/" + serverResponse.Status + ", " + serverResponse.Data);
				return null;
			}

			ResponseProtocol.ResponseEventReward responseEventReward = ResponseProtocol.ResponseEventReward.CreateFromJson(serverResponse.Data);
			if (responseEventReward.State != ResponseProtocol.ResponseEventReward.StatusCode.SUCCESS)
			{
				Utility.DebugLog(Instance.ToString() + "/EventReward/Error Code : " + responseEventReward.State);
			}

			return responseEventReward;
		}
	}
}