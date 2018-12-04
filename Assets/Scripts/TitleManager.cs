using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

    public static bool isPractice = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Return))
        {
            ExamMode();
        }

        if(Input.GetKey(KeyCode.Space)){
            PracticeMode();
        }
	}

    public void ExamMode(){
        isPractice = false;
        SceneManager.LoadScene("TypeScene");
    }

    public void PracticeMode(){
        isPractice = true;

        SceneManager.LoadScene("TypeScene");
    }

    public static bool GetPractic(){
        return isPractice;
    }

}
