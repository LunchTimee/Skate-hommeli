using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    SpriteRenderer rend;
    public float parallaxingSpeedx;
    public float parallaxingSpeedy;
    public Material mat;

    public Transform player;
    public pelaaja p;
    Vector2 oldPos;
    Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        p = player.GetComponent<pelaaja>();
        startPos = transform.localPosition;

        oldPos = player.position;

        rend = GetComponent<SpriteRenderer>();

        mat = rend.material;

    }

    // Update is called once per frame
    void Update()
    {

        float dir = (player.position.y > p.startPos.y) ? 1f : -1f;
        transform.localPosition = new Vector2(0, startPos.y +(Mathf.Abs(player.position.y - p.startPos.y) * dir * parallaxingSpeedy));

        float x = (oldPos.x - player.position.x) * parallaxingSpeedx;
        float y = (oldPos.y - player.position.y) * parallaxingSpeedy;

        Vector2 newOffset = mat.GetTextureOffset("_MainTex");

        newOffset.x += x;
        

        oldPos = player.transform.position;

        mat.SetTextureOffset("_MainTex", newOffset); 
    }
}
