using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player playerInput;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    private ObstacleHealth ob;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float cameraXrotation;
    public float cameraYrotation;
    public float cameraZrotation;


    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] public ParticleSystem Missile0;
    [SerializeField] public ParticleSystem Missile1;
    [SerializeField] public ParticleSystem Missile2;
    [SerializeField] public ParticleSystem Missile3;
    [SerializeField] GameObject canController;
    

    [SerializeField] Canvas p_Turn;
    [SerializeField] private AudioClip TankMovingSFX;
    [SerializeField] private AudioClip TankShootingSFX;
    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject playerMCamera;
    [SerializeField] GameObject primaryCamera;
    [SerializeField] GameObject ArmatoMissileCamera;
    [SerializeField] GameObject ChiMissileCamera;
    [SerializeField] GameObject CromMissileCamera;

    [SerializeField] GameObject KVMissileCamera;
    [SerializeField] GameObject aim0;
    [SerializeField] GameObject aim1;
    [SerializeField] GameObject aim2;
    [SerializeField] GameObject aim3;
    [SerializeField] public CanvasGroup playerTurnFade;

    BattleSystem BS;
    AudioSource sfx;
    




    bool alreadyAttacked;
    public bool isPlayerDead;
    public float rotateHorizontal;
    bool isMoving;



    private float gravityValue = -9.81f;

    private void Awake()
    {
        // p_camera.enabled = false;
        playerInput = new Player();
        controller = GetComponent<CharacterController>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        enemyHealth = FindObjectOfType<EnemyHealth>();
        sfx = GetComponent<AudioSource>();
        BS = FindObjectOfType<BattleSystem>();
       
    }

    private void OnEnable()
    {
        playerInput.Enable();

    }

    private void OnDisable()
    {
        playerInput.Disable();
    }




    private void Start()
    {

    }

    void Update()
    {
        CheckState();
        playerControls();


    }



    public void CheckState()
    {
        if (BS.state == BattleState.PLAYERTURN)

        {
            int selectedTanks = PlayerPrefs.GetInt("selectedTanks");

            // LoadCameraRotation();
            ShowController();
            // ShowController();
            p_Turn.enabled = true;
            Invoke("PlayerFadeUI", 2f);
            if (selectedTanks == 0)
            {
                aim0.SetActive(true);
            }
            else if (selectedTanks == 1)
            {
                aim1.SetActive(true);
            }
            else if (selectedTanks == 2)
            {
                aim2.SetActive(true);
            }
            else if (selectedTanks == 3)
            {
                aim3.SetActive(true);
            }

            playerCamera.SetActive(true);

            // p_Turn.enabled = true;

        }
        else
        {
            HideController();
            aim0.SetActive(false);
            aim1.SetActive(false);
            aim2.SetActive(false);
            aim3.SetActive(false);
            // playerCamera.transform.rotation = originalPos;
            p_Turn.enabled = false;
            playerCamera.SetActive(false);
            PlayerFadeInUI();



        }



    }

    // void ControllerTrue()
    // {
    //     Controller.enabled = true;

    // }

    void playerControls()
    {

        // Controller.enabled = true;
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;

        }

        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();

        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);//movementInput.y
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            sfx.clip = TankMovingSFX;
            sfx.Play();
            gameObject.transform.forward = move;
            isMoving = true;

        }

        // Player shoots the missile
        if (playerInput.PlayerMain.Fire.triggered && groundedPlayer)
        {

             HideController();
            playerMCamera.SetActive(true);
            // playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            Debug.Log("FIRE!!!");


            int selectedTanks = PlayerPrefs.GetInt("selectedTanks");


            if (!Missile0.isPlaying && !Missile1.isPlaying && !Missile2.isPlaying && !Missile3.isPlaying)
            {



                // p_camera.enabled = false;
                if (selectedTanks == 0)
                {
                    Missile0.Play();
                    ArmatoMissileCamera.SetActive(true);
                }
                else if (selectedTanks == 1)
                {
                    Missile1.Play();
                    ChiMissileCamera.SetActive(true);

                }
                else if (selectedTanks == 2)
                {
                    Missile2.Play();
                    CromMissileCamera.SetActive(true);

                }
                else if (selectedTanks == 3)
                {
                    Missile3.Play();
                    KVMissileCamera.SetActive(true);
                }

                else
                {
                    ArmatoMissileCamera.SetActive(false);
                    ChiMissileCamera.SetActive(false);
                    CromMissileCamera.SetActive(false);
                    KVMissileCamera.SetActive(false);

                }

                sfx.clip = TankShootingSFX;
                sfx.Play();



                if (playerHealth.hitPoints > 1)
                {
                    isPlayerDead = false;
                    Debug.Log("Enemy's turn starts in 5 seconds");

                    if (BS.state == BattleState.PLAYERTURN)
                    {
                        BS.state = BattleState.MISSILE;

                        Invoke("ProceedTurn", 5f);
                    }

                    else
                    {
                        return;


                    }

                }



            }


        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }
    void ProceedTurn()
    {
        BS.state = BattleState.ENEMYTURN;

    }

    private void PlayerFadeUI()
    {
        playerTurnFade.alpha -= Time.deltaTime;
    }

    private void PlayerFadeInUI()
    {
        playerTurnFade.alpha += Time.deltaTime;
    }

    private void HideController()
    {
        canController.SetActive(false);

        // if(canController.isActiveAndEnabled)
        // {
        //     canController.enabled = false;
        // }

        // ControlUI.alpha -= 1;
    }

    private void ShowController()
    {

        canController.SetActive(true);
    }

    // private void SaveCameraRotation()
    // {
    //     cameraXrotation = PlayerPrefs.GetFloat("MyRotationX");
    //     cameraYrotation = PlayerPrefs.GetFloat("MyRotationY");
    //     cameraZrotation = PlayerPrefs.GetFloat("MyRotationZ");

    //     PlayerPrefs.SetFloat("MyRotationX", transform.eulerAngles.x);
    //     PlayerPrefs.SetFloat("MyRotationY", transform.eulerAngles.y);
    //     PlayerPrefs.SetFloat("MyRotationZ", transform.eulerAngles.z);
    // }

    // private void LoadCameraRotation()
    // {
    //     playerCamera.transform.rotation = Quaternion.Euler(cameraXrotation, cameraYrotation, cameraZrotation);
    // }

}

