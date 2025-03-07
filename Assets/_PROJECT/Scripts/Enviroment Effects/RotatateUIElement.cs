using UnityEngine;

public class RotatateUIElement : MonoBehaviour
{
    // Скорость вращения (в градусах в секунду)
    public float rotationSpeed = 50f;

    private RectTransform rectTransform;

    void Start()
    {
        // Получаем RectTransform текущего объекта
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Вращаем UI-элемент вокруг оси Z (по умолчанию для UI)
        rectTransform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
