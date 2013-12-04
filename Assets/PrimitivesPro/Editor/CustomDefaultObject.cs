// Version 1.5.3
// ©2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using System;
using PrimitivesPro.Editor;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PrimitivesPro.GameObjects.DefaultObject))]
public class CustomDefaultObject : Editor
{
    private bool useFlipNormals;

    public override void OnInspectorGUI()
    {
        var obj = Selection.activeGameObject.GetComponent<PrimitivesPro.GameObjects.DefaultObject>();

        if (target != obj)
        {
            return;
        }

        useFlipNormals = obj.flipNormals;
        bool uiChange = false;

        uiChange |= Utils.Toggle("Flip normals", ref useFlipNormals);
        uiChange |= Utils.Toggle("Share material", ref obj.shareMaterial);

        Utils.StatWindow(Selection.activeGameObject);

        Utils.SaveMesh<PrimitivesPro.GameObjects.DefaultObject>(this);

        Utils.SavePrefab<PrimitivesPro.GameObjects.DefaultObject>(this);

        Utils.DuplicateObject<PrimitivesPro.GameObjects.DefaultObject>(this);

        if (uiChange)
        {
            if (useFlipNormals)
            {
                obj.FlipNormals();
            }
        }
    }
}
