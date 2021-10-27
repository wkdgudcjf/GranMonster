using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using System.Collections.Generic;

using granmonster;

[System.Serializable]
public class IAPManager : MonoBehaviour, IStoreListener
{
	private static IStoreController m_storeController;
	private static IExtensionProvider m_storeExtensionProvider;

	private GrancoinShopManager.Exchange m_purchsingProductData;
	private string m_payload;

	public override string ToString() { return "IAPManager"; }

	public void InitializePurchasing(List<GrancoinShopManager.Exchange> exchanges)
	{
		if (IsInitialized())
		{
			return;
		}

		ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		foreach (GrancoinShopManager.Exchange exchange in exchanges)
		{
			builder.AddProduct(exchange.ExchangeKey, ProductType.Consumable);
		}

		UnityPurchasing.Initialize(this, builder);
	}

	private bool IsInitialized()
	{
		return m_storeController != null &&
			m_storeExtensionProvider != null;
	}

	public void ProcessPurchase(GrancoinShopManager.Exchange exchange, string payload)
	{
		if (IsInitialized() == false)
		{
			Utility.DebugLog(ToString() + "/ProcessPurchase/Not initialized.");
			return;
		}

		Product product = m_storeController.products.WithID(exchange.ExchangeKey);
		if (product == null ||
			product.availableToPurchase == false)
		{
			Utility.DebugLog(ToString() + "/ProcessPurchase/Not purchasing product, either is not found or is not available for purchase.");
			return;
		}

		if (payload == null)
		{
			Utility.DebugLog(ToString() + "/ProcessPurchase/Invalid payload.");
			return;
		}

		m_purchsingProductData = exchange;
		m_payload = payload;

		m_storeController.InitiatePurchase(product);
	}

	public void RestorePurchases()
	{
		if (IsInitialized() == false)
		{
			Utility.DebugLog(ToString() + "/RestorePurchases/Not initialized.");
			return;
		}

		if (Application.platform != RuntimePlatform.IPhonePlayer &&
			Application.platform != RuntimePlatform.OSXPlayer)
		{
			Utility.DebugLog(ToString() + "/RestorePurchases/Not supported on this platform. Current platform is " + Application.platform);
			return;
		}

		// TODO: App Store일 때에는 Resotre를 자동으로 해주지 않는다. 나중에 작업하자.
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		Utility.DebugLog("OnInitialized: PASS");

		m_storeController = controller;
		m_storeExtensionProvider = extensions;
	}

	public void OnInitializeFailed(InitializationFailureReason reason)
	{
		Utility.DebugLog("OnInitializedFailed: " + reason);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs arguments)
	{
		// 구글 or 애플 스토어 영수증 검증
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
		bool validPurchase = true;
		CrossPlatformValidator validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
			AppleTangle.Data(), Application.identifier);

		try
		{
			IPurchaseReceipt[] receipts = validator.Validate(arguments.purchasedProduct.receipt);
			foreach (IPurchaseReceipt receipt in receipts)
			{
#if UNITY_ANDROID
				GooglePlayReceipt googlePlayReceipt = receipt as GooglePlayReceipt;
				if (googlePlayReceipt != null)
				{
					RequestProtocol.Purchase requestPurchase = new RequestProtocol.Purchase
					{
						userKey = User.GetUserKey(),
						appKey = GlobalConstants.GranmonsterGameKey,
						coin = m_purchsingProductData.ExchangeCoin,
						price = m_purchsingProductData.ExchangeMoney,
						payload = m_payload,
						productId = arguments.purchasedProduct.definition.id,
						purchaseToken = googlePlayReceipt.purchaseToken,
						appTypeEnum = AppTypeEnum.ANDROID,
					};

					ServerResponse serverResponse = ServerConnection.Purchase(JsonUtility.ToJson(requestPurchase));
					if (RestApiCallStatusMethods.Error(serverResponse.Status))
					{
						Utility.DebugLog(ToString() + "/ProcessPurchase/Purchase request failed.");
					}
				}
#elif UNITY_IOS
				AppleInAppPurchaseReceipt appleReceipt = receipt as AppleInAppPurchaseReceipt;
				if (appleReceipt != null)
				{
					RequestProtocol.Purchase requestPurchase = new RequestProtocol.Purchase
					{
						userKey = User.GetUserKey(),
						appKey = GlobalConstants.GranmonsterGameKey,
						coin = m_purchsingProductData.ExchangeCoin,
						price = m_purchsingProductData.ExchangeMoney,
						payload = m_payload,
						productId = arguments.purchasedProduct.definition.id,
						purchaseToken = appleReceipt.originalTransactionIdentifier,
						appTypeEnum = AppTypeEnum.IPHONE,
					};

					ServerResponse serverResponse = ServerConnection.Purchase(JsonUtility.ToJson(requestPurchase));
					if (RestApiCallStatusMethods.Error(serverResponse.Status))
					{
						Utility.DebugLog(ToString() + "/ProcessPurchase/Purchase request failed.");
					}
				}
#endif
			}
		}
		catch (IAPSecurityException)
		{
			Utility.DebugLog(ToString() + "/ProcessPurchase/Invalid receipt, not unlocking content");
			validPurchase = false;
		}

		if (validPurchase)
		{
			// Unlock the appropriate content here.
		}

		m_purchsingProductData = null;
		m_payload = string.Empty;

		Utility.DebugLog("유저 코인 정보가 갱신되었습니다 : (" + User.GetCoin() + ")");

		return PurchaseProcessingResult.Complete;
#endif
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Utility.DebugLog(ToString() + "/OnPurchaseFailed/Product: " +
			product.definition.storeSpecificId + ", Reason: " + failureReason);
	}
}