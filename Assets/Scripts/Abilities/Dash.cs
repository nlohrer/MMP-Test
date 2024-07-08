using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dash : Ability
{
    public Image DashIcon;
    public float DashTime = 0.2f; // wie lange dashed man
    public float SpeedFactor = 2f; // erhöhung des speeds

    private ProgressBar DashProgressBar; // abklingzeit visuell
    private float TimeSinceLastAbilityUseRatio;
    private float InverseCooldown; // für den visuellen timer


    protected override void Start() // progressbar setup mainly
    {
        base.Start();
        DashProgressBar = DashIcon.GetComponent<ProgressBar>();
        TimeSinceLastAbilityUseRatio = 0f;
        InverseCooldown = 1f / Cooldown;

    }

    private void Update() // logik für progress bar
    {
        TimeSinceLastAbilityUseRatio += Time.deltaTime * InverseCooldown;
        DashProgressBar.Fill = TimeSinceLastAbilityUseRatio;
    }

    public override bool CheckForCommand() // wird space gedrückt?
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public override void SetReady(bool ready) // ready oder nicht ready setzen
    {
        return;
    }

    protected override void InternalUse() // dash ausführen
    {
        TimeSinceLastAbilityUseRatio = 0f;
        DashProgressBar.Fill = TimeSinceLastAbilityUseRatio;
        StartCoroutine(BoostPlayerSpeed());
    }

    private IEnumerator BoostPlayerSpeed() // tatsächliche logik von dash
    {
        Player.CanMoveManually = false; // spieler nur in dash richtung kein manuelles bewegen
        float originalPlayerSpeed = Player.Speed; // sichern vom player speed
        Player.Speed = originalPlayerSpeed * SpeedFactor; // speed erhöhen

        if (Player.movement == Vector2.zero) // standard richtung festgelegt nach links dashen falls spieler sich nicht bewegt
        {
            Player.movement = Vector2.left;
        }

        yield return new WaitForSeconds(DashTime); // DashTime warten

        Player.CanMoveManually = true; // nach DashTime manuelles bewegen wieder anmachen
        Player.Speed = originalPlayerSpeed; // speed wieder auf den normalen speed setzen
    }
}