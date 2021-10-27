using UnityEngine;
using granmonster;

public class GranCoinManager : MonoBehaviour {
	public UnityEngine.UI.Text granCoin;

	private void Awake()
	{
		User.AddGranCoinValueChangedHandler((value) =>
		{
			if (granCoin != null)
			{
				granCoin.text = value.ToString("N0");
			}
		});
	}
}
