using UnityEngine;

public class RotateElement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private Vector3 _rotateAngular;

    private Transform _transform;

    void Start()
    {
        // Получаем RectTransform текущего объекта
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        // Вращаем UI-элемент вокруг оси Z (по умолчанию для UI)
        _transform.Rotate(_rotateAngular * rotationSpeed * Time.deltaTime);
    }
}
