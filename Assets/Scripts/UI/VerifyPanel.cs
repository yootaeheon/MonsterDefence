using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using UnityEngine;

public class VerifyPanel : MonoBehaviour
{
    [SerializeField] NickNamePanel nickNamePanel;

    private void OnEnable()
    {
        SendVerifyMail();
    }

    private void OnDisable()
    {
        if (checkVerifyRoutine != null)
        {
            StopCoroutine(checkVerifyRoutine);
        }

    }

    private void SendVerifyMail()
    {
        FirebaseUser User = BackendManager.Auth.CurrentUser;
        User.SendEmailVerificationAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SendEmailVerificationAsync was canceled.");
                    gameObject.SetActive(false);
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SendEmailVerificationAsync encountered an error: " + task.Exception);
                    gameObject.SetActive(false);
                    return;
                }

                Debug.Log("Email sent successfully.");
                checkVerifyRoutine = StartCoroutine(CheckVerifyRoutine());
            });
    }

    Coroutine checkVerifyRoutine;
    IEnumerator CheckVerifyRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(3f);

        while (true)
        {
            BackendManager.Auth.CurrentUser.ReloadAsync().ContinueWithOnMainThread(task => 
            { 
                if (task.IsCanceled)
                {
                    Debug.LogError("ReloadAsync was canceled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError($"ReloadAsync encounterd an error : {task.Exception.Message}");
                    return;
                }

                if (BackendManager.Auth.CurrentUser.IsEmailVerified == true)
                {
                    Debug.Log("인증 확인");
                    nickNamePanel.gameObject.SetActive (true);  
                    gameObject.SetActive (false);
                }

            });

            yield return delay;
        }
    }
}
