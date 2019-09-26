using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;
    private Rigidbody rb;
    //this counts how many of the objects have been picked up
    private int count;
    //set score counter
    private int score;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";

    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        //if the player collides with the pickup objects
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            //increment score
            score++;
            count = count + 1;
            SetCountText();
        }
        //addition to roll a ball game
        //if the player comes in contact with one of the obstacles, subtract from score;
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            score -= 1;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.other.CompareTag("Obstacle"))
        {

        }

    }

    void SetCountText()
    {
        //output the score to the screen instead of the count
        countText.text = "Count: " + score.ToString();
        if(count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}
