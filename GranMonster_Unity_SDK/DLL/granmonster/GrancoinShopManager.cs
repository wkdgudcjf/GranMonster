using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace granmonster
{
	///<summary>
	///그랑코인 구매와 관련된 데이터와 그랑코은 구매창을 로드하고 관리하는 클래스입니다.
	///싱글톤으로 사용되며 수동으로 초기화할 수 있습니다.
	///초기화를 하지 않고 별도의 메소드 호출 시에는 내부적으로 초기화 과정을 거치도록 되어있습니다.
	///</summary>
	public class GrancoinShopManager
	{
		public class Exchange
		{
			public string ExchangeKey { get; private set; }
			public int ExchangeMoney { get; private set; }
			public int ExchangeCoin { get; private set; }
			public string ExchangeName { get; private set; }
			public string ExchangeExplane { get; private set; }
			public string ExchangeImagePath { get; private set; }

			public Exchange()
			{
				ExchangeKey = string.Empty;
				ExchangeMoney = 0;
				ExchangeCoin = 0;
				ExchangeName = string.Empty;
				ExchangeExplane = string.Empty;
				ExchangeImagePath = string.Empty;
			}

			public Exchange(string exchangeKey, int exchangeMoney, int exchangeCoin,
				string exchangeName, string exchangeExplane, string exchangeImagePath)
			{
				ExchangeKey = exchangeKey;
				ExchangeMoney = exchangeMoney;
				ExchangeCoin = exchangeCoin;
				ExchangeName = exchangeName;
				ExchangeExplane = exchangeExplane;
				ExchangeImagePath = exchangeImagePath;
			}
		}

		private static GrancoinShopManager Instance { get; set; }
		private static Dictionary<Orientation.Type, int> MaxShopObjectCountPerPage = new Dictionary<Orientation.Type, int>()
		{
			{ Orientation.Type.Vertical, 8 },
			{ Orientation.Type.Horizontal, 8 },
		};

		private Dictionary<Orientation.Type, AssetBundle> GrancoinShopAssetBundle { get; set; }
		private GameObject RootObject { get; set; }
		private Orientation ShopOrientation { get; set; }

		private GrancoinShopManager() { }

		///<summary>
		///현재 개체를 나타내는 문자열을 반환합니다.
		///</summary>
		public override string ToString() { return "GrancoinShopManager"; }

		///<summary>
		///GrancoinShopManager 클래스의 싱글톤 객체를 초기화합니다.
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

					Instance = new GrancoinShopManager
					{
						GrancoinShopAssetBundle = new Dictionary<Orientation.Type, AssetBundle>()
					};

					Array array = Enum.GetValues(typeof(Orientation.Type));
					List<Orientation.Type> orientationValues = array.OfType<Orientation.Type>().ToList();

					Assembly assembly = Assembly.GetExecutingAssembly();
					foreach (Orientation.Type orientationValue in orientationValues)
					{
						if (orientationValue == Orientation.Type.None)
						{
							continue;
						}

						Orientation orientation = new Orientation(orientationValue);
						using (MemoryStream memoryStream = new MemoryStream())
						{
							string resourcePath = "granmonster.Properties.shopwindow(" + orientation.ToString().ToLower() + ")";
							using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
							{
								AssetBundle assetBundle = null;
								byte[] buffer = new byte[1024 * 1024 * 30]; // 30MB

								int read = 0;
								while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
								{
									memoryStream.Write(buffer, 0, read);
								}

								assetBundle = AssetBundle.LoadFromMemory(memoryStream.ToArray());
								Instance.GrancoinShopAssetBundle.Add(orientationValue, assetBundle);
							}

							memoryStream.Flush();
						}
					}

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
		///GrancoinShopManager 클래스의 싱글톤 객체가 초기화되었는지 확인합니다.
		///</summary>
		public static bool IsInitialized()
		{
			return Instance != null;
		}

		///<summary>
		///그랑코인 구매창을 DLL에 포함된 에셋번들 리소스로부터 로드합니다.
		///로드된 그랑몬스터 위젯 객체는 매개변수로 받은 Transform의 자식으로 들어갑니다.
		///이 다이얼로그에서 구매 버튼이 눌려졌을 때 구매 프로세스를 수행할 수 있도록 매개변수로 콜백함수를 전달하십시오.
		///위젯을 종료하는 순간에 어떠한 일을 수행하고 싶은 경우 마지막 매개변수로 콜백함수를 전달하십시오.
		///</summary>
		internal static System.Collections.IEnumerator LoadGrancoinShopObject(string appKey, Orientation orientation, Transform parent,
			MonoBehaviour monoBehaviour, Action<Exchange, string> onPurchaseButtonClick, Action onDialogOpenCallBack = null, Action onDialogCloseCallBack = null)
		{
			if (User.IsInitialized() == false)
			{
				Utility.DebugLog("GrancoinShopManager/LoadGrancoinShopObject/User not loggined.");
				yield break;
			}

			if (IsInitialized() == false)
			{
				if (Initialize == false)
				{
					Utility.DebugLog("GrancoinShopManager/LoadGrancoinShopObject/Singleton instance is not initialized");
				}
			}

			// ShopWindow 생성 및 초기화
			AssetBundle assetBundle = Instance.GrancoinShopAssetBundle[orientation.GetValue()];
			Instance.ShopOrientation = orientation;

			if (assetBundle == null)
			{
				Utility.DebugLog("GrancoinShopManager/LoadGrancoinShopObject/Invalid orientation type.");
				yield break;
			}

			string resourceName = "ShopWindow(" + Instance.ShopOrientation.ToString() + ")";
			Instance.RootObject = UnityEngine.Object.Instantiate(assetBundle.LoadAsset(resourceName)) as GameObject;
			if (Instance.RootObject == null)
			{
				Utility.DebugLog("GrancoinShopManager/LoadGrancoinShopObject/Load AssetBundle Failed.");
				yield break;
			}

			Instance.RootObject.transform.SetParent(parent);
			Instance.RootObject.transform.localScale = Vector3.one;
			Instance.RootObject.SetActive(true);

			RectTransform rectTransform = Instance.RootObject.GetComponent<RectTransform>();
			rectTransform.anchoredPosition3D = Vector3.zero;
			rectTransform.sizeDelta = Vector2.zero;
			rectTransform.rotation = new Quaternion(0, 0, 0, 0);

			// 로딩창을 연다.
			Transform loadingPanel = Instance.RootObject.transform.Find("Loading");
			loadingPanel.gameObject.AddComponent<LoadingPanel>();
			loadingPanel.gameObject.SetActive(true);

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
				Utility.DebugLog(Instance.ToString() + "/LoadGrancoinShopObject/Error Code : " + responseExchangeList.State);
				loadingPanel.gameObject.SetActive(false);
				yield break;
			}

			// UI 세팅
			int availableShopObjectCount = 0;
			Transform exchangeList = Instance.RootObject.transform.Find("Contents/StockListPanel/ListView/ScrollView/Content");

			foreach (ResponseProtocol.ResponseExchange responseExchange in responseExchangeList.ExchangeList)
			{
				// 상점에서 판매하는 아이템 리스트 생성
				GameObject exchangeListItem = UnityEngine.Object.Instantiate(
					assetBundle.LoadAsset("ShopObject(" + Instance.ShopOrientation.ToString() + ")")) as GameObject;

				if (exchangeList == null ||
					exchangeListItem == null)
				{
					Utility.DebugLog("GrancoinShopManager/LoadGrancoinShopObject/Load AssetBundle Failed");
					loadingPanel.gameObject.SetActive(false);
					yield break;
				}

				exchangeListItem.transform.SetParent(exchangeList);
				exchangeListItem.transform.localScale = Vector3.one;

				rectTransform = exchangeListItem.GetComponent<RectTransform>();
				rectTransform.anchoredPosition3D = Vector3.zero;
				rectTransform.rotation = new Quaternion(0, 0, 0, 0);

				// 상품 세팅.
				// 상품 정보(가격, 아이콘 등)는 프로그램에서 관리하지 않고 디자인으로 해결한다.
				string iconImagePath = string.Empty;
				switch (requestData.orientation.GetValue())
				{
					case Orientation.Type.Vertical: iconImagePath = responseExchange.ExchangeVImagePath; break;
					case Orientation.Type.Horizontal: iconImagePath = responseExchange.ExchangeHImagePath; break;
					default:
						Utility.DebugLog(Instance.ToString() + "/LoadGrancoinShopObject/Invalid orientation.");
						loadingPanel.gameObject.SetActive(false);
						yield break;
				}

				WWW www = new WWW(ServerConnection.GetProductIconResourceUrl(requestData.orientation) + iconImagePath);
				yield return www;

				Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
				UnityEngine.UI.Image image = exchangeListItem.GetComponent<UnityEngine.UI.Image>();

				image.sprite = sprite;
				image.type = UnityEngine.UI.Image.Type.Simple;
				image.preserveAspect = false;

				// 버튼에 콜백 함수 등록
				exchangeListItem.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
				{
					// 그랑몬스터 서버에게 페이로드를 먼저 요청한다.
					RequestProtocol.Payload requestPayload = new RequestProtocol.Payload
					{
						appKey = requestData.appKey,
						userKey = requestData.userKey,
						appTypeEnum = Utility.GetRuntimePlatform(),
					};

					ServerResponse payloadResponse = ServerConnection.Payload(JsonUtility.ToJson(requestPayload));
					if (RestApiCallStatusMethods.Error(payloadResponse.Status))
					{
						Utility.DebugLog(Instance.ToString() + "/LoadGrancoinShopObject/Payload request failed.");
						loadingPanel.gameObject.SetActive(false);
						return;
					}

					ResponseProtocol.ResponsePayLoad responsePayLoad = ResponseProtocol.ResponsePayLoad.CreateFromJson(payloadResponse.Data);
					if (responsePayLoad.State != ResponseProtocol.ResponsePayLoad.StatusCode.SUCCESS)
					{
						Utility.DebugLog(Instance.ToString() + "/LoadGrancoinShopObject/Error Code : " + responsePayLoad.State);
						loadingPanel.gameObject.SetActive(false);
						return;
					}

					// 페이로드를 받았으면 유저가 등록한 결제 로직을 수행한다.
					onPurchaseButtonClick(new Exchange(responseExchange.ExchangeKey, responseExchange.ExchangeMoney, responseExchange.ExchangeCoin,
						responseExchange.ExchangeName, responseExchange.ExchangeVImagePath, responseExchange.ExchangeHImagePath), responsePayLoad.UserPayload);
				});

				++availableShopObjectCount;
			}

			// 해상도 대응(2 by 4로 고정)
			RectTransform scrollViewRectTransform = exchangeList.parent.GetComponent<RectTransform>();
			UnityEngine.UI.GridLayoutGroup gridLayoutGroup = exchangeList.GetComponent<UnityEngine.UI.GridLayoutGroup>();
			gridLayoutGroup.cellSize = new Vector2(scrollViewRectTransform.rect.width / 4, scrollViewRectTransform.rect.height / 2);

			// 보여져야 할 상품이 한 페이지에 최대로 보여질 수 있는 상품의 수보다 작을 때,
			// '준비 중인 상품입니다.'와 같이 더미 오브젝트를 추가한다.
			if (MaxShopObjectCountPerPage.TryGetValue(requestData.orientation.GetValue(), out int maxShopObjectCountPerPage) == false)
			{
				Utility.DebugLog("GrancoinShopManager/LoadGrancoinShopObject/Invalid Orientation.");
				loadingPanel.gameObject.SetActive(false);
				yield break;
			}

			if (availableShopObjectCount < maxShopObjectCountPerPage)
			{
				for (int i = availableShopObjectCount; i < maxShopObjectCountPerPage; ++i)
				{
					// 상점에서 판매하는 아이템 리스트 생성
					GameObject exchangeListItem = UnityEngine.Object.Instantiate(
						assetBundle.LoadAsset("ShopObject(" + Instance.ShopOrientation.ToString() + ")")) as GameObject;

					if (exchangeList == null ||
						exchangeListItem == null)
					{
						Utility.DebugLog("GrancoinShopManager/LoadGrancoinShopObject/Load AssetBundle Failed");
						continue;
					}

					exchangeListItem.transform.SetParent(exchangeList);
					exchangeListItem.transform.localScale = Vector3.one;

					rectTransform = exchangeListItem.GetComponent<RectTransform>();
					rectTransform.anchoredPosition3D = Vector3.zero;
					rectTransform.rotation = new Quaternion(0, 0, 0, 0);
				}
			}

			// 닫기 버튼 이벤트 등록
			string closeButtonPath = string.Empty;
			switch (Instance.ShopOrientation.GetValue())
			{
				case Orientation.Type.Vertical: closeButtonPath = "Contents/ShopInfoPanel/CloseButton"; break;
				case Orientation.Type.Horizontal: closeButtonPath = "Contents/Background/CloseButton"; break;
				default: Utility.DebugLog("GrancoinShopManager/LoadGrancoinShopObject/Invalid Orientation."); break;
			}

			Transform closeButton = Instance.RootObject.transform.Find(closeButtonPath);
			closeButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { OnDestroy(); });

			// 위젯 컨트롤에 필요한 MonoBehavior 추가
			Instance.RootObject.AddComponent<GrancoinShopHandler>();
			Instance.RootObject.GetComponent<GrancoinShopHandler>().OnShopClose = onDialogCloseCallBack;

			// 로딩 완료
			loadingPanel.gameObject.SetActive(false);
		}

		public static ResponseProtocol.ResponseExhaust RequestPurchaseByGranCoin(string appKey, int coin)
		{
			ResponseProtocol.ResponseExhaust responseExhaust = new ResponseProtocol.ResponseExhaust
			{
				State = ResponseProtocol.ResponseExhaust.StatusCode.UNKNOWN,
				AfterCoin = 0
			};

			// 유저가 로그인 하지 않은 상태라면 진행할 수 없다.
			if (User.IsInitialized() == false)
			{
				Utility.DebugLog(Instance.ToString() + "/RequestPurchaseByGranCoin/User not loggined.");
				return responseExhaust;
			}

			// 가지고 있는 그랑코인이 부족하면 구매할 수 없다.
			if (User.GetCoin() < coin)
			{
				Utility.DebugLog(Instance.ToString() + "/RequestPurchaseByGranCoin/Not enough gran coin.");
				responseExhaust.State = ResponseProtocol.ResponseExhaust.StatusCode.NOT_ENOUGH_COIN;
				return responseExhaust;
			}

			responseExhaust.AfterCoin = User.GetCoin();

			// 먼저 그랑 코인을 사용하기 위해 서버에 Payload를 요청한다.
			RequestProtocol.Payload requestPayload = new RequestProtocol.Payload
			{
				userKey = User.GetUserKey(),
				appKey = appKey,
				appTypeEnum = Utility.GetRuntimePlatform(),
			};

			ServerResponse payloadResponse = ServerConnection.Payload(JsonUtility.ToJson(requestPayload));
			if (RestApiCallStatusMethods.Error(payloadResponse.Status))
			{
				Utility.DebugLog(Instance.ToString() + "/RequestPurchaseByGranCoin/Payload request failed.");
				return responseExhaust;
			}

			ResponseProtocol.ResponsePayLoad responsePayLoad = ResponseProtocol.ResponsePayLoad.CreateFromJson(payloadResponse.Data);
			if (responsePayLoad.State != ResponseProtocol.ResponsePayLoad.StatusCode.SUCCESS)
			{
				Utility.DebugLog(Instance.ToString() + "/RequestPurchaseByGranCoin/Payload response data error : " + responsePayLoad.State);
				return responseExhaust;
			}

			// Payload 요청이 정상적으로 수락되었기 때문에 결제 로직을 진행한다.
			RequestProtocol.Exhaust requestExhaust = new RequestProtocol.Exhaust
			{
				userKey = User.GetUserKey(),
				appKey = appKey,
				coin = coin,
				payload = responsePayLoad.UserPayload,
				appTypeEnum = Utility.GetRuntimePlatform(),
			};

			ServerResponse exhaustResponse = ServerConnection.Exhaust(JsonUtility.ToJson(requestExhaust));
			if (RestApiCallStatusMethods.Error(exhaustResponse.Status))
			{
				Utility.DebugLog(Instance.ToString() + "/RequestPurchaseByGranCoin/Exhaust Rest Api call error : " + responsePayLoad.State);
				return responseExhaust;
			}

			responseExhaust = ResponseProtocol.ResponseExhaust.CreateFromJson(exhaustResponse.Data);
			if (responseExhaust.State == ResponseProtocol.ResponseExhaust.StatusCode.SUCCESS)
			{
				// 구매에 성공하였으므로 유저의 코인 정보를 갱신한다.
				User.SetCoin(responseExhaust.AfterCoin);
			}

			return responseExhaust;
		}

		internal static void OnDestroy()
		{
			Instance.RootObject.GetComponent<GrancoinShopHandler>().OnShopClose?.Invoke();

			UnityEngine.Object.Destroy(Instance.RootObject.gameObject);

			Instance.RootObject = null;
			Instance.ShopOrientation = new Orientation(Orientation.Type.None);
		}
	}

	internal class GrancoinShopHandler : MonoBehaviour
	{
		public Action OnShopClose { get; set; }

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				GrancoinShopManager.OnDestroy();
			}
		}
	}
}
