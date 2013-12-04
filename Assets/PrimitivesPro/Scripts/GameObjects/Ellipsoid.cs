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
    /// class for creating Ellipsoid primitive
    /// </summary>
    public class Ellipsoid : BaseObject
    {
        /// <summary>
        /// width of the ellipsoid
        /// </summary>
        public float width;

        /// <summary>
        /// height of the ellipsoid
        /// </summary>
        public float height;

        /// <summary>
        /// length of the ellipsoid
        /// </summary>
        public float length;

        /// <summary>
        /// number of spere segments
        /// </summary>
        public int segments;

        /// <summary>
        /// create Ellipsoid game object
        /// </summary>
        /// <param name="width">width of ellipsoid</param>
        /// <param name="height">height of ellipsoid</param>
        /// <param name="length">length of ellipsoid</param>
        /// <param name="segments">number of segments</param>
        /// <param name="normals">type of normals to be generated</param>
        /// <param name="pivotPosition">position of the model pivot</param>
        /// <returns>Ellipsoid class with Ellipsoid game object</returns>
        public static Ellipsoid Create(float width, float height, float length, int segments, NormalsType normals, PivotPosition pivotPosition)
        {
            var sphereObject = new GameObject("EllipsoidPro");

            sphereObject.AddComponent<MeshFilter>();
            var renderer = sphereObject.AddComponent<MeshRenderer>();

            renderer.sharedMaterial = new Material(Shader.Find("Diffuse"));

            var ellipsoid = sphereObject.AddComponent<Ellipsoid>();
            ellipsoid.GenerateGeometry(width, height, length, segments, normals, pivotPosition);

            return ellipsoid;
        }

        /// <summary>
        /// create Ellipsoid game object
        /// </summary>
        /// <param name="width">width of ellipsoid</param>
        /// <param name="height">height of ellipsoid</param>
        /// <param name="length">length of ellipsoid</param>
        /// <param name="segments">number of segments</param>
        /// <param name="normalsType">type of normals to be generated</param>
        /// <param name="pivotPosition">position of the model pivot</param>
        public void GenerateGeometry(float width, float height, float length, int segments, NormalsType normalsType, PivotPosition pivotPosition)
        {
            // generate new mesh and clear old one
            var meshFilter = GetComponent<MeshFilter>();

            if (meshFilter.sharedMesh == null)
            {
                meshFilter.sharedMesh = new Mesh();
            }

            var mesh = meshFilter.sharedMesh;

            // generate geometry
            GenerationTimeMS = Primitives.EllipsoidPrimitive.GenerateGeometry(mesh, width, height, length, segments, normalsType, pivotPosition);

            this.width = width;
            this.height = height;
            this.length = length;
            this.segments = segments;
            this.normalsType = normalsType;
            this.flipNormals = false;
            this.pivotPosition = pivotPosition;
        }

        /// <summary>
        /// regenerate mesh geometry with class variables
        /// </summary>
        public override void GenerateGeometry()
        {
            GenerateGeometry(width, height, length, segments, normalsType, pivotPosition);
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

                Primitives.EllipsoidPrimitive.GenerateGeometry(meshCollider.sharedMesh, width, height, length, segments, normalsType, pivotPosition);

                meshCollider.enabled = false;
                meshCollider.enabled = true;
            }

            base.GenerateColliderGeometry();
        }

        public override System.Collections.Generic.Dictionary<string, object> SaveState(bool collision)
        {
            var dic = base.SaveState(collision);

            dic["width"] = width;
            dic["height"] = height;
            dic["length"] = length;
            dic["segments"] = segments;

            return dic;
        }

        public override System.Collections.Generic.Dictionary<string, object> LoadState(bool collision)
        {
            var dic = base.LoadState(collision);

            width = (float)dic["width"];
            height = (float)dic["height"];
            length = (float)dic["length"];
            segments = (int)dic["segments"];

            return dic;
        }
    }
}
