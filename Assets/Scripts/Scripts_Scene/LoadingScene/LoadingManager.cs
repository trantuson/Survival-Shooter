using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    public Transform movingObject; // Đối tượng sẽ đi theo progressBar
    public RectTransform startPoint; // Điểm bắt đầu của thanh
    public RectTransform endPoint;   // Điểm kết thúc của thanh

    void Start()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        string sceneToLoad = SceneLoader.TargetScene;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            progressText.text = (progress * 100f).ToString("F0") + "%";

            // Cập nhật vị trí đối tượng theo progress
            Vector3 pos = Vector3.Lerp(startPoint.position, endPoint.position, progress);
            movingObject.position = pos;

            // Khi load gần xong (90%), chờ 1s rồi cho vào
            if (operation.progress >= 0.9f)
            {
                progressText.text = "100%";
                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}

