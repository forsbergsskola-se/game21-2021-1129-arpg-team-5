using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Team5.Entities;
using UnityEngine;

public class MusicEnableAndDisable : MonoBehaviour
{
//     public Entity BossEnity;
//     public StudioEventEmitter BossMusic;
//
//     public StudioEventEmitter NormalMusic;
//
//     public Transform Player;
//
//     public Transform Boss;
//
//     private bool isRunning;
//     // Start is called before the first frame update
//     void Start()
//     {
//         NormalMusic.Play();
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         if (BossEnity.IsDead)
//         {
//             BossMusicStop();
//             enabled = false;
//         }
//         else if (Vector3.Distance(Player.position, Boss.position) <= 70)
//         {
//             BossMusicStart();
//         }
//         else
//         {
//             BossMusicStop();
//         }
//         
//     }
//
//     void BossMusicStart()
//     {
//         if (!isRunning)
//         {
//             isRunning = true;
//             NormalMusic.Stop();
//             BossMusic.Play();
//         }
//     }
//     void BossMusicStop()
//     {
//         if (isRunning)
//         {
//             isRunning = false;
//             NormalMusic.Play();
//             BossMusic.Stop();
//         }
//     }
}
