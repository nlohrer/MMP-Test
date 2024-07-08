using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public float Cooldown; 

    protected float TimeAbilityWasLastUsed = -1f; // wann wurde die Ability zuletzt benutzt
    protected Player Player; 

    public abstract bool CheckForCommand(); // check ob taste gedrückt wird
    public abstract void SetReady(bool ready); 
    protected abstract void InternalUse(); // fähigkeit wird tatsächlich ausgeführt

    protected virtual void Start()
    {
        Player = FindFirstObjectByType<Player>();
    }

    public virtual void Use()
    {
        if (CanUse()) // kann ability benutzt werden? 
        {
            InternalUse(); // benutze ability
            TimeAbilityWasLastUsed = Time.time; // last used auf jetzt setzen
            SetReady(false); // ability auf nicht ready setzen
        }
    }

    public bool CanUse() // kann die ability benutzt werden?
    {
        return Time.time - TimeAbilityWasLastUsed >= Cooldown;
    }
}
