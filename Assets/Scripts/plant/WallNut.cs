using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : plantbase
{ 
    private string animationName;
    private float state1;
    private float state2;

    public override void OnInitForPlant()
    {
        hp = 3000;
        state1 = (Hp / 3) * 2;
        state2 = Hp / 3;
}

    protected override void HpUpdateEvent()
    { 
        if (Hp < state1 && Hp > state2)
        {
            // ×´Ì¬1
            animationName = "WallNutHurt";
        }
        else if (Hp < state2)
        {
            // ×´Ì¬2
            animationName = "WallNutBroke";
        }
        else
        {
            // Õý³£
            animationName = "WallNut";
        }
        animator.Play(animationName);
    }
}
