// Version 1.5.3
// ©2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using System.Diagnostics;
using UnityEngine;

namespace PrimitivesPro.Primitives
{
    /// <summary>
    /// class for creating Box primitive
    /// </summary>
    public class BoxPrimitive : Primitive
    {
        /// <summary>
        /// generate mesh geometry for box
        /// </summary>
        /// <param name="mesh">mesh to be generated</param>
        /// <param name="width">width of cube</param>
        /// <param name="height">height of cube</param>
        /// <param name="depth">depth of cube</param>
        /// <param name="widthSegments">number of triangle segments in width direction</param>
        /// <param name="heightSegments">number of triangle segments in height direction</param>
        /// <param name="depthSegments">number of triangle segments in depth direction</param>
        /// <param name="cubeMap">enable 6-sides cube map uv mapping</param>
        /// <param name="flipUV">flag to flip uv mapping</param>
        /// <param name="pivot">position of the model pivot</param>
        public static float GenerateGeometry(Mesh mesh, float width, float height, float depth, int widthSegments, int heightSegments, int depthSegments, bool cubeMap, bool flipUV, PivotPosition pivot)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            width = Mathf.Clamp(width, 0, 100);
            height = Mathf.Clamp(height, 0, 100);
            depth = Mathf.Clamp(depth, 0, 100);
            heightSegments = Mathf.Clamp(heightSegments, 1, 100);
            widthSegments = Mathf.Clamp(widthSegments, 1, 100);
            depthSegments = Mathf.Clamp(depthSegments, 1, 100);

            mesh.Clear();

            const int parallelSides = 3;

            var sideParams = new[]
            {
                new { sizeX = width, sizeY = height, segX = widthSegments, segY = heightSegments, zPos = new[]{depth/-2, depth/2}},
                new { sizeX = width, sizeY = depth, segX = widthSegments, segY = depthSegments,   zPos = new[]{height, 0.0f}},
                new { sizeX = depth, sizeY = height, segX = depthSegments, segY = heightSegments, zPos = new[]{width/2, width/-2}},
            };

            int numTriangles = 0;
            int numVertices = 0;

            for (int i=0; i<parallelSides; i++)
            {
                numTriangles += sideParams[i].segX*sideParams[i].segY*6*2;
                numVertices += (sideParams[i].segX + 1)*(sideParams[i].segY + 1)*2;
            }

            var pivotOffset = Vector3.zero;
            switch (pivot)
            {
                case PivotPosition.Center: pivotOffset = new Vector3(0.0f, -height/2, 0.0f);
                    break;
                case PivotPosition.Top: pivotOffset = new Vector3(0.0f, -height, 0.0f);
                    break;
            }

            if (numVertices > 60000)
            {
                UnityEngine.Debug.LogError("Too much vertices!");
                return 0.0f;
            }

            var vertices = new Vector3[numVertices];
            var uvs = new Vector2[numVertices];
            var triangles = new int[numTriangles];

            int index = 0;
            for (int i=0; i<parallelSides; i++)
            {
                float uvFactorX = 1.0f/sideParams[i].segX;
                float uvFactorY = 1.0f/sideParams[i].segY;
                float scaleX = sideParams[i].sizeX/sideParams[i].segX;
                float scaleY = sideParams[i].sizeY/sideParams[i].segY;

                for (int s = 0; s < 2; s++)
                {
                    for (float y = 0.0f; y < sideParams[i].segY + 1; y++)
                    {
                        for (float x = 0.0f; x < sideParams[i].segX + 1; x++)
                        {
                            if (i ==0)
                            {
                                vertices[index] = new Vector3(x * scaleX - width / 2f, y * scaleY, sideParams[i].zPos[s]);
                            }
                            else if (i==1)
                            {
                                vertices[index] = new Vector3(x*scaleX - width/2f, sideParams[i].zPos[s], y*scaleY - depth/2f);
                            }
                            else
                            {
                                vertices[index] = new Vector3(sideParams[i].zPos[s], y*scaleY, x*scaleX - depth / 2f);
                            }
                            vertices[index] += pivotOffset;
                            uvs[index] = new Vector2(x*uvFactorX, y*uvFactorY);

                            if (s == 1)
                            {
                                uvs[index].x = 1.0f - uvs[index].x;
                            }

                            if (flipUV)
                            {
                                uvs[index].x = 1.0f - uvs[index].x;
                                //uvs[index].y = 1.0f - uvs[index].y;
                            }

                            if (cubeMap)
                            {
                                uvs[index] = GetCube6UV(i, s, uvs[index]);
                            }

                            index++;
                        }
                    }
                }
            }

            var flipTris = new[] {0, 2};

            var indexSide = 0;
            index = 0;
            for (int i = 0; i < parallelSides; i++)
            {
                for (int s = 0; s < 2; s++)
                {
                    for (int y = 0; y < sideParams[i].segY; y++)
                    {
                        for (int x = 0; x < sideParams[i].segX; x++)
                        {
                            triangles[index + 0 + flipTris[s]]  = indexSide + (y * (sideParams[i].segX + 1)) + x;
                            triangles[index + 1]                = indexSide + ((y + 1) * (sideParams[i].segX + 1)) + x;
                            triangles[index + 2 - flipTris[s]]  = indexSide + (y * (sideParams[i].segX + 1)) + x + 1;

                            triangles[index + 3 + flipTris[s]]  = indexSide + ((y + 1) * (sideParams[i].segX + 1)) + x;
                            triangles[index + 4]                = indexSide + ((y + 1) * (sideParams[i].segX + 1)) + x + 1;
                            triangles[index + 5 - flipTris[s]]  = indexSide + (y * (sideParams[i].segX + 1)) + x + 1;
                            index += 6;
                        }
                    }

                    indexSide += (sideParams[i].segX + 1)*(sideParams[i].segY + 1);
                }
            }

            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            MeshUtils.CalculateTangents(mesh);
            mesh.Optimize();
            mesh.RecalculateBounds();

            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// generate uv coordinates for a texture with 6 sides of the box
        /// </summary>
        static Vector2 GetCube6UV(int sideID, int paralel, Vector2 factor)
        {
            factor.x = factor.x*0.3f;
            factor.y = factor.y*0.5f;

            switch (sideID)
            {
                case 0:
                    if (paralel == 0)
                    {
                        factor.y += 0.5f;
                        return factor;
                    }
                    else
                    {
                        factor.y += 0.5f;
                        factor.x += 2.0f / 3;
                        return factor;
                    }
                case 1:
                    if (paralel == 0)
                    {
                        factor.x += 1.0f / 3;
                        return factor;
                    }
                    else
                    {
                        factor.x += 2.0f / 3;
                        return factor;
                    }
                case 2:
                    if (paralel == 0)
                    {
                        factor.y += 0.5f;
                        factor.x += 1.0f / 3;
                        return factor;
                    }
                    else
                    {
                        return factor;
                    }
            }

            return Vector2.zero;
        }
    }
}
