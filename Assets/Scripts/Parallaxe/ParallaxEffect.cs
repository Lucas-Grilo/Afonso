using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffectMultiplier;

    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffectMultiplier));
        float distance = (cam.transform.position.x * parallaxEffectMultiplier);

        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
