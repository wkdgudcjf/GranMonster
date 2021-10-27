using System.IO;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
	private static float Frequecy = 1.5f;

	private UnityEngine.UI.Image loadingImagePanel;
	private List<Sprite> loadingImages;
	private float elapsed = 0.0f;

	private void Awake()
	{
		loadingImages = new List<Sprite>();
		loadingImagePanel = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
		Assembly assembly = Assembly.GetExecutingAssembly();

		for (int i = 0; i < 8; ++i)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				string resourcePath = "granmonster.Properties.Loading " + (i + 1) + ".png";
				using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
				{
					int read = 0;
					byte[] buffer = new byte[1024];

					while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
					{
						memoryStream.Write(buffer, 0, read);
					}

					Texture2D texture = new Texture2D(100, 100);
					texture.LoadImage(memoryStream.ToArray());

					Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
					loadingImages.Add(sprite);
				}

				memoryStream.Flush();
			}
		}
	}

	private void FixedUpdate()
	{
		elapsed += Time.deltaTime;
		int newSpriteIndex = (int)((elapsed % Frequecy) / (Frequecy / loadingImages.Count));
		loadingImagePanel.sprite = loadingImages[newSpriteIndex];
		loadingImagePanel.SetNativeSize();
	}
}
