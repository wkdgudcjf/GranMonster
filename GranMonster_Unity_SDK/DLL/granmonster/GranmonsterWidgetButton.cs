using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace granmonster
{
	public class GranmonsterWidgetButton
	{
		private static GranmonsterWidgetButton Instance { get; set; }
		private GameObject RootObject { get; set; }

		private GranmonsterWidgetButton() { }

		///<summary>
		///현재 개체를 나타내는 문자열을 반환합니다.
		///</summary>
		public override string ToString() { return "GranmonsterWidgetButton"; }

		///<summary>
		///GranmonsterShopButton 클래스의 싱글톤 객체를 초기화합니다.
		///</summary>
		public static bool Initialize(string appKey, Orientation orientation,
			Transform widgetParent, MonoBehaviour monoBehaviour, Action onDialogOpenCallBack = null, Action onDialogCloseCallBack = null)
		{
			try
			{
				if (IsInitialized())
				{
					Utility.DebugLog("이미 " + Instance.ToString() + " 싱글톤 객체가 생성되어 있습니다.");
					return true;
				}

				Instance = new GranmonsterWidgetButton();

				Assembly assembly = Assembly.GetExecutingAssembly();
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (Stream stream = assembly.GetManifestResourceStream("granmonster.Properties.granmonsterwidgetbutton"))
					{
						AssetBundle assetBundle = null;
						byte[] buffer = new byte[1024 * 1024 * 1]; // 1MB

						int read = 0;
						while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
						{
							memoryStream.Write(buffer, 0, read);
						}

						assetBundle = AssetBundle.LoadFromMemory(memoryStream.ToArray());
						Instance.RootObject = UnityEngine.Object.Instantiate(assetBundle.LoadAsset("Open Widget Button")) as GameObject;
						Instance.RootObject.AddComponent<FloatingButton>();
						Instance.RootObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
						{
							// 버튼이 눌려졌을 때 서버로부터 위젯 오픈 허가를 받아야 한다.
							if (ServerConnection.WidgetVisible() == false)
							{
								Utility.DebugLog("WidgetInternal/Request was rejected by server.");
								Instance.RootObject.GetComponent<UnityEngine.UI.Button>().gameObject.SetActive(false);
								return;
							}

							// 서버가 위젯 다이얼로그 오픈을 허용했다면 언제든지 열 수 있도록 이벤트를 등록한다.
							monoBehaviour.StartCoroutine(WidgetManager.LoadWidgetObject(
								appKey, orientation, widgetParent, monoBehaviour, onDialogOpenCallBack, onDialogCloseCallBack));
							onDialogOpenCallBack?.Invoke();

							Instance.RootObject.GetComponent<FloatingButton>()?.OnButtonClick();
						});

						assetBundle.Unload(false);
					}

					memoryStream.Flush();
				}

				return true;
			}
			catch
			{
				Utility.DebugLog("알 수 없는 이유로 " + Instance.ToString() + " 싱글톤 객체 생성에 실패하였습니다.");
				return false;
			}
		}

		///<summary>
		///GranmonsterShopButton 클래스의 싱글톤 객체가 초기화되었는지 확인합니다.
		///</summary>
		public static bool IsInitialized()
		{
			return Instance != null && Instance.RootObject != null;
		}

		public static void Show(Transform parent, Vector3 anchoredPosition3D)
		{
			if (IsInitialized() == false ||
				Instance.RootObject == null)
			{
				Utility.DebugLog(Instance.ToString() + "/Show/Singleton instance is not initialized.");
				return;
			}

			if (ServerConnection.WidgetVisible() == false)
			{
				Utility.DebugLog(Instance.ToString() + "/Show/Request was rejected by server.");
				return;
			}

			Instance.RootObject.transform.SetParent(parent);
			Instance.RootObject.transform.localScale = Vector3.one;

			RectTransform rectTransform = Instance.RootObject.GetComponent<RectTransform>();
			rectTransform.anchoredPosition3D = anchoredPosition3D;
			rectTransform.rotation = new Quaternion(0, 0, 0, 0);

			Instance.RootObject.SetActive(true);
		}

		public static void Hide()
		{
			if (IsInitialized() == false ||
				Instance.RootObject == null)
			{
				Utility.DebugLog(Instance.ToString() + "/Hide/Singleton instance is not initialized.");
				return;
			}

			Instance.RootObject.SetActive(false);
		}
	}
}