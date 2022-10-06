using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class test : MonoBehaviour {

	ParticleSystem	_effect;

	float			_size=1;
	bool			_emit=true;

	void Start() {

		this._effect=this.GetComponent<ParticleSystem>();
	}

	// Use this for initialization
	void OnGUI () {

		this.transform.localScale=new Vector3(this._size*1,1,this._size*1);


		ParticleSystem.EmissionModule em=this._effect.emission;

		em.enabled=this._emit;

		if(this._size==0.1f) {

			this._emit=false;
		}

		GUILayout.BeginHorizontal();

		this._size=GUILayout.HorizontalSlider(this._size,0.1f,1,GUILayout.Width(100));

		if(GUILayout.Button("remove",EditorStyles.miniButton,GUILayout.Width(100))) {

			this._emit=!this._emit;
		}

		GUILayout.FlexibleSpace();
	}
	
}
