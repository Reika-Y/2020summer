using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[System.Serializable]
public struct MapData
{
    public Tilemap map;
    public Color color;
}

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    private MapData terrain;
    [SerializeField]
    Image image;

    // マップ用テクスチャ
    Texture2D texture;

    void Start()
    {
        // テクスチャ作成
        Vector3Int size = terrain.map.size;
        texture = new Texture2D(size.x, size.y, TextureFormat.ARGB32, false);

        texture.filterMode = FilterMode.Point;

        Vector3Int origin = terrain.map.origin;

        for(int y = 0; y < size.y; y++)
        {
            for(int x = 0; x < size.x; x++)
            {
                // グリッド座標
                var cellPos = new Vector3Int(origin.x + x, origin.y + y, origin.z);
                if (terrain.map.GetTile(cellPos))
                {
                    texture.SetPixel(x, y, terrain.color);
                }
            }
        }

        texture.Apply();

        image.rectTransform.sizeDelta = new Vector2(size.x, size.y);
        image.sprite = Sprite.Create(texture, new Rect(0, 0, size.x, size.y), Vector2.zero);

        var leftDownWorldPos = terrain.map.CellToWorld(origin);
        var rightUpWorldPos = terrain.map.CellToWorld(origin + size);
        image.transform.position = (leftDownWorldPos + rightUpWorldPos) * 0.5f;
    }

    private void OnDestroy()
    {
        Destroy(texture);
    }
}
