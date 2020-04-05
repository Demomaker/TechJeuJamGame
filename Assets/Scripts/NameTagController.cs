using TMPro;
using UnityEngine;

public class NameTagController : MonoBehaviour 
{
    private TextMeshPro nameTag;
    private string name;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        nameTag = GetComponent<TextMeshPro>();
        name = transform.parent.gameObject.name;
        nameTag.text = name;
    }
}