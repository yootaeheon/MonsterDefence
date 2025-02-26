using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        //터치가 감지되었는지 확인
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // 터치의 상태를 확인하고 원하는 동작을 실행
            if (touch.phase == TouchPhase.Began)
            {
                // 터치가 시작될 때의 동작
                Debug.Log("터치가 시작되었습니다!");
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // 터치가 이동 중일 때의 동작
                Debug.Log("터치가 이동 중입니다.");
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // 터치가 끝날 때의 동작
                Debug.Log("터치가 끝났습니다.");
            }
        }
    }


}
