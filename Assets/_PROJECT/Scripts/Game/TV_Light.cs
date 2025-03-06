using UnityEngine;

public class TV_Light: MonoBehaviour
{
    public Light Light;
    Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.cyan, Color.magenta };
    private void OnTriggerEnter(Collider other)
    { 
        Debug.Log(other.gameObject.tag);

            Light.color = colors[Random.Range(0, colors.Length)];

    }
}
