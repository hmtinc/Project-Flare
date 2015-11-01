using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GraphicsS : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	



	}

	void Update(){


	}

	public void SetSet() {
		//AA
		GameObject.Find ("AA").GetComponent<Slider> ().value = QualitySettings.antiAliasing / 2;
		GameObject.Find ("AA Text").GetComponent<Text> ().text = QualitySettings.antiAliasing.ToString () + "x AA"; 
		
		//Triple Buffering 
		if (QualitySettings.maxQueuedFrames == 3) {
			GameObject.Find ("TB").GetComponent<Toggle>().isOn = true;
		}
		else {
			GameObject.Find ("TB").GetComponent<Toggle>().isOn = false;
		}
		
		//Quality 
		switch (QualitySettings.GetQualityLevel()) {
		case 1 :
			GameObject.Find("Quality AA").GetComponent<Text>().text = "Lowest";
			break;
		case 2: 
			GameObject.Find("Quality AA").GetComponent<Text>().text = "Low";
			break;
		case 3: 
			GameObject.Find("Quality AA").GetComponent<Text>().text = "Medium";
			break;
		case 4: 
			GameObject.Find("Quality AA").GetComponent<Text>().text = "High";
			break;
		case 5: 
			GameObject.Find("Quality AA").GetComponent<Text>().text = "Ultra";
			break;
		default:
			GameObject.Find("Quality AA").GetComponent<Text>().text = "Mobile";
			break;
			
			
		}
		GameObject.Find("Q").GetComponent<Slider>().value = QualitySettings.GetQualityLevel();
		
		//AF
		if (QualitySettings.anisotropicFiltering == AnisotropicFiltering.Enable) {
			GameObject.Find("AF").GetComponent<Toggle>().isOn = true;
		}
		else {
			GameObject.Find("AF").GetComponent<Toggle>().isOn = false;
		}
		
		//Vsync
		if (QualitySettings.vSyncCount == 1) {
			GameObject.Find("V").GetComponent<Toggle>().isOn = true;
		}
		else {
			GameObject.Find("V").GetComponent<Toggle>().isOn = false;;
		}
	}
	
	public void AAChange() {
		string valueString = GameObject.Find ("AA").GetComponent<Slider> ().value.ToString();
		QualitySettings.antiAliasing = int.Parse (valueString) * 2;
		GameObject.Find ("AA Text").GetComponent<Text> ().text = QualitySettings.antiAliasing.ToString () + "x AA"; 
	}

	public void TBChange() {

		if (GameObject.Find ("TB").GetComponent<Toggle> ().isOn == true) {
			QualitySettings.maxQueuedFrames = 3;
		} else {
			QualitySettings.maxQueuedFrames = 0;
		}
	}

	public void QChange() {

		string QString = GameObject.Find ("Q").GetComponent<Slider> ().value.ToString();
		int Qint = int.Parse (QString);
		switch (Qint) {
		case 1 :
			GameObject.Find("Quality AA").GetComponent<Text>().text = "Lowest";
			break;
		case 2: 
			GameObject.Find("Quality AA").GetComponent<Text>().text = "Low";
			break;
		case 3: 
			GameObject.Find("Quality AA").GetComponent<Text>().text = "Medium";
			break;
		case 4: 
			GameObject.Find("Quality AA").GetComponent<Text>().text = "High";
			break;
		case 5: 
			GameObject.Find("Quality AA").GetComponent<Text>().text = "Ultra";
			break;
		default:
			GameObject.Find("Quality AA").GetComponent<Text>().text = "Mobile";
			break;
			
			
		}

		QualitySettings.SetQualityLevel (Qint);
	
	}

	public void AFChange () {
		if (GameObject.Find ("AF").GetComponent<Toggle> ().isOn == true) {
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
		} else {
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
		}
	}

	public void VChange () {
		if (GameObject.Find ("V").GetComponent<Toggle> ().isOn == true) {
			QualitySettings.vSyncCount = 1;
		} else {
			QualitySettings.vSyncCount = 0;
		}
	}

	public void CMChange () {
		if (GameObject.Find ("CM").GetComponent<Toggle> ().isOn == true) {
			Time.captureFramerate = 24;
			Screen.SetResolution(1140, 466, true);

		} else {
			Time.captureFramerate = 60;
		}
	}
}
