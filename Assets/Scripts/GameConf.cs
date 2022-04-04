using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConf", menuName = "GameConf")]
public class GameConf : ScriptableObject
{
    [Header("“Ù¿÷")]
    public GameObject EFAudio;
    public AudioClip ButtonClick;
    public AudioClip Pause;
    public AudioClip Shovel;
    public AudioClip Place;
    public AudioClip SunClick;

    public AudioClip ZombieEat;
    public AudioClip ZombieHurtForPea;
    public AudioClip ZombieGroan;

    public AudioClip ZombieComing;

    public AudioClip GameOver;

    [Tooltip("Sunlight")]
    public GameObject Sun;

    [Tooltip("SunFlower")]
    public GameObject SunFlower;
    public GameObject Peashooter;
    public GameObject HighNut;
    public GameObject CherryBomb;
    public GameObject WallNut;

    public GameObject pea;

    public Sprite Bullet;
    public Sprite BulletHit;

    public GameObject ZombieHead;
    public GameObject Zombie;
    public GameObject ZombieDead;

    

    //public 

}
