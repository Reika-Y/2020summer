using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;

// シーンを検索し、定義する
public class SceneFinder : AssetPostprocessorEx
{
    // コマンド
    private const string COMMAND = "Tools/Create/EnumScene";
    // 検索対象のパス
    private const string SEARCH_PATH = "t:Scene";
    // 書き出し先のパス
    private const string EXPORT_PATH = "Assets/Scripts/Scene/";
    // ファイル名
    private const string FILE_NAME = "EnumScene";
    // 拡張子
    private const string EXTENSION = ".cs";

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        List<string[]> assetsList = new List<string[]>()
        {
            importedAssets, deletedAssets
         };

        List<string> targetDirectoryNameList = new List<string>()
        {
            "Assets/Scenes"
        };

        if (ExistsDirectoryInAssets(assetsList, targetDirectoryNameList))
        {
            Create();
        }
    }

    // スクリプトの作成
    [MenuItem(COMMAND)]
    private static void Create()
    {
        var list = new List<Object>();

        // 全てのシーンを検索する
        foreach (var guid in AssetDatabase.FindAssets(SEARCH_PATH))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            list.Add((Object)AssetDatabase.LoadMainAssetAtPath(path));
        }

        CreateSceneScript(FILE_NAME, EXPORT_PATH, list);
    }

    // 検索して書き出す
    static void CreateSceneScript(string name, string path, List<Object>obj)
    {
        StringBuilder builder = new StringBuilder();

        builder.AppendFormat("// {0}", "シーン管理用クラス").AppendLine();
        builder.AppendFormat("enum {0}", name).AppendLine();
        builder.AppendLine("{");

        foreach(var o in obj)
        {
            builder.Append("\t").AppendFormat("{0},", o.name).AppendLine();
        }

        builder.AppendLine("}");

        string exportPath = path + name + EXTENSION;

        // 存在を確認
        if(!File.Exists(exportPath))
        {
            // 作ったら、閉じる
            File.Create(exportPath).Close();
        }

        // 書き出し
        File.WriteAllText(exportPath, builder.ToString(), Encoding.UTF8);
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
    }
}
