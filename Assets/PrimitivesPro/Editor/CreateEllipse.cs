// Version 1.5.3
// ©2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using PrimitivesPro.Editor;
using UnityEditor;

[CustomEditor(typeof(PrimitivesPro.GameObjects.Ellipse))]
public class CreateEllipse : Editor
{
    private bool useFlipNormals = false;

    [MenuItem(MenuDefinition.Ellipse)]
    static void Create()
    {
        var obj = PrimitivesPro.GameObjects.Ellipse.Create(1, 2, 20);
        obj.SaveStateAll();

        Selection.activeGameObject = obj.gameObject;
    }

    public override void OnInspectorGUI()
    {
        var obj = Selection.activeGameObject.GetComponent<PrimitivesPro.GameObjects.Ellipse>();

        if (target != obj)
        {
            return;
        }

        bool colliderChange = Utils.MeshColliderSelection(obj);

        EditorGUILayout.Separator();

        useFlipNormals = obj.flipNormals;
        bool uiChange = false;

        uiChange |= Utils.SliderEdit("Radius0", 0, 100, ref obj.radius0);
        uiChange |= Utils.SliderEdit("Radius1", 0, 100, ref obj.radius1);

        EditorGUILayout.Separator();

        uiChange |= Utils.SliderEdit("Segments", 3, 100, ref obj.segments);

        EditorGUILayout.Separator();

        uiChange |= Utils.Toggle("Flip normals", ref useFlipNormals);
        uiChange |= Utils.Toggle("Share material", ref obj.shareMaterial);

        Utils.StatWindow(Selection.activeGameObject);

        Utils.SaveMesh<PrimitivesPro.GameObjects.Ellipse>(this);

        Utils.SavePrefab<PrimitivesPro.GameObjects.Ellipse>(this);

        Utils.DuplicateObject<PrimitivesPro.GameObjects.Ellipse>(this);

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
