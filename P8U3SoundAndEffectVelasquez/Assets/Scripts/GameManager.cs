using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float score;
    private PlayerController playerControllerScript;
    public Transform startingPoint;
    public float lerpSpeed;


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;

        playerControllerScript.gameOver = true;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!playerControllerScript.gameOver)
        {
            if (playerControllerScript.doubleSpeed)
            {
                score += 2;
            }
            else
            {
                score ++;
            }
            Debug.Log("Score" + score);
        }
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfjourney = distanceCovered / journeyLength;

        playerControllerScript.GetComponent<Animator>().SetFloat("SpeedMultiplier", 0.5f);

        while(fractionOfjourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfjourney = distanceCovered / journeyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfjourney);
            yield return null;
        }

        playerControllerScript.GetComponent<Animator>().SetFloat("SpeedMultiplier", 1.0f);
        playerControllerScript.gameOver = false;
    }
}
