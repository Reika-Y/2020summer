using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


// マップの読み込みクラス
public class MapLoader : SingletonMonoBehaviour<MapLoader>
{
    // 読み込んだマップの保存用
    private Dictionary<string, List<Vector3Int>> mapDic;

    private void Awake()
    {
        base.Awake();
        mapDic = new Dictionary<string, List<Vector3Int>>();
    }

    public List<Vector3Int> Search(Tilemap map)
    {
        if (mapDic == null)
        {
            return AddDic(map);
        }

        if (mapDic.ContainsKey(map.ToString()))
        {
            return mapDic[map.ToString()];
        }

        return AddDic(map);
    }

    private List<Vector3Int> AddDic(Tilemap map)
    {
        var list = new List<Vector3Int>();
        var bound = map.cellBounds;

        for (int y = bound.yMax - 1; y >= bound.yMin; y--)
        {
            for (int x = bound.xMin; x < bound.xMax; x++)
            {
                var position = new Vector3Int(x, y, 0);
                if(map.HasTile(position))
                {
                    list.Add(position);
                }
            }
        }

        mapDic.Add(map.ToString(), list);

        return list;
    }
}
