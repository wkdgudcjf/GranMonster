using UnityEngine;
using UnityEngine.SceneManagement;

using granmonster;

public class MainStageManager : MonoBehaviour {
	public Transform parent;
	public Transform[] testButtons;

	private void Start()
	{
		if (WidgetManager.IsInitialized() == false)
		{
			Utility.DebugLog("로딩 시간을 줄이기 위해 위젯을 미리 로드합니다.");
			string debugMessage = WidgetManager.Initialize ?
				"위젯 미리 로드에 성공하였습니다." : "위젯 미리 로드에 실패하였습니다.";
			Utility.DebugLog(debugMessage);
		}

		if (GrancoinShopManager.IsInitialized() == false)
		{
			Utility.DebugLog("로딩 시간을 줄이기 위해 그랑코인 샵을 미리 로드합니다.");
			string debugMessage = GrancoinShopManager.Initialize ?
				"그랑코인 샵 미리 로드에 성공하였습니다." : "그랑코인 샵 미리 로드에 실패하였습니다.";
			Utility.DebugLog(debugMessage);
		}
	}

	public void OnButtonClick(string nextScene)
	{
		SceneManager.LoadScene(nextScene);
	}

	public void OnYesButtonClicked()
	{
		Debug.Log("YES!!");
	}

	public void OnNoButtonClicked()
	{
		Debug.Log("NO!!");
	}

	public void OnCloseButtonClicked()
	{
		Debug.Log("CLOSE!!");
	}
}
