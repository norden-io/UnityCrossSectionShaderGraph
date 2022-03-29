using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrossSectionPlane : MonoBehaviour {
	public string plane = "1";
	public Material[] materials = new Material[0];

	private void Start() {
		var renderer = GetComponent<Renderer>();
		if (materials.Length == 0 && renderer != null) {
			materials = renderer.sharedMaterials;
		}
	}

	void Update() {
		foreach (var material in materials) {
			material.SetVector($"_Plane_{plane}_Position", transform.position);
			material.SetVector($"_Plane_{plane}_Normal", transform.up);
		}
	}
}
