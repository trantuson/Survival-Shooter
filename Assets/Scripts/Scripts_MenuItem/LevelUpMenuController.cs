using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpMenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel; // Panel menu
    [SerializeField] private Transform choicesContainer; // Container cho các nút lựa chọn
    [SerializeField] private GameObject choicePrefab; // Prefab cho một lựa chọn
    [SerializeField] private LevelUpData levelUpData; // Dữ liệu level
    private PlayerStats playerStats; // Tham chiếu tới PlayerStats


    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>(); // Tìm PlayerStats trong scene
    }
    public void ShowMenu(int level)
    {
        // Xóa các nút cũ
        foreach (Transform child in choicesContainer)
        {
            Destroy(child.gameObject);
        }

        // Lấy random 3 lựa chọn từ level tương ứng
        List<LevelUpChoice> choices = levelUpData.GetRandomChoicesForLevel(level, 3);

        if (choices != null)
        {
            foreach (var choice in choices)
            {
                var tempChoice = choice;

                GameObject choiceButton = Instantiate(choicePrefab, choicesContainer);
                choiceButton.transform.Find("Icon").GetComponent<Image>().sprite = tempChoice.icon;
                choiceButton.transform.Find("Description").GetComponent<TMP_Text>().text = tempChoice.description;

                Button button = choiceButton.GetComponent<Button>();
                button.onClick.AddListener(() => ApplyChoice(tempChoice));
            }
        }
        menuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void ApplyChoice(LevelUpChoice choice)
    {
        // Áp dụng hiệu ứng của lựa chọn
        if (playerStats != null)
        {
            playerStats.IncreaseStats(choice.statType, choice.amount);

            if(choice.modifiers != null)
            {
                foreach(var modifier in choice.modifiers)
                {
                    playerStats.IncreaseStats(modifier.statType, modifier.amount);
                }
            }
        }
        // Ẩn menu
        menuPanel.SetActive(false);
        Time.timeScale = 1; // Tiếp tục game
    }
}
