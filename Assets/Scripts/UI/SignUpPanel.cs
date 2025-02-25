using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class SignUpPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField emialInputField;
    [SerializeField] TMP_InputField passwordInputField;
    [SerializeField] TMP_InputField passwordConfirmInputField;

    public void SignUp()
    {
        string email = emialInputField.text;
        string pass = passwordInputField.text;
        string confirm = passwordConfirmInputField.text;

        if (email == "")
        {
            Debug.LogWarning("�̸����� �Է����ּ���");
            return;
        }

        if (pass != confirm)
        {
            Debug.LogWarning("�н����尡 ��ġ���� �ʽ��ϴ�");
            return;
        }

        BackendManager.Auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            AuthResult result = task.Result;
            Debug.Log($"User signed in successfully: {result.User.DisplayName} ({result.User.UserId})");
            gameObject.SetActive(false);
        });
    }
}