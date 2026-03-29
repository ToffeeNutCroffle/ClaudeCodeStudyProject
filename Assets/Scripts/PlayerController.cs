using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0f;
    private int count;
    public Text countText;
    public GameObject winTextObject;
    public string gameOverText = "You Lose!";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            count++;
            other.gameObject.SetActive(false);
            SetCountText();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            winTextObject.GetComponent<Text>().text = gameOverText;
            winTextObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3(movementX, 0, movementY);
        rb.AddForce(movement * speed);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count;
        if (count >= 12)
        {
            winTextObject.GetComponent<Text>().text = "You Win!";
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

}
