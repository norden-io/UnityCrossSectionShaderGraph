using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping;
using UnityEngine;
using UnityEngine.Rendering;

#nullable enable

[DefaultExecutionOrder(-100000)]
public class SplitSubmeshes : MonoBehaviour
{
	private void Start()
	{
		Split();
	}

	void Split()
	{
		foreach (var mf in GetComponentsInChildren<MeshFilter>()) {
			var meshes = SplitMesh(mf);
			if (meshes is null) 
				continue;

			var renderer = mf.GetComponent<Renderer>();
			var prefab   = Instantiate(renderer.gameObject, renderer.transform, true);

			var children = meshes.Zip(renderer.sharedMaterials, (mesh, mat) =>
			{
				var go = Instantiate(prefab, renderer.transform, true);
				
				var go_mf = go.GetComponent<MeshFilter>();
				go_mf.sharedMesh = mesh;
				
				var go_renderer = go.GetComponent<Renderer>();
				go_renderer.materials = new []{ mat };

				return go;
			}).ToList();
			
			Destroy(prefab);
			renderer.enabled = false;
		}
	}

	public static Mesh[]? SplitMesh(MeshFilter mf)
	{
		if (mf.sharedMesh.subMeshCount <= 1) {
			return null; // Cannot split
		}
		
		return Enumerable.Range(0, mf.sharedMesh.subMeshCount)
			.Select(i_submesh => ExtractSubmesh(mf.sharedMesh, i_submesh))
			.ToArray();
	}

	// https://forum.unity.com/threads/is-it-possible-to-split-submeshes-into-different-game-objects.1203928/
	public static Mesh ExtractSubmesh(Mesh mesh, int submesh)
	{
		Mesh              newMesh    = new Mesh();
		SubMeshDescriptor descriptor = mesh.GetSubMesh(submesh);

		newMesh.vertices = mesh.vertices.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();

		if (mesh.tangents != null && mesh.tangents.Length == mesh.vertices.Length) {
			newMesh.tangents = mesh.tangents.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();
		}

		if (mesh.boneWeights != null && mesh.boneWeights.Length == mesh.vertices.Length) {
			newMesh.boneWeights = mesh.boneWeights.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();
		}

		if (mesh.uv != null && mesh.uv.Length == mesh.vertices.Length) {
			newMesh.uv = mesh.uv.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();
		}

		if (mesh.uv2 != null && mesh.uv2.Length == mesh.vertices.Length) {
			newMesh.uv2 = mesh.uv2.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();
		}

		if (mesh.uv3 != null && mesh.uv3.Length == mesh.vertices.Length) {
			newMesh.uv3 = mesh.uv3.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();
		}

		if (mesh.uv4 != null && mesh.uv4.Length == mesh.vertices.Length) {
			newMesh.uv4 = mesh.uv4.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();
		}

		if (mesh.uv5 != null && mesh.uv5.Length == mesh.vertices.Length) {
			newMesh.uv5 = mesh.uv5.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();
		}

		if (mesh.uv6 != null && mesh.uv6.Length == mesh.vertices.Length) {
			newMesh.uv6 = mesh.uv6.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();
		}

		if (mesh.uv7 != null && mesh.uv7.Length == mesh.vertices.Length) {
			newMesh.uv7 = mesh.uv7.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();
		}

		if (mesh.uv8 != null && mesh.uv8.Length == mesh.vertices.Length) {
			newMesh.uv8 = mesh.uv8.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();
		}

		if (mesh.colors != null && mesh.colors.Length == mesh.vertices.Length) {
			newMesh.colors = mesh.colors.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();
		}

		if (mesh.colors32 != null && mesh.colors32.Length == mesh.vertices.Length) {
			newMesh.colors32 = mesh.colors32.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();
		}

		var triangles = mesh.triangles.Skip(descriptor.indexStart).Take(descriptor.indexCount).ToArray();
		for (int i = 0; i < triangles.Length; i++) {
			triangles[i] -= descriptor.firstVertex;
		}

		newMesh.triangles = triangles;

		if (mesh.normals != null && mesh.normals.Length == mesh.vertices.Length) {
			newMesh.normals = mesh.normals.Skip(descriptor.firstVertex).Take(descriptor.vertexCount).ToArray();
		}
		else {
			newMesh.RecalculateNormals();
		}

		newMesh.Optimize();
		newMesh.OptimizeIndexBuffers();
		newMesh.RecalculateBounds();
		newMesh.name = mesh.name + $" Submesh {submesh}";
		return newMesh;
	}
}
