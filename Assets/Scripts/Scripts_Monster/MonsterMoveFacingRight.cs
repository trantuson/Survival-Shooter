using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterMoveFacingRight : MonoBehaviour
{
    private AIPath aiPath;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
            spriteRenderer.flipX = false; // Quay mặt phải
        else if (aiPath.desiredVelocity.x <= -0.01f)
            spriteRenderer.flipX = true;  // Quay mặt trái
    }
}
