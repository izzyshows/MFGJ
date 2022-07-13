using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using TMPro;

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
    float buffer = 0;
    public bool flashGreen = false;

    private Inventory inventory;
    SpriteRenderer sprite;

    public int life = 100;

    [SerializeField]
    private TextMeshProUGUI lifeText;
    private bool damaged = false;
    private double bufferDamage = 0, numFlickering = 0;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
        lifeText.SetText(life + "");
    }

    void Update() {
        // Took damage
        if (damaged) {
            if(numFlickering < 6) {
                if (bufferDamage > 0.1f) {
                    for (int i = 0; i < transform.childCount; i++) {
                        SpriteRenderer rend = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                        if (rend) {
                            rend.enabled = !rend.enabled;
                            bufferDamage = 0;
                        }
                    }
                    numFlickering++;
                }
                bufferDamage = bufferDamage + Time.deltaTime;
            }
            else {
                damaged = false;
                numFlickering = 0;
            }  
        }

        // attack
        if (Input.GetButton("Fire1") && !attackEnabled) {
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

        //change color
        if (flashGreen)
        {
            buffer += Time.deltaTime;

            if (buffer > 0.15f)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    GameObject go = transform.GetChild(i).gameObject;
                    SpriteRenderer rend = go.GetComponent<SpriteRenderer>();
                    if (rend)
                    {
                        rend.color = new Color(1, 1, 1, 1);
                        buffer = 0;
                        flashGreen = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    GameObject go = transform.GetChild(i).gameObject;
                    SpriteRenderer rend = go.GetComponent<SpriteRenderer>();
                    if (rend)
                    {
                        rend.color = new Color(0, 1, 0, 1);
                    }
                }
            }
        }
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

        if(collider.CompareTag("Slime") && !damaged) {
            life = life - 10;
            lifeText.SetText(life + "");
            damaged = true;
        }
    }

    public void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.RedPotion:
                flashGreen = true;
                inventory.RemoveItem2(item);
                break;
        }
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;

    }
}
