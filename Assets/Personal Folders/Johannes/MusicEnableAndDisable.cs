using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class MusicEnableAndDisable : MonoBehaviour
{
    public StudioEventEmitter BossMusic;

    public StudioEventEmitter NormalMusic;

    public Transform Player;

    public Transform Boss;
    // Start is called before the first frame update
    void Start()
    {
        NormalMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Player.position, Boss.position) == 10)
        {
            NormalMusic.Stop();
            BossMusic.Play();
        }
    }
}
