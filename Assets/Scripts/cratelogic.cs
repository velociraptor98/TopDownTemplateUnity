using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cratelogic : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            print("Player");
            GunLogic gunlogic = other.GetComponentInChildren<GunLogic>();
            if(gunlogic)
            {
                gunlogic.reload();
                Destroy(gameObject);
            }
        }
    }
}
