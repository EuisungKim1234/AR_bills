using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1440, 3040, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
