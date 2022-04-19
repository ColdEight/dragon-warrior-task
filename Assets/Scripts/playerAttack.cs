using UnityEngine;

public class playerAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private float coolDownTimer = Mathf.Infinity;
    private Animator anim;
    private MovePlayer movePlayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        movePlayer = GetComponent<MovePlayer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && coolDownTimer > attackCoolDown && movePlayer.canAttack())
            Attack();
        coolDownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        coolDownTimer = 0;
        fireballs[findFireBall()].transform.position = firePoint.position;
        fireballs[findFireBall()].GetComponent<fireBall>().SetDirection(Mathf.Sign(transform.lossyScale.x)); 
    }

    private int findFireBall()
    {
        for(int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
