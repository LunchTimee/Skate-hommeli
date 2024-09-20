using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelaaja : MonoBehaviour
{

    public float MovementSpeed = 5f;
    public float JumpPower = 10f;
    Rigidbody2D rb;

    SpriteRenderer rend;
    Animator Anim;
    bool grounded;
    bool walking;
    bool skating;
    public float WalkingSpeed, SkatingSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        MovementSpeed = WalkingSpeed;
        Debug.Log("Start");
        rend = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
       rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Anim.SetBool("walking", false);
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * Time.deltaTime * MovementSpeed, Space.World);
            rend.flipX = false;
            walking = true;
            Anim.SetBool("walking", true);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * Time.deltaTime * MovementSpeed, Space.World);
            rend.flipX = true;
            walking = true;
            Anim.SetBool("walking", true);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector2.down * JumpPower, ForceMode2D.Impulse);
        }

            bool grounded = false;
        LayerMask layerMask = LayerMask.GetMask("ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, layerMask);
        Debug.DrawRay(transform.position, Vector2.down * 1.2f, Color.red);
        if (hit)
        {
            grounded = true;
            Debug.Log("ground found");
            
        }
        Anim.SetBool("grounded", grounded);

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            Anim.SetBool("walking", false);
        }
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            Anim.SetBool("skating", !Anim.GetBool("skating"));
            MovementSpeed = Anim.GetBool("skating") ? SkatingSpeed : WalkingSpeed;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);

        }
    }
}
