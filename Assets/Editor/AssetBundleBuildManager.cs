using System.IO;
using Unity.VisualScripting;
using UnityEditor;

public class AssetBundleManager
{
    [MenuItem("Mytool/AssetBundle Build")]
    public static void AssetBundleBuild()
    {
        string directory = "./Bundle";

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        BuildPipeline.BuildAssetBundles(directory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

        EditorUtility.DisplayDialog("에셋 번들 빌드", "에셋 번들 빌드 완료", "완료");
    }
}
