using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowSetting : MonoBehaviour
{
    [SerializeField] private GameObject panelSetting;
    [SerializeField] private GameObject panelSettingSound;
    [SerializeField] private GameObject buttonSetting;
    [SerializeField] private GameObject buttonOK;

    [SerializeField] private GameObject panelGameOver;
    public void ShowSetTing()
    {
        panelSetting.SetActive(true);
        Time.timeScale = 0f;
        buttonSetting.SetActive(false);
    }
    public void ContinueGame()
    {
        panelSetting.SetActive(false);
        Time.timeScale = 1f;
        buttonSetting.SetActive(true);
    }
    public void ShowSettingSource()
    {
        panelSetting.SetActive(false);
        panelSettingSound.SetActive(true);
        Time.timeScale = 0f;
        buttonSetting.SetActive(false);
        buttonOK.SetActive(true);
    }
    public void CloseSetting()
    {
        panelSettingSound.SetActive(false);
        Time.timeScale = 1f;
        buttonOK.SetActive(false);
        buttonSetting.SetActive(true);
    }
    public void BackHome()
    {
        SceneManager.LoadScene("Home");
    }
    public void ShowGameOver()
    {
        panelGameOver.SetActive(true);
    }
    public void RestartGameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        panelGameOver.SetActive(false);
        Time.timeScale = 1f;
    }
}
