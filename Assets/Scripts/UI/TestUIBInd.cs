using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestUIBInd : UIBInder
{
    // Start is called before the first frame update
    void Awake()
    {
        BindAll();
    }

    private void Start()
    {
        //GetUI<TextMeshProUGUI>("Text2").text = "10";
    }

    // GetUI("Key") : Key �̸��� ���ӿ�����Ʈ ��������
    // GetUI<Image>("Key") : Key �̸��� ���ӿ�����Ʈ���� Image ������Ʈ ��������
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //GetUI<TextMeshProUGUI>("Text2").text = "100";
        }
    }

    public void Click(PointerEventData eventData)
    {
        Debug.Log("TextTest");
    }
}
