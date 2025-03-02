using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerKick : MonoBehaviour
{
    [SerializeField] private GameObject _kickObject;
    [SerializeField] private float _kickDuration = 0.2f;
    private PlayerMovement _player;
    void Start()
    {
        _kickObject.transform.parent = null;
        _kickObject.SetActive(false);
        _player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(_player.KickKey) && _kickObject.active == false)
        {
            StartCoroutine(Kick());
        }
    }
    IEnumerator Kick()
    {
        _player.KickAnimation();
        _kickObject.SetActive(true);
        yield return new WaitForSeconds(_kickDuration);
        _kickObject.SetActive(false);
    }
}
