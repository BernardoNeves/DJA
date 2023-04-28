using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item
{
    public virtual void Update(PlayerHealth playerHealth, int stacks)
    {

    }

    public virtual void Onhit(EnemyHealth enemyHealth, int stacks)
    {

    }
}

public class HealingItem : Item
{
    public override void Update(PlayerHealth playerHealth, int stacks)
    {
        playerHealth.Heal(3 + 2 * stacks);
        //GameManager.instance.player.GetComponent<PlayerHealth>().Heal(3 + 2 * stacks
    }
}

public class FireDamageItem : Item
{
    public override void Onhit(EnemyHealth enemyHealth, int stacks)
    {
        enemyHealth.Damage(3 + 2 * stacks);
    }
}