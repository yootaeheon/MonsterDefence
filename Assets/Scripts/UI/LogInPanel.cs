using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogInPanel : MonoBehaviour 
{
    [SerializeField] TMP_InputField emialInputField;
    [SerializeField] TMP_InputField passwordInputField;

    [SerializeField] NickNamePanel nickNamePanel;
    [SerializeField] VerifyPanel verifyPanel;

    public void LogIn()
    {
        string email = emialInputField.text;
        string password = passwordInputField.text;

        BackendManager.Auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            AuthResult result = task.Result;
            Debug.Log($"User signed in successfully: {result.User.DisplayName} ({result.User.UserId})");
            CheckUserInfo();
        });
    }

    public void CheckUserInfo()
    {
        FirebaseUser user = BackendManager.Auth.CurrentUser;
        if (user == null)
            return;

        Debug.Log($"Display Name : {user.DisplayName}");
        Debug.Log($"Email : {user.Email}");
        Debug.Log($"Email Verified : {user.IsEmailVerified}");
        Debug.Log($"User ID : {user.UserId}");

        if (user.IsEmailVerified == false)
        {
            verifyPanel.gameObject.SetActive(true);
        }
        else if (user.DisplayName == "")
        {
            nickNamePanel.gameObject.SetActive(true);
        }

        SceneManager.LoadScene(1);
    }
}