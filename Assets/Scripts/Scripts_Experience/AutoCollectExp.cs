using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCollectExp : MonoBehaviour
{
    public float attractDistance = 2f; // khoảng cách khi player đến gần thì hút
    public float moveSpeed = 8f; // tốc độ hút 
    public float pushDuration = 0.3f; // Thời gian đẩy nhẹ exp ra xa player (khi player vừa tiếp cận)
    public float pushDistance = 1.3f; // Khoảng cách sẽ đẩy exp ra xa (khi vừa bị hút lần đầu)

    private Transform player;
    private bool isAttracting = false; // Đã bắt đầu hút exp về player chưa?
    private bool isPushed = false; // Có đang trong giai đoạn "đẩy nhẹ" exp ra xa không?
    private Vector3 pushDirection; // Hướng đẩy ra xa player (tính tại thời điểm hút lần đầu)
    private float pushTimeLeft; // Thời gian còn lại để tiếp tục đẩy exp ra.

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attractDistance && !isAttracting)
        {
            // Bắt đầu giai đoạn hút và đẩy
            isAttracting = true;
            isPushed = true;

            // Tính hướng đẩy khỏi player tại thời điểm hút
            pushDirection = (transform.position - player.position).normalized;
            pushTimeLeft = pushDuration;
        }

        if (isPushed && pushTimeLeft > 0)
        {
            // tính vận tốc : vận tốc = quãng đường / thời gian
            transform.position += pushDirection * (pushDistance / pushDuration) * Time.deltaTime;
            pushTimeLeft -= Time.deltaTime;
        }
        else if (isAttracting)
        {
            isPushed = false;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}
