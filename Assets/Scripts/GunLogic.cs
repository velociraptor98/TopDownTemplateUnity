using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform SpawnPoint;

    const float MAX_Cooldown = 0.5f;
    [SerializeField] private float currentCooldown = 0.0f;
    const int MAX_Ammo = 10;
    private int ammoCount = MAX_Ammo;
    [SerializeField]
    Text ammotext;
    bool is_GunEquip = false;
    Rigidbody rgGun;
    Collider guncollider;

    private void Start()
    {
        rgGun = this.GetComponent<Rigidbody>();
        guncollider = this.GetComponent<Collider>();
    }
    public void setAmmoText()
    {
        if(ammotext)
        {
            ammotext.text = ("Ammo: " + ammoCount);
        }
    }
    // Update is called once per frame
    void Update()
    {
        setAmmoText();
        if(is_GunEquip == false)
        {
            return;
        }
        if (currentCooldown > 0.0f)
        {
            currentCooldown -= Time.deltaTime;
        }
     if(Input.GetButtonDown("Fire1") && currentCooldown <= 0.0f && ammoCount >0)
        {
            if (bullet && SpawnPoint)
            {
                Instantiate(bullet, SpawnPoint.position, SpawnPoint.rotation * bullet.transform.rotation);
                currentCooldown = MAX_Cooldown;
                --ammoCount;
                setAmmoText();
            }

        }
    }
    public void reload()
    {
        ammoCount = MAX_Ammo;
    }
    public void EquipGun()
    {
        is_GunEquip = true;
        if(rgGun)
        {
            rgGun.useGravity = false;
        }
        if(guncollider)
        {
            guncollider.enabled = false;
        }
    }
    public void UnequipGun()
    {
        is_GunEquip = false;
        if(rgGun)
        {
            rgGun.useGravity = true;
        }
        if(guncollider)
        {
            guncollider.enabled = true;
        }
    }
}
 