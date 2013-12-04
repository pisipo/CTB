// Version 1.5.3
// ©2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using PrimitivesPro.Editor;
using UnityEditor;

[CustomEditor(typeof(PrimitivesPro.GameObjects.Circle))]
public class CreateCircle : Editor
{
    private bool useFlipNormals = false;

    [MenuItem(MenuDefinition.Circle)]
    static void Create()
    {
        var obj = PrimitivesPro.GameObjects.Circle.Create(1.0f, 20);
        obj.SaveStateAll();

        Selection.activeGameObject = obj.gameObject;
    }

    public override void OnInspectorGUI()
    {
        var obj = Selection.activeGameObject.GetComponent<PrimitivesPro.GameObjects.Circle>();

        if (target != obj)
        {
            return;
        }

        bool colliderChange = Utils.MeshColliderSelection(obj);

        EditorGUILayout.Separator();

        useFlipNormals = obj.flipNormals;
        bool uiChange = false;

        uiChange |= Utils.SliderEdit("Radius", 0, 100, ref obj.radius);
        uiChange |= Utils.SliderEdit("Segments", 3, 100, ref obj.segments);

        EditorGUILayout.Separator();

        uiChange |= Utils.Toggle("Flip normals", ref useFlipNormals);
        uiChange |= Utils.Toggle("Share material", ref obj.shareMaterial);

        Utils.StatWindow(Selection.activeGameObject);

        Utils.SaveMesh<PrimitivesPro.GameObjects.Circle>(this);

        Utils.SavePrefab<PrimitivesPro.GameObjects.Circle>(this);

        Utils.DuplicateObject<PrimitivesPro.GameObjects.Circle>(this);

        if (uiChange || colliderChange)
        {
            if (obj.generationMode == 0 && !colliderChange)
            {
                obj.GenerateGeometry();

                if (useFlipNormals)
                {
                    obj.FlipNormals();
                }
            }
            else
            {
                obj.GenerateColliderGeometry();
            }
        }
    }
}
