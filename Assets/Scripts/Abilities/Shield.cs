using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shield : Ability
{
    public Image ShieldIcon;
    public GameObject ShieldCircle;

    private ProgressBar ShieldProgressBar;
    private float TimeSinceLastAbilityUseRatio;
    private float InverseCooldown;

    protected override void Start()
    {
        base.Start();
        ShieldProgressBar = ShieldIcon.GetComponent<ProgressBar>();
        TimeSinceLastAbilityUseRatio = 0f;
        InverseCooldown = 1f / Cooldown;
    }

    private void Update()
    {
        TimeSinceLastAbilityUseRatio += Time.deltaTime * InverseCooldown;
        ShieldProgressBar.Fill = TimeSinceLastAbilityUseRatio;
    }

    public override bool CheckForCommand()
    {
        return Input.GetMouseButtonDown(1);
    }

    public override void SetReady(bool ready)
    {
        return;
    }

    protected override void InternalUse()
    {
        TimeSinceLastAbilityUseRatio = 0f;
        ShieldProgressBar.Fill = TimeSinceLastAbilityUseRatio;
        StartCoroutine(SetInvulnerable());
    }

    private IEnumerator SetInvulnerable()
    {
        Player.InvulMode = true;
        ShieldCircle.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        ShieldCircle.SetActive(false);
        Player.InvulMode = false;
    }
}