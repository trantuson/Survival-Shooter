using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))] // attribute (thuộc tính) dùng để đảm bảo rằng GameObject có Component AIPath
public class BossMovement : MonoBehaviour
{
    private AIPath aiPath;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float lastDirectionX = 1f; // Mặc định nhìn phải
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. Điều khiển animation
        float speed = aiPath.velocity.magnitude;
        if(aiPath.canMove == false || speed < 0.01f)
        {
            animator.SetFloat("Speed", 0f);
        }
        else
        {
            animator.SetFloat("Speed", speed);
        }

        //// 2. Lật mặt theo hướng di chuyển X
        //if (aiPath.desiredVelocity.x >= 0.01f)
        //    spriteRenderer.flipX = false; // Quay mặt phải
        //else if (aiPath.desiredVelocity.x <= -0.01f)
        //    spriteRenderer.flipX = true;  // Quay mặt trái
        // Lật mặt (chỉ khi đang di chuyển và tốc độ đủ lớn)
        // Lật mặt: chỉ khi đang di chuyển thực sự
        if (aiPath.canMove && aiPath.desiredVelocity.x != 0)
        {
            lastDirectionX = aiPath.desiredVelocity.x;
        }

        // Flip theo hướng cuối cùng
        spriteRenderer.flipX = lastDirectionX < 0;
    }
}
