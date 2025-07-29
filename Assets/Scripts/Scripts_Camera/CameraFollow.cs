using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Collider2D mapBox2D;
    [SerializeField] private float smoother = 1f;

    private float camHalfHeight;
    private float camHalfWidth;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private void Start()
    {
        // tính chiều cao và chiều rộng
        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;

        // lấy giới hạn map
        Bounds bounds = mapBox2D.bounds;
        minBounds = bounds.min;
        maxBounds = bounds.max;
    }
    private void LateUpdate()
    {
        // lấy vị trí mong muốn dựa trên player
        Vector3 targetPositon = new Vector3(player.transform.position.x,player.transform.position.y,transform.position.z);

        // giới hạn Y và X
        float clampedPosY = Mathf.Clamp(targetPositon.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);
        float clampedPosX = Mathf.Clamp(targetPositon.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);

        // cập nhật vị trí của camera
        Vector3 smootherPosition = Vector3.Lerp(transform.position, new Vector3(clampedPosX,clampedPosY,transform.position.z), smoother);
        transform.position = smootherPosition;
    }
}
