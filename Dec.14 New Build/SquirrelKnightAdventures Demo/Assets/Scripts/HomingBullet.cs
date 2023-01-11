using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        StartCoroutine(countDown());
    }

    public IEnumerator countDown()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
