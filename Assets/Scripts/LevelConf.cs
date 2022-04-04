using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConf
{
    public float firstZombie;
    public float createZombieCD;
    public float halfWaveCD;
    public float lastWaveCD;
    public float lastWaveZombies;

    public void getLevelConf(int level)
    {
        switch (level)
        {
            case 1:
                firstZombie = 5;
                createZombieCD = 10;
                halfWaveCD = 1000000;
                lastWaveCD = 120;
                lastWaveZombies = 8;
                return;
                
            case 2:
                firstZombie = 12;
                createZombieCD = 8;
                halfWaveCD = 100;
                lastWaveCD = 180;
                lastWaveZombies = 20;
                return;  
                        
            default:
                break;
        }
    }
    
}
