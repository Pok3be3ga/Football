using UnityEngine;
using UnityEngine.UI;

public class BallShadow : MonoBehaviour
{
    [SerializeField] private BallBounce _ball;
    private RectTransform _transform;
    private Image _image;
    private float _Ypos;
    private Quaternion _rotation;
    private float _imageScale;
    void Start()
    {
        _image = GetComponentInChildren<Image>();
        _transform = GetComponent<RectTransform>();
        _Ypos = _transform.position.y;
        _rotation = _transform.rotation;
    }
    void Update()
    {
        _transform.position = new Vector3(_ball.transform.position.x, _Ypos, _ball.transform.position.z);
        _transform.rotation = _rotation;

        _imageScale = Mathf.Lerp(0.7f, 0.4f, Vector3.Distance(_transform.position, _ball.transform.position) / 4f);
        _image.rectTransform.localScale = new Vector3(_imageScale, _imageScale, _imageScale);
    }
}
