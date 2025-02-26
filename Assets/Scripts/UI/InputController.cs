using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        //��ġ�� �����Ǿ����� Ȯ��
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ��ġ�� ���¸� Ȯ���ϰ� ���ϴ� ������ ����
            if (touch.phase == TouchPhase.Began)
            {
                // ��ġ�� ���۵� ���� ����
                Debug.Log("��ġ�� ���۵Ǿ����ϴ�!");
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // ��ġ�� �̵� ���� ���� ����
                Debug.Log("��ġ�� �̵� ���Դϴ�.");
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // ��ġ�� ���� ���� ����
                Debug.Log("��ġ�� �������ϴ�.");
            }
        }
    }


}
