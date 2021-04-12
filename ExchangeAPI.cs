using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;
using System.Text;
using LitJson;
using TMPro;
using UnityEngine;

public class ExchangeAPI : MonoBehaviour
{
    string DownJson;

    public string dollar;
    public float dollarToWon;

    public List<GameObject> targetImg = new List<GameObject>();

    void Start()
    {
        StartCoroutine(GetText());

    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://www.koreaexim.go.kr/site/program/financial/exchangeJSON?authkey=gMss0Xwbzeb6IXyVtbILxClEYfgDFcn5&data=AP01");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            DownJson = www.downloadHandler.text;
            Debug.Log("J" + DownJson);

            // .json 파일 생성
            System.IO.File.WriteAllText(Application.persistentDataPath + "/jsonDATA.json", DownJson.ToString());
            //Debug.Log(jtc.ttb);


            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;

            //Debug.Log("json만들엇어");
            LoadJson();
        }
    }

    // 파싱위해 .json 파일 불러옴
    public void LoadJson()
    {
        Debug.Log("불러오기");

        string Jsonstring = File.ReadAllText(Application.persistentDataPath + "/jsonDATA.json");

        JsonData jsonEx = JsonMapper.ToObject(Jsonstring);

        Debug.Log("제이슨카운트" + jsonEx.Count);

        dollar = jsonEx[jsonEx.Count-1]["deal_bas_r"].ToString();
        dollarToWon = 1 / float.Parse(dollar);
        Debug.Log("오늘 달라 환율 = " + jsonEx[jsonEx.Count-1]["deal_bas_r"].ToString());
        Debug.Log("1원 = " + dollarToWon);
        
        LoadTargetImage();
    }


    public void LoadTargetImage()
    {
        
        for (int i=0;i<8; i++)
        {
            GameObject _obj = GameObject.Find("Target" + i);
            targetImg.Add(_obj);

            int k = i / 2;
            Debug.Log("k = " + k);
            if (k==0)
            {
                targetImg[i].transform.Find("Text (TMP)").GetComponent<TextMeshPro>().text = (dollarToWon*1000).ToString() + " USD";
            }
            else if(k==1)
            {
                targetImg[i].transform.Find("Text (TMP)").GetComponent<TextMeshPro>().text = (dollarToWon*5000).ToString() + " USD";
            }
            else if(k==2)
            {
                targetImg[i].transform.Find("Text (TMP)").GetComponent<TextMeshPro>().text = (dollarToWon*10000).ToString() + " USD";
            }
            else if(k==3)
            {
                targetImg[i].transform.Find("Text (TMP)").GetComponent<TextMeshPro>().text = (dollarToWon*50000).ToString() + " USD";
            }
            else
            {
                Debug.Log("에러에러에러에러에러에러");
            }
        }
    }
}