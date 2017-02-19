using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageAnim : MonoBehaviour {

	public Image damage_Image;  
	public Color flash_Color;  
	public float flash_Speed = 5;  
	bool damaged = false;  


	// Update is called once per frame  
	void Update () {  
		//测试的输入代码段  
		if(Input.GetMouseButtonDown(1)){  
			TakeDamage();  
		}  

		PlayDamagedEffect ();  
	}  
	/// <summary>  
	/// 角色受伤后的屏幕效果  
	/// </summary>  
	void PlayDamagedEffect(){  
		if (damaged) {  
			
			damage_Image.color = flash_Color;
			Debug.Log ("damage2" + damage_Image.color);
		} else {  
			//damage_Image.color = Color.Lerp(damage_Image.color,Color.clear,flash_Speed * Time.deltaTime);  

		}  
		damaged = false;  

	}  
	/// <summary>  
	/// 角色受伤  
	/// </summary>  
	public void TakeDamage(){
		Debug.Log ("damage");
		damaged = true;  

	}  
}
