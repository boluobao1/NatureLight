using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreateAndOperate;

public class DataManager : MonoBehaviour {

    [Header("光气候参数")]
    public GameObject LightArea;
    public GameObject K_value;
    public GameObject OutdoorIlluminance;

    [Header("灯的参数")]
    public GameObject Light;
    public GameObject Light1;
    public GameObject Light2;

    [Header("窗户参数")]
    public GameObject WindowWidth;
    public GameObject WindowHeight;
    public GameObject WindowSillWidth;
    public GameObject WindowSillHeight;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //设置光气候参数
    public void SetSunPara()
    {
        switch(LightArea.GetComponent<Dropdown>().value)
        {
            case 0:

                K_value.GetComponent<InputField>().text = "0.85";
                OutdoorIlluminance.GetComponent<InputField>().text = "18000";
                break;
            case 1:
                K_value.GetComponent<InputField>().text = "0.90";
                OutdoorIlluminance.GetComponent<InputField>().text = "16500";
                break;
            case 2:
                K_value.GetComponent<InputField>().text = "1.00";
                OutdoorIlluminance.GetComponent<InputField>().text = "15000";
                break;
            case 3:
                K_value.GetComponent<InputField>().text = "1.10";
                OutdoorIlluminance.GetComponent<InputField>().text = "13500";
                break;
            case 4:
                K_value.GetComponent<InputField>().text = "1.20";
                OutdoorIlluminance.GetComponent<InputField>().text = "12000";
                break;
            default:
                break;
        }
    }

    //设置灯的参数
    public void SetLightPara()
    { }

    //设置窗户的参数
    public void SetWinderPara(CreatedWindow.WindowParameter WindowPara)
    {
        WindowWidth.GetComponent<InputField>().text = WindowPara.WindowWidth.ToString();
        WindowHeight.GetComponent<InputField>().text = WindowPara.WindowHeight.ToString();
        WindowSillWidth.GetComponent<InputField>().text = "0.2";
        WindowSillHeight.GetComponent<InputField>().text = (WindowPara.WindowPosition.y - 0.5f*WindowPara.WindowHeight-(-3.11009f)).ToString();
    }

    //设置



}
