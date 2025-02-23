using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


//UIBinding
//���� ���ӿ��� UI������ �����ϰ� ���� ������, 
// �ν����ͻ󿡼� �����Ͽ� �����ϱⰡ ���� ����.
// ������ ��ũ��Ʈ�󿡼� ���ε��ϵ�����.
public class UIBInder : MonoBehaviour
{
    private Dictionary<string, GameObject> gameObjectDic;
    private Dictionary<(string, System.Type), Component> componentDic;

    // ���� �ð��� ���ӿ�����Ʈ�� ���ε�
    protected void Bind()
    {
        //false�� �ϸ� ��Ȱ��ȭ ������Ʈ�� ��ã��, true�� ��Ȱ��ȭ Ȱ��ȭ ��� ã��.
        Transform[] transforms = GetComponentsInChildren<Transform>(true);
        // 2* 2 ũ�⸦ 4��� �ϴ�.
        gameObjectDic = new Dictionary<string, GameObject>(transforms.Length << 2);
        foreach (Transform child in transforms)
        {
            gameObjectDic.TryAdd(child.gameObject.name, child.gameObject);
        }

        componentDic = new Dictionary<(string, System.Type), Component>();
    }

    // ���� ���� �ð��� ���ӿ�����Ʈ�� ��� ������Ʈ ���ε�
    protected void BindAll()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>(true);
        gameObjectDic = new Dictionary<string, GameObject>(transforms.Length << 2);

        foreach (Transform child in transforms)
        {
            gameObjectDic.TryAdd(child.gameObject.name, child.gameObject);
        }

        Component[] components = GetComponentsInChildren<Component>(true);
        componentDic = new Dictionary<(string, System.Type), Component>(components.Length << 4);
        foreach (Component child in components)
        {
            componentDic.TryAdd((child.gameObject.name, components.GetType()), child);
        }
    }

    // �̸��� name�� UI ���ӿ�����Ʈ ��������
    // GetUI("Key") : Key �̸��� ���ӿ�����Ʈ ��������
    public GameObject GetUI(in string name)
    {
        gameObjectDic.TryGetValue(name, out GameObject gameObject);
        return gameObject;
    }

    // �̸��� name�� UI���� ������Ʈ T ��������
    // GetUI<Image>("Key") : Key �̸��� ���ӿ�����Ʈ���� Image ������Ʈ ��������
    public T GetUI<T>(in string name) where T : Component
    {
        (string, System.Type) key = (name, typeof(T));

        //������Ʈ ��ųʸ��� �̹� ������(ã�ƺ� �� �ִ� ���): �̹� ã���� ��.
        componentDic.TryGetValue(key, out Component component);
        if (component != null)
            return component as T;
        // ������Ʈ ��ųʸ��� ���� ���ٸ� ã�� �� ��ųʸ��� �߰��ϰ� ��.
        gameObjectDic.TryGetValue(name, out GameObject gameObject);
        if (gameObject == null)
            return null;

        component = gameObject.GetComponent<T>();
        if (component == null)
            return null;

        componentDic.TryAdd(key, component);
        return component as T;
    }

    public enum EventType { Click, Enter, Exit, Up, Down, Move, BeginDrag, EndDrag, Drag, Drop }
    public void AddEvent(in string name, EventType eventType, UnityAction<PointerEventData> callback)
    {
        gameObjectDic.TryGetValue(name, out GameObject gameObject);
        if (gameObject == null)
            return;

        EventReceiver receiver = gameObject.GetOrAddComponent<EventReceiver>();
        switch (eventType)
        {
            case EventType.Click:
                receiver.OnClicked += callback;
                break;
            case EventType.Enter:
                receiver.OnEntered += callback;
                break;
            case EventType.Exit:
                receiver.OnExited += callback;
                break;
            case EventType.Up:
                receiver.OnUped += callback;
                break;
            case EventType.Down:
                receiver.OnDowned += callback;
                break;
            case EventType.Move:
                receiver.OnMoved += callback;
                break;
            case EventType.BeginDrag:
                receiver.OnBeginDraged += callback;
                break;
            case EventType.EndDrag:
                receiver.OnEndDraged += callback;
                break;
            case EventType.Drag:
                receiver.OnDraged += callback;
                break;
            case EventType.Drop:
                receiver.OnDroped += callback;
                break;

        }
    }

    public void RemoveEvent(in string name, EventType eventType, UnityAction<PointerEventData> callback)
    {
        gameObjectDic.TryGetValue(name, out GameObject gameObject);
        if (gameObject == null)
            return;

        EventReceiver receiver = gameObject.GetOrAddComponent<EventReceiver>();
        switch (eventType)
        {
            case EventType.Click:
                receiver.OnClicked -= callback;
                break;
            case EventType.Enter:
                receiver.OnEntered -= callback;
                break;
            case EventType.Exit:
                receiver.OnExited -= callback;
                break;
            case EventType.Up:
                receiver.OnUped -= callback;
                break;
            case EventType.Down:
                receiver.OnDowned -= callback;
                break;
            case EventType.Move:
                receiver.OnMoved -= callback;
                break;
            case EventType.BeginDrag:
                receiver.OnBeginDraged -= callback;
                break;
            case EventType.EndDrag:
                receiver.OnEndDraged -= callback;
                break;
            case EventType.Drag:
                receiver.OnDraged -= callback;
                break;
            case EventType.Drop:
                receiver.OnDroped -= callback;
                break;

        }
    }
}