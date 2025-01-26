using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{

    [SerializeField] GameObject jumpScare;
    private float jumpScareChance = 0.1f;
    [SerializeField] bool TimeBased = false;
    [SerializeField] float JumpScareDuration = 0.2f;

    private bool isJumpScareTriggered;


    void Start()
    {
        if (TimeBased) StartCoroutine(TimeBasedJumpScare());
    }

    void Update()
    {


        ////Only for testing
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    Camera mainCam = Camera.main;
        //    Vector3 imageSpot = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z + 1);
        //    GameObject clone = Instantiate(jumpScare, imageSpot, Quaternion.identity);
        //    Destroy(clone);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !isJumpScareTriggered)
        {
                Camera mainCam = Camera.main;
                Vector3 imageSpot = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z + 1);
                GameObject clone = Instantiate(jumpScare, imageSpot, Quaternion.identity);
                StartCoroutine(DelayDeleteJumpscare(clone, JumpScareDuration));
                isJumpScareTriggered = true;
        }
    }

    /// <summary>
    /// For time based jump scare
    /// </summary>
    /// <returns></returns>
    private IEnumerator TimeBasedJumpScare()
    {
        while (true)
        {
            float temp = Random.Range(0f, 1f);
            if (temp < jumpScareChance)
            {
                Camera mainCam = Camera.main;
                Vector3 imageSpot = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z + 1);
                GameObject clone = Instantiate(jumpScare, imageSpot, Quaternion.identity);
                StartCoroutine(DelayDeleteJumpscare(clone, JumpScareDuration));
                isJumpScareTriggered = true;
            }

            if (isJumpScareTriggered)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator DelayDeleteJumpscare(GameObject jumpScare, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(jumpScare);
    }
}
