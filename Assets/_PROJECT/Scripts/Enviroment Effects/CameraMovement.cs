using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _ball;
    [SerializeField] private float _minX = -10f;
    [SerializeField] private float _maxX = 10f;
    [SerializeField] private float _smoothSpeed = 0.15f;

    private float _startX;
    private Vector3 _targetPosition;   // Целевая позиция камеры
    private void Awake()
    {
        _startX = transform.position.x;

        ResetCamera();
    }
    void LateUpdate()
    {
        float targetX = Mathf.Clamp(_ball.position.x, _minX, _maxX);

        _targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        // Плавное перемещение камеры к целевой позиции
        //transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
       // if (targetX < (_maxX + Mathf.Abs(_minX)) / 4f || targetX > (_maxX + Mathf.Abs(_minX)) - (_maxX + Mathf.Abs(_minX)) / 4f)
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _smoothSpeed);
    }
    public void ResetCamera()
    {
        transform.position = new Vector3(_startX, transform.position.y, transform.position.z);
    }
}