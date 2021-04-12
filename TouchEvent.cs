using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchEvent : MonoBehaviour
{
    public GameObject totalTxt;
    public string obj;
    int count = 0;
    public float totalSum = 0;
    public float totalSum_final = 0;

    public List<float> sumData = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcClick();
    }

    void ProcClick()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            ClickPosProc(Input.mousePosition);

        }


    }


    //터치 좌표에 있는 오브젝트를 구한다.

    void ClickPosProc(Vector3 tPos)
    {

        Ray ray = Camera.main.ScreenPointToRay(tPos);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            obj = hit.collider.gameObject.GetComponent<TextMeshPro>().text;
            string[] sp = obj.Split('\x020');
            sumData.Add(float.Parse(sp[0]));

            Debug.Log(count + "     " + sumData[count]);
            count++;

            CalculateCurrency();    
            //Debug.Log("오브젝트" + obj);
        }

    }

    public void CalculateCurrency()
    {
        for(int i=0; i<sumData.Count; i++)
        {
            totalSum += sumData[i];            
        }
        totalSum_final = totalSum;
        totalTxt.GetComponent<TextMeshProUGUI>().text = totalSum_final.ToString();
        Debug.Log("총액은 " + totalSum_final);
        totalSum = 0;  
    }

    public void RenewData()
    {
        count = 0;
        totalSum_final = 0;
        totalTxt.GetComponent<TextMeshProUGUI>().text = totalSum_final.ToString();
        sumData.Clear();
    }
}
