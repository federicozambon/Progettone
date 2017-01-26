using UnityEngine;
using System.Collections;

public class RotationPostExplosion : MonoBehaviour
{
    Transform lookAt;
    Player player;
    public float speed = 5f;
    Enemy enemyScript;

    void Awake()
    {
        enemyScript = GetComponent<Enemy>();
        player = FindObjectOfType<Player>();
        lookAt = player.gameObject.transform;
    }

    public float timer = 0;

    float velocity;

    public IEnumerator Looking()
    {
        timer = 0;
        while (speed * timer <= 0.98f)
        {
            timer += Time.deltaTime;
            Vector3 direction = lookAt.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Mathf.SmoothDamp(0,1,ref velocity, speed * timer));
            yield return null;
        }

        enemyScript.knockbacked = false;
        enemyScript.enemyRb.isKinematic = true;
        enemyScript.repulsion = false;

        StopAllCoroutines();
    }
}
