using UnityEngine;

public class TriggerGate : MonoBehaviour
{
    [SerializeField] private bool _playerGate;
    [SerializeField] private GameManagerOnePlayer _gameManagerOne;
    [SerializeField] private Collider _collider;
    private void Start()
    {
        _collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            _gameManagerOne.Goal(_playerGate);
        }
    }
    public void OnOfCollider(bool b)
    {
        _collider.enabled = b;
    }

}
