using UnityEngine;
using UnityEngine.SceneManagement;

using granmonster;

public class WidgetTestManager : MonoBehaviour {
	public Transform buttonParent;
	public Transform widgetParent;

	private void Start()
	{
		// 반드시 그랑몬스터 위젯 버튼을 먼저 초기화 해야합니다.
		bool success = GranmonsterWidgetButton.Initialize(GlobalConstants.GranmonsterGameKey,
			new Orientation(Orientation.Type.Horizontal), widgetParent, this, OnWidgetOpen, OnWidgetClose);
		Debug.Log("Initialize : " + success);

		// 100, 100 위치에 플로팅 버튼을 띄웁니다.
		// 위치는 상황에 맞게 어느 곳에서나 띄울 수 있습니다.
		GranmonsterWidgetButton.Show(buttonParent, new Vector3(100, 100, 0));
	}

	public void OnWidgetOpen()
	{
		// NOTE: 위젯 다이얼로그를 열고나서 해야할 작업이 있다면 이곳에 코드를 작성합니다.
		Utility.DebugLog("WidgetTestManager/OnWidgetOpen");
	}

	public void OnWidgetClose()
	{
		// NOTE: 위젯 다이얼로그를 닫은 후에 해야할 작업이 있다면 이곳에 코드를 작성합니다.
		Utility.DebugLog("WidgetTestManager/OnWidgetClose");
	}

	public void OnBackButtonClick()
	{
		SceneManager.LoadScene("Main");
	}
}
