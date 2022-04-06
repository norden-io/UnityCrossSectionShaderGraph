using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class CrossSection : MonoBehaviour {
	public Material sectionMaterial, normalMaterial;
	//public bool keepNormalMaterial = true;

	[SerializeField] CrossSectionPlane _planePrefab;
	CrossSectionPlane _planeXY, _planeXZ, _planeYZ;
	Renderer[] _renderers;
	Material[] _sharedMats = new Material[2];

	void Awake() {
		_renderers = GetComponentsInChildren<Renderer>();

		if (_planePrefab == null)
			_planePrefab = ((GameObject)Resources.Load("CrossSectionPlane")).GetComponent<CrossSectionPlane>();
		if (sectionMaterial == null) 
			sectionMaterial = (Material)Resources.Load("Materials/CrossSectionFresnel");
		if (normalMaterial == null)
			normalMaterial = (Material)Resources.Load("Materials/CrossSectionTextured");

		Create();
	}

	void Create() {
		_sharedMats[0] = new(normalMaterial);
		_sharedMats[1] = new(sectionMaterial);

		foreach (var renderer in _renderers) {
			renderer.sharedMaterials = _sharedMats;
		}

		if (_planeXY != null) Destroy(_planeXY);
		_planeXY = Instantiate(_planePrefab, transform);
		_planeXY.name = $"CrossSection Plane XY";
		_planeXY.plane = "1";
		_planeXY.materials = _sharedMats;
		var rot = _planeXY.transform.localRotation.eulerAngles;
		rot.x = 90;
		_planeXY.transform.localRotation = Quaternion.Euler(rot);

		if (_planeXZ != null) Destroy(_planeXZ);
		_planeXZ = Instantiate(_planePrefab, transform);
		_planeXZ.name = $"CrossSection Plane XZ";
		_planeXZ.plane = "2";
		_planeXZ.materials = _sharedMats;

		if (_planeYZ != null) Destroy(_planeYZ);
		_planeYZ = Instantiate(_planePrefab, transform);
		_planeYZ.name = $"CrossSection Plane YZ";
		_planeYZ.plane = "3";
		_planeYZ.materials = _sharedMats;
		rot = _planeYZ.transform.localRotation.eulerAngles;
		rot.y = 180;
		rot.z = -90;
		_planeYZ.transform.localRotation = Quaternion.Euler(rot);
	}
}
