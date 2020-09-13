using UnityEngine;

public class Obj : MonoBehaviour
{
    protected int power = 0;

    protected int hp = 10;

    protected int teamNo = 0;

    public void Damage(Obj obj)
    {
        // 同じチームは攻撃しない
        if(obj.teamNo == teamNo)
        {
            return;
        }

        hp -= obj.power;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
