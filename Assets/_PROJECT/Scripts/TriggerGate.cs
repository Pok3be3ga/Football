using UnityEngine;

public class TriggerGate : MonoBehaviour
{
    [SerializeField] private bool _playerGate;
    [SerializeField] private GameManagerOne _gameManagerOne;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _gameManagerOne.Goal(_playerGate);
        }
    }
}
