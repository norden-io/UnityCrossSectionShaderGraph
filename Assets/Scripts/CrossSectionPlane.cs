using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class CrossSectionPlane : MonoBehaviour {

	public string plane = "1";
	public List<Material> materials = new List<Material>();
	public float KeyMoveSpeed = 1;

	void Update() {
		if (Input.GetKey(KeyCode.UpArrow)) {
			transform.position += transform.forward * KeyMoveSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			transform.position -= transform.forward * KeyMoveSpeed * Time.deltaTime;
		}

		foreach (var material in materials) {
			material.SetVector($"_Plane{plane}Position", transform.position);
			material.SetVector($"_Plane{plane}Normal", transform.forward);
		}
	}
}
