// Version 1.5.3
// �2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using System;
using PrimitivesPro.Primitives;
using UnityEngine;
using System.Collections;

namespace PrimitivesPro.GameObjects
{
    /// <summary>
    /// class for creating Cylinder primitive
    /// </summary>
    public class Cylinder : BaseObject
    {
        /// <summary>
        /// radius of the cylinder
        /// </summary>
        public float radius;

        /// <summary>
        /// height of the cylinder
        /// </summary>
        public float height;

        /// <summary>
        /// number of triangle segments in radius direction
        /// </summary>
        public int sides;

        /// <summary>
        /// number of triangle segments in height direction
        /// </summary>
        public int heightSegments;

        /// <summary>
        /// create Cylinder game object
        /// </summary>
        /// <param name="radius">radius of cylinder</param>
        /// <param name="height">height of cylinder</param>
        /// <param name="sides">number of triangle segments in radius direction</param>
        /// <param name="heightSegments">number of triangle segments in height direction</param>
        /// <returns>Cylinder class with Cylinder game object</returns>
        /// <param name="pivotPosition">position of the model pivot</param>
        /// <param name="normals">type of normals to be generated</param>
        public static Cylinder Create(float radius, float height, int sides, int heightSegments, NormalsType normals, PivotPosition pivotPosition)
        {
            var cylinderObject = new GameObject("CylinderPro");

            cylinderObject.AddComponent<MeshFilter>();
            var renderer = cylinderObject.AddComponent<MeshRenderer>();

            renderer.sharedMaterial = new Material(Shader.Find("Diffuse"));

            var cylinder = cylinderObject.AddComponent<Cylinder>();
            cylinder.GenerateGeometry(radius, height, sides, heightSegments, normals, pivotPosition);

            return cylinder;
        }

        /// <summary>
        /// re/generate mesh geometry based on parameters
        /// </summary>
        /// <param name="radius">radius of cylinder</param>
        /// <param name="height">height of cylinder</param>
        /// <param name="sides">number of triangle segments in radius</param>
        /// <param name="heightSegments">number of triangle segments in height</param>
        /// <param name="pivotPosition">position of the model pivot</param>
        /// <param name="normalsType">type of normals to be generated</param>
        public void GenerateGeometry(float radius, float height, int sides, int heightSegments, NormalsType normalsType, PivotPosition pivotPosition)
        {
            // generate new mesh and clear old one
            var meshFilter = GetComponent<MeshFilter>();

            if (meshFilter.sharedMesh == null)
            {
                meshFilter.sharedMesh = new Mesh();
            }

            var mesh = meshFilter.sharedMesh;

            // generate cone with same radiuses
            GenerationTimeMS = Primitives.ConePrimitive.GenerateGeometry(mesh, radius, radius, height, sides, heightSegments, normalsType, pivotPosition);

            this.radius = radius;
            this.height = height;
            this.sides = sides;
            this.heightSegments = heightSegments;
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

                Primitives.ConePrimitive.GenerateGeometry(meshCollider.sharedMesh, radius, radius, height, sides, heightSegments, normalsType, pivotPosition);

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
