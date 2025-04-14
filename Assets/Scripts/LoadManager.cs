/*using System.Collections;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    IEnumerator Start()
    {
        AssetBundle asset = AssetBundle.LoadFromFile("Bundle/need");

        if (asset == null)
        {
            yield break;
        }

        var loadAssets = asset.LoadAllAssets();

        yield return new WaitUntil(() => this.enabled == false);
    }
}
*/