using UnityEngine;

public class TriggerGate : MonoBehaviour
{
    [SerializeField] private bool _playerGate;
    [SerializeField] private GameManagerOnePlayer _gameManagerOne;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Ball"))
        {
            _gameManagerOne.Goal(_playerGate);
        }
    }
}
