using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using granmonster;

public class GoogleLoginManager : MonoBehaviour {
	public UnityEngine.UI.Button granmonsterLoginButton;
	public UnityEngine.UI.Button googleLoginButton;

	public override string ToString() { return "GoogleLoginManager"; }

	IEnumerator TryAuthentification()
	{
		yield return new WaitForSeconds(2.0f);

		Social.localUser.Authenticate((bool success) =>
		{
			if (success)
			{
				ServerResponse loginResponse = ServerConnection.Login(Social.localUser.id, GlobalConstants.GranmonsterGameKey);
				if (RestApiCallStatusMethods.Error(loginResponse.Status))
				{
					Utility.DebugLog(ToString() + "/TryAuthentification/" + loginResponse.Data);
					return;
				}

				// 첫 로그인 이벤트를 진행합니다.
				ResponseProtocol.ResponseLogin responseLogin = ResponseProtocol.ResponseLogin.CreateFromJson(loginResponse.Data);
				if (responseLogin.State != ResponseProtocol.ResponseLogin.StatusCode.SUCCESS)
				{
					Utility.DebugLog(ToString() + "/TryAuthentification/Error code : " + responseLogin.State);
					return;
				}

				if (responseLogin.FirstLogin)
				{
					granmonster.EventManager.EventComplete(GlobalConstants.GranmonsterGameKey, "install");
					granmonster.EventManager.EventComplete(GlobalConstants.GranmonsterGameKey, "first_run");
				}

				OnLoginSuccess();
			}
			else
			{
				Debug.Log("구글 로그인에 실패하였습니다.");
			}
		});

		EndBlocking();
	}

	public void Authentification()
	{
		BeginBlocking();

		GooglePlayGames.BasicApi.PlayGamesClientConfiguration configuration = new GooglePlayGames.BasicApi.PlayGamesClientConfiguration.Builder().Build();
		GooglePlayGames.PlayGamesPlatform.InitializeInstance(configuration);
		GooglePlayGames.PlayGamesPlatform.DebugLogEnabled = true;
		GooglePlayGames.PlayGamesPlatform.Activate();

		StartCoroutine("TryAuthentification");
	}

	private void OnLoginSuccess()
	{
		// 로그인에 성공했으면 다음 Scene으로 넘어가도록 한다.
		SceneManager.LoadScene("Main");
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