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
    /// class for creating Ring primitive
    /// </summary>
    public class Ring : BaseObject
    {
        /// <summary>
        /// radius0 of the ring
        /// </summary>
        public float radius0;

        /// <summary>
        /// radius1 of the ring
        /// </summary>
        public float radius1;

        /// <summary>
        /// number of spere segments
        /// </summary>
        public int segments;

        /// <summary>
        /// create Ring game object
        /// </summary>
        /// <param name="radius0">radius0 of ring</param>
        /// <param name="radius1">radius1 of ring</param>
        /// <param name="segments">number of segments</param>
        /// <returns>Ring game object</returns>
        public static Ring Create(float radius0, float radius1, int segments)
        {
            var sphereObject = new GameObject("RingPro");

            sphereObject.AddComponent<MeshFilter>();
            var renderer = sphereObject.AddComponent<MeshRenderer>();

            renderer.sharedMaterial = new Material(Shader.Find("Diffuse"));

            var ring = sphereObject.AddComponent<Ring>();
            ring.GenerateGeometry(radius0, radius1, segments);

            return ring;
        }

        /// <summary>
        /// create Ring game object
        /// </summary>
        /// <param name="radius0">radius0 of ring</param>
        /// <param name="radius1">radiu1 of ring</param>
        /// <param name="segments">number of segments</param>
        public void GenerateGeometry(float radius0, float radius1, int segments)
        {
            // generate new mesh and clear old one
            var meshFilter = GetComponent<MeshFilter>();

            if (meshFilter.sharedMesh == null)
            {
                meshFilter.sharedMesh = new Mesh();
            }

            var mesh = meshFilter.sharedMesh;

            // generate geometry
            GenerationTimeMS = Primitives.RingPrimitive.GenerateGeometry(mesh, radius0, radius1, segments);

            this.radius0 = radius0;
            this.radius1 = radius1;
            this.segments = segments;
            this.flipNormals = false;
        }

        /// <summary>
        /// regenerate mesh geometry with class variables
        /// </summary>
        public override void GenerateGeometry()
        {
            GenerateGeometry(radius0, radius1, segments);
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

                Primitives.RingPrimitive.GenerateGeometry(meshCollider.sharedMesh, radius0, radius1, segments);

                meshCollider.enabled = false;
                meshCollider.enabled = true;
            }

            base.GenerateColliderGeometry();
        }

        public override System.Collections.Generic.Dictionary<string, object> SaveState(bool collision)
        {
            var dic = base.SaveState(collision);

            dic["radius0"] = radius0;
            dic["radius1"] = radius1;
            dic["segments"] = segments;

            return dic;
        }

        public override System.Collections.Generic.Dictionary<string, object> LoadState(bool collision)
        {
            var dic = base.LoadState(collision);

            radius0 = (float)dic["radius0"];
            radius1 = (float)dic["radius1"];
            segments = (int)dic["segments"];

            return dic;
        }
    }
}
