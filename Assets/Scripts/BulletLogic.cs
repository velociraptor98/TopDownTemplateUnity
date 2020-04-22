using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody bulletRigidBody;
    [SerializeField]
    private float bulletSpeed = 8.0f;
    [SerializeField]
    private float bulletLifetime = 3.0f;
    void Start()
    {
        bulletRigidBody = this.GetComponent<Rigidbody>();
        if(bulletRigidBody)
        {
            bulletRigidBody.velocity = transform.up * bulletSpeed;
        }
    }
    
    void Update()
    {
        bulletLifetime -= Time.deltaTime;
        if(bulletLifetime<=0.0f)
        {
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Target")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if(other.tag == "Enemy")
        {
            EnemyLogic enemy = other.gameObject.GetComponent<EnemyLogic>();
            enemy.takeDamage(10);
            Destroy(this.gameObject);
        }
    }
}
