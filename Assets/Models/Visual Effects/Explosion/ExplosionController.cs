using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{
    public Light explosionLight;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        explosionLight.intensity = Mathf.Lerp(explosionLight.intensity, 0f, 5*Time.time);
    }
}