using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{   
    public float scrollSpeed;
    void Update()
    {
        transform.position += new Vector3(-scrollSpeed * Time.deltaTime,0f);
        if(transform.position.x < -18.25f)
        {
            transform.position = new Vector3(18.25f, transform.position.y);
        }
    }
}
