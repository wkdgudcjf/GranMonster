using System.Collections.Generic;

namespace granmonster
{
	///<summary>
	///그랑몬스터 SDK에서 유저를 관리하기 위한 클래스입니다.
	///유저의 고유 키, 로그인 타입, 게임 머니 등의 정보를 가지고 있습니다.
	///로그인에 성공하면 데이터가 세팅됩니다.
	///</summary>
	public class User
	{
		private static User Instance { get; set; }

		private User() { }

		///<summary>
		///현재 개체를 나타내는 문자열을 반환합니다.
		///</summary>
		public override string ToString() { return "User"; }

		///<summary>
		///User 클래스의 싱글톤 객체를 초기화합니다.
		///</summary>
		public static bool Initialize
		{
			get
			{
				try
				{
					Instance = new User();
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
		///User 클래스의 싱글톤 객체가 초기화되었는지 확인합니다.
		///</summary>
		public static bool IsInitialized()
		{
			return Instance != null;
		}

		internal static void OnLoginSuccess(string userKey, string appKey, string email, int coin)
		{
			if (IsInitialized() == false)
			{
				Utility.DebugLog("User/OnLoginSuccess/Singleton instance is not initialized.");
				return;
			}

			Instance.UserKey = userKey;
			Instance.AppKey = appKey;
			Instance.Email = email;
			Instance.Coin = coin;
		}

		public static string GetUserKey() { return Instance?.UserKey; }
		public static string GetAppKey() { return Instance?.AppKey; }
		public static string GetEmail() { return Instance?.Email; }
		public static int GetCoin() { return Instance.Coin; }

		public static void SetCoin(int coin)
		{
			Instance.Coin = coin;
			CallBack(coin);
		}

		private string UserKey { get; set; }
		private string AppKey { get; set; }
		private string Email { get; set; }
		private int Coin { get; set; }

		public delegate void OnGranCoinValueChanged(int value);
		private static List<OnGranCoinValueChanged> callbacks = new List<OnGranCoinValueChanged>();

		public static void AddGranCoinValueChangedHandler(OnGranCoinValueChanged callback)
		{
			if (callbacks.Contains(callback) == false)
			{
				callbacks.Add(callback);
			}
		}

		public static void RemoveGranCoinValueChangedHandler(OnGranCoinValueChanged callback)
		{
			while (callbacks.Contains(callback))
			{
				callbacks.Remove(callback);
			}
		}

		private static void CallBack(int value)
		{
			foreach (OnGranCoinValueChanged callback in callbacks)
			{
				callback(value);
			}
		}
	}
}