// Version 1.5.3
// ©2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using PrimitivesPro.Editor;
using PrimitivesPro.Primitives;
using UnityEditor;

[CustomEditor(typeof(PrimitivesPro.GameObjects.Ellipsoid))]
public class CreateEllipsoid : Editor
{
    private bool useFlipNormals = false;

    [MenuItem(MenuDefinition.Ellipsoid)]
    static void Create()
    {
        var obj = PrimitivesPro.GameObjects.Ellipsoid.Create(1, 2, 1, 20, NormalsType.Vertex, PivotPosition.Center);
        obj.SaveStateAll();

        Selection.activeGameObject = obj.gameObject;
    }

    public override void OnInspectorGUI()
    {
        var obj = Selection.activeGameObject.GetComponent<PrimitivesPro.GameObjects.Ellipsoid>();

        if (target != obj)
        {
            return;
        }

        bool colliderChange = Utils.MeshColliderSelection(obj);

        EditorGUILayout.Separator();

        useFlipNormals = obj.flipNormals;
        bool uiChange = false;

        uiChange |= Utils.SliderEdit("Width", 0, 100, ref obj.width);
        uiChange |= Utils.SliderEdit("Height", 0, 100, ref obj.height);
        uiChange |= Utils.SliderEdit("Length", 0, 100, ref obj.length);

        EditorGUILayout.Separator();

        uiChange |= Utils.SliderEdit("Segments", 4, 100, ref obj.segments);

        EditorGUILayout.Separator();

        uiChange |= Utils.NormalsType(ref obj.normalsType);
        uiChange |= Utils.PivotPosition(ref obj.pivotPosition);

        uiChange |= Utils.Toggle("Flip normals", ref useFlipNormals);
        uiChange |= Utils.Toggle("Share material", ref obj.shareMaterial);

        Utils.StatWindow(Selection.activeGameObject);

        Utils.SaveMesh<PrimitivesPro.GameObjects.Ellipsoid>(this);

        Utils.SavePrefab<PrimitivesPro.GameObjects.Ellipsoid>(this);

        Utils.DuplicateObject<PrimitivesPro.GameObjects.Ellipsoid>(this);

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
