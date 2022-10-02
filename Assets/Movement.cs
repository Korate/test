using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform CameraTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void LateUpdate()
    {
        Debug.Log(CameraTarget.rotation);
        
        if (CameraTarget != null)
        {   
            transform.position = CameraTarget.position;
            transform.rotation = CameraTarget.rotation;
            Debug.Log(CameraTarget.rotation);
            //if(CameraTarget.di)
            //Vector3 pos = Vector2.Lerp((Vector2)transform.position, (Vector2)CameraTarget.position, 3 * Time.deltaTime);
            //pos.z = -1;
            //transform.position = pos;
            //transform.position.z = -1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //transform.rotation = CameraTarget.rotation;

    }
}
