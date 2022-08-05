 using System.Collections.Generic;
using UnityEngine;

namespace griush
{
    namespace Water
    {
        public class PlaneMesh
        {
            public readonly Vector3[] Vertices;
            public readonly int[] Triangles;
            public readonly Vector2[] UVs;
            public readonly int Resolution;

			FixedSizeList<Vector3> vertices;
			FixedSizeList<int> triangles;
			FixedSizeList<Vector2> uvs;

			public PlaneMesh(Vector2 size, int resolution)
			{
				this.Resolution = resolution;

				int numVerts = resolution * resolution;

				vertices = new FixedSizeList<Vector3>(numVerts);
				triangles = new FixedSizeList<int>((resolution - 1) * (resolution - 1) * 2 * 3);
				uvs = new FixedSizeList<Vector2>(numVerts);

				float xStep = size.x / (resolution - 1);
				float yStep = size.y / (resolution - 1);
				for (int x = 0; x < resolution; x++)
				{
					for (int y = 0; y < resolution; y++)
					{
						vertices.items[x + y * resolution] = new Vector3(xStep * x, 0, yStep * y);
						uvs.items[x + y * resolution] = new Vector2(xStep * x, yStep * y);
					}
				}

				for (int ti = 0, vi = 0, y = 0; y < resolution - 1; y++, vi++)
				{
					for (int x = 0; x < resolution - 1; x++, ti += 6, vi++)
					{
						triangles.items[ti] = vi;
						triangles.items[ti + 3] = triangles.items[ti + 2] = vi + 1;
						triangles.items[ti + 4] = triangles.items[ti + 1] = vi + resolution - 1 + 1;
						triangles.items[ti + 5] = vi + resolution - 1 + 2;
					}
				}

				Vertices = vertices.items;
				Triangles = triangles.items;
				UVs = uvs.items;
			}
		}

		public class FixedSizeList<T>
		{
			public T[] items;
			public int nextIndex;
			public int size;

			public FixedSizeList(int size)
			{
				this.size = size;
				items = new T[size];
			}

			public void Add(T item)
			{
				items[nextIndex] = item;
				nextIndex++;
			}

			public void AddRange(IEnumerable<T> items)
			{
				foreach (var item in items)
				{
					Add(item);
				}
			}
		}
	}
}