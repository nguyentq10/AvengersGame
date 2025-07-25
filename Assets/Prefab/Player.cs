using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public Text coinText;
    public int maxHealth = 3;
    public Text health;
    public Animator animator;
    public Rigidbody2D rb;
    public float jumpHeight;
    public bool isGround = true;
    private float movement;
    public float moveSpeed = 5f;
    private bool facingRight = true;
    public Transform attackPoint;
    public float attackRadius = 1f;
    public LayerMask attackLayer;
    public int currentCoin = 0;
    public AudioClip coinSound;
    public AudioClip attackSound;
    public AudioClip dameSound;
   public AudioClip healthsound;
    private AudioSource audioSource;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (maxHealth <= 0)
        {
            
            Die();
        }
        coinText.text = currentCoin.ToString();
        health.text = maxHealth.ToString();
        movement = Input.GetAxis("Horizontal");
        if (movement < 0f && facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;
        }
        else if (movement > 0f && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            Jump();
            isGround = false;
            animator.SetBool("Jump", true);
        }

        if (Mathf.Abs(movement) > .1f)
        {
            animator.SetFloat("Run", 1f);
        }else if(movement < .1f)
        {
            animator.SetFloat("Run", 0f);
        }
        if (Input.GetKey(KeyCode.Q))        {
            animator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * moveSpeed;
    }

    void Jump()
    {
        rb.AddForce(new Vector2 (0f, jumpHeight), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
            animator.SetBool("Jump", false);
        }
    }

    public void Attack()
    {
        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
        audioSource.PlayOneShot(attackSound);
        if (collInfo)
        {
            
            if (collInfo.gameObject.GetComponent<PatrolEnermy>() != null)
            {
                collInfo.gameObject.GetComponent<PatrolEnermy>().TakeDamage(1);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public void TakeDamage(int damage)
    {
        
        if (maxHealth <= 0)
        {
            return;
        }
        maxHealth -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            currentCoin++;
            other.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Collected");
            audioSource.PlayOneShot(coinSound);
            Destroy(other.gameObject, 1f);
        }
        if (other.gameObject.tag == "Health")
        {
            maxHealth += 10;
           other.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Collected Health");
            audioSource.PlayOneShot(healthsound);
            Destroy(other.gameObject, 1f);
        }
        if (other.gameObject.tag == "VictoryPoint")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        }
        if (other.gameObject.tag == "WaterDie")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Die");
        }
    }
    void Die()
    {
        Debug.Log("Player died");
        FindFirstObjectByType<GameManager>().isGameActive = false;
        Destroy(this.gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Die");
    }
}
