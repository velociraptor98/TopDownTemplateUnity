using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody bulletRigidBody;
    [SerializeField]
    private float bulletSpeed = 8.0f;
    void Start()
    {
        bulletRigidBody = this.GetComponent<Rigidbody>();
        if(bulletRigidBody)
        {
            bulletRigidBody.velocity = transform.up * bulletSpeed;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Target")
        {
            Destroy(other.gameObject);
        }
    }
}
