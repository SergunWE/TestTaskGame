using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Skin", menuName = "SOData/PlayerSkin")]
public class PlayerSkinSo : ScriptableObject
{
    public bool Purchased;
    [field: SerializeField] public Sprite MenuSprite { get; private set; }
    [field: SerializeField] public Sprite PlayerSprite { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Purchased = false;
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
}
