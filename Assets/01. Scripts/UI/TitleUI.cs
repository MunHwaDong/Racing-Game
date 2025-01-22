using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    void Start()
    {
        _startButton.onClick.AddListener(() => EventBus.Publish(EventType.COUNTDOWN));
        
        EventBus.Subscribe(EventType.COUNTDOWN, OnStart);
    }

    void OnStart()
    {
        gameObject.SetActive(false);
    }
}
