using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public float positioninx = 0;
    public float positioniny = 0;
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var x = playerTransform.position.x + positioninx;
        var y = playerTransform.position.y + positioniny;
        transform.position = new Vector3(x,y, transform.position.z);
    }
}
