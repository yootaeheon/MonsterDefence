using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;

public class NickNamePanel : MonoBehaviour
{
    [SerializeField] TMP_InputField nickNameInputField;

    public void Confirm()
    {
        string nickName = nickNameInputField.text;
        if (nickName == "")
        {
            Debug.LogWarning("닉네임 설정 해주세용");
            return;
        }


        UserProfile profile = new UserProfile();
        profile.DisplayName = nickName;

        BackendManager.Auth.CurrentUser.UpdateUserProfileAsync(profile)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");
            });
    }
}