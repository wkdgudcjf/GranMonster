using UnityEngine;
using UnityEngine.SceneManagement;

using granmonster;

public class ShopTestManager : MonoBehaviour {
	public Transform buttonParent;
	public Transform widgetParent;

	public IAPManager IAPManager;

	private void Start ()
	{
		// 반드시 그랑몬스터 상점 버튼을 먼저 초기화 해야합니다.
		GranmonsterShopButton.Initialize(GlobalConstants.GranmonsterGameKey, new Orientation(Orientation.Type.Horizontal),
			widgetParent, this, IAPManager.InitializePurchasing, IAPManager.ProcessPurchase, OnShopOpen, OnShopClose);

		// 200, 200 위치에 플로팅 버튼을 띄웁니다.
		// 위치는 상황에 맞게 어느 곳에서나 띄울 수 있습니다.
		GranmonsterShopButton.Show(buttonParent, new Vector3(200, 200, 0));
	}

	public void OnShopOpen()
	{
		// NOTE: 그랑코인 상점 다이얼로그를 열고나서 해야할 작업이 있다면 이곳에 코드를 작성합니다.
		Utility.DebugLog("ShopTestManager/OnShopOpen");
	}

	public void OnShopClose()
	{
		// NOTE: 그랑코인 상점 다이얼로그를 닫은 후에 해야할 작업이 있다면 이곳에 코드를 작성합니다.
		Utility.DebugLog("ShopTestManager/OnShopClose");
	}

	public void OnBackButtonClick()
	{
		SceneManager.LoadScene("Main");
	}
}