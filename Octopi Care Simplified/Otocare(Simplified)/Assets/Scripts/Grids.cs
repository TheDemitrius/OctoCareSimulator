using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grids : MonoBehaviour {

    public GameObject[] LocomotionZones;
    public ParticleSystem EmotionParticles;
    public ParticleSystemRenderer EMPRenderer;
    Transform Target;
    public float Speed = 0.5f, SlideSpeed = 9;
    public float RotSpeed = 0.5f;
    public float closeDistance = 5.0f;
    public bool Locomoting = false, Stopped = false,EmotionalOverride = false,WithToy = false;
    public string Override;
    public GameObject[] Expression,Clothing;
    public Material Happy, Sad, MoreBubbles;


    // Use this for initialization
    void Start () {
        LocomotionZones = GameObject.FindGameObjectsWithTag("LocomotionZone");
        StartCoroutine("WeAreGoingThere");
        if (EmotionalOverride)
        {
            Sadness();
        }
	}
	
	// Update is called once per frame
    public IEnumerator WeAreGoingThere ()
    {
        
        Stopped = true;
        Locomoting = true;
        if (!WithToy)
        {
            int TargetNo = (Random.Range(0, LocomotionZones.Length));
            Target = LocomotionZones[TargetNo].transform;
        }
        else
        {
            GameObject Toy = GameObject.FindGameObjectWithTag("LostToy");
            if (Toy != null)
            {
                Target = Toy.transform;
            }
            else
            {
                WithToy = false;
            }
            
        }
        
        yield return new WaitForSeconds(Random.Range(1,10));
        Locomoting = false;
    }

    //I'm sure there is a more efficient way to do this - but right at this moment I cannot think of it!
    public void Toy()
    {
        if (!EmotionalOverride)
        {
            foreach (GameObject F in Expression)
            {
                F.SetActive(false);
            }
            Expression[3].SetActive(true);
        }
            EMPRenderer.material = Happy;
            EmotionParticles.Play();
        StartCoroutine("ResetEmotions");
        
    }
    public void Wash()
    {
        if (!EmotionalOverride)
        {
            foreach (GameObject F in Expression)
            {
                F.SetActive(false);
            }

            Expression[5].SetActive(true);
        }
        EMPRenderer.material = MoreBubbles;
        EmotionParticles.Play();
        StartCoroutine("ResetEmotions");
    }
    public void Food()
    {
        if (!EmotionalOverride)
        {
            foreach (GameObject F in Expression)
            {
                F.SetActive(false);
            }
            Expression[2].SetActive(true);
            EMPRenderer.material = Happy;
            EmotionParticles.Play();
            StartCoroutine("ResetEmotions");
        }
    }
    public void Sadness()
    {
        foreach (GameObject F in Expression)
        {
            F.SetActive(false);
        }
        Expression[1].SetActive(true);
    }

    void Update () {
        if (Target != null)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //print(GetComponent<Rigidbody2D>().velocity.magnitude);
            }


            Vector2 offset = Target.position - transform.position;
            float sqrLen = offset.sqrMagnitude;

            // square the distance we compare with
            if (sqrLen > closeDistance * closeDistance)
            {

                //Quaternion targetRotation = Quaternion.LookRotation(Target.position - transform.position);
                //float str = Mathf.Min(RotSpeed * Time.deltaTime, 1);
                //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
                var dir = Target.position - transform.position;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle * RotSpeed, (Vector3.forward)); 
                //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward * RotSpeed);
                print("Going)");
                transform.Translate(Vector2.up * Time.deltaTime * Speed);

            }
            if (sqrLen < closeDistance * closeDistance)
            {
                //emit here//

                //float str = Mathf.Min(RotSpeed * Time.deltaTime, 1);
                var dir = (new Vector3(5,transform.position.y,transform.position.z) - transform.position);
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle * 0, (Vector3.forward));
                

                if (Locomoting == false)
                {
                    StartCoroutine("WeAreGoingThere");
                }
            }
            
        }
	}
    public IEnumerator ResetEmotions()
    {
        yield return new WaitForSeconds(3);
        if (!EmotionalOverride)
        {
            foreach (GameObject F in Expression)
            {
                F.SetActive(false);
            }

            Expression[0].SetActive(true);
        }
    }
}
