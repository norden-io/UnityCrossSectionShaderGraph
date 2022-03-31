using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class CrossSection : MonoBehaviour {

	CrossSectionPlane _planePrefab, _planeXY, _planeXZ, _planeYZ;
	Renderer[] _renderers;
	Material _fresnelMat, _sectionMat;
	Material[] _sharedMats = new Material[2];

	void Awake() {
		_renderers = GetComponentsInChildren<Renderer>();

		_planePrefab = ((GameObject)Resources.Load("CrossSectionPlane")).GetComponent<CrossSectionPlane>();
		_fresnelMat = (Material)Resources.Load("Materials/CrossSectionFresnel");
		_sectionMat = (Material)Resources.Load("Materials/CrossSectionTextured");

		_sharedMats[0] = new(_sectionMat);
		_sharedMats[1] = new(_fresnelMat);

		foreach (var renderer in _renderers) {
			renderer.sharedMaterials = _sharedMats;
		}

		_planeXY = Instantiate(_planePrefab, transform);
		_planeXY.name = $"CrossSection Plane XY";
		_planeXY.plane = "1";
		_planeXY.materials = _sharedMats;
		var rot = _planeXY.transform.localRotation.eulerAngles;
		rot.x = 90;
		_planeXY.transform.localRotation = Quaternion.Euler(rot);

		_planeXZ = Instantiate(_planePrefab, transform);
		_planeXZ.name = $"CrossSection Plane XZ";
		_planeXZ.plane = "2";
		_planeXZ.materials = _sharedMats;

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
