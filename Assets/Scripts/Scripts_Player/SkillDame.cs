using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDame : MonoBehaviour
{
    private int damage = 5;
    private Coroutine deactivateCoroutine;

    private void OnEnable()
    {
        // Nếu coroutine cũ chưa kết thúc, dừng lại
        if (deactivateCoroutine != null)
        {
            StopCoroutine(deactivateCoroutine);
        }
        // Mỗi lần bật lại skill, chạy lại thời gian tắt
        deactivateCoroutine = StartCoroutine(SetActiveTime());
    }
    public void SetDamage(int value)
    {
        damage = value;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            HeathManager heathManager = collision.GetComponent<HeathManager>();
            if (heathManager != null)
            {
                heathManager.TakeDame(damage);
            }
        }
    }
    private IEnumerator SetActiveTime()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}
