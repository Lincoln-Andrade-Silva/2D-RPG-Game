using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Tilemap tileMap;
    public bool fixedCamera;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private float halfHeight;
    private float halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit =
            tileMap.localBounds.min + new Vector3(halfWidth + 0.3f, halfHeight + 0.3f, 0f);
        topRightLimit =
            tileMap.localBounds.max + new Vector3(-halfWidth - 0.3f, -halfHeight - 0.3f, 0f);

        if (!fixedCamera)
        {
            PlayerController.instance.SetBounds(tileMap.localBounds.min, tileMap.localBounds.max);
        }
    }

    // Update is called once per frame
    void Update() { }

    void LateUpdate()
    {
        if (!fixedCamera)
        {
            transform.position = new Vector3(
                target.position.x,
                target.position.y,
                transform.position.z
            );

            //keep camera insede the bounds
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                transform.position.z
            );
        }
    }
}
