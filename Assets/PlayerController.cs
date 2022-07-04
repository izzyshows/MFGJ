using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;

    [SerializeField]
    private float smoothInputSpeed = .2f;

    [SerializeField]
    private float playerSpeed = 10f;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction fireAction;

    [SerializeField]
    private GameObject sword;

    private bool attackEnabled = false;
    private double bufferAttack = 0, timeAttack = 1;


    void Start() {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        fireAction = playerInput.actions["Fire"];
    }

    void Update() {

        // attack
        if(fireAction.triggered && !attackEnabled) {
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
        Vector2 inputMove = moveAction.ReadValue<Vector2>();
        currentInputVector = Vector2.SmoothDamp(currentInputVector, inputMove, ref smoothInputVelocity, smoothInputSpeed);
        transform.position = new Vector3(transform.position.x + currentInputVector.x * playerSpeed * Time.deltaTime, transform.position.y + currentInputVector.y * playerSpeed * Time.deltaTime);
    }
    
    private Inventory inventory;
    private void Awake() {
        inventory = new Inventory();
    }
}
