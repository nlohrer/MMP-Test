using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float Speed = 8f;
    public int MaxHP = 3;
    public int HP = 3; // current HP 
    public float AttackCooldown = 1f; // wie schnell schießen?
    public float InvulOnHitDuration = 0.5f; // wie lange keinen schaden wenn getroffen
    public Vector2 movement;
    public GameObject Projectile;
    public GameObject Gun;
    public bool InvulMode = false;
    public Ability[] Abilities;
    public GameObject[] InkList;
    public AudioClip HitAudio;
    public AudioClip StarPowerupAudio;
    public AudioClip StarPowerDownAudio;
    public int ActiveStars = 0;
    public GameObject PauseOverlay;

    public Shoot shoot;
    public bool CanMoveManually = true; // manuelles steuern des spielers

    private Rigidbody2D Rb2d;
    private SpriteRenderer Renderer;
    private static readonly float InverseSqrt = 1f / Mathf.Sqrt(2f); // berechnung für schräges bewegen 
    private float LastTimeGotHit = 0f; // tracking für invulnerability frames
    private UIManager UIManager;
    private bool GunEnabled = true;
    private bool InFiringMode = false;
    private float OriginalSpeed;
    private bool Paused = false;
    private GunRotation GunRotation;

    void Start()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
        UIManager = FindFirstObjectByType<UIManager>();
        OriginalSpeed = Speed;
        GunRotation = GetComponentInChildren<GunRotation>();
    }

    void Update() // player check
    {
        if (HP <= 0) return; // bei tod des spielers return

        CheckForPause();
        if (Paused)
        {
            return;
        }

        if (CanMoveManually) // movement setzen wenn es gewollt ist
        {
            SetMovement();
        }

        foreach (var ability in Abilities) // alle abilities checken
        {
            if (ability.CanUse()) // kann benutzt werden?
            {
                if (ability is Shield && InvulMode)
                {
                    continue;
                }
                ability.SetReady(true); // ready setzen
                if (ability.CheckForCommand()) // wird taste gedrückt?
                {
                    ability.Use(); // ability benutzen
                }
            }

        }
    }

    private void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Paused)
            {
                Unpause();
            } else
            {
                Pause();
            }
        }
    }

    private void SetMovement() // player movement setzen
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // horizontalen input
        float verticalInput = Input.GetAxisRaw("Vertical"); // verticalen input

        if (horizontalInput == 1) // sprite nach rechts flippen, wenn nach rechts bewegen
        {
            Renderer.flipX = true;

        }
        else if (horizontalInput == -1) // sprite nach links flippen wenn nach links bewegen
        {
            Renderer.flipX = false;
        }
        if (horizontalInput != 0 && verticalInput != 0) // wenn man sich schräg bewegt anpassung damit man nicht zu schnell wird
        {
            horizontalInput *= InverseSqrt;
            verticalInput *= InverseSqrt;
        }


        movement = new Vector2(horizontalInput, verticalInput); // movement setzen
    }

    public void GetHealed(int amount) // implementierung von Life powerup
    {
        if (HP < MaxHP) // wenn HP unter MAx hp amount wieder herstellen
        {
            HP++;
            UIManager.UpdateHearts(HP); // anzeige updaten
        }
    }
    public void GetHit(int damage) // player nimmt schaden
    {
        if (Time.time - LastTimeGotHit < InvulOnHitDuration || InvulMode) // wenn invul mode an keinen schaden nehmen
        {
            return;
        }

        HP -= damage; // hp abziehen
        UIManager.UpdateHearts(HP); // herzen updaten
        if (HP <= 0) // wenn player nun tod gameover einleiten
        {
            StartCoroutine(GameOver());
        }
        else // sonst hit animation einleiten
        {
            StartCoroutine(HitAnimation());
        }
        LastTimeGotHit = Time.time; // tracker für invul frames setzen
        SoundManager.Instance.PlayClip(HitAudio, transform, 0.4f);
    }

    //Maschine gun und Stern start funktion 
    public void MakePowerUp(float duration, int ability) // 0 == stern; 1 == MashineGun
    {
        switch (ability) // checke ob Stern (0) oder Maschine gun (1)
        {
            case 0:
                StartCoroutine(Invulnerability(duration)); // starte Stern
                break;
            case 1:
                StartCoroutine(FiringPower(duration)); // starte Maschine Gun
                break;
        }
    }

    private IEnumerator Invulnerability(float duration)
    {
        ActiveStars++;
        SoundManager.Instance.PlayClip(StarPowerupAudio, transform, 0.1f);
        Speed = OriginalSpeed * 1.5f;
        InvulMode = true; // unsterblichkeit setzen
        Renderer.color = Color.yellow; // stern animation
        GunEnabled = false;
        shoot.CanShoot = false; // schießen des spielers ausmachen
        Gun.gameObject.GetComponent<SpriteRenderer>().enabled = InFiringMode; // für clarity gun sprite disablen
        yield return new WaitForSeconds(duration - 1); // übergebene Duration abwarten
        ActiveStars--;
        if (ActiveStars <= 0)
        {
            Renderer.color = Color.white; // stern color ausmachen
            SoundManager.Instance.PlayClip(StarPowerDownAudio, transform, 0.1f);
            yield return new WaitForSeconds(0.2f);
            Renderer.color = Color.yellow;
            yield return new WaitForSeconds(0.4f);
            Renderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            Renderer.color = Color.yellow;
            yield return new WaitForSeconds(0.1f);
            Renderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            Renderer.color = Color.yellow;
            yield return new WaitForSeconds(0.1f);
            Renderer.color = Color.white;
            if (ActiveStars <= 0)
            {
                Speed = OriginalSpeed;
                GunEnabled = true;
                InvulMode = false;  // unsterblichkeit ausmachen
                shoot.CanShoot = true;  // schießen wieder anmachen
                Gun.gameObject.GetComponent<SpriteRenderer>().enabled = true; // gun sprite wieder an machen 
            }
            else
            {
                Renderer.color = Color.yellow;
            }
        }
    }

    // maschine gun implementation
    private IEnumerator FiringPower(float duration)
    {
        InFiringMode = true;
        Gun.gameObject.GetComponent<SpriteRenderer>().enabled = true; // gun sprite wieder an machen
        float i = 0;
        shoot.CanShoot = false; // manuelles schießen ausmachen
        while (i < duration)
        { // 5 mal die sekunde richtung mauszeiger schießen
            shoot.ShootExternal();
            yield return new WaitForSeconds(0.2f);
            i += 0.2f;
        }
        shoot.CanShoot = true; // manuelles schießen wieder anmachen
        Gun.gameObject.GetComponent<SpriteRenderer>().enabled = GunEnabled; // gun sprite wieder an machen
        InFiringMode = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GunRotation.enabled = false;
        Paused = true;
        PauseOverlay.SetActive(Paused);
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        GunRotation.enabled = true;
        Paused = false;
        PauseOverlay.SetActive(Paused);
    }

    public void UnpauseAndBackToMainMenu()
    {
        Unpause();
        SceneManager.LoadScene(0);
    }

    public void SpawnInk()
    {
        StartCoroutine(Ink());
    }

    private IEnumerator Ink()
    {
        int random = Random.Range(0, InkList.Length - 1);
        var ink = InkList[random];
        ink.SetActive(true);
        yield return new WaitForSeconds(1.9f);
        var inkImage = ink.GetComponent<Image>();
        StartCoroutine(FadeAway(inkImage));
        yield return new WaitForSeconds(0.2f);
        inkImage.color = Color.white;
        ink.SetActive(false);
    }

    private IEnumerator FadeAway(Image image)
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime * 5)
        {
            image.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }


    private IEnumerator GameOver() // gameover implementation
    {
        movement = Vector2.zero; // geschwindigkeit 0 setzen
        Renderer.color = Color.red; // spieler auf rot setzen
        // death animation
        yield return new WaitForSeconds(1f); // 1 sec warten
        SceneManager.LoadScene(2); // GameOverScreen 
    }

    private IEnumerator HitAnimation() //wenn spieler gehitet wird während InvulOnHitDuration rot setzen für clarity
    {
        Renderer.color = Color.red;
        yield return new WaitForSeconds(InvulOnHitDuration);
        Renderer.color = Color.white;
    }

    private void FixedUpdate() // speed setzen
    {
        Rb2d.velocity = movement * Speed;
    }
}
