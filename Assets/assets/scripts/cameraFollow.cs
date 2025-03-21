using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public float agressiivisuus;
    public float followSpeed;
    public Transform target;
    public pelaaja p;
    // Start is called before the first frame update
    void Start()
    {
        p = target.GetComponent<pelaaja>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xOffset = p.rb.velocity.x * agressiivisuus;
        float yOffset = p.rb.velocity.y * agressiivisuus;

        Vector3 newPos = new Vector3(target.position.x + xOffset, target.position.y + yOffset + 3, -10f);

        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
