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

    // GetUI("Key") : Key 이름의 게임오브젝트 가져오기
    // GetUI<Image>("Key") : Key 이름의 게임오브젝트에서 Image 컴포넌트 가져오기
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
