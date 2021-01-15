using UnityEngine;
using Invector.vCharacterController;

using System;


public class Player : MonoBehaviour
{    
    public Animator ani;
    public int comboatkCount;
    private float timer;

    [Header("攻擊中心點")]
    public Transform atkPoint;
    [Header("攻擊長度"), Range(0f, 5f)]
    public float atkLength;
    [Header("攻擊力"), Range(0, 500)]
    public float atk = 70;
    [Header("連擊間隔"), Range(0, 5)]
    public float interval = 1;
    public float HP = 1000;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
   
    
    public void Damage(float damage)
    {
        //接收傷害
        HP -= damage;
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

    private void Update()
    {
        Attack();
        QuitGame();
    }


    private void Attack()
    {
        if(comboatkCount <4 )
        {
            if(timer < interval)
            {
                timer += Time.deltaTime; //疊加

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    comboatkCount++;
                    timer = 0;    //歸零   

                    if (Physics.Raycast(atkPoint.position, atkPoint.forward, out hit, atkLength, 1 << 9))
                    {
                        hit.collider.GetComponent<Enemy>().Damage(atk);
                    }

                    

                }
            }
            else
            {
                timer = 0;  
                comboatkCount = 0;
            }
        }

        if (comboatkCount == 4) comboatkCount = 0;
        ani.SetInteger("連擊", comboatkCount);


    }

    /// <summary>
    /// 繪製圖示事件
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(atkPoint.position, atkPoint.forward * atkLength);
    }
    //射線擊中的物件
    private RaycastHit hit;

    
    public void QuitGame()
    {
        if (Input.GetKeyDown("escape"))
        {
            print("退出遊戲");
            Application.Quit();
        }

    }
            

    
}
   

   
