using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Retry(){
        SceneManager.LoadScene("TypeScene");
    }

}
