using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public float Cooldown;

    protected float TimeAbilityWasLastUsed = -1f;
    protected Player Player;

    public abstract bool CheckForCommand();
    public abstract void SetReady(bool ready);
    protected abstract void InternalUse();

    protected virtual void Start()
    {
        Player = FindFirstObjectByType<Player>();
    }

    public void Use()
    {
        if (CanUse())
        {
            InternalUse();
            TimeAbilityWasLastUsed = Time.time;
            SetReady(false);
        }
    }

    public bool CanUse()
    {
        return Time.time - TimeAbilityWasLastUsed >= Cooldown;
    }
}