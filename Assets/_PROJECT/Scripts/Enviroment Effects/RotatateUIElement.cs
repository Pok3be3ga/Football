using UnityEngine;

public class RotatateUIElement : MonoBehaviour
{
    // �������� �������� (� �������� � �������)
    public float rotationSpeed = 50f;

    private RectTransform rectTransform;

    void Start()
    {
        // �������� RectTransform �������� �������
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // ������� UI-������� ������ ��� Z (�� ��������� ��� UI)
        rectTransform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
