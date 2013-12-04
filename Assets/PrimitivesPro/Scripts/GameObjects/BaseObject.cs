// Version 1.5.3
// ©2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using System.Collections.Generic;
using PrimitivesPro.Primitives;
using UnityEngine;

namespace PrimitivesPro.GameObjects
{
    public abstract class BaseObject : MonoBehaviour
    {
        /// <summary>
        /// flag for generating geometry every frame
        /// </summary>
        public bool generateGemoetryEveryFrame = false;

        /// <summary>
        /// flag to flip normals
        /// </summary>
        public bool flipNormals;

        /// <summary>
        /// flag to flip uv mapping
        /// </summary>
        public bool flipUVMapping;

        /// <summary>
        /// whether or not duplicate materials
        /// </summary>
        public bool shareMaterial;

        /// <summary>
        /// mesh collider business
        /// </summary>
        public bool generateMeshCollider;

        /// <summary>
        /// mode for mesh or collider generation
        /// </summary>
        public int generationMode;

        /// <summary>
        /// normals type to be generated
        /// </summary>
        public NormalsType normalsType;

        /// <summary>
        /// position of the model pivot
        /// </summary>
        public PivotPosition pivotPosition;

        /// <summary>
        /// current state of primitives variables
        /// </summary>
        public Dictionary<string, object> state = new Dictionary<string, object>();

        /// <summary>
        /// current state of primitives collision variables
        /// </summary>
        public Dictionary<string, object> stateCollision = new Dictionary<string, object>();

        /// <summary>
        /// save state of this class
        /// </summary>
        public virtual Dictionary<string, object> SaveState(bool collision)
        {
            return collision ? stateCollision : state;
        }

        /// <summary>
        /// load state of this class
        /// </summary>
        /// <param name="collision"></param>
        public virtual Dictionary<string, object> LoadState(bool collision)
        {
            return collision ? stateCollision : state;
        }

        public void SaveStateAll()
        {
            SaveState(true);
            SaveState(false);
        }

        public virtual void GenerateColliderGeometry()
        {
            SaveState(true);
        }

        public virtual void GenerateGeometry()
        {
        }

        void Update()
        {
            if (generateGemoetryEveryFrame)
            {
                GenerateGeometry();

                if (generateMeshCollider)
                {
                    AddMeshCollider(true);
                    GenerateColliderGeometry();
                }
            }
        }

        /// <summary>
        /// Flip normals to opposite direction
        /// </summary>
        public void FlipNormals()
        {
            if (generationMode == 1)
            {
                return;
            }

            flipNormals = !flipNormals;

            var mesh = GetComponent<MeshFilter>().sharedMesh;

            // reverse normals
            MeshUtils.ReverseNormals(mesh);

            // recalculate tangents
            MeshUtils.CalculateTangents(mesh);
        }

        public void FlipUVMapping()
        {
            flipUVMapping = !flipUVMapping;
        }

        /// <summary>
        /// add collider to the object
        /// </summary>
        public void AddCollider()
        {
            var mesh = GetComponent<MeshFilter>().sharedMesh;

            if (mesh)
            {
                if (GetType() == typeof(Sphere) || GetType() == typeof(Ellipsoid) || GetType() == typeof(GeoSphere))
                {
                    gameObject.AddComponent<SphereCollider>();
                }
                else if (GetType() == typeof(Capsule) || GetType() == typeof(Cone) || GetType() == typeof(Cylinder) || GetType() == typeof(Tube))
                {
                    gameObject.AddComponent<CapsuleCollider>();
                }
                else
                {
                    gameObject.AddComponent<BoxCollider>();
                }
            }
        }

        /// <summary>
        /// duplicate this object
        /// </summary>
        public GameObject Duplicate(bool duplicateMaterials)
        {
            var duplicate = Object.Instantiate(gameObject) as GameObject;
            var originalMesh = GetComponent<MeshFilter>().sharedMesh;
            var dupMesh = MeshUtils.CopyMesh(originalMesh);
            duplicate.GetComponent<MeshFilter>().sharedMesh = dupMesh;

            if (duplicateMaterials)
            {
                var meshRenderer = duplicate.GetComponent<MeshRenderer>();

                if (meshRenderer && meshRenderer.sharedMaterials.Length > 0)
                {
                    meshRenderer.sharedMaterials = MeshUtils.CopyMaterials(meshRenderer.sharedMaterials);
                }
            }

            return duplicate;
        }

        /// <summary>
        /// add mesh collider
        /// </summary>
        public void AddMeshCollider(bool add)
        {
            if (add)
            {
                var meshCollider = gameObject.AddComponent<MeshCollider>();

                if (meshCollider)
                {
                    meshCollider.enabled = false;
                    meshCollider.enabled = true;
                }
            }
            else
            {
                var meshCollider = gameObject.GetComponent<MeshCollider>();

                if (meshCollider)
                {
                    Object.DestroyImmediate(meshCollider);
                }
            }
        }

        /// <summary>
        /// measured time [ms] of last mesh generation
        /// </summary>
        public float GenerationTimeMS { get; set; }
    }
}
