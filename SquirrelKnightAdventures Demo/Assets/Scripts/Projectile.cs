using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 targetPosition;

    public float speed;

    private void Start()
    {
        targetPosition = GameObject.Find("Player").transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }
    
}
