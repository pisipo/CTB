// Version 1.5.3
// ©2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using PrimitivesPro.Editor;
using PrimitivesPro.Primitives;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PrimitivesPro.GameObjects.Triangle))]
public class CreateTriangle : Editor
{
    private bool useFlipNormals;

    [MenuItem(MenuDefinition.Triangle)]
    static void Create()
    {
        var obj = PrimitivesPro.GameObjects.Triangle.Create(1, 1);
        obj.SaveStateAll();

        Selection.activeGameObject = obj.gameObject;
    }

    public override void OnInspectorGUI()
    {
        var obj = Selection.activeGameObject.GetComponent<PrimitivesPro.GameObjects.Triangle>();

        if (target != obj)
        {
            return;
        }

        bool colliderChange = Utils.MeshColliderSelection(obj);

        EditorGUILayout.Separator();

        useFlipNormals = obj.flipNormals;
        bool uiChange = false;

        uiChange |= Utils.SliderEdit("Side Length", 0, 100, ref obj.side);

        uiChange |= Utils.SliderEdit("Subdivision", 0, 7, ref obj.subdivision);

        EditorGUILayout.Separator();

        uiChange |= Utils.Toggle("Flip normals", ref useFlipNormals);
        uiChange |= Utils.Toggle("Share material", ref obj.shareMaterial);

        Utils.StatWindow(Selection.activeGameObject);

        Utils.SaveMesh<PrimitivesPro.GameObjects.Triangle>(this);

        Utils.SavePrefab<PrimitivesPro.GameObjects.Triangle>(this);

        Utils.DuplicateObject<PrimitivesPro.GameObjects.Triangle>(this);

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
