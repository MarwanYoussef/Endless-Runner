using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{

    private CharacterController controller;
    private float speed = 5.0f;
    private Vector3 moveVector;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    private float animationDur = 2.0f;
    private float jump = 8.0f;

    private float timer = 60;
    public Text timerScore;
    private float score = 0;
    public Text scoreValue;
    private float boostMeter = 0;
    public Text boostScore;

    private float timeLeft = 5.0f;

    private bool isDead = false;
    private bool isInvincble = false;
    private float startTime;

    public toggleDeathMenu deathMenu;

    private AudioSource collectibleSource;
    private AudioSource obstacleSource;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        timerScore.text = "Timer: " + timer.ToString();
        scoreValue.text = "Score: " + score.ToString();
        boostScore.text = "Boost: " + boostMeter.ToString();
        startTime = Time.time;
        collectibleSource = GetComponents<AudioSource>()[0];
        obstacleSource = GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void Update()
    {
  
        if (isDead)
            return;

        if (Time.time - startTime < animationDur)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }
        moveVector = Vector3.zero;
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jump;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        
        if (Input.GetMouseButton(0)) {
            if(Input.mousePosition.x > Screen.width/2)
            {
                moveVector.x = speed;
            } else
            {
                moveVector.x = -speed;
            }

            if(Input.mousePosition.y > Screen.height/2)
            {
                verticalVelocity = jump;
            } else
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }
        }
        if (boostMeter % 50 == 0 && boostMeter != 0)
        {
            isInvincble = true;
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
                isInvincble = false;
        }

        if (isInvincble && timeLeft > 0) {
            moveVector.x = moveVector.x * 2;
            moveVector.y = verticalVelocity;
            moveVector.z = speed * 2;
            controller.Move(moveVector * Time.deltaTime);
            
        } else
        {
            moveVector.y = verticalVelocity;
            moveVector.z = speed;
            controller.Move(moveVector * Time.deltaTime);
        }
        timer -= Time.deltaTime;
        if (timer == 0)
            death();
        score += Time.deltaTime * speed;
        timerScore.text = "Timer: " + ((int) timer).ToString();
        scoreValue.text = "Score: " + ((int) score).ToString();
        boostScore.text = "Boost: " + ((int) boostMeter).ToString();
    }

    private void invincible()
    {
        Vector3 invVector = Vector3.zero;
        invVector.x = Input.GetAxis("Horizontal") * (speed * 2);
        invVector.z = speed * 2;
        controller.Move(invVector * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Bomb" && isInvincble != true)
        {
            death();
            obstacleSource.Play();
            Destroy(hit.gameObject);
        }
        else if (hit.gameObject.tag == "Bomb" && isInvincble == true)
            Destroy(hit.gameObject);

        if (hit.gameObject.tag == "Gold")
        {
            increaseTime();
            collectibleSource.Play();
            Destroy(hit.gameObject);
        }

        if (hit.gameObject.tag == "Collectible" && isInvincble != true)
        {
            increaseBoostMeter();
            collectibleSource.Play();
            Destroy(hit.gameObject);
        }
        else if (hit.gameObject.tag == "Collectible" && isInvincble == true)
            Destroy(hit.gameObject);

        if (hit.gameObject.tag == "Iron" && isInvincble != true) { 
            decreaseTimeLimit();
            obstacleSource.Play();
            Destroy(hit.gameObject);
        } else if (hit.gameObject.tag == "Iron" && isInvincble == true)
            Destroy(hit.gameObject);
    }

    private void decreaseTimeLimit()
    {
        timer -= 10.0f;
    }

    private void increaseBoostMeter()
    {
        boostMeter += 10;
    }

    private void increaseTime()
    {
        timer += 2.0f;
    }

    private void death()
    {
        Debug.Log("dead");
        isDead = true;
        onDeath();
    }

    public void onDeath()
    {
        deathMenu.toggleEndMenu(score);
    }
}
