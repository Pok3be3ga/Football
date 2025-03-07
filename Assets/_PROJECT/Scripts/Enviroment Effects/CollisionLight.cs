using UnityEngine;
using UnityEngine.Rendering;

public class CollisionLight : MonoBehaviour
{
    private Material _mat;
    private bool _onOff;
    void Start()
    {
        _mat = GetComponent<MeshRenderer>().material;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) OnOFF();
    }
    [ContextMenu("On")]
    public void OnOFF()
    {
        if(_mat == null) _mat = GetComponent<MeshRenderer>().material;
        if (_onOff)
        {
            _mat.DisableKeyword("_EMISSION");
            _onOff = false;
        }
        else
        {
            _mat.EnableKeyword("_EMISSION");
            _onOff = true;
        }

    }
}
