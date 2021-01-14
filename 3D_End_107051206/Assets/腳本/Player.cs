using UnityEngine;
using Invector.vCharacterController;

public class Player : MonoBehaviour
{
    private float HP = 100;
    private Animator ani;
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }


    //受傷
    public void Damage()
    {
        HP -= 55;
        ani.SetTrigger("受傷觸發");

        if (HP <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        ani.SetTrigger("死亡觸發");
        vThirdPersonController vt = GetComponent<vThirdPersonController>();
        vt.lockMovement = true;
        vt.lockRotation = true;
        

    }
}
