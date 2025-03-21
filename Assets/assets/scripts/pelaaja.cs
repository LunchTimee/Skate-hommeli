using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelaaja : MonoBehaviour
{
    public float GroundRaycastSize;
    public float MovementSpeed = 5f;
    public float JumpPower = 10f;
    public float groundSlamPower;
    public float WalkingSpeed, SkatingSpeed;
    public int AirJumpCount;
    int jumps;
    [Header("Otsikkos")]
    public float PotkuVoima;
    public Rigidbody2D rb;
    SpriteRenderer rend;
    Animator Anim;
    bool grounded;
    bool walking;
    bool skating;
    bool canJump;
    GameObject skateBoardCollider;
    public Vector2 startPos;
    
    float horizontal;
    float vertical;
    public bool isKick;

    // Start is called before the first frame update
    void Start()
    {
        skateBoardCollider = transform.GetChild(0).gameObject;
        startPos = transform.position;

        MovementSpeed = WalkingSpeed;
        Debug.Log("Start");
        rend = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
       rb=GetComponent<Rigidbody2D>();
    }

    public void Kick()
    {
        Debug.Log("Kick");
        rb.AddForce(transform.right * (rend.flipX ? -1f : 1f)* PotkuVoima, ForceMode2D.Impulse);
        
    }





    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown(KeyCode.Y))
        Kick();

        bool grounded = false;
        LayerMask layerMask = LayerMask.GetMask("ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, GroundRaycastSize, layerMask);
        bool hit2 = false;
        bool hit3 = false;

       // Debug.DrawRay(transform.GetChild(0).GetChild(1).position, Vector2.down * GroundRaycastSize, Color.red);
       // Debug.DrawRay(transform.GetChild(0).GetChild(0).position, Vector2.down * GroundRaycastSize, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * GroundRaycastSize, Color.red);
        if (skating)
        {
            RaycastHit2D r2 = Physics2D.Raycast(transform.GetChild(0).GetChild(0).position, Vector2.down, GroundRaycastSize, layerMask);
            hit2 = r2;
            RaycastHit2D r3 = Physics2D.Raycast(transform.GetChild(0).GetChild(1).position, Vector2.down, GroundRaycastSize, layerMask);
            hit3 = r3;
        }
        if (hit || hit2 || hit3)
        {
            grounded = true;
            canJump = true;
            jumps = 0;
            Debug.Log("ground found");
            
        }
        
        Anim.SetBool("grounded", grounded);



        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(!skating)
        {
            rb.velocity = new Vector2(horizontal * MovementSpeed, rb.velocity.y);
        }
        

        Anim.SetBool("walking", false);
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            
            rend.flipX = false;
            walking = true;
            Anim.SetBool("walking", true);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            
            rend.flipX = true;
            walking = true;
            Anim.SetBool("walking", true);
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !grounded)
        {
            rb.AddForce(Vector2.down * groundSlamPower, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {

        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            Anim.SetBool("walking", false);
        }
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            skating = !skating;
            Anim.SetBool("skating", skating);
            MovementSpeed = Anim.GetBool("skating") ? SkatingSpeed : WalkingSpeed;
            skateBoardCollider.SetActive(Anim.GetBool("skating"));
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && grounded && canJump)
        {
            canJump = false;
            rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            Debug.Log("1");
        }
        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && !grounded)
        {
            Debug.Log("2");
            if(jumps < AirJumpCount)
            {
            Debug.Log("3");
                rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                jumps++;
            }
            
        }
    }
}
