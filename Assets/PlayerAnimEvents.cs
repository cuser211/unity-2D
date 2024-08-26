using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void AnimationTriggers()
    {
        player.AttackOver();
    }
}
