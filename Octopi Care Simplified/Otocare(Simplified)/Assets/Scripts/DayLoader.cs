using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayLoader : MonoBehaviour {
    //private AsyncOperation ASL;
    public bool Reset = false;
    public int Override = 0;
    // Use this for initialization
    void Start() {
        //Set Up the Day by heading to correct scene if this isn't the right one!

        if (Reset)
        {
            print("Reset Initiated");
            PlayerPrefs.SetInt("CycleN", 0);

        }
        if (Override != 0)
        {
            print("Override Setting " + Override + " Engaged");
            PlayerPrefs.SetInt("CycleN", Override);
            int Cycle = PlayerPrefs.GetInt("CycleN");
            print("This Cycle is No# " + Cycle);
            SceneManager.LoadSceneAsync(Cycle + 1);
        }
        else
        {
            int Cycle = PlayerPrefs.GetInt("CycleN");
            print("This Cycle is No# " + Cycle);
            SceneManager.LoadSceneAsync(Cycle + 1);
        }

    }



    // Update is called once per frame
    void Update () {
		
	}
}
