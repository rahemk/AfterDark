using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    private float length, startpos;
    [SerializeField] float Yaxis = 4.4f;
    public GameObject cam;
    public float parallaxEffect;
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1-parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + dist, Yaxis, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
