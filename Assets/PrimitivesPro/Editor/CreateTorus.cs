// Version 1.5.3
// ©2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using PrimitivesPro.Editor;
using PrimitivesPro.Primitives;
using UnityEditor;

[CustomEditor(typeof(PrimitivesPro.GameObjects.Torus))]
public class CreateTorus : Editor
{
    private bool useFlipNormals = false;

    [MenuItem(MenuDefinition.Torus)]
    static void Create()
    {
        var obj = PrimitivesPro.GameObjects.Torus.Create(1, 0.5f, 20, 20, NormalsType.Vertex, PivotPosition.Center);
        obj.SaveStateAll();

        Selection.activeGameObject = obj.gameObject;
    }

    public override void OnInspectorGUI()
    {
        var obj = Selection.activeGameObject.GetComponent<PrimitivesPro.GameObjects.Torus>();

        if (target != obj)
        {
            return;
        }

        bool colliderChange = Utils.MeshColliderSelection(obj);

        EditorGUILayout.Separator();

        useFlipNormals = obj.flipNormals;
        bool uiChange = false;

        uiChange |= Utils.SliderEdit("Torus radius", 0, 100, ref obj.radius0);
        uiChange |= Utils.SliderEdit("Cone radius", 0, 100, ref obj.radius1);

        EditorGUILayout.Separator();

        uiChange |= Utils.SliderEdit("Torus segments", 3, 250, ref obj.torusSegments);
        uiChange |= Utils.SliderEdit("Cone segments", 3, 100, ref obj.coneSegments);

        EditorGUILayout.Separator();

        uiChange |= Utils.NormalsType(ref obj.normalsType);
        uiChange |= Utils.PivotPosition(ref obj.pivotPosition);

        uiChange |= Utils.Toggle("Flip normals", ref useFlipNormals);
        uiChange |= Utils.Toggle("Share material", ref obj.shareMaterial);

        Utils.StatWindow(Selection.activeGameObject);

        Utils.SaveMesh<PrimitivesPro.GameObjects.Torus>(this);

        Utils.SavePrefab<PrimitivesPro.GameObjects.Torus>(this);

        Utils.DuplicateObject<PrimitivesPro.GameObjects.Torus>(this);

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
