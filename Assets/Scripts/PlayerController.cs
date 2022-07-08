using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

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

    private Inventory inventory;
    //private playerPosition = 0,0,0;

    void Start() {
        anim = GetComponent<Animator>();
        inventory = new Inventory();
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
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

    //public void GetPosition()
    //{
    //    position = GameObject.Find("Player").transform.position;
    //    return position;
    //}

    //public static PlayerController GetPosition(Vector3 position, PlayerController player)
    //{
    //    //Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

    //    //ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
    //    //itemWorld.SetItem(item);

    //    //return itemWorld;

    //    PlayerController player = GameObject.Find("Player").transform.position;
    //}

    private Vector3 playerPosition;

    public Vector3 GetPosition()
    {
        if (GameObject.FindWithTag("Player"))
        {
            playerPosition = GameObject.FindWithTag("Player").transform.position;
        }
    }
}
