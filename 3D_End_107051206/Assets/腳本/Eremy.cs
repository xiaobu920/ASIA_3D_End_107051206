using UnityEngine;
using UnityEngine.AI;

public class Eremy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 50)]
    public float speed = 3;
    [Header("停止距離"), Range(0, 50)]
    public float stopDistance = 1.3f;
    [Header("攻擊冷卻時間"), Range(0, 50)]
    public float AttackCD = 2f;
    [Header("攻擊中心點")]
    public Transform atkPoint;
    [Header("攻擊長度"), Range(0f, 5f)]
    public float atkLength;


    private Transform player;
    private NavMeshAgent nav;
    private Animator ani;

    /// <summary>
    /// 計時器
    /// </summary>
    private float timer;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        player = GameObject.Find("Yong").transform;
        nav.speed = speed;
        nav.stoppingDistance = stopDistance;
    }

    private void Update()
    {
        Track();
        Attack();
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



    /// <summary>
    /// 追蹤
    /// </summary>
    private void Track()
    {
        nav.SetDestination(player.position);        
        ani.SetBool("戰鬥模式", nav.remainingDistance > stopDistance);
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        if (nav.remainingDistance < stopDistance)
        {
            timer += Time.deltaTime;

            Vector3 pos = player.position;
            pos.y = transform.position.y;
            transform.LookAt(pos);

            if (timer >= AttackCD)
            {
                ani.SetTrigger("攻擊觸發");
                timer = 0;
                if (Physics.Raycast(atkPoint.position, atkPoint.forward, out hit, atkLength, 1 << 8))
                {
                     hit.collider.GetComponent<Player>().Damage();
                }
            }
            
        }
    }




}
