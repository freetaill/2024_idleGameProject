using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{

    SoundManager soundManager;
    public string testSoundName;
    public string testBGMName;

    void Start()
    {
        soundManager = SoundManager.Instance;
        soundManager.PlayBGM("bgm01");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //soundManager.PlayBGM("bgm01");
            soundManager.Play(testSoundName);
        }

        if (Input.GetMouseButtonDown(1))
        {
            //soundManager.PlayBGM("bgm01");
            soundManager.PlayBGM(testBGMName);
        }
    }


}
