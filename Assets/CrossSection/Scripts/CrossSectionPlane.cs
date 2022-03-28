using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class CrossSectionPlane : MonoBehaviour {

	public string plane = "1";
	public List<Material> materials = new List<Material>();
	public float KeyMoveSpeed = 1;

	void Update() {
		foreach (var material in materials) {
			material.SetVector($"_Plane_{plane}_Position", transform.position);
			material.SetVector($"_Plane_{plane}_Normal", transform.forward);
		}
	}
}
