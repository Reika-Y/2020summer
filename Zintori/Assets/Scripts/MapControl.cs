using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapControl : SingletonMonoBehaviour<MapControl>
{
    // 地形判定用のマップ
    [SerializeField]
    private Tilemap map = null;

    [SerializeField]
    private Color[] teamColor;

    [SerializeField]
    private TileBase tileBase;

    private void Awake()
    {
        base.Awake();
    }

    // タイルが存在するか
    public bool CheckPosition(Vector3 position)
    {
        var checkPosition = map.WorldToCell(position);
        if(map.GetTile(checkPosition))
        {
            return true;
        }
        return false;
    }


    // タイルのセット
    public void SetTile(Vector3Int position, int num)
    {
        if(map.GetTile(position) && teamColor.Length >= num)
        {
            map.SetColor(position, teamColor[num]);
        }
    }

    public void SetTile (List<Vector3Int> list, Vector3 p, int num)
    {
        var offset = map.WorldToCell(p);
        foreach(var pos in list)
        {
            if(map.GetTile(pos + offset) && teamColor.Length >= num)
            {
                map.SetTile(pos + offset,tileBase);
                map.SetColor(pos + offset, teamColor[num++]);
            }
        }
    }
}
