using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }

    [SerializeField] int level;
    public int Level { get { return level; } set { level = value; } }

    [SerializeField] int curStage;
    public int CurStage { get { return curStage; } set { curStage = value; } }

    [SerializeField] int gold;
    public int Gold { get { return gold; } set { gold = value; } }

    public DatabaseReference userDataRef { get; private set; }
    public DatabaseReference nickNameRef { get; private set; }
    public DatabaseReference levelRef { get; private set; }
    public DataSnapshot levelSnapshot { get; private set; }
    public DatabaseReference curStageRef { get; private set; }
    public DatabaseReference goldRef { get; private set; }
    public DataSnapshot goldSnapshot { get; private set; }

    public DataSnapshot snapShot { get; set; }
    public string json { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if (Instance != null)
            return;

        string userId = BackendManager.Auth.CurrentUser.UserId;
        userDataRef = BackendManager.Database.RootReference.Child(userId);

        nickNameRef = userDataRef.Child("nickName");
        levelRef = userDataRef.Child("level");
        levelRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log("���ҷ���");
            }
            levelSnapshot = task.Result;
            Debug.Log("�ҷ���");
        });
        curStageRef = userDataRef.Child("curStage");
        goldRef = userDataRef.Child("gold");
        goldRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                goldSnapshot = task.Result;
            }
        });

        userDataRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogWarning("�� �������� ��ҵ�");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogWarning($"�� �������� ������ : {task.Exception.Message}");
                return;
            }

            snapShot = task.Result;
            if (snapShot.Value == null)
            {
                UserData userData = new UserData();
                userData.userId = BackendManager.Auth.CurrentUser.UserId;
                userData.eMail = BackendManager.Auth.CurrentUser.Email;
                userData.nickName = BackendManager.Auth.CurrentUser.DisplayName;
                userData.level = 1;
                userData.curStage = 0;
                userData.gold = 0;

                json = JsonUtility.ToJson(userData);
                userDataRef.SetRawJsonValueAsync(json);
            }
            else
            {
                json = snapShot.GetRawJsonValue();

                UserData userData = JsonUtility.FromJson<UserData>(json);

                Debug.Log(userData.userId);
                Debug.Log(userData.eMail);
                Debug.Log(userData.nickName);
                Debug.Log(userData.level);
                Debug.Log(userData.curStage);
                Debug.Log(userData.gold);
                Debug.Log(gold);
            }
        });

        levelRef.ValueChanged += LevelRef_ValueChanged;
        levelRef.ValueChanged += curStageRef_ValueChanged;
        levelRef.ValueChanged += goldRef_ValueChanged;
    }

    private void OnDisable()
    {
        if (Instance != null)
            return;

        levelRef.ValueChanged -= LevelRef_ValueChanged;
        levelRef.ValueChanged -= curStageRef_ValueChanged;
        levelRef.ValueChanged -= goldRef_ValueChanged;
    }

    private void LevelRef_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // ������ ���̽��� �ٲ�� ���� ������ ����
        Debug.Log($"�� ���� �̺�Ʈ Ȯ�� : {e.Snapshot.Value.ToString()}");
        /* level = int.Parse(e.Snapshot.Value.ToString());*/
    }

    private void curStageRef_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // ������ ���̽��� �ٲ�� ���� ������ ����
        Debug.Log($"�� ���� �̺�Ʈ Ȯ�� : {e.Snapshot.Value.ToString()}");
        /* curStage = int.Parse(e.Snapshot.Value.ToString());*/
    }

    private void goldRef_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // ������ ���̽��� �ٲ�� ���� ������ ����
        Debug.Log($"�� ���� �̺�Ʈ Ȯ�� : {e.Snapshot.Value.ToString()}");
        /*gold = int.Parse(e.Snapshot.Value.ToString());*/
    }
}

[System.Serializable]
public class UserData : MonoBehaviour
{
    public static UserData Instance { get; private set; }
    public string userId { get; set; }
    public string eMail { get; set; }
    public string nickName { get; set; }
    public int level { get; set; }
    public int curStage { get; set; }
    public int gold { get; set; }
}