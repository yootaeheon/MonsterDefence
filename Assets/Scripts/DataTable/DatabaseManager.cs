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

    public DatabaseReference userDataRef;
    public DatabaseReference nickNameRef;
    public DatabaseReference levelRef;
    public DatabaseReference curStageRef;
    public DatabaseReference goldRef;

    private void Awake()
    {
      /*  if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
    }

    private void OnEnable()
    {
        string userId = BackendManager.Auth.CurrentUser.UserId;
        userDataRef = BackendManager.Database.RootReference.Child(userId);

        nickNameRef = userDataRef.Child("nickName");
        levelRef = userDataRef.Child("level");
        curStageRef = userDataRef.Child("curStage");
        goldRef = userDataRef.Child("gold");

        userDataRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogWarning("값 가져오기 취소됨");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogWarning($"값 가져오기 실패함 : {task.Exception.Message}");
                return;
            }

            DataSnapshot snapShot = task.Result;
            if (snapShot.Value == null)
            {
                UserData userData = new UserData();
                userData.userId = BackendManager.Auth.CurrentUser.UserId;
                userData.eMail = BackendManager.Auth.CurrentUser.Email;
                userData.nickName = BackendManager.Auth.CurrentUser.DisplayName;
                userData.level = 1;
                userData.curStage = 1;
                userData.gold = 0;

                string json = JsonUtility.ToJson(userData);
                userDataRef.SetRawJsonValueAsync(json);
            }
            else
            {
                string json = snapShot.GetRawJsonValue();

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
        levelRef.ValueChanged -= LevelRef_ValueChanged;
        levelRef.ValueChanged -= curStageRef_ValueChanged;
        levelRef.ValueChanged -= goldRef_ValueChanged;
    }

    private void LevelRef_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // 데이터 베이스가 바뀌면 다음 값으로 설정
        Debug.Log($"값 변경 이벤트 확인 : {e.Snapshot.Value.ToString()}");
        level = int.Parse(e.Snapshot.Value.ToString());
    }

    private void curStageRef_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // 데이터 베이스가 바뀌면 다음 값으로 설정
        Debug.Log($"값 변경 이벤트 확인 : {e.Snapshot.Value.ToString()}");
        curStage = int.Parse(e.Snapshot.Value.ToString());
    }

    private void goldRef_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // 데이터 베이스가 바뀌면 다음 값으로 설정
        Debug.Log($"값 변경 이벤트 확인 : {e.Snapshot.Value.ToString()}");
        gold = int.Parse(e.Snapshot.Value.ToString());
    }

    /*private void LevelUp()
    {
    데이터베이스에 요청
        levelRef.SetValueAsync(level + 1);
    }*/


}

[System.Serializable]
public class UserData
{
    public string userId;
    public string eMail;
    public string nickName;
    public int level;
    public int curStage;
    public int gold;
}