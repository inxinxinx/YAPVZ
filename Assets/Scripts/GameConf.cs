using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConf", menuName = "GameConf")]
public class GameConf : ScriptableObject
{
    [Tooltip("Sunlight")]
    public GameObject Sun;

    [Tooltip("SunFlower")]
    public GameObject SunFlower;
    public GameObject Peashooter;
    public GameObject HighNut;
    public GameObject CherryBomb;

    public GameObject pea;

    public Sprite Bullet;
    public Sprite BulletHit;

    public GameObject ZombieHead;
    public GameObject Zombie;
    public GameObject ZombieDead;

}
