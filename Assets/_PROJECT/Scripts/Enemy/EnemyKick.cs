using UnityEngine;

public class EnemyKick : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;
    private float _timer;
    void Start()
    {
        transform.parent = null;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _enemyMovement.KickAnimation();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _timer += Time.deltaTime;
            if (_timer > 2) 
            {
                _enemyMovement.OnContact();
                _timer = 0;
            }
        }
    }
}
