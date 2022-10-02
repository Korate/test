

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Speed = 3.25f;
    public Transform CameraTarget;
    public Vector2 TargetDistance;
    // Start is called before the first frame update
    void Start()
    {
        TargetDistance = transform.position - CameraTarget.position;
    }
    private void LateUpdate()
    {
        if (CameraTarget != null)
        {
            Vector3 pos = Vector2.Lerp((Vector2)transform.position, (Vector2)CameraTarget.position, Speed * Time.deltaTime);
            pos.z = -1;
            transform.position = pos;
            //transform.position.z = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

