using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping;
using UnityBase.Extensions;
using UnityEngine;
using UnityEngine.Rendering;

#nullable enable

[DefaultExecutionOrder(-100000)]
public class MergeSubmeshes : MonoBehaviour
{
	private void Start()
	{
		Merge();
	}

	void Merge()
	{
		foreach (var mf in GetComponentsInChildren<MeshFilter>()) {
			var p = mf.transform.position;
			mf.transform.position = Vector3.zero;
			var r = mf.transform.rotation;
			mf.transform.rotation = Quaternion.identity;
			var s = mf.transform.localScale;
			mf.transform.localScale = mf.transform.lossyScale.Invert();
			
			CombineInstance[] combine = new CombineInstance[mf.sharedMesh.subMeshCount];
			for (int submesh = 0; submesh < mf.sharedMesh.subMeshCount; submesh++) {
				combine[submesh].mesh = mf.sharedMesh;
				combine[submesh].subMeshIndex = submesh;
				combine[submesh].transform = mf.transform.localToWorldMatrix;
			}
			
			mf.mesh = new Mesh();
			mf.mesh.CombineMeshes(combine);
			
			var renderer = mf.GetComponent<Renderer>();
			renderer.materials = new []{renderer.material};
			
			mf.transform.position   = p;
			mf.transform.rotation   = r;
			mf.transform.localScale = s;
		}
	}


}
