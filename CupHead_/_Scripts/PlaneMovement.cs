using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] ParticleSystem smokeParticle;
    [SerializeField] float planeSpeed;

    private bool canCollide = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(xMove, yMove);

        rb.velocity = movement * planeSpeed;


        // Animation
        if (yMove > 0)
        {
            PlaneUp();
        }
        else if (yMove < 0)
        {
            PlaneDown();
        }
        else
        {
            PlaneIdle();
        }
    }

    void PlaneUp()
    {
        anim.SetBool("up", true);
        anim.SetBool("down", false);
    }

    void PlaneDown()
    {
        anim.SetBool("up", false);
        anim.SetBool("down", true);
    }

    void PlaneIdle()
    {
        anim.SetBool("up", false);
        anim.SetBool("down", false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (canCollide && other.gameObject.CompareTag("Enemies"))
        {
            StartCoroutine(PlayerHurtRoutine());
        }
        if(other.gameObject.CompareTag("Coins"))
        {
            Gamemanager.instance.IncreaseCoins();
        }
    }

    private IEnumerator PlayerHurtRoutine()
    {
        anim.SetBool("isHurt", true);

        canCollide = false;

        rb.isKinematic = true;

        if (Gamemanager.instance != null)
        {
            Gamemanager.instance.PlayerHurt();
        }

        yield return new WaitForSeconds(3f);

        canCollide = true;

        rb.isKinematic = false;

        anim.SetBool("isHurt", false);
    }
}
