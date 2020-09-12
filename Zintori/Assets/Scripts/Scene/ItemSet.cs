using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Items
{
    public GameObject item;
    public int count;
}

public class ItemSet : SingletonMonoBehaviour<ItemSet>
{
    [SerializeField]
    private const int listSize = 3;

    private Items[] itemList;

    void Awake()
    {
        base.Awake();
        itemList = new Items[listSize];
    }

    public void AddItem(GameObject obj, int count = 1)
    {
        for(int i = 0;i < listSize;i++)
        {
            if(itemList[i].item == obj)
            {
                itemList[i].count += count;
                return;
            }
            if(!itemList[i].item)
            {
                itemList[i].item = obj;
                itemList[i].count = count;
                return;
            }
        }
    }


}
