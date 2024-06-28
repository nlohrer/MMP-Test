using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dash : Ability
{
    public Image DashIcon;
    public float DashTime = 0.2f;
    public float SpeedFactor = 2f;

    private ProgressBar DashProgressBar;
    private float TimeSinceLastAbilityUseRatio;
    private float InverseCooldown;


    protected override void Start()
    {
        base.Start();
        DashProgressBar = DashIcon.GetComponent<ProgressBar>();
        TimeSinceLastAbilityUseRatio = 0f;
        InverseCooldown = 1f / Cooldown;

    }

    private void Update()
    {
        TimeSinceLastAbilityUseRatio += Time.deltaTime * InverseCooldown;
        DashProgressBar.Fill = TimeSinceLastAbilityUseRatio;
    }

    public override bool CheckForCommand()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public override void SetReady(bool ready)
    {
        return;
    }

    protected override void InternalUse()
    {
        TimeSinceLastAbilityUseRatio = 0f;
        DashProgressBar.Fill = TimeSinceLastAbilityUseRatio;
        StartCoroutine(BoostPlayerSpeed());
    }

    private IEnumerator BoostPlayerSpeed()
    {
        Player.CanMoveManually = false;
        float originalPlayerSpeed = Player.Speed;
        Player.Speed = originalPlayerSpeed * SpeedFactor;

        if (Player.movement == Vector2.zero)
        {
            Player.movement = Vector2.left;
        }

        yield return new WaitForSeconds(DashTime);

        Player.CanMoveManually = true;
        Player.Speed = originalPlayerSpeed;
    }
}