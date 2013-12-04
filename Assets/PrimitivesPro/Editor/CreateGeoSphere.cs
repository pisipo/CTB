// Version 1.5.3
// ©2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using PrimitivesPro.Editor;
using PrimitivesPro.Primitives;
using UnityEditor;

[CustomEditor(typeof(PrimitivesPro.GameObjects.GeoSphere))]
public class CreateGeoSphere : Editor
{
    private bool useFlipNormals = false;

    [MenuItem(MenuDefinition.GeoSphere)]
    static void Create()
    {
        var obj = PrimitivesPro.GameObjects.GeoSphere.Create(1, 2, GeoSpherePrimitive.BaseType.Icosahedron, NormalsType.Vertex, PivotPosition.Center);
        obj.SaveStateAll();

        Selection.activeGameObject = obj.gameObject;
    }

    public override void OnInspectorGUI()
    {
        var obj = Selection.activeGameObject.GetComponent<PrimitivesPro.GameObjects.GeoSphere>();

        if (target != obj)
        {
            return;
        }

        bool colliderChange = Utils.MeshColliderSelection(obj);

        EditorGUILayout.Separator();

        useFlipNormals = obj.flipNormals;
        bool uiChange = false;

        uiChange |= Utils.SliderEdit("Radius", 0, 100, ref obj.radius);
        uiChange |= Utils.SliderEdit("Subdivision", 0, 6, ref obj.subdivision);

        var oldBaseType = obj.baseType;
        EditorGUILayout.BeginHorizontal();
        obj.baseType = (GeoSpherePrimitive.BaseType)EditorGUILayout.EnumPopup("Base type", obj.baseType);
        EditorGUILayout.EndHorizontal();
        uiChange |= oldBaseType != obj.baseType;

        EditorGUILayout.Separator();

        uiChange |= Utils.NormalsType(ref obj.normalsType);
        uiChange |= Utils.PivotPosition(ref obj.pivotPosition);

        uiChange |= Utils.Toggle("Flip normals", ref useFlipNormals);
        uiChange |= Utils.Toggle("Share material", ref obj.shareMaterial);

        Utils.StatWindow(Selection.activeGameObject);

        Utils.SaveMesh<PrimitivesPro.GameObjects.GeoSphere>(this);

        Utils.SavePrefab<PrimitivesPro.GameObjects.GeoSphere>(this);

        Utils.DuplicateObject<PrimitivesPro.GameObjects.GeoSphere>(this);

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
