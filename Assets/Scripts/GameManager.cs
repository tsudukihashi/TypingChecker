using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Text alphabetText;
    public Text timerText;

    public Text useFingerText;

    public GameObject explainText;

    public Text missText;


    public AudioClip typeSE;
    public AudioClip enterSE;

    public GameObject[] Fingers;
    private Color fingerColor = Color.red;

    private int NowKeyIndex = 0;

    private bool isPracticeMode = false;

    private bool isCount = false;
    public bool isPassed = true;

    public float timer = 0.0f;

    private float limit = 30.0f;

    public int missCount = 0;
    public int missLimit = 3;


     public Dictionary<string, string> spelMiss = new Dictionary<string, string>();

    public  static GameManager m_instance;

    private const string alphabet = "abcdefghijklmnopqrstuvwxyz";
    private string finger;



    private void Awake()
    {
        m_instance = this;
    }
    // Use this for initialization
    void Start () {

        isPracticeMode = TitleManager.GetPractic();


        NowKeyIndex = 0;
        explainText.SetActive(true);

        isPassed = true;
        //Debug.Log(alphabet[alphabet.Length - 1]); Z
        DisplayText(NowKeyIndex);

        useFingerText.gameObject.SetActive(isPracticeMode);

        for (int i = 0; i < Fingers.Length; i++)
        {
            Fingers[i].gameObject.SetActive(isPracticeMode);
            }


    }

    // Update is called once per frame
    void Update () {


        if (!isCount){
            if (Input.GetKeyDown(KeyCode.Return))
            {
                AudioSource.PlayClipAtPoint(enterSE, transform.position);
                isCount = true;
                explainText.SetActive(false);
            }
            return;
        }


        timer += Time.deltaTime;

        if(isPracticeMode){
            useFingerText.text = useFinger(alphabet[NowKeyIndex]);

        }

        if (timer >= limit){
            Debug.Log("timer" + timer + "limit" + limit);
            isPassed = false;
        }

        if(Input.anyKeyDown &&(!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))){

            AudioSource.PlayClipAtPoint(typeSE, transform.position);

            if(Input.GetKeyDown(KeyCode.Escape)){
                SceneManager.LoadScene("TypeScene");
            }
            if (Input.GetKeyDown(alphabet[NowKeyIndex].ToString()))
            {


            }else{

                missCount++;

                spelMiss.Add( missCount.ToString() , alphabet[NowKeyIndex].ToString() + " => " +Input.inputString+ " , ");


            }
            NowKeyIndex++;
   


            if (NowKeyIndex >= alphabet.Length)
            {
                if (missCount >= missLimit)
                {
                    isPassed = false;
                }
                isCount = false;
                SceneManager.LoadScene("ResultScene");
                return;
            }
            DisplayText(NowKeyIndex);

        }


	}

    private void FixedUpdate()
    {
        timerText.text = timer.ToString("F2");

        missText.text = missCount.ToString();
    }

    public void DisplayText(int index){
        string temp = "<size=20><color=#ff0000>" + alphabet[index] + "</color></size>";

        string text = alphabet.Remove(index, 1).Insert(index, temp);

        alphabetText.text = text;

    }

    //charを与えたら使用する指を答えてくれる関数
    public string useFinger(char alphabet){

        for (int i = 0; i < Fingers.Length; i++)
        {
            Fingers[i].gameObject.GetComponent<Image>().color = Color.white;
        }
        switch (alphabet){
            case 'a':
            case 'q':
            case 'z':
                Fingers[0].gameObject.GetComponent<Image>().color = fingerColor;
                return "左手小指";

            case 's':
            case 'w':
            case 'x':
                Fingers[1].gameObject.GetComponent<Image>().color = fingerColor;
                return "左手薬指";
            case 'e':
            case 'd':
            case 'c':
                Fingers[2].gameObject.GetComponent<Image>().color = fingerColor;
                return "左手中指";
            case 'r':
            case 'f':
            case 'v':
            case 't':
            case 'g':
            case 'b':
                Fingers[3].gameObject.GetComponent<Image>().color = fingerColor;
                return "左手人差し指";
            case 'y':
            case 'h':
            case 'n':
            case 'u':
            case 'j':
            case 'm':
                Fingers[6].gameObject.GetComponent<Image>().color = fingerColor;
                return "右手人差し指";
            case 'i':
            case 'k':
            case ',':
                Fingers[7].gameObject.GetComponent<Image>().color = fingerColor;
                return "右手中指";
            case 'o':
            case 'l':
            case '.':
                Fingers[8].gameObject.GetComponent<Image>().color = fingerColor;
                return "右手薬指";
            case 'p':
            case ';':
            case '/':
            case '@':
            case ':':
            case '[':
            case ']':
                Fingers[9].gameObject.GetComponent<Image>().color = fingerColor;
                return "右手薬指";
            default:
                return "わかりません";
        }
    }
}
