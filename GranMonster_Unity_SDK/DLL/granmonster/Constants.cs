using System;

namespace granmonster
{
	///<summary>
	///Rest API를 호출하여 서버로부터 응답을 받았을 때 상태에 대한 코드입니다.
	///음수일 경우 에러를 나타내며, 0보다 크거나 같을 경우 정상을 나타냅니다.
	///</summary>
	public enum RestApiCallStatus : Int32
	{
		///<summary>알 수 없는 에러입니다.</summary>
		UnknownError = Int32.MinValue,

		///<summary>DLL 모듈에서 초기화를 실패한 경우입니다.</summary>
		InitializeFailed = -9999,

		///<summary>
		///서버에서 Http 에러 코드를 넘겨준 경우입니다.
		///에러 코드에 대한 내용은 데이터를 통해 확인할 수 있습니다.
		///</summary>
		HttpError = -9998,

		///<summary>정상적으로 처리된 경우입니다.</summary>
		Success = 0,
	}

	///<summary>
	///<c>RestApiCallStatus</c>를 통한 작업의 편의를 돕는 Global Static Methods의 집합입니다.
	///</summary>
	public static class RestApiCallStatusMethods
	{
		///<summary>
		///매개 변수로 넘겨받은 <c>RestApiCallStatus</c>가 에러 코드인지 확인합니다.
		///에러라면 true를, 에러가 아니라면 false를 반환합니다.
		///</summary>
		///<param name="restApiCallStatus">restApiCallStatus</param>
		public static bool Error(this RestApiCallStatus restApiCallStatus)
		{
			return restApiCallStatus < 0;
		}

		public static bool Success(this RestApiCallStatus restApiCallStatus)
		{
			return restApiCallStatus >= 0;
		}

		public static string StatusDescription(this RestApiCallStatus restApiCallStatus)
		{
			switch (restApiCallStatus)
			{
				case RestApiCallStatus.UnknownError: return "알 수 없는 에러입니다.";
				case RestApiCallStatus.InitializeFailed: return "DLL 모듈 초기화 실패입니다.";
				case RestApiCallStatus.HttpError: return "서버로부터 에러 코드를 넘겨받았습니다. 에러 데이터를 확인하십시오.";
				case RestApiCallStatus.Success: return "정상적으로 처리되었습니다.";

				default:
					Utility.DebugLog("정의되지 않은 RestApiCallStatus.");
					return string.Empty;
			}
		}
	}

	public class Orientation
	{
		public enum Type
		{
			None = -1,
			Vertical = 0,
			Horizontal = 1,
		}

		private Orientation() { value = Type.None; }

		public Orientation(Type type) { value = type; }
		public Orientation(int type) { value = FromInt(type).GetValue(); }

		public Type GetValue() { return value; }

		public static Orientation FromInt(int type)
		{
			switch (type)
			{
				case (int)Type.None: return new Orientation(Type.None);
				case (int)Type.Vertical: return new Orientation(Type.Vertical);
				case (int)Type.Horizontal: return new Orientation(Type.Horizontal);

				default:
					Utility.DebugLog("Orientation/FromInt/Invalud Type.");
					return new Orientation();
			}
		}

		public override string ToString()
		{
			switch (value)
			{
				case Type.None: return "None";
				case Type.Vertical: return "Vertical";
				case Type.Horizontal: return "Horizontal";

				default:
					Utility.DebugLog("Orientation/ToString/Invalud Type.");
					return "None";
			}
		}

		private Type value;
	}
}
