using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
    Normal,
    Bouncy,
    Hard
}

public class Player : MonoBehaviour
{

    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int CurHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        private int _curScore = 0;
        public int CurScore
        {
            get { return _curScore; }
            set { _curScore = value; }
        }

        private int _curCoins = 0;
        public int CurCoins
        {
            get { return _curCoins; }
            set { _curCoins = value; }
        }

        private int _damage = 1;
        public int Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }

        public void Init()
        {
            CurHealth = maxHealth;
        }
    }

    public int fallBoundary = -10;

    [HideInInspector]
    public PlayerStates currentState = PlayerStates.Normal;

    public PlayerStats stats = new PlayerStats();

    public PhysicMaterial bouncyPhysics;
    public PhysicMaterial playerPhysics;

    public float heavyMass = 50.0f;

    //color stuff
    public Color normalColor;
    public Color bouncyColor;
    public Color hardColor;
    private Light pointLight;

    public Material backCubes;

    //sound
    AudioManager audioManager;
    public string hurt;
    //public string hard;

    private void Start()
    {
        stats.Init();
        pointLight = GetComponentInChildren<Light>();

       audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        if (transform.position.y <= fallBoundary)
            DamagePlayer(9999);

        if (Input.GetButtonDown("ChangeState"))
            ToggleStates();

        switch (currentState)
        {
            case PlayerStates.Normal:
                Normal();
                break;
            case PlayerStates.Bouncy:
                Bouncy();
                break;
            case PlayerStates.Hard:
                Hard();
                break;
        }

        UpdateHealth();

    }

    void Normal()
    {
        GetComponent<Renderer>().material.color = normalColor;
        gameObject.GetComponent<Rigidbody>().mass = 1.0f;
        gameObject.GetComponent<SphereCollider>().material = playerPhysics;
        GetComponent<PlayerMovement>().jumpForce = 350.0f;
        stats.maxHealth = 100;
        backCubes.color = normalColor;
    }

    void Bouncy()
    {
        GetComponent<Renderer>().material.color = bouncyColor;
        gameObject.GetComponent<Rigidbody>().mass = 0.5f;
        gameObject.GetComponent<SphereCollider>().material = bouncyPhysics;
        GetComponent<PlayerMovement>().jumpForce = 250.0f;
        stats.maxHealth = 1;
        backCubes.color = bouncyColor;
    }

    void Hard()
    {
        GetComponent<Renderer>().material.color = hardColor;
        gameObject.GetComponent<Rigidbody>().mass = heavyMass;
        gameObject.GetComponent<SphereCollider>().material = playerPhysics;
        GetComponent<PlayerMovement>().jumpForce = 350.0f;
        stats.maxHealth = 100;
        backCubes.color = hardColor;
    }

    public void DamagePlayer(int damage)
    {
        audioManager.PlaySound(hurt);
        stats.CurHealth -= damage;
        if (stats.CurHealth <= 0)
        {
            GameManager.KillPlayer(this);
        }
    }

    void UpdateHealth()
    {
        if(stats.CurHealth == stats.maxHealth)
        {
            pointLight.color = Color.yellow;
        }
        else if(stats.CurHealth < stats.maxHealth)
        {
            pointLight.color = Color.red;
            pointLight.intensity = 300.0f/stats.CurHealth;
        }
    }

    void ToggleStates()
    {
        if(currentState == PlayerStates.Normal)
        {
            if (GameManager.Instance.isHard)
                currentState = PlayerStates.Hard;
            else
                currentState = PlayerStates.Normal;
        }
        else if (currentState == PlayerStates.Hard)
        {
            if (GameManager.Instance.isBouncy)
                currentState = PlayerStates.Bouncy;
            else
                currentState = PlayerStates.Normal;
        }
        else if (currentState == PlayerStates.Bouncy)
        {
            currentState = PlayerStates.Normal;
        }
    }

    private void OnDestroy()
    {
        backCubes.color = normalColor;
    }

}
