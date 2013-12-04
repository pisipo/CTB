// Version 1.5.3
// ©2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using System;
using PrimitivesPro.Primitives;
using UnityEngine;
using System.Collections;

namespace PrimitivesPro.GameObjects
{
    /// <summary>
    /// class for creating Capsule primitive
    /// </summary>
    public class Capsule : BaseObject
    {
        /// <summary>
        /// radius of the capsule
        /// </summary>
        public float radius;

        /// <summary>
        /// height of the capsule (height of the central cylinder)
        /// </summary>
        public float height;

        /// <summary>
        /// number of capsule sides
        /// </summary>
        public int sides;

        /// <summary>
        /// number of segments of central cylinder
        /// </summary>
        public int heightSegments;

        /// <summary>
        /// create Capsule game object
        /// </summary>
        /// <param name="radius">radius of capsule</param>
        /// <param name="sides">number of segments</param>
        /// <param name="heightSegments">number of segments of central cylinder</param>
        /// <param name="normals">type of normals to be generated</param>
        /// <param name="pivotPosition">position of the model pivot</param>
        /// <returns>Capsule class with Capsule game object</returns>
        public static Capsule Create(float radius, float height, int sides, int heightSegments, NormalsType normals, PivotPosition pivotPosition)
        {
            var capsuleObject = new GameObject("CapsulePro");

            capsuleObject.AddComponent<MeshFilter>();
            var renderer = capsuleObject.AddComponent<MeshRenderer>();

            renderer.sharedMaterial = new Material(Shader.Find("Diffuse"));

            var capsule = capsuleObject.AddComponent<Capsule>();
            capsule.GenerateGeometry(radius, height, sides, heightSegments, normals, pivotPosition);

            return capsule;
        }

        /// <summary>
        /// create Capsule game object
        /// </summary>
        /// <param name="radius">radius of capsule</param>
        /// <param name="sides">number of segments</param>
        /// <param name="normalsType">type of normals to be generated</param>
        /// <param name="heightSegments">number of segments of central cylinder</param>
        /// <param name="pivotPosition">position of the model pivot</param>
        public void GenerateGeometry(float radius, float height, int sides, int heightSegments, NormalsType normalsType, PivotPosition pivotPosition)
        {
            // generate new mesh and clear old one
            var meshFilter = GetComponent<MeshFilter>();

            if (meshFilter.sharedMesh == null)
            {
                meshFilter.sharedMesh = new Mesh();
            }

            var mesh = meshFilter.sharedMesh;

            // generate geometry
            GenerationTimeMS = Primitives.CapsulePrimitive.GenerateGeometry(mesh, radius, height, sides, heightSegments, normalsType, pivotPosition);

            this.radius = radius;
            this.height = height;
            this.heightSegments = heightSegments;
            this.sides = sides;
            this.normalsType = normalsType;
            this.flipNormals = false;
            this.pivotPosition = pivotPosition;
        }

        /// <summary>
        /// regenerate mesh geometry with class variables
        /// </summary>
        public override void GenerateGeometry()
        {
            GenerateGeometry(radius, height, sides, heightSegments, normalsType, pivotPosition);
        }

        public override void GenerateColliderGeometry()
        {
            var meshCollider = GetComponent<MeshCollider>();
            var meshFilter = GetComponent<MeshFilter>();

            if (meshCollider && meshFilter)
            {
                // create new mesh different from visual mesh
                if (meshCollider.sharedMesh == meshFilter.sharedMesh)
                {
                    meshCollider.sharedMesh = new Mesh();
                }

                meshCollider.sharedMesh.Clear();

                Primitives.CapsulePrimitive.GenerateGeometry(meshCollider.sharedMesh, radius, height, sides, heightSegments, normalsType, pivotPosition);

                meshCollider.enabled = false;
                meshCollider.enabled = true;
            }

            base.GenerateColliderGeometry();
        }

        public override System.Collections.Generic.Dictionary<string, object> SaveState(bool collision)
        {
            var dic = base.SaveState(collision);

            dic["radius"] = radius;
            dic["height"] = height;
            dic["sides"] = sides;
            dic["heightSegments"] = heightSegments;

            return dic;
        }

        public override System.Collections.Generic.Dictionary<string, object> LoadState(bool collision)
        {
            var dic = base.LoadState(collision);

            radius = (float)dic["radius"];
            height = (float)dic["height"];
            sides = (int)dic["sides"];
            heightSegments = (int)dic["heightSegments"];

            return dic;
        }
    }
}
