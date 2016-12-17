using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public Light explosionLight;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        explosionLight.intensity = Mathf.Lerp(explosionLight.intensity, 0f, 5*Time.time);
    }
}