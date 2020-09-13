using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttack : MonoBehaviour
{
    private Obj player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Obj>();
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obj")
        {
            var obj = col.gameObject.GetComponent<Obj>();
            //obj.Damage(player);
        }
    }
}
