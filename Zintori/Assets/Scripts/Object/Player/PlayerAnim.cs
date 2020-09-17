using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ANIM_ID
{ 
    IDLE,
    RUN
}


// アニメーション制御用
public class PlayerAnim : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    // アニメーション管理用
    private Dictionary<ANIM_ID, int> moveAnim;
    // 現在のモーション
    private ANIM_ID animId;

    void Start()
    {
        InitAnim();
    }

    private void InitAnim()
    {
        animId = ANIM_ID.IDLE;

        moveAnim = new Dictionary<ANIM_ID, int>();
        moveAnim.Add(ANIM_ID.RUN, Animator.StringToHash("IsRun"));
    }

    public void ChangeAnim(ANIM_ID id)
    {
        animId = id;
        switch(animId)
        {
            case ANIM_ID.IDLE:
                if (animator == null) break;
                animator.SetBool(moveAnim[ANIM_ID.RUN], false);
                break;
            case ANIM_ID.RUN:
                if (animator == null) break;
                animator.SetBool(moveAnim[animId], true);
                break;
        }
    }
}
