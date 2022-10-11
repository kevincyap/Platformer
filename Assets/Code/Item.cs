using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public string ID;
    public string itemName;
    public string description;
    public Sprite sprite;
    public AudioClip audioClip;
}
