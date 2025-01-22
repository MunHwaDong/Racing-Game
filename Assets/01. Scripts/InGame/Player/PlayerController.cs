using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Coroutine _coroutine = null;
    private Vector3 _screenBound;
    private float _speed = 3f;
    private float _spriteWidth;
    
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _spriteWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        _screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        EventBus.Subscribe(EventType.COUNTDOWN, Movement);
        EventBus.Subscribe(EventType.END, GameOver);
    }

    void Movement()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(MovementControll());
    }

    IEnumerator MovementControll()
    {
        while (true)
        {
            float move = _playerInput.actions["Move"].ReadValue<Vector2>().x;
            
            gameObject.transform.position += new Vector3(move * Time.deltaTime * _speed, 0, 0);
            
            if(Clamp(gameObject.transform.position.x))
                gameObject.transform.position += new Vector3(-move * Time.deltaTime * _speed, 0, 0);
            
            yield return null;
        }
    }

    bool Clamp(float pos)
    {
        if (pos > -_screenBound.x - _spriteWidth || pos < _screenBound.x + _spriteWidth) return true;
        
        return false;
    }

    void GameOver()
    {
        StopCoroutine(_coroutine);
    }
}
