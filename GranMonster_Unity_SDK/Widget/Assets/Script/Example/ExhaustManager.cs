using UnityEngine;
using granmonster;

public class ExhaustManager : MonoBehaviour {
	public int exhaustAmount;

	public void OnExhaustTestButtonClicked()
	{
		ResponseProtocol.ResponseExhaust responseExhaust = GrancoinShopManager.RequestPurchaseByGranCoin(
			GlobalConstants.GranmonsterGameKey, exhaustAmount);

		if (responseExhaust.State == ResponseProtocol.ResponseExhaust.StatusCode.SUCCESS)
		{
			// 그랑코인으로 아이템 구매에 성공하였습니다.
			Utility.DebugLog("그랑코인으로 아이템 구매에 성공하였습니다.");
		}
		else if (responseExhaust.State == ResponseProtocol.ResponseExhaust.StatusCode.NOT_ENOUGH_COIN)
		{
			Debug.Log("그랑코인이 부족합니다!");
		}
	}
}
