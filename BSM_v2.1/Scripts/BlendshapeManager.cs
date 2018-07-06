using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendshapeManager : MonoBehaviour {

	[HeaderAttribute("*Blendshapes")]
	public Blendshape[] _bs;
	private int index_select = -1;						// not select blendshape value is "-1"



	#region Propertyes
	public int selectIndex{
		get{
			return index_select;
		}
		set{
			if (value >= _bs.Length) {
				index_select = -1;
				Debug.Log ("無効な数値を入れようとしています");
			}
		}
	}

	public int BlendshapeCount{
		get{
			return _bs.Length;
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		if (_bs.Length == 0) {
			Debug.LogError ("有効なコンポーネントがありません");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.A)) {
			BS_ChangeUp ();
		} else if (Input.GetKeyDown (KeyCode.D)) {
			BS_ChangeDown ();
		} else if (Input.GetKey (KeyCode.W)) {
			BS_ValueUp ();
		} else if (Input.GetKey (KeyCode.S)) {
			BS_ValueDown ();
		} else if (Input.GetKeyDown (KeyCode.LeftAlt)) {
			AddIndex (1);
		}else if(Input.GetKeyDown(KeyCode.RightAlt)){
			AddIndex (-1);
		}


	}
	public void AutoSetting(){
		SkinnedMeshRenderer[] smr=GetComponentsInChildren<SkinnedMeshRenderer>();
		for(int i=0;i<smr.Length;i++){
			if(smr[i].sharedMesh.blendShapeCount>0){
				if(smr[i].transform.gameObject.GetComponent<Blendshape>()==null){
					Blendshape _blendSP= smr[i].transform.gameObject.AddComponent<Blendshape>();
					_blendSP.GetBlendshape();
				}
			}
		}
		_bs = GetComponentsInChildren<Blendshape> ();
		if (_bs.Length == 0) {
			Debug.Log ("見つかりませんでした");
		}
	}


	public void AddIndex(int count=1){

		if(index_select!=-1){
			if(_bs[index_select].canvasText!=null){
				_bs[index_select].canvasText.color=Color.black;
			}
		}

		index_select += count;
		if (index_select > _bs.Length-1) {
			index_select = 0;
		} else if (index_select < 0) {
			index_select = _bs.Length-1;
		}
		
		if(index_select!=-1){
			if(_bs[index_select].canvasText!=null){
				_bs[index_select].canvasText.color=Color.red;
			}
		}
		
	}


	public void BS_ValueDown(){
		if (index_select != -1) {
			_bs [index_select].ValueDown();
		}
	}

	public void BS_ValueUp(){
		if (index_select != -1) {
			_bs [index_select].ValueUp();
		}

	}

	public void BS_ChangeUp(){
		if (index_select != -1) {
			_bs [index_select].ChangeUp();
		}
	}

	public void BS_ChangeDown(){
		if (index_select != -1) {
			_bs [index_select].ChangeDown();
		}
	}

}
