using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSingleton : MonoBehaviour
{
    public static FruitSingleton instance;
    [SerializeField]
    AudioClip succesAudioclip = null;
    [SerializeField]
    AudioClip failedAudioclip = null;
    [SerializeField]
    AudioSource audioSource = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            onTriggerBehave = new OnTriggerBehave[2];
            int allElemnts = FindObjectsByType<OnTriggerBehave>(FindObjectsSortMode.None).Length;
            if (allElemnts < 2 || allElemnts % 2 > 0)
                Debug.Log("Some problems with Your pairs on scene");
            maxPair = allElemnts / 2;
        }
        else
            Destroy(gameObject);
    }

    int score = 0;
    int maxPair = 0;
    int actualIndex = 0;
    OnTriggerBehave[] onTriggerBehave;

    public void PutFruit(OnTriggerBehave oTB)
    {
        if (actualIndex == 0)
        {
            onTriggerBehave[actualIndex] = oTB;

            actualIndex++;
        }
        else
        {
            if (oTB != onTriggerBehave[0])
            {
                onTriggerBehave[actualIndex] = oTB;
                if (onTriggerBehave[actualIndex].CheckPairCollection(onTriggerBehave[0].pairElment))
                {
                    Destroy(onTriggerBehave[0].gameObject);
                    Destroy(onTriggerBehave[actualIndex].gameObject);
                    score += 1;
                    PlaySuccesSound();
                    if (score == maxPair)
                    {
                        Debug.Log("You win");
                    }
                    actualIndex = 0;
                }
                else
                {
                    onTriggerBehave[0].TurnOffAnimation();
                    onTriggerBehave[0] = onTriggerBehave[1];

                    PlayFailedSound();
                }


            }

        }

    }

    void PlaySuccesSound()
    {
        audioSource.PlayOneShot(succesAudioclip);
        
    }

    void PlayFailedSound()
    {
        audioSource.PlayOneShot(failedAudioclip);
    }
}
