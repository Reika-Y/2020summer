using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using System.IO;

// AssetPostprocessorの拡張クラス
public class AssetPostprocessorEx : AssetPostprocessor
{
    // 入力されたassetsの中に、ディレクトリのパスがdirectoryNameの物はあるか
    protected static bool ExistsDirectoryInAssets(List<string[]> assetsList, List<string> targetDirectoryNameList)
    {
        // 入力されたassetsListに以下の条件を満たすか要素が含まれているか判定
        // assetsに含まれているファイルのディレクトリ名だけをリストにして取得
        // 上記のリストと入力されたディレクトリ名のリストの一致している物のリストを取得
        // 一致している物があるか
        return assetsList
                .Any(assets => assets
                .Select(asset => asset.Replace("/" + Path.GetFileName(asset), ""))
                .Intersect(targetDirectoryNameList)
                .Count() > 0);
    }
}
