using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MovingObejects : MonoBehaviour
{   
    [SerializeField] private AudioClip _collisionSFX;
    private Animator anim;
    public float speed; // Adjusted to public so you can set it from the Spawner
    [SerializeField] private string collisiontrigger;

    [SerializeField] private float speedIncreaseFactor =2f; // Adjust this value to control the speed increase rate
    private float speedIncreaseTimer = 0f;

 

    private void Start() 
    {
        anim = GetComponent<Animator>();    
    }
    private void Update()
    {
    transform.position += Vector3.left * speed * Time.deltaTime;

    // Increase the speed over time
    speedIncreaseTimer += Time.deltaTime;
    if (speedIncreaseTimer >= 20f) // Adjust this value as needed
    {
        speed += speedIncreaseFactor; // Increase the speed
        speedIncreaseTimer = 0f; // Reset the timer
    }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger(collisiontrigger); 
            AudioManager.instance.PlayBombExplodeSoundClip(_collisionSFX);
        }
}


    public void gameObjectSetactive()
    {
        gameObject.SetActive(false);
    }

}
