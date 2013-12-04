// Version 1.5.3
// ©2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using PrimitivesPro.Editor;
using PrimitivesPro.Primitives;
using UnityEditor;

[CustomEditor(typeof(PrimitivesPro.GameObjects.Sphere))]
public class CreateSphere : Editor
{
    private bool useFlipNormals = false;

    [MenuItem(MenuDefinition.Sphere)]
    static void Create()
    {
        var obj = PrimitivesPro.GameObjects.Sphere.Create(1, 20, 0, 0, NormalsType.Vertex, PivotPosition.Center);
        obj.SaveStateAll();

        Selection.activeGameObject = obj.gameObject;
    }

    public override void OnInspectorGUI()
    {
        var obj = Selection.activeGameObject.GetComponent<PrimitivesPro.GameObjects.Sphere>();

        if (target != obj)
        {
            return;
        }

        bool colliderChange = Utils.MeshColliderSelection(obj);

        EditorGUILayout.Separator();

        useFlipNormals = obj.flipNormals;
        bool uiChange = false;

        uiChange |= Utils.SliderEdit("Radius", 0, 100, ref obj.radius);
        uiChange |= Utils.SliderEdit("Segments", 4, 100, ref obj.segments);
        uiChange |= Utils.SliderEdit("Hemisphere", 0, 1, ref obj.hemisphere);
        uiChange |= Utils.SliderEdit("Inner radius", 0, 100, ref obj.innerRadius);

        if (obj.innerRadius > obj.radius)
        {
            obj.innerRadius = obj.radius;
        }

        EditorGUILayout.Separator();

        uiChange |= Utils.NormalsType(ref obj.normalsType);
        uiChange |= Utils.PivotPosition(ref obj.pivotPosition);

        uiChange |= Utils.Toggle("Flip normals", ref useFlipNormals);
        uiChange |= Utils.Toggle("Share material", ref obj.shareMaterial);

        Utils.StatWindow(Selection.activeGameObject);

        Utils.SaveMesh<PrimitivesPro.GameObjects.Sphere>(this);

        Utils.SavePrefab<PrimitivesPro.GameObjects.Sphere>(this);

        Utils.DuplicateObject<PrimitivesPro.GameObjects.Sphere>(this);

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
