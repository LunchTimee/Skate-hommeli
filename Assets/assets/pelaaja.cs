using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelaaja : MonoBehaviour
{

    public float MovementSpeed = 5f;
    public float JumpPower = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * MovementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * MovementSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Time.deltaTime * JumpPower);
        }
    }
}
