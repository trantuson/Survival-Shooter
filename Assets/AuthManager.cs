using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class UserData
{
    public string username;
    public string PasswordHash;
    public string Salt;
}

public class AuthManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI resultText;

    private string baseUrl = "http://localhost:5168/api/User";

    public void Register()
    {
        string salt = Guid.NewGuid().ToString("N"); // sinh chuỗi salt ngẫu nhiên (32 ký tự)
        string hashedPassword = GenerateHash(passwordInput.text, salt);

        UserData user = new UserData
        {
            username = usernameInput.text,
            PasswordHash = hashedPassword,
            Salt = salt
        };

        StartCoroutine(PostRequest($"{baseUrl}/register", user));
    }

    public void Login()
    {
        // Gửi password thô và xử lý hash + salt phía server (nếu bạn code như vậy)
        UserData user = new UserData
        {
            username = usernameInput.text,
            PasswordHash = passwordInput.text,
            Salt = "" // Có thể bỏ nếu server không yêu cầu Salt khi login
        };

        StartCoroutine(PostRequest($"{baseUrl}/login", user));
    }

    IEnumerator PostRequest(string url, UserData userData)
    {
        string json = JsonUtility.ToJson(userData);
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            resultText.text = "Thành công: " + request.downloadHandler.text;
        }
        else
        {
            resultText.text = $"Lỗi: {request.error}\nStatus: {request.responseCode}\nChi tiết: {request.downloadHandler.text}";
            Debug.LogError($"Error: {request.error}, StatusCode: {request.responseCode}, Response: {request.downloadHandler.text}");
        }
    }

    private string GenerateHash(string password, string salt)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] combined = Encoding.UTF8.GetBytes(password + salt);
            byte[] hash = sha.ComputeHash(combined);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
