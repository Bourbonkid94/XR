using UnityEngine;

public class light : MonoBehaviour
{
    public Light light;
    // Start is called before the first frame update
    void Start() {
        light = GetComponent<Light>();
        if ( Input.GetKeyDown("tab"))
        {
            light.color = (1,0,1,1);
        }
    }
}
