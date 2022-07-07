using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private  UI_Inventory uiInventory;
    [SerializeField]
    private float playerSpeed = 10f;

    [SerializeField]
    private GameObject sword;

    private bool attackEnabled = false;
    private double bufferAttack = 0, timeAttack = 1;

    private Animator anim;


    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        // attack
        if(Input.GetButton("Fire1") && !attackEnabled) {
            attackEnabled = true;
            bufferAttack = 0;
            sword.SetActive(true);
        }
        
        if(attackEnabled) {
            if(bufferAttack > timeAttack) {
                sword.SetActive(false);
                attackEnabled = false;
            }
            bufferAttack = bufferAttack + Time.deltaTime;
        }

        // movement
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(input.x < 0) {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        anim.SetBool("run", input.x != 0.0f || input.y != 0.0f);
        transform.position = new Vector3(transform.position.x + input.x * playerSpeed * Time.deltaTime, transform.position.y + input.y * playerSpeed * Time.deltaTime);
    }
    
    private Inventory inventory;
    private void Awake() {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            // Touching Item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
}
