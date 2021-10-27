using UnityEditor;

public class AssetBundleBuilder : Editor {
	[MenuItem("Assets/BuildBundle")]
	static void BuildBundle()
	{
		BuildPipeline.BuildAssetBundles("Assets/AssetBundles",
			BuildAssetBundleOptions.None, BuildTarget.Android);
	}
}