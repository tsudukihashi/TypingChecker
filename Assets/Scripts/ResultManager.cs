using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class ResultManager : MonoBehaviour {

    private bool isPractice = false;

    public GameObject PassedText;
    public GameObject FailedText;

    public Text missText;
    public Text timerText;
    public Text spellText;
    public Text percentText;
    public Text postText;
    public Text scoreText;
    public Text timePerSecText;

    private float score = 100.0f;

    TextAsset csvFile;
    List<string> csvDatas = new List<string>();



    // Use this for initialization
    void Start () {
        isPractice = TitleManager.GetPractic();

        Debug.Log(isPractice);

        csvFile = Resources.Load("post") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while(reader.Peek() != -1){
            string line = reader.ReadLine();
            csvDatas.Add(line);
        }

        //Debug.Log(csvDatas[0]);
        float miss = GameManager.m_instance.missCount;
        float wariai = (26.0f - miss) / 26.0f;
        float per = wariai * 100.0f;
        
        float time = GameManager.m_instance.timer;
        float persec = time / 26.0f;

        timePerSecText.text = persec.ToString("F2") + "文字/秒";
        
        score = 100.0f * 1.0f / time * wariai * wariai;

        if(GameManager.m_instance.isPassed){
            score += 20;
        }

        scoreText.text = score.ToString("F2");

        CheckPost(per);
        
        percentText.text = "正答率 " +  per.ToString("F2") +"%";
        if(GameManager.m_instance.isPassed){
            PassedText.SetActive(true);
        }else{
            FailedText.SetActive(true);
        }

        missText.text = GameManager.m_instance.missCount.ToString();
        timerText.text = GameManager.m_instance.timer.ToString("F2");

        foreach(KeyValuePair<string, string> pair in GameManager.m_instance.spelMiss){
            spellText.text +=  pair.Value;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.Space)){

            if (isPractice)
            {
                SceneManager.LoadScene("TitleScene");
                return;
            }

            SceneManager.LoadScene("TypeScene");
        }

        if(Input.GetKey(KeyCode.Escape)){
            SceneManager.LoadScene("TitleScene");
        }
	}

    void CheckPost(float per){
        int score = (int)per;

        if(score == 100){
            postText.text = csvDatas[0];
        }else if(score >= 90){
            postText.text = csvDatas[1];
        }else if(score >= 80){
            postText.text = csvDatas[2];
        }
        else if (score >= 70)
        {
            postText.text = csvDatas[3];
        }
        else if (score >= 60)
        {
            postText.text = csvDatas[4];
        }
        else if (score >= 50)
        {
            postText.text = csvDatas[5];
        }
        else if (score >= 40)
        {
            postText.text = csvDatas[6];
        }
        else if (score >= 30)
        {
            postText.text = csvDatas[7];
        }
        else if (score >= 20)
        {
            postText.text = csvDatas[8];
        }
        else if (score >= 10)
        {
            postText.text = csvDatas[9];
        }
        else if (score >= 0)
        {
            postText.text = csvDatas[10];
        }
    }
}
