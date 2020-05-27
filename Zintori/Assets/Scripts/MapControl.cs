using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapControl : SingletonMonoBehaviour<MapControl>
{

    [SerializeField]
    private Tilemap map = null;

    enum ChipState
    {
        Non,            // なし
        Temporary,      // アイテム設置中
        Done            // 完了
    }

    // マップ情報の構造体
    struct MapData
    {
        public int playerId;
        public ChipState chipState;
    }

    List<MapData> mapDataList;

    private void Awake()
    {
        base.Awake();

        mapDataList = new List<MapData>();
        var bound = map.cellBounds;
        for (int y = bound.yMax - 1; y >= bound.yMin; y--)
        {
            for (int x = bound.xMin; x < bound.xMax; x++)
            {
                MapData data;
                data.chipState = ChipState.Non;
                data.playerId = 0;
                mapDataList.Add(data);
            }
        }
        Debug.Log(mapDataList.Count);
    }

    // タイルがどこに存在するか
    void OutputPosition(Tilemap map)
    {
        var builder = new StringBuilder();
        var bound = map.cellBounds;

        for(int y = bound.yMax - 1; y >= bound.yMin; y--)
        {
            for(int x = bound.xMin; x < bound.xMax; x++)
            {
                builder.Append(map.HasTile(new Vector3Int(x, y, 0)) ? "■" : "□");
            }
            builder.Append("\n");
        }

        Debug.Log(builder.ToString());
    }

    // Sprite情報取得
    void OutputSpriteType(Tilemap map)
    {
        var bound = map.cellBounds;
        var spriteList = new List<Sprite>();

        // sptiteのリストアップ
        for(int y = bound.yMax - 1; y > bound.yMin; y--)
        {
            for(int x = bound.xMin; x < bound.xMax; x++)
            {
                var tile = map.GetTile<Tile>(new Vector3Int(x, y, 0));
                if(tile != null && !spriteList.Contains(tile.sprite))
                {
                    spriteList.Add(tile.sprite);
                }
            }
        }

        // どの場所で使われているかの出力
        var builder = new StringBuilder();
        for (int y = bound.yMax - 1; y > bound.yMin; y--)
        {
            for (int x = bound.xMin; x < bound.xMax; x++)
            {
                var tile = map.GetTile<Tile>(new Vector3Int(x, y, 0));
                if (tile == null)
                {
                    builder.Append("＿");
                }
                else
                {
                    var index = spriteList.IndexOf(tile.sprite);
                    builder.Append(index);
                }
            }
            builder.Append("\n");
        }

        Debug.Log(builder.ToString());
    }
}
