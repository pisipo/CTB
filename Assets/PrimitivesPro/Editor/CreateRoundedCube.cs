// Version 1.5.3
// �2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using PrimitivesPro.Editor;
using PrimitivesPro.Primitives;
using UnityEditor;

[CustomEditor(typeof(PrimitivesPro.GameObjects.RoundedCube))]
public class CreateRoundedCube : Editor
{
    private bool useCube = false;
    private bool useFlipNormals = false;

    [MenuItem(MenuDefinition.RoundedCube)]
    static void Create()
    {
        var obj = PrimitivesPro.GameObjects.RoundedCube.Create(1, 1, 1, 20, 0.5f, NormalsType.Vertex, PivotPosition.Center);
        obj.SaveStateAll();

        Selection.activeGameObject = obj.gameObject;
    }

    public override void OnInspectorGUI()
    {
        var obj = Selection.activeGameObject.GetComponent<PrimitivesPro.GameObjects.RoundedCube>();

        if (target != obj)
        {
            return;
        }

        bool colliderChange = Utils.MeshColliderSelection(obj);

        EditorGUILayout.Separator();

        useFlipNormals = obj.flipNormals;
        bool uiChange = false;

        var oldwidth = obj.width;
        var oldLength = obj.height;

        uiChange |= Utils.SliderEdit("Width", 0, 100, ref obj.width);
        uiChange |= Utils.SliderEdit("Height", 0, 100, ref obj.height);
        uiChange |= Utils.SliderEdit("Depth", 0, 100, ref obj.length);
        uiChange |= Utils.Toggle("Cube", ref useCube);
        EditorGUILayout.Separator();

        if (useCube)
        {
            if (oldwidth != obj.width)
            {
                obj.length = obj.height = obj.width;
            }
            else if (oldLength != obj.height)
            {
                obj.width = obj.length = obj.height;
            }
            else
            {
                obj.width = obj.height = obj.length;
            }
        }

        uiChange |= Utils.SliderEdit("Segments", 0, 100, ref obj.segments);
        uiChange |= Utils.SliderEdit("Roudness", 0, 1, ref obj.roundness);

        EditorGUILayout.Separator();

        uiChange |= Utils.NormalsType(ref obj.normalsType);
        uiChange |= Utils.PivotPosition(ref obj.pivotPosition);

        uiChange |= Utils.Toggle("Flip normals", ref useFlipNormals);
        uiChange |= Utils.Toggle("Share material", ref obj.shareMaterial);

        Utils.StatWindow(Selection.activeGameObject);

        Utils.SaveMesh<PrimitivesPro.GameObjects.RoundedCube>(this);

        Utils.SavePrefab<PrimitivesPro.GameObjects.RoundedCube>(this);

        Utils.DuplicateObject<PrimitivesPro.GameObjects.RoundedCube>(this);

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
