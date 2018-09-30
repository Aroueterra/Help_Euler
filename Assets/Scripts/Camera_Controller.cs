using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Transform target;
    Camera mycam;
    // Use this for initialization
    public BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;
    private bool cameraExists;
    void Start()
    {
        mycam = GetComponent<Camera>();
        DontDestroyOnLoad(transform.gameObject);

        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        theCamera = GetComponent<Camera>();
        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

    }

    // Update is called once per frame
    void Update()
    {
        mycam.orthographicSize = (Screen.height / 100f) / 2;
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, 0.1f) + new Vector3(0, 0, -10);
        }
        float clampX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampY = Mathf.Clamp(transform.position.y, minBounds.y + halfWidth, maxBounds.y - halfWidth);
        transform.position = new Vector3(clampX, clampY, transform.position.z);
    }

    public void SetBounds(BoxCollider2D Area_Bounds)
    {
        boundBox = Area_Bounds;
        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }
}
