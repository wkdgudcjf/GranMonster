using System;
using UnityEngine;

namespace granmonster
{
	public class Utility
	{
		internal delegate void DebugLogInternal(string logMessage);

		///<summary>
		///디버그 빌드에서만 동작하는 Debug::Log() 함수입니다.
		///</summary>
		public static void DebugLog(object logMessage)
		{
			if (Debug.isDebugBuild)
			{
				Debug.Log(logMessage);
			}
		}

		internal static AppTypeEnum GetRuntimePlatform()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				return AppTypeEnum.ANDROID;
			}
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				return AppTypeEnum.IPHONE;
			}

			DebugLog("Utility/GetRuntimePlatform/Invalid platform.");
			return AppTypeEnum.ANDROID;
		}

		internal static bool AppInstalled(string package)
		{
			bool installed = false;
			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject currentActivity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");

				AndroidJavaObject launchIntent = null;
				try
				{
					launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", package);
				}
				catch (Exception exeption)
				{
					DebugLog("Utility/AppInstalled/" + exeption.Message);
				}

				installed = launchIntent != null;
			}
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				// TODO: 아이폰일 때 처리
			}

			return installed;
		}
	}
}
