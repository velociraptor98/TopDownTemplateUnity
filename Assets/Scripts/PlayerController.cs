using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int PlayerHealth = 100;
    [SerializeField]
    private float _speed = 5.0f;
    private CharacterController _playerController;
    private Vector3 _playerMovement;
    private float horizontal;
    private float vertical;
    // Start is called before the first frame update
    GameObject interactiveObject = null;
    [SerializeField]
    Text HealthText;
    [SerializeField]
    private Transform equipPos;
    private GameObject equippedObject = null;
    void Start()
    {
         SetHealthText();
        _playerController = GetComponent<CharacterController>();   
    }
    
    private void SetHealthText()
    {
        if(HealthText)
        {
            HealthText.text = "Health: "+PlayerHealth;
        }
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if(interactiveObject && Input.GetButtonDown("Fire2"))
        {
            if (equippedObject == null)
            {
                GunLogic gun = interactiveObject.GetComponent<GunLogic>();
                if (gun)
                {
                    interactiveObject.transform.position = equipPos.position;
                    interactiveObject.transform.parent = this.gameObject.transform;
                    interactiveObject.transform.rotation = equipPos.rotation;
                    gun.EquipGun();
                    equippedObject = interactiveObject;
                }
            }
            else if(equippedObject)
            {
                GunLogic gun = equippedObject.GetComponent<GunLogic>();
                if (gun)
                {
                    equippedObject.transform.parent = null;
                    gun.UnequipGun();
                    equippedObject = null;
                }
            }
        }

    }
    private void FixedUpdate()
    {
        _playerMovement.x = horizontal * _speed * Time.deltaTime;
        _playerMovement.z= vertical * _speed * Time.deltaTime;
        RotateCharacterToMouseCursor();
        //if(_playerMovement!=Vector3.zero)
        //{
        //transform.forward = Quaternion.Euler(0, -90, 0) * _playerMovement.normalized;
        //}
        _playerController.Move(_playerMovement);
    }
    void RotateCharacterToMouseCursor()
    {
        Vector3 mousepos = Input.mousePosition;
        Vector3 playerscreenspace = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = mousepos - playerscreenspace;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Weapon") == true )
        {
            interactiveObject = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Weapon" && interactiveObject == other.gameObject)
        {
            interactiveObject = null;
        }
    }
    public  void TakeDamage(int damagePoints)
    {
        PlayerHealth -= damagePoints;
        SetHealthText();
        if(PlayerHealth<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
