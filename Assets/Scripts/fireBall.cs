using UnityEngine;

public class fireBall : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private float lifeTime;

    private BoxCollider2D boxcollider2D;
    private Animator animator;


    private void Awake() {
        boxcollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed,0,0);

        lifeTime += Time.deltaTime;
        if (lifeTime > 3) gameObject.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D other) {
        hit = true;
        boxcollider2D.enabled = false;
        animator.SetTrigger("explotion");
    }

    public void SetDirection(float _Direction)
    {
        lifeTime = 0;
        direction = _Direction;
        gameObject.SetActive(true);
        hit = false;
        boxcollider2D.enabled = true;
        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _Direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }


}
