using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blendshape: MonoBehaviour {


	#region Inspector setting
	public Text canvasText;
	public Group[] group = new Group[1];
	public blend[] bl;
	#endregion // Inspector setting

	private const int max_blend = 100;					//ブレンドシェイプの値の最大値、最小値
	private const int min_blend = 0;


	private SkinnedMeshRenderer SMR;
	private Mesh skinnedMesh;
	private int index = 0;
	private float nowBlendSpValue = 100;
	private float spd = 1.0f;
	private int range, index_max;
	private bool isGroupOnly=false;
	[HideInInspector] public int selectGroup = 0;

	#if UNITY_EDITOR
	[SerializeField]
	private bool debugFlag;
	#endif

	#region Class

	[System.Serializable]
	public class Group
	{
		public string groupName="name";
		[HideInInspector] public int groupSize = 0;

	}

	[System.Serializable]
	public class blend
	{
		//public string tex;
		public int group=0;
		[HideInInspector]
		public bool flag = false;
	}

	#endregion // Class




	// Use this for initialization
	void Start () {
		SMR = GetComponent<SkinnedMeshRenderer> ();
		skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
		range = skinnedMesh.blendShapeCount;
		index_max = range - 1;
		//bl = new blend[index_max];

		//bl [0].flag = true;

		if (group.Length == 1) {
			isGroupOnly = true;
		}
		for (int n = 0; n < group.Length; n++) {
			int no = 0;
			for (int x = 0; x < bl.Length; x++) {
				Debug.Log (bl[x].group);
				if (bl [x].group == n) {
					no += 1;
				}
			}
			group [n].groupSize = no;
		}

	}

	// Update is called once per frame
	void Update () {

		#if UNITY_EDITOR

		if(debugFlag==false){
			return;
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			ChangeUp ();
		}
		else if (Input.GetKeyDown (KeyCode.D)) {
			ChangeDown ();
		}
		else if (Input.GetKey (KeyCode.W)) {
			ValueUp ();
		}
		else if (Input.GetKey (KeyCode.S)) {
			ValueDown ();
		}


		//selectGroupを切り替えた時の処理
		if(Input.GetKeyDown(KeyCode.LeftShift)){

			//
			selectGroup += 1;

			//
			if (selectGroup > group.Length-1) {
				selectGroup = 0;
			}

			resetI();
			UpdateText();
		}else if(Input.GetKeyDown(KeyCode.RightShift)){

			//
			selectGroup -= 1;

			//
			if (selectGroup < 0) {
				selectGroup = group.Length - 1;
			}

			resetI();
			UpdateText();
		}
		#endif




	}

	public void ValueUp(){

		//同じグループの値を初期化
		for (int x = 0; x < range; x++) {
			if (bl [index].group == bl [x].group) {
				SMR.SetBlendShapeWeight (x, min_blend);
			}
		}

		//値を増加
		nowBlendSpValue += spd;

		//最大値より大きい場合の処理
		if (nowBlendSpValue > max_blend) {
			nowBlendSpValue = max_blend;
		}


		//ブレンドシェイプの値を更新
		SMR.SetBlendShapeWeight (index, nowBlendSpValue);

		//テキストを更新
		UpdateText ();
	}
	public void ValueDown(){

		//同じグループの値を初期化
		for (int x = 0; x < range; x++) {
			if (bl [index].group == bl [x].group) {
				SMR.SetBlendShapeWeight (x, min_blend);
			}
		}

		//値を減らす
		nowBlendSpValue -= spd;

		//最小値未満の場合の処理
		if (nowBlendSpValue < min_blend) {
			nowBlendSpValue = min_blend;
		}

		//値を更新
		SMR.SetBlendShapeWeight (index, nowBlendSpValue);

		//テキストを更新
		UpdateText ();
	}
	public void ChangeUp(){

		//
		if (index < index_max) {
			index += 1;
			if (!isGroupOnly) {
				while (bl [index].group != selectGroup) {
					index += 1;
					if (index > index_max) {
						index = 0;
					}
				}
			}


		} else {
			index = 0;
			if (!isGroupOnly) {
				while(bl[index].group!=selectGroup){
					index += 1;
				}
			}
		}


		//同じグループの値を初期化
		for (int x = 0; x < range; x++) {
			if (bl [index].group == bl [x].group) {
				SMR.SetBlendShapeWeight (x, min_blend);
			}
		}

		//変数を初期化
		nowBlendSpValue = max_blend;

		//ブレンドシェイプを更新
		SMR.SetBlendShapeWeight (index, nowBlendSpValue);

		//テキストを更新
		UpdateText ();
	}
	public void ChangeDown(){

		if (index > 0) {
			index -= 1;
			if (!isGroupOnly) {
				while(bl[index].group!=selectGroup){
					index -= 1;
					if (index < 0) {
						index = index_max;
					}
				}
			}
		} else {
			index = index_max;
			if (!isGroupOnly) {
				while(bl[index].group!=selectGroup){
					index -= 1;
				}
			}
		}

		//同じグループの値を初期化
		for (int i = 0; i < range; i++) {
			if (bl [index].group == bl [i].group) {
				SMR.SetBlendShapeWeight (i, min_blend);
			}
		}

		//変数を初期化
		nowBlendSpValue = max_blend;

		//ブレンドシェイプを更新
		SMR.SetBlendShapeWeight (index, nowBlendSpValue);

		//テキストを更新
		UpdateText ();
	}


	//テキストを更新するための関数
	public void UpdateText(){

		if (canvasText == null) {
			return;
		}


		int no=0;
		for (int n = 0; n <= index; n++) {
			if (bl [n].group == selectGroup) {
				no += 1;
			}
		}

		string pasent = string.Format ("{0:D3}", (int)nowBlendSpValue);
		canvasText.text = group[selectGroup].groupName+"   "+SMR.sharedMesh.GetBlendShapeName(index)+"   "+pasent + "   " + no.ToString () + "/" + group [selectGroup].groupSize.ToString();


	}

	public void resetI(){
		index = 0;
		while(bl[index].group!=selectGroup){
			index += 1;
		}
	}

	public void MaxValue(){
		nowBlendSpValue = max_blend;
		SMR.SetBlendShapeWeight (index, nowBlendSpValue);
		UpdateText ();
	}
	public void MinValue(){
		nowBlendSpValue = min_blend;
		SMR.SetBlendShapeWeight (index, nowBlendSpValue);
		UpdateText ();
	}



	public void GetBlendshape(){
		SMR = GetComponent<SkinnedMeshRenderer> ();
		skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
		range = skinnedMesh.blendShapeCount;
		index_max = range - 1;
		bl = new blend[range];
	}

}
