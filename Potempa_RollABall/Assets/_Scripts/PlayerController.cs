using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;
    private Rigidbody rb;
    //this counts how many of the objects have been picked up
    private int count;
    //set score
    private int score;
    //create an empty container for the prefab
    public GameObject pickupPreFab;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";

        //control how many pickups to spawn
        int pickupsSpawned = 12;
        while(pickupsSpawned > 0)
        {
            pickupsSpawned--;
            SpawnPickup();
        }
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
            count++;
            SetCountText();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //addition to roll a ball game
        //if the player comes in contact with one of the obstacles, subtract from score;
        if (collision.other.CompareTag("Obstacle"))
        {
            score -= 1;
            SetCountText();
        }

    }

    void SetCountText()
    {
        //output the score to the screen instead of the count
        countText.text = "Score: " + score.ToString() + "\nItems Left: " + (12 - count).ToString();
        if(count >= 12)
        {
            string message = "";
            //set the message based on the score
            if (score <= 0)
            {
                message = "You're supposed to score points,\nnot loose them!\nYour score: ";
            }
            else if (score <= 6)
            {
                message = "Nice Try!\nYour score: ";
            }
            else if(score < 12)
            {
                message = "Good Job!\nYour score: ";
            }
            else
            {
                message = "Perfect score!\nYour score: ";
            }
            //output the message
            winText.text = message + score.ToString() + "\nRestart in 2 sec.";
            //call restartlevel
            Invoke("RestartLevel", 2f);
            
        }
    }
    
    void RestartLevel()
    {
        //reload the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    //randomly generate pickups
    void SpawnPickup()
    {
        //pick random position
        Vector3 randomPos = new Vector3(Random.Range(-8f, 8f), .5f, Random.Range(-8f, 8f));
        //create the object at the position
        Instantiate(pickupPreFab, randomPos, pickupPreFab.transform.rotation);
    }
}
