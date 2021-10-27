using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace granmonster
{
	public class GranmonsterShopButton
	{
		private static GranmonsterShopButton Instance { get; set; }
		private GameObject RootObject { get; set; }

		private GranmonsterShopButton() { }

		///<summary>
		///현재 개체를 나타내는 문자열을 반환합니다.
		///</summary>
		public override string ToString() { return "GranmonsterShopButton"; }

		public static bool Initialize(string appKey, Orientation orientation, Transform shopParent, MonoBehaviour monoBehaviour,
			Action<List<GrancoinShopManager.Exchange>> initializer, Action<GrancoinShopManager.Exchange, string> onPurchaseButtonClick,
			Action onDialogOpenCallBack = null, Action onDialogCloseCallBack = null)
		{
			try
			{
				if (IsInitialized())
				{
					Utility.DebugLog("이미 " + Instance.ToString() + " 싱글톤 객체가 생성되어 있습니다.");
					return true;
				}

				Instance = new GranmonsterShopButton();

				Assembly assembly = Assembly.GetExecutingAssembly();
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (Stream stream = assembly.GetManifestResourceStream("granmonster.Properties.granmonstershopbutton"))
					{
						AssetBundle assetBundle = null;
						byte[] buffer = new byte[1024 * 1024 * 1]; // 1MB

						int read = 0;
						while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
						{
							memoryStream.Write(buffer, 0, read);
						}

						assetBundle = AssetBundle.LoadFromMemory(memoryStream.ToArray());
						Instance.RootObject = UnityEngine.Object.Instantiate(assetBundle.LoadAsset("Open Shop Button")) as GameObject;
						Instance.RootObject.AddComponent<FloatingButton>();
						Instance.RootObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
						{
							// 버튼이 눌려졌을 때 서버로부터 그랑코인 샵 오픈 허가를 받아야 한다.
							if (ServerConnection.GrancoinShopVisible() == false)
							{
								Utility.DebugLog("GrancoinShopInternal/Request was rejected by server.");
								Instance.RootObject.GetComponent<UnityEngine.UI.Button>().gameObject.SetActive(false);
								return;
							}

							// 서버가 그랑코인 샵 다이얼로그 오픈을 허용했다면 언제든지 열 수 있도록 이벤트를 등록한다.
							monoBehaviour.StartCoroutine(GrancoinShopManager.LoadGrancoinShopObject(appKey, orientation, shopParent,
								monoBehaviour, onPurchaseButtonClick, onDialogOpenCallBack, onDialogCloseCallBack));
							onDialogOpenCallBack?.Invoke();

							Instance.RootObject.GetComponent<FloatingButton>()?.OnButtonClick();
						});

						assetBundle.Unload(false);
					}

					memoryStream.Flush();
				}

				// 서버에 Exchange List 데이터 요청
				RequestProtocol.ExchangeList requestData = new RequestProtocol.ExchangeList
				{
					userKey = User.GetUserKey(),
					appKey = appKey,
					orientation = orientation,
					appTypeEnum = Utility.GetRuntimePlatform(),
				};

				ServerResponse serverResponse = ServerConnection.RequestExchangeList(JsonUtility.ToJson(requestData));
				ResponseProtocol.ResponseExchangeList responseExchangeList = ResponseProtocol.ResponseExchangeList.CreateFromJson(serverResponse.Data);
				if (responseExchangeList.State != ResponseProtocol.ResponseExchangeList.StatusCode.SUCCESS)
				{
					Utility.DebugLog(Instance.ToString() + "/Initialize/Error Code : " + responseExchangeList.State);
					return false;
				}

				// 서버로부터 전달받은 데이터를 로컬 메모리에 저장한다.
				List<GrancoinShopManager.Exchange> exchanges = new List<GrancoinShopManager.Exchange>();
				foreach (ResponseProtocol.ResponseExchange responseExchange in responseExchangeList.ExchangeList)
				{
					exchanges.Add(new GrancoinShopManager.Exchange(responseExchange.ExchangeKey, responseExchange.ExchangeMoney, responseExchange.ExchangeCoin,
						responseExchange.ExchangeName, responseExchange.ExchangeVImagePath, responseExchange.ExchangeHImagePath));
				}

				// 클라이언트의 IAP 모듈의 아이템 구성을 하도록 한다.
				initializer(exchanges);

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

			// 서버로부터 위젯 오픈 허가를 받아야 버튼을 보여줄 수 있다.
			if (ServerConnection.GrancoinShopVisible() == false)
			{
				Utility.DebugLog(Instance.ToString() + "/Show/Request was rejected by server.");
				return;
			}

			// 객체화 및 초기화
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