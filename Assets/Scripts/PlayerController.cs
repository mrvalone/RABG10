using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //needed to restart the game when the player enters the death zone (trigger event)
using TMPro;
using System;
using System.Drawing;

public class PlayerController : MonoBehaviour
{

    //These public variables are initialized in the Inspector
    public float speed;
    public TMP_Text countText;
    public TMP_Text timeText;  //  variable to display the timer text in Unity
    public float startingTime;  // variable to hold the game's starting time
    public string min;
    public string sec;
    public GameObject cam1;
    public GameObject cam2;
    public Material blueGlow;
    public Material purpleGlow;
    public Material greenGlow;
    public GameObject VentDoor;
    public GameObject ControlDoor;
    public GameObject WinDoor;
    public int count;

    //These private variables are initialized in the Start
    private Rigidbody rb;
    private Renderer rend;
    private bool gameOver; //  bool to define game state on or off.
    private string currentColor;
    private Boolean fanActive;
    private bool controlA;
    private Scene scene;

    // Audio
    public AudioClip coinSFX;
    public AudioClip powerUp;
    public AudioClip zap;
    public AudioClip buzzer;
    public AudioClip sizzle;
    private AudioSource audioSource;


    void Start()
    {
        if (WinDoor != null)
        {
            WinDoor.SetActive(false);
        }
        currentColor = "none";
        fanActive = false;
        controlA = false;
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        startingTime = Time.time;
        gameOver = false;
        cam2.SetActive(false);
        scene = SceneManager.GetActiveScene();

        audioSource = GetComponent<AudioSource>();  // access the audio source component of player

    }
    private void Update()
    {

        if (gameOver) // condition that the game is NOT over; returns the false value
            return;
        float timer = Time.time - startingTime;     // local variable to updated time
        min = ((int)timer / 60).ToString();     // calculates minutes
        sec = (timer % 60).ToString("f0");      // calculates seconds

        timeText.text = "Elapsed Time: " + min + ":" + sec;     // update UI time text
    }

    private void FixedUpdate()
    {
        //get the user input on the horizontal and vertical axis
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //apply a force to the rigidbody using the input and the speed
        rb.AddForce(new Vector3(moveHorizontal, 0, moveVertical) * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //This event/function handles trigger events (collsion between a game object with a rigid body)

        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();

            ////PLAY SOUND EFFECT
            audioSource.clip = coinSFX;
            audioSource.Play();

        }

        if (other.gameObject.tag == "DeathZone")
        {
            audioSource.clip = sizzle;
            audioSource.Play();
            SceneManager.LoadScene(scene.name);
            Debug.Log("code ran");
        }

        if (other.gameObject.CompareTag("DeathZoneB"))
        {
            if (currentColor != "blue")
            {
                audioSource.clip = sizzle;
                audioSource.Play();
                SceneManager.LoadScene(scene.name);
            }
            
        }

        if (other.gameObject.CompareTag("DeathZoneP"))
        {
            if (currentColor != "purple")
            {
                audioSource.clip = sizzle;
                audioSource.Play();
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
        }

        if (other.gameObject.CompareTag("DeathZoneG"))
        {
            if (currentColor != "green")
            {
                audioSource.clip = sizzle;
                audioSource.Play();
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
        }

        if (other.gameObject.CompareTag("Grow"))
        {
            if (transform.localScale.x <= 2.0f)
            {
                transform.localScale *= 1.25f;    // increase scale by 25%
            }
        }

        if (other.gameObject.CompareTag("Shrink"))
        {
            if (transform.localScale.x >= 0.5f)
            {
                transform.localScale *= 0.75f;     // decreases scale by 25%
            }
        }

        if (other.gameObject.CompareTag("Jump"))
        {
            if (fanActive)
            {
                rb.AddForce(new Vector3(0.0f, 900.0f, 0.0f));
            }
        }

        if (other.gameObject.CompareTag("Inside") && cam2 != null)
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }

        if (other.gameObject.CompareTag("Outside") && cam2 != null)
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
        }

        if (other.gameObject.CompareTag("Blue"))
        {
            currentColor = "blue";
            rend.material = blueGlow;
            audioSource.clip = zap;
            audioSource.Play();

        }

        if (other.gameObject.CompareTag("Purple"))
        {
            currentColor = "purple";
            rend.material = purpleGlow;
            audioSource.clip = zap;
            audioSource.Play();
        }

        if (other.gameObject.CompareTag("Green"))
        {
            currentColor = "green";
            rend.material = greenGlow;
            audioSource.clip = zap;
            audioSource.Play();
        }

        if (other.gameObject.CompareTag("Power"))
        {
            fanActive = true;
            if(scene.name == ("LevelOne"))
            {
                audioSource.clip = powerUp;
                audioSource.Play();
            }
            
        }

        if (other.gameObject.CompareTag("ControlA"))
        {
            ControlDoor.transform.Translate(Vector3.down * 12.0f);
            controlA = true;
            audioSource.clip = powerUp;
            audioSource.Play();
        }

        if (other.gameObject.CompareTag("ControlB"))
        {
            VentDoor.transform.Translate(Vector3.right * 5.0f);
            audioSource.clip = powerUp;
            audioSource.Play();
        }

        if (other.gameObject.CompareTag("Locked") && !controlA)
        {
            audioSource.clip = buzzer;
            audioSource.Play();
        }

        if (other.gameObject.CompareTag("WinDoor"))
        {
            SceneManager.LoadScene("LevelTwo");
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            WinDoor.SetActive(true);
            count = 0;

            if (scene.name == "LevelTwo")
            {
                SceneManager.LoadScene("WIN");
            }
        }
    }
}
