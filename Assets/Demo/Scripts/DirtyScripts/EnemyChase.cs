using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed = 4f;
    public Transform player;
    public bool isChasing = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isChasing = true;
        }
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isChasing = false;
        }
    }

    void Update()
    {
        if (isChasing && player != null)
        {
            Vector3 dir = player.position - transform.position;
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }
}
