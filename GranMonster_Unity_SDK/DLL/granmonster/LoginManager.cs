using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace granmonster
{
	///<summary>
	///구글 로그인, 그랑몬스터 로그인 등 로그인과 관련된 역할을 수행하는 클래스입니다.
	///</summary>
	class LoginManager
	{
		private static LoginManager instance = null;

		private LoginManager() { }

		///<summary>
		///현재 개체를 나타내는 문자열을 반환합니다.
		///</summary>
		public override string ToString() { return "LoginManager"; }

		///<summary>
		///LoginManager 클래스의 싱글톤 객체를 초기화합니다.
		///</summary>
		public static bool Initialize
		{
			get
			{
				try
				{
					if (IsInitialize())
					{
						Utility.DebugLog("이미 " + instance.ToString() + " 싱글톤 객체가 생성되어 있습니다.");
						return true;
					}

					instance = new LoginManager();

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
		///LoginManager 클래스의 싱글톤 객체가 초기화되었는지 확인합니다.
		///</summary>
		public static bool IsInitialize()
		{
			return instance != null;
		}
	}
}