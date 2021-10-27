using UnityEngine;
using UnityEngine.SceneManagement;

using granmonster;

public class GranmonsterLoginManager : MonoBehaviour {
	public UnityEngine.UI.Button granmonsterLoginButton;
	public UnityEngine.UI.Button googleLoginButton;

	public void Authentification()
	{
		// 로그인 요청
		BeginBlocking();

		ServerResponse loginResponse = ServerConnection.Login(GlobalConstants.TestUserKey, GlobalConstants.GranmonsterGameKey, "whquddn20@naver.com");
		if (RestApiCallStatusMethods.Error(loginResponse.Status))
		{
			Utility.DebugLog(loginResponse.Data);
			EndBlocking();

			return;
		}

		OnLoginSuccess();
	}

	private void OnLoginSuccess()
	{
		// 로그인에 성공했으면 다음 Scene으로 넘어가도록 한다.
		SceneManager.LoadScene("Main");
		EndBlocking();
	}

	public void BeginBlocking()
	{
		granmonsterLoginButton.enabled = false;
		googleLoginButton.enabled = false;
	}

	public void EndBlocking()
	{
		granmonsterLoginButton.enabled = true;
		googleLoginButton.enabled = true;
	}
}
