using Unity.VisualScripting;
using UnityEngine;

public class PatrolEnermy : MonoBehaviour
{   public float moveSpeed = 2f; // Speed of the enemy movement
    public Transform checkPoint; // Point to check for ground
    public float distance = 0.5f; // Distance to check for ground
    public LayerMask layerMask; // Layer mask to specify which layers to check for ground
    public bool facingLeft = true; // Direction the enemy is facing
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);

        RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, Vector2.down, distance, layerMask);

        if (hit == false && facingLeft)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingLeft = false;
        }
        else if (hit == false && facingLeft == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingLeft = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (checkPoint == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(checkPoint.position, Vector2.down * distance);
    }
}
