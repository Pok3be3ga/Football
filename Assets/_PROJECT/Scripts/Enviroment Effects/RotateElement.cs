using UnityEngine;

public class RotateElement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private Vector3 _rotateAngular;

    private Transform _transform;

    void Start()
    {
        // �������� RectTransform �������� �������
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        // ������� UI-������� ������ ��� Z (�� ��������� ��� UI)
        _transform.Rotate(_rotateAngular * rotationSpeed * Time.deltaTime);
    }
}
