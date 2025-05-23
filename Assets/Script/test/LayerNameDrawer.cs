#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LayerNameAttribute))]
public class LayerNameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string[] layers = GetAllLayerNames();
        int index = Mathf.Max(0, System.Array.IndexOf(layers, property.stringValue));
        index = EditorGUI.Popup(position, label.text, index, layers);
        property.stringValue = layers[index];
    }

    private string[] GetAllLayerNames()
    {
        var layers = new System.Collections.Generic.List<string>();
        for (int i = 0; i < 32; i++)
        {
            string layerName = LayerMask.LayerToName(i);
            if (!string.IsNullOrEmpty(layerName))
                layers.Add(layerName);
        }
        return layers.ToArray();
    }
}
#endif
