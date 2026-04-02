#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NpcRoute))]
public class NpcRouteDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        
        float halfWidth = position.width / 2f;
        Rect spawnRect = new Rect(position.x, position.y, halfWidth - 2f, position.height);
        Rect endRect = new Rect(position.x + halfWidth + 2f, position.y, halfWidth - 2f, position.height);
        
        EditorGUI.PropertyField(spawnRect, property.FindPropertyRelative("spawnPoint"), GUIContent.none);
        EditorGUI.PropertyField(endRect, property.FindPropertyRelative("endPoint"), GUIContent.none);
        
        EditorGUI.indentLevel = indent;
        
        EditorGUI.EndProperty();
    }
}
#endif