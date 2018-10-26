using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grids : MonoBehaviour {

    public GameObject[] LocomotionZones;
    Transform Target;
    public float Speed = 0.5f;
    public float RotSpeed = 0.5f;
    public float closeDistance = 5.0f;
    public bool Locomoting = false, Stopped = false;

    // Use this for initialization
    void Start () {
        LocomotionZones = GameObject.FindGameObjectsWithTag("LocomotionZone");
        StartCoroutine("WeAreGoingThere");
	}
	
	// Update is called once per frame
    public IEnumerator WeAreGoingThere ()
    {
        
        Stopped = true;
        Locomoting = true;
        int TargetNo = (Random.Range(0, LocomotionZones.Length));
        Target = LocomotionZones[TargetNo].transform;
        
        yield return new WaitForSeconds(Random.Range(1,10));
        Locomoting = false;
    }

    public IEnumerator Puff ()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
        yield return null;
    }

	void Update () {
        if (Target != null)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                print(GetComponent<Rigidbody>().velocity.magnitude);
            }


            Vector3 offset = Target.position - transform.position;
            float sqrLen = offset.sqrMagnitude;

            // square the distance we compare with
            if (sqrLen > closeDistance * closeDistance)
            {
               
                Quaternion targetRotation = Quaternion.LookRotation(Target.position - transform.position);
                float str = Mathf.Min(RotSpeed * Time.deltaTime, 1);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
                StartCoroutine("Puff");
                //transform.Translate(Vector3.forward * Time.deltaTime * Speed);

            }
            if (sqrLen < closeDistance * closeDistance)
            {
                //emit here//
                
                float str = Mathf.Min(RotSpeed * Time.deltaTime, 1);
                Quaternion Sit = Quaternion.LookRotation(new Vector3(transform.position.x, 50, 0) - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, Sit, str);
                if (Stopped)
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    
                    GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f));
                    Stopped = false;
                }
                if (Locomoting == false)
                {
                    StartCoroutine("WeAreGoingThere");
                }
            }
            
        }
	}
}
