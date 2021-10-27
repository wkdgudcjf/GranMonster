using UnityEngine;
using UnityEngine.SceneManagement;

using granmonster;

public class EventManager : MonoBehaviour {
	public Transform widgetParent;
	public Transform testUi;
	public Transform console;

	public void RequestEventComplete(string eventKey)
	{
		ResponseProtocol.ResponseEvent responseEvent =
			granmonster.EventManager.EventComplete(GlobalConstants.GranmonsterGameKey, eventKey);
		console.GetComponentInChildren<UnityEngine.UI.Text>().text = responseEvent != null ? "" + responseEvent.State : "실패하였습니다.";
	}

	public void RequestEventReward(string eventKey)
	{
		ResponseProtocol.ResponseEventReward responseEventReward =
			granmonster.EventManager.EventReward(GlobalConstants.GranmonsterGameKey, eventKey);
		console.GetComponentInChildren<UnityEngine.UI.Text>().text = responseEventReward != null ? "" + responseEventReward.State : "실패하였습니다.";
	}

	public void OnBackButtonClick()
	{
		SceneManager.LoadScene("Main");
	}
}
