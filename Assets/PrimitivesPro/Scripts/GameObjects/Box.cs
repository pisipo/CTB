// Version 1.5.3
// ©2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using PrimitivesPro.Primitives;
using UnityEngine;

namespace PrimitivesPro.GameObjects
{
    /// <summary>
    /// class for creating Box primitive
    /// </summary>
    public class Box : BaseObject
    {
        /// <summary>
        /// width of the cube
        /// </summary>
        public float width;

        /// <summary>
        /// height of the cube
        /// </summary>
        public float height;

        /// <summary>
        /// depth of the cube
        /// </summary>
        public float depth;

        /// <summary>
        /// number of triangle segments in width direction
        /// </summary>
        public int widthSegments;

        /// <summary>
        /// number of triangle segments in height direction
        /// </summary>
        public int heightSegments;

        /// <summary>
        /// number of triangle segments in depth direction
        /// </summary>
        public int depthSegments;

        /// <summary>
        /// flag whether to generate uv mapping for 6-side cube map
        /// </summary>
        public bool cubeMap;

        /// <summary>
        /// create Box game object
        /// </summary>
        /// <param name="width">width of cube</param>
        /// <param name="height">height of cube</param>
        /// <param name="depth">depth of cube</param>
        /// <param name="widthSegments">number of triangle segments in width direction</param>
        /// <param name="heightSegments">number of triangle segments in height direction</param>
        /// <param name="depthSegments">number of triangle segments in depth direction</param>
        /// <param name="pivot">position of the model pivot</param>
        /// <param name="cubeMap">enable 6-sides cube map uv mapping</param>
        /// <returns>Box class with Box game object</returns>
        public static Box Create(float width, float height, float depth, int widthSegments, int heightSegments, int depthSegments, bool cubeMap, PivotPosition pivot)
        {
            var planeObject = new GameObject("BoxPro");

            planeObject.AddComponent<MeshFilter>();
            var renderer = planeObject.AddComponent<MeshRenderer>();

            renderer.sharedMaterial = new Material(Shader.Find("Diffuse"));

            var cube = planeObject.AddComponent<Box>();
            cube.GenerateGeometry(width, height, depth, widthSegments, heightSegments, depthSegments, cubeMap, pivot);

            return cube;
        }

        /// <summary>
        /// re/generate mesh geometry based on parameters
        /// </summary>
        /// <param name="width">width of cube</param>
        /// <param name="height">height of cube</param>
        /// <param name="depth">depth of cube</param>
        /// <param name="widthSegments">number of triangle segments in width direction</param>
        /// <param name="heightSegments">number of triangle segments in height direction</param>
        /// <param name="depthSegments">number of triangle segments in depth direction</param>
        /// <param name="cubeMap">enable 6-sides cube map uv mapping</param>
        /// <param name="pivot">position of the model pivot</param>
        public void GenerateGeometry(float width, float height, float depth, int widthSegments, int heightSegments, int depthSegments, bool cubeMap, PivotPosition pivot)
        {
            // generate new mesh and clear old one
            var meshFilter = GetComponent<MeshFilter>();

            if (meshFilter.sharedMesh == null)
            {
                meshFilter.sharedMesh = new Mesh();
            }

            var mesh = meshFilter.sharedMesh;
            mesh.Clear();

            // generate geometry
            GenerationTimeMS = Primitives.BoxPrimitive.GenerateGeometry(mesh, width, height, depth, widthSegments, heightSegments, depthSegments, cubeMap, flipUVMapping, pivot);

            this.width = width;
            this.height = height;
            this.depth = depth;
            this.widthSegments = widthSegments;
            this.heightSegments = heightSegments;
            this.depthSegments = depthSegments;
            this.cubeMap = cubeMap;
            this.flipNormals = false;
            this.pivotPosition = pivot;
        }

        /// <summary>
        /// regenerate mesh geometry with class variables
        /// </summary>
        public override void GenerateGeometry()
        {
            GenerateGeometry(width, height, depth, widthSegments, heightSegments, depthSegments, cubeMap, pivotPosition);
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

                Primitives.BoxPrimitive.GenerateGeometry(meshCollider.sharedMesh, width, height, depth, widthSegments, heightSegments, depthSegments, cubeMap, flipUVMapping, pivotPosition);

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
            dic["depth"] = depth;
            dic["widthSegments"] = widthSegments;
            dic["heightSegments"] = heightSegments;
            dic["depthSegments"] = depthSegments;

            return dic;
        }

        public override System.Collections.Generic.Dictionary<string, object> LoadState(bool collision)
        {
            var dic = base.LoadState(collision);

            width = (float)dic["width"];
            height = (float)dic["height"];
            depth = (float)dic["depth"];
            widthSegments = (int)dic["widthSegments"];
            heightSegments = (int)dic["heightSegments"];
            depthSegments = (int)dic["depthSegments"];

            return dic;
        }
    }
}
