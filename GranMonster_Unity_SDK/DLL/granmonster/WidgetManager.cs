using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace granmonster
{
	///<summary>
	///위젯의 데이터를 로드하고 관리하는 클래스입니다.
	///싱글톤으로 사용되며 수동으로 초기화를 할 수 있습니다.
	///초기화를 하지 않고 별도의 메소드 호출 시에는 내부적으로 초기화 과정을 거치도록 되어있습니다.
	///</summary>
	public class WidgetManager
	{
		private class GameData
		{
			public string AppKey { get; set; }
			public string AppName { get; set; }
			public string AppUrl { get; set; }
			public string AppImageIconPath { get; set; }
			public List<string> AppImageVerticalBannerPath { get; set; }
			public List<string> AppImageHorizontalBannerPath { get; set; }
			public string AppPackage { get; set; }
			public List<MissionData> MissionDatas { get; set; }
			public bool AppInstall { get; set; }

			public GameData()
			{
				AppKey = string.Empty;
				AppName = string.Empty;
				AppUrl = string.Empty;
				AppImageIconPath = string.Empty;
				AppImageVerticalBannerPath = new List<string>();
				AppImageHorizontalBannerPath = new List<string>();
				AppPackage = string.Empty;
				MissionDatas = null;
				AppInstall = false;
			}

			public GameData(string appKey, string appName, string appUrl, string appImageIconPath,
				List<string> appImageVerticalBannerPath, List<string> appImageHorizontalBannerPath,
				string appPackage, List<MissionData> missionDatas, bool appInstall)
			{
				AppKey = appKey;
				AppName = appName;
				AppUrl = appUrl;
				AppImageIconPath = appImageIconPath;
				AppImageVerticalBannerPath = appImageVerticalBannerPath;
				AppImageHorizontalBannerPath = appImageHorizontalBannerPath;
				AppPackage = appPackage;
				MissionDatas = missionDatas;
				AppInstall = appInstall;
			}

			public GameData(ResponseProtocol.AppList appList)
			{
				AppKey = appList.AppKey;
				AppName = appList.AppName;
				AppUrl = appList.AppUrl;
				AppImageIconPath = appList.AppImageIconPath;

				AppImageVerticalBannerPath = new List<string>()
				{
					"/appVBanner1/" + appList.AppImageVBannerPath1,
					"/appVBanner2/" + appList.AppImageVBannerPath2,
					"/appVBanner3/" + appList.AppImageVBannerPath3
				};
				AppImageHorizontalBannerPath = new List<string>()
				{
					"/appHBanner1/" + appList.AppImageHBannerPath1,
					"/appHBanner2/" + appList.AppImageHBannerPath2,
					"/appHBanner3/" + appList.AppImageHBannerPath3
				};
				AppPackage = appList.AppPackage;

				List<MissionData> temp = new List<MissionData>();
				foreach (ResponseProtocol.AppEventList appEventList in appList.AppEventList)
				{
					temp.Add(new MissionData(appEventList));
				}

				MissionDatas = temp;
				AppInstall = appList.AppInstall;
			}
		}

		private class MissionData
		{
			public enum MissionState
			{
				Progress,
				Complete,
				Received,
			}

			public int AppEventId { get; set; }
			public int AppId { get; set; }
			public string AppEventContent { get; set; }
			public string AppEventKey { get; set; }
			public int AppEventCoin { get; set; }
			public bool AppEventRewardEnable { get; set; }
			public bool AppEventSuccessEnable { get; set; }

			public MissionData()
			{
				AppEventId = 0;
				AppId = 0;
				AppEventContent = string.Empty;
				AppEventKey = string.Empty;
				AppEventCoin = 0;
				AppEventRewardEnable = false;
				AppEventSuccessEnable = false;
			}

			public MissionData(int appEventId, int appId, string appEventContent,
				string appEventKey, int appEventCoin, bool appEventRewardEnable, bool appEventSuccessEnable)
			{
				AppEventId = appEventId;
				AppId = appId;
				AppEventContent = appEventContent;
				AppEventKey = appEventKey;
				AppEventCoin = appEventCoin;
				AppEventRewardEnable = appEventRewardEnable;
				AppEventSuccessEnable = appEventSuccessEnable;
			}

			public MissionData(ResponseProtocol.AppEventList appEventList)
			{
				AppEventId = appEventList.AppEventId;
				AppId = appEventList.AppId;
				AppEventContent = appEventList.AppEventContent;
				AppEventKey = appEventList.AppEventKey;
				AppEventCoin = appEventList.AppEventCoin;
				AppEventRewardEnable = appEventList.AppEventRewardEnable;
				AppEventSuccessEnable = appEventList.AppEventSuccessEnable;
			}
		}

		private static WidgetManager Instance { get; set; }

		private Dictionary<Orientation.Type, AssetBundle> WidgetAssetBundle { get; set; }
		private GameObject RootObject { get; set; }
		private GameData SelectedGame { get; set; }
		private int SelectedThumbnailImageIndex { get; set; }
		private Orientation WidgetOrientation { get; set; }

		private Dictionary<string, GameData> GameDatas { get; set; }
		private Dictionary<string, Sprite> BannerImageCache { get; set; }

		private WidgetManager() { }

		///<summary>
		///현재 개체를 나타내는 문자열을 반환합니다.
		///</summary>
		public override string ToString() { return "WidgetManager"; }

		///<summary>
		///WidgetManager 클래스의 싱글톤 객체를 초기화합니다.
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

					Instance = new WidgetManager
					{
						WidgetAssetBundle = new Dictionary<Orientation.Type, AssetBundle>(),
						SelectedThumbnailImageIndex = 0
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
							string resourcePath = "granmonster.Properties.adwindow(" + orientation.ToString().ToLower() + ")";
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
								Instance.WidgetAssetBundle.Add(orientationValue, assetBundle);
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
		///WidgetManager 클래스의 싱글톤 객체가 초기화되었는지 확인합니다.
		///</summary>
		public static bool IsInitialized()
		{
			return Instance != null;
		}

		///<summary>
		///그랑몬스터 위젯을 DLL에 포함된 에셋번들 리소스로부터 로드합니다.
		///로드된 그랑몬스터 위젯 객체는 매개변수로 받은 Transform의 자식으로 들어갑니다.
		///위젯을 종료하는 순간에 어떠한 일을 수행하고 싶은 경우 마지막 매개변수로 콜백함수를 전달하십시오.
		///위젯 로딩 중에 다시 이 함수를 호출하지 않도록 예외처리를 하여야 합니다.
		///</summary>
		internal static System.Collections.IEnumerator LoadWidgetObject(string appKey, Orientation orientation,
			Transform parent, MonoBehaviour monoBehaviour, Action onDialogOpenCallBack = null, Action onDialogCloseCallBack = null)
		{
			if (User.IsInitialized() == false)
			{
				Utility.DebugLog("WidgetManager/LoadWidgetObject/User not loggined.");
				yield break;
			}

			if (IsInitialized() == false)
			{
				if (Initialize == false)
				{
					Utility.DebugLog("WidgetManager/LoadWidgetObject/Singleton instance is not initialized.");
					yield break;
				}
			}
			else
			{
				OnDestroy();
			}

			Instance.GameDatas = new Dictionary<string, GameData>();
			Instance.BannerImageCache = new Dictionary<string, Sprite>();

			// AdWindow 생성 및 초기화
			AssetBundle assetBundle = Instance.WidgetAssetBundle[orientation.GetValue()];
			Instance.WidgetOrientation = orientation;

			string resourceName = "AdWindow(" + Instance.WidgetOrientation.ToString() + ")";
			Instance.RootObject = UnityEngine.Object.Instantiate(assetBundle.LoadAsset(resourceName)) as GameObject;
			if (Instance.RootObject == null)
			{
				Utility.DebugLog("WidgetManager/LoadWidgetObject/Load AssetBundle Failed.");
				yield break;
			}

			// 로딩창을 연다.
			Transform loadingPanel = Instance.RootObject.transform.Find("Loading");
			loadingPanel.gameObject.AddComponent<LoadingPanel>();
			loadingPanel.gameObject.SetActive(true);

			Instance.RootObject.transform.SetParent(parent);
			Instance.RootObject.transform.localScale = Vector3.one;
			Instance.RootObject.SetActive(true);

			RectTransform rectTransform = Instance.RootObject.GetComponent<RectTransform>();
			rectTransform.anchoredPosition3D = Vector3.zero;
			rectTransform.sizeDelta = Vector2.zero;
			rectTransform.rotation = new Quaternion(0, 0, 0, 0);

			// 서버에 App List 데이터 요청
			RequestProtocol.AppList requestData = new RequestProtocol.AppList
			{
				userKey = User.GetUserKey(),
				appKey = appKey,
				orientation = orientation,
				appTypeEnum = Utility.GetRuntimePlatform(),
			};

			ServerResponse serverResponse = ServerConnection.RequestAppList(JsonUtility.ToJson(requestData));
			if (RestApiCallStatusMethods.Error(serverResponse.Status))
			{
				Utility.DebugLog("WidgetManager/LoadWidgetObject/" + serverResponse.Status + ", " + serverResponse.Data);
				loadingPanel.gameObject.SetActive(false);
				yield break;
			}

			// 서버로부터 전달받은 데이터를 로컬 메모리에 저장한다.
			Dictionary<string, GameData> tempGameDatas = new Dictionary<string, GameData>();
			ResponseProtocol.ResponseAppList responseAppList = ResponseProtocol.ResponseAppList.CreateFromJson(serverResponse.Data);
			if (responseAppList == null ||
				responseAppList.State != ResponseProtocol.ResponseAppList.StatusCode.SUCCESS)
			{
				Utility.DebugLog("WidgetManager/LoadWidgetObject/Error Code : " + responseAppList.State);
				loadingPanel.gameObject.SetActive(false);
				yield break;
			}

			foreach (ResponseProtocol.AppList app in responseAppList.AppList)
			{
				if (app.AppPackage == "" ||
					app.AppPackage == string.Empty ||
					tempGameDatas.ContainsKey(app.AppPackage))
				{
					continue;
				}

				tempGameDatas.Add(app.AppPackage, new GameData(app));
			}
			Instance.GameDatas = tempGameDatas;

			foreach (ResponseProtocol.AppList app in responseAppList.AppList)
			{
				// 게임 리스트 생성
				string gameListPath = "";
				switch (Instance.WidgetOrientation.GetValue())
				{
					case Orientation.Type.Vertical: gameListPath = "Contents/GameList/ListView/ScrollView/Content"; break;
					case Orientation.Type.Horizontal: gameListPath = "Contents/Footer/GameList/ListView/ScrollView/Content"; break;
					default: Utility.DebugLog("WidgetManager/LoadWidgetObject/Invalid Orientation."); break;
				}

				Transform gameList = Instance.RootObject.transform.Find(gameListPath);
				GameObject gameListItem = UnityEngine.Object.Instantiate(
					assetBundle.LoadAsset("GameListItem(" + Instance.WidgetOrientation.ToString() + ")")) as GameObject;

				if (gameList == null ||
					gameListItem == null)
				{
					Utility.DebugLog("WidgetManager/LoadWidgetObject/Load AssetBundle Failed.");
					continue;
				}

				gameListItem.transform.SetParent(gameList);
				gameListItem.transform.localScale = Vector3.one;

				rectTransform = gameListItem.GetComponent<RectTransform>();
				rectTransform.anchoredPosition3D = Vector3.zero;
				rectTransform.rotation = new Quaternion(0, 0, 0, 0);

				// 게임 아이콘 세팅
				WWW www = new WWW(ServerConnection.GetAppIconImageResourceUrl() + app.AppImageIconPath);
				yield return www;

				Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
				UnityEngine.UI.Image image = gameListItem.GetComponent<UnityEngine.UI.Image>();

				image.sprite = sprite;
				image.type = UnityEngine.UI.Image.Type.Simple;
				image.preserveAspect = true;

				// 버튼 이벤트 등록
				gameListItem.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { monoBehaviour.StartCoroutine(OnSelectGame(app.AppPackage)); });
			}

			// 닫기 버튼 이벤트 등록
			string closeButtonPath = string.Empty;
			switch (Instance.WidgetOrientation.GetValue())
			{
				case Orientation.Type.Vertical: closeButtonPath = "Contents/Top/CloseButton"; break;
				case Orientation.Type.Horizontal: closeButtonPath = "Contents/TaskBar/CloseButton"; break;
				default: Utility.DebugLog("WidgetManager/LoadWidgetObject/Invalid Orientation."); break;
			}

			Transform closeButton = Instance.RootObject.transform.Find(closeButtonPath);
			closeButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { OnDestroy(); });

			// 위젯 컨트롤에 필요한 MonoBehavior 추가
			Instance.RootObject.AddComponent<WidgetHandler>();
			Instance.RootObject.GetComponent<WidgetHandler>().OnWidgetClose = onDialogCloseCallBack;

			// 유저의 코인 보유량 서버 데이터로 갱신 및 세팅
			string userCoinLabelPath = null;
			switch (Instance.WidgetOrientation.GetValue())
			{
				case Orientation.Type.Vertical: userCoinLabelPath = "Contents/TaskBar/GranCoin/Text"; break;
				case Orientation.Type.Horizontal: userCoinLabelPath = "Contents/TaskBar/GranCoin/Text"; break;
				default: Utility.DebugLog("WidgetManager/LoadWidgetObject/Invalid Orientation."); break;
			}

			User.AddGranCoinValueChangedHandler((value) =>
			{
				Transform userCoinLabel = Instance.RootObject.transform.Find(userCoinLabelPath);
				userCoinLabel.GetComponent<UnityEngine.UI.Text>().text = value.ToString("N0");
			});
			User.SetCoin(responseAppList.UserCoin);

			yield return OnSelectGame(Instance.GameDatas.Keys.First());

			loadingPanel.gameObject.SetActive(false);
		}

		private static System.Collections.IEnumerator OnSelectGame(string gameKey)
		{
			if (Instance.GameDatas.TryGetValue(gameKey, out GameData selectedGame) == false)
			{
				Utility.DebugLog("WidgetManager/OnSelectGame/Invalid GameKey.");
				yield break;
			}
			else
			{
				Instance.SelectedGame = selectedGame;
			}

			// 게임 이름 Label 세팅
			string gameTitleLabelPath = null;
			switch (Instance.WidgetOrientation.GetValue())
			{
				case Orientation.Type.Vertical: gameTitleLabelPath = "Contents/TaskBar/GameTitle/Text"; break;
				case Orientation.Type.Horizontal: gameTitleLabelPath = "Contents/TaskBar/GameTitle/Text"; break;
				default: Utility.DebugLog("WidgetManager/LoadWidgetObject/Invalid Orientation."); break;
			}

			Transform gameTitleLabel = Instance.RootObject.transform.Find(gameTitleLabelPath);
			gameTitleLabel.GetComponent<UnityEngine.UI.Text>().text = Instance.SelectedGame.AppName;

			// 게임 스플래시 이미지 변경.
			List<string> splashImageUrls = new List<string>();
			switch (Instance.WidgetOrientation.GetValue())
			{
				case Orientation.Type.Vertical: splashImageUrls = Instance.GameDatas[gameKey].AppImageVerticalBannerPath; break;
				case Orientation.Type.Horizontal: splashImageUrls = Instance.GameDatas[gameKey].AppImageHorizontalBannerPath; break;
				default: Utility.DebugLog("WidgetManager/LoadWidgetObject/Invalid Orientation."); break;
			}

			if (splashImageUrls.Count != 3)
			{
				Utility.DebugLog("WidgetManager/LoadWidgetObject/Icon image URL empty. (GameKey : " + Instance.GameDatas[gameKey] + ")");
				yield break;
			}

			foreach (string splashImageUrl in splashImageUrls)
			{
				int index = splashImageUrls.IndexOf(splashImageUrl) + 1;
				string splashImageHierarchy = "";
				string scrollPointsHierarchy = "";

				switch (Instance.WidgetOrientation.GetValue())
				{
					case Orientation.Type.Vertical:
						splashImageHierarchy = "Contents/Top/ScrollView/Content/SplashImage " + index;
						scrollPointsHierarchy = "Contents/Top/ScrollPoints";
						break;

					case Orientation.Type.Horizontal:
						splashImageHierarchy = "Contents/Body/ScrollView/Content/SplashImage " + index;
						scrollPointsHierarchy = "Contents/Body/ScrollPoints";
						break;

					default:
						Utility.DebugLog("WidgetManager/LoadWidgetObject/Invalid Orientation.");
						break;
				}

				// 캐시 데이터가 있으면 캐시 데이터를 쓴다.
				Sprite sprite = null;
				Transform splashImage = Instance.RootObject.transform.Find(splashImageHierarchy);
				Transform scrollPoints = Instance.RootObject.transform.Find(scrollPointsHierarchy);

				if (Instance.BannerImageCache.ContainsKey(splashImageUrl))
				{
					sprite = Instance.BannerImageCache[splashImageUrl];
				}
				else
				{
					WWW www = new WWW(ServerConnection.GetAppBannerImageResourceUrl(Instance.WidgetOrientation) + splashImageUrl);
					yield return www;

					sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
					Instance.BannerImageCache.Add(splashImageUrl, sprite);
				}

				UnityEngine.UI.Image image = splashImage.GetComponent<UnityEngine.UI.Image>();
				image.sprite = sprite;
				image.type = UnityEngine.UI.Image.Type.Simple;
				image.preserveAspect = false;

				// 해상도 대응
				RectTransform splashImageRectTransform = splashImage.GetComponent<RectTransform>();
				RectTransform splashImageParentRectTransform = splashImage.parent.GetComponent<RectTransform>();
				splashImageRectTransform.anchoredPosition3D = new Vector2(splashImageParentRectTransform.rect.width * (index - 1), splashImageRectTransform.anchoredPosition3D.y);
				splashImageRectTransform.sizeDelta = new Vector2(splashImageParentRectTransform.rect.width, splashImageParentRectTransform.rect.height);

				if (image.gameObject.GetComponent<SwipeDetector>() == null)
				{
					image.gameObject.AddComponent<SwipeDetector>();
					image.gameObject.GetComponent<SwipeDetector>().SetScrollViewContent(
						splashImageUrls.IndexOf(splashImageUrl), splashImage.parent.GetComponent<RectTransform>(), scrollPoints);
				}
				else
				{
					image.gameObject.GetComponent<SwipeDetector>().Init();
				}
			}

			// 미션 리스트 생성.
			string missionListPath = "";
			switch (Instance.WidgetOrientation.GetValue())
			{
				case Orientation.Type.Vertical: missionListPath = "Contents/MissionList/ListView/ScrollView/Content"; break;
				case Orientation.Type.Horizontal: missionListPath = "Contents/Body/MissionList/ListView/ScrollView/Content"; break;
				default: Utility.DebugLog("WidgetManager/LoadWidgetObject/Invalid Orientation."); break;
			}

			Transform missionList = Instance.RootObject.transform.Find(missionListPath);
			if (missionList == null)
			{
				Utility.DebugLog("WidgetManager/OnSelectGame/Load AssetBundle Failed.");
				yield break;
			}

			// 새로운 미션 리스트를 넣기 전에 기존에 있던 미션 리스트를 모두 없앤다.
			foreach (Transform childObject in missionList)
			{
				UnityEngine.Object.Destroy(childObject.gameObject);
			}

			AssetBundle assetBundle = Instance.WidgetAssetBundle[Instance.WidgetOrientation.GetValue()];
			if (assetBundle == null)
			{
				Utility.DebugLog("WidgetManager/LoadWidgetObject/Invalid orientation type.");
				yield break;
			}

			// 새로운 미션 리스트를 넣는다.
			foreach (MissionData missionData in Instance.SelectedGame.MissionDatas)
			{
				GameObject missionListItem = UnityEngine.Object.Instantiate(
					assetBundle.LoadAsset("MissionListItem(" + Instance.WidgetOrientation.ToString() + ")")) as GameObject;
				if (missionListItem == null)
				{
					Utility.DebugLog("WidgetManager/OnSelectGame/Load AssetBundle Failed.");
					continue;
				}

				missionListItem.transform.SetParent(missionList);
				missionListItem.transform.localScale = Vector3.one;

				RectTransform rectTransform = missionListItem.GetComponent<RectTransform>();
				rectTransform.anchoredPosition3D = Vector3.zero;
				rectTransform.rotation = new Quaternion(0, 0, 0, 0);

				try
				{
					missionListItem.transform.Find("Mission").GetComponent<UnityEngine.UI.Text>().text = missionData.AppEventContent;
					missionListItem.transform.Find("Reward").GetComponent<UnityEngine.UI.Text>().text = missionData.AppEventCoin.ToString("N0");

					MissionData.MissionState missionState = MissionData.MissionState.Progress;
					string buttonString = string.Empty;

					if (missionData.AppEventRewardEnable == false &&
						missionData.AppEventSuccessEnable == false)
					{
						buttonString = "ing";
					}
					else if (missionData.AppEventRewardEnable == false &&
						missionData.AppEventSuccessEnable == true)
					{
						buttonString = "Clear";
						missionState = MissionData.MissionState.Complete;

						missionListItem.transform.Find("ReceiveButton").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
						{
							ResponseProtocol.ResponseEventReward responseEventReward =
								EventManager.EventReward(selectedGame.AppKey, missionData.AppEventKey);
							if (responseEventReward.State == ResponseProtocol.ResponseEventReward.StatusCode.SUCCESS)
							{
								// 유저의 코인 보유량 갱신
								User.SetCoin(responseEventReward.Coin);

								// 보상 받기 버튼을 사라지게 하고 미션을 완료했다고 표시
								missionListItem.transform.Find("ReceiveButton").gameObject.SetActive(false);
								missionListItem.transform.Find("State")?.gameObject.SetActive(true);
								missionListItem.transform.Find("State").GetComponent<UnityEngine.UI.Text>().text = "완료";

								// 서버로부터 데이터를 받기 전에 최신 UI를 유지하기 위해서 로컬 데이터를 바꾼다.
								missionData.AppEventRewardEnable = true;
							}
						});
					}
					else if (missionData.AppEventRewardEnable == true &&
						missionData.AppEventSuccessEnable == true)
					{
						buttonString = "Clear";
						missionState = MissionData.MissionState.Received;
					}
					else
					{
						Utility.DebugLog("WidgetManager/LoadWidgetObject/Invalid mission state.");
						buttonString = "---";
						missionState = MissionData.MissionState.Progress;
					}

					if (missionState == MissionData.MissionState.Complete)
					{
						// 보상을 받을 수 있는 상태라면 보상받기 버튼을 활성화 한다.
						missionListItem.transform.Find("ReceiveButton").gameObject.SetActive(true);
						missionListItem.transform.Find("State").gameObject.SetActive(false);
					}
					else
					{
						// 보상을 받을 수 있는 상태가 아니라면 보상받기 버튼을 비활성화 하고 텍스트를 넣는다.
						// 보상을 받을 수 있는 상태라면 보상받기 버튼을 활성화 한다.
						missionListItem.transform.Find("ReceiveButton").gameObject.SetActive(false);
						missionListItem.transform.Find("State")?.gameObject.SetActive(true);
						missionListItem.transform.Find("State").GetComponent<UnityEngine.UI.Text>().text = buttonString;
					}
				}
				catch
				{
					Utility.DebugLog("WidgetManager/OnSelectGame/Cannot Access Children Object.");
					continue;
				}
			}

			// Play 버튼 또는 Install 버튼 세팅.
			string playButtonHierarchy = "";
			string installButtonHierarchy = "";
			switch (Instance.WidgetOrientation.GetValue())
			{
				case Orientation.Type.Vertical:
					playButtonHierarchy = "Contents/PlayButton";
					installButtonHierarchy = "Contents/InstallButton";
					break;

				case Orientation.Type.Horizontal:
					playButtonHierarchy = "Contents/Footer/PlayButton";
					installButtonHierarchy = "Contents/Footer/InstallButton";
					break;

				default:
					Utility.DebugLog("WidgetManager/OnSelectGame/Invalid orientation.");
					yield break;
			}

			Transform playButton = Instance.RootObject.transform.Find(playButtonHierarchy);
			Transform installButton = Instance.RootObject.transform.Find(installButtonHierarchy);
			bool appInstalled = Utility.AppInstalled(Instance.GameDatas[gameKey].AppPackage);

			playButton.gameObject.SetActive(appInstalled);
			installButton.gameObject.SetActive(!appInstalled);

			if (Instance.SelectedGame.AppPackage == Application.identifier)
			{
				// 현재 플레이 중인 게임이라면 아무것도 안 하도록 한다.
				// TODO: 버튼을 비활성화 시켜서 더욱 직관적인 UI가 되도록 해야한다.
			}
			else if (appInstalled)
			{
				// 앱이 설치됐을 때 'Play' 버튼을 누르면 해당 게임이 켜지도록 함.
				playButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
				playButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate {
					AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
					AndroidJavaObject currentActivity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");

					bool succuess = false;
					AndroidJavaObject launchIntent = null;
					try
					{
						launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", Instance.SelectedGame.AppPackage);
						succuess = true;
					}
					catch (Exception exception)
					{
						Utility.DebugLog("WidgetManager/OnSelectGame/Launch application failed. (Exception : " + exception + ")");
					}

					if (succuess)
					{
						currentActivity.Call("startActivity", launchIntent);

						RequestProtocol.InvokePlay requestInvokePlay = new RequestProtocol.InvokePlay
						{
							userKey = User.GetUserKey(),
							appKey = User.GetAppKey(),
							desAppKey = Instance.SelectedGame.AppKey,
							appTypeEnum = Utility.GetRuntimePlatform(),
						};

						ServerResponse serverResponse = ServerConnection.InvokePlay(JsonUtility.ToJson(requestInvokePlay));
						if (RestApiCallStatusMethods.Error(serverResponse.Status))
						{
							// 비정상적인 에러이다.
							Utility.DebugLog("WidgetManager/OnSelectGame/Invoke play failed.");
							return;
						}

						ResponseProtocol.ResponseInvokePlay responseInvokePlay =
							ResponseProtocol.ResponseInvokePlay.CreateFromJson(JsonUtility.ToJson(serverResponse.Data));
						if (responseInvokePlay.State != ResponseProtocol.ResponseInvokePlay.StatusCode.SUCCESS)
						{
							// 비정상적인 에러이다.
							Utility.DebugLog("WidgetManager/OnSelectGame/Invoke play was rejected by server.");
							return;
						}

						// 서버로 플레이 버튼이 눌려졌다고 잘 보내짐.
						Utility.DebugLog("WidgetManager/OnSelectGame/Invoke play succeed.");
					}

					androidJavaClass.Dispose();
					currentActivity.Dispose();
					packageManager.Dispose();
					launchIntent.Dispose();
				});
				installButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
			}
			else
			{
				// 앱이 설치가 안 됐을 때 'Install' 버튼을 누르면 해당 게임을 설치할 수 있는 페이지로 이동.
				installButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
				installButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate {
					Application.OpenURL(Instance.SelectedGame.AppUrl);

					RequestProtocol.InvokeInstall requestInvokeInstall = new RequestProtocol.InvokeInstall
					{
						userKey = User.GetUserKey(),
						appKey = User.GetAppKey(),
						desAppKey = Instance.SelectedGame.AppKey,
						appTypeEnum = Utility.GetRuntimePlatform(),
					};

					ServerResponse serverResponse = ServerConnection.InvokeInstall(JsonUtility.ToJson(requestInvokeInstall));
					if (RestApiCallStatusMethods.Error(serverResponse.Status))
					{
						// 비정상적인 에러이다.
						Utility.DebugLog("WidgetManager/OnSelectGame/Invoke install failed.");
						return;
					}

					ResponseProtocol.ResponseInvokeInstall responseInvokeInstall =
						ResponseProtocol.ResponseInvokeInstall.CreateFromJson(serverResponse.Data);
					if (responseInvokeInstall.State != ResponseProtocol.ResponseInvokeInstall.StatusCode.SUCCESS)
					{
						// 비정상적인 에러이다.
						Utility.DebugLog("WidgetManager/OnSelectGame/Invoke install was rejected by server.");
						return;
					}

					// 서버로 인스톨 버튼이 눌려졌다고 잘 보내짐.
					Utility.DebugLog("WidgetManager/OnSelectGame/Invoke install succeed.");
				});
				playButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
			}

			Instance.RootObject.transform.SetAsLastSibling();
		}

		internal static void OnDestroy()
		{
			Instance?.RootObject?.GetComponent<WidgetHandler>()?.OnWidgetClose?.Invoke();

			UnityEngine.Object.Destroy(obj: Instance.RootObject?.gameObject);

			Instance.RootObject = null;
			Instance.SelectedGame = null;
			Instance.WidgetOrientation = new Orientation(Orientation.Type.None);

			Instance.GameDatas?.Clear();
			Instance.BannerImageCache?.Clear();
			Instance.GameDatas = null;
			Instance.BannerImageCache = null;
		}
	}

	internal class WidgetHandler : MonoBehaviour
	{
		public Action OnWidgetClose { get; set; }

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				WidgetManager.OnDestroy();
			}
		}
	}
}