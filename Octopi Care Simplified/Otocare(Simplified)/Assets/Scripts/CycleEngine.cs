using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleEngine : MonoBehaviour {
    public int PureActions = 0, Cycle = 0,DayRequirement;
    public int[] DayRequirements;
    public GameObject[] ClotheStuffsCC,ClotheStuffsNC,Blankets;
    


    // Use this for initialization


    void Start () {
        //Will draw day number from player prefs here
        Cycle = PlayerPrefs.GetInt("CycleN");
        DayRequirement = DayRequirements[Cycle];
        //This way, we can array the requirement for each day. Any positive action (clicking on backpack etc) can be put here as something needed.
        //Variable actions we'll have to see about.
        
        
	}


	
	// Update is called once per frame
	void Update () {
		if (PureActions == DayRequirement)
        {
            Reward();
        }
        if (Input.GetKey (KeyCode.LeftControl))
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    PlayerPrefs.SetInt("CycleN", 0);
                    Application.Quit();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
    void Reward()
    {
        ClotheStuffsCC[Cycle].SetActive(true);
        ClotheStuffsNC[Cycle].SetActive(true);
        if (Cycle == 1 || Cycle == 3 || Cycle == 5)
        {
            foreach (GameObject Sleepy in Blankets)
            {
                Sleepy.SetActive(true);
            }
        }
        if (Cycle == 2 ||Cycle == 4)
        {
            ClotheStuffsCC[0].SetActive(true);
        }
    }

    public void PureAction()
    {
        PureActions++;
    }
    public void EndCycle()
    {
        Cycle++;
        PlayerPrefs.SetInt("CycleN", Cycle);
        //Update Day Number in player prefs
        Application.Quit();
        Destroy(gameObject);
        print("Play Again Soon! <3");
    }
}
