using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shield : Ability
{
    public Image ShieldIcon;
    public GameObject ShieldCircle;

    private ProgressBar ShieldProgressBar; // für visuelle progress bar
    private float TimeSinceLastAbilityUseRatio;
    private float InverseCooldown;

    protected override void Start() // setup für visuelle progress bar
    {
        base.Start();
        ShieldProgressBar = ShieldIcon.GetComponent<ProgressBar>();
        TimeSinceLastAbilityUseRatio = 0f;
        InverseCooldown = 1f / Cooldown;
    }

    private void Update() // tracker für visuelle progress bar
    {
        TimeSinceLastAbilityUseRatio += Time.deltaTime * InverseCooldown;
        ShieldProgressBar.Fill = TimeSinceLastAbilityUseRatio;
    }

    public override bool CheckForCommand() // wird rechtsclick gedrückt?
    {
        return Input.GetMouseButtonDown(1);
    }

    public override void SetReady(bool ready) // ability ready setzen 
    {
        return;
    }

    protected override void InternalUse() // ausführung des schilds
    {
        TimeSinceLastAbilityUseRatio = 0f;
        ShieldProgressBar.Fill = TimeSinceLastAbilityUseRatio;
        StartCoroutine(SetInvulnerable());
    }

    private IEnumerator SetInvulnerable() // logik des schilds
    {
        Player.InvulMode = true; // player schaden ausmachen
        ShieldCircle.SetActive(true); // schild sichtbar machen
        yield return new WaitForSeconds(0.75f); // schild dauer abwarten
        ShieldCircle.SetActive(false); // schild unsichtbar machen
        if (Player.ActiveStars <= 0)
        {
            Player.InvulMode = false; // player schaden wieder anmachen
        }
    }
}