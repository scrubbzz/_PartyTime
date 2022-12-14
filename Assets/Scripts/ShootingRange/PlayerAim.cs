using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace StraightShootin
{

    // object that stores all variables the player class uses that are explicitly shared to other classes.
    // passed to other classes through functions 
    #region Player Data Class
    [System.Serializable]
    public class PassablePlayerAimData
    {
   
        public float chargeValue { get; set; }
        public float chargeLimit = 50f;

        public Color arrowColour;

        public Vector3 defaultArrowPos;
        public Quaternion defaultArrowRot;


        public RaycastHit hit;


        public PassablePlayerAimData()
        {
            hit = new RaycastHit();
        }

    }
    # endregion 


    /// <summary>
    /// Handles player input, and sending commands to the arrow that it generates. 
    /// </summary>
    /// TODO: Move variables and input checking to their own smaller classes
    public class PlayerAim : Minigames.Generic.PlayerGeneric, Minigames.Generic.ITimeControllable
    {
        [SerializeField] PassablePlayerAimData playerData;

        #region Non-Passable Data

        // input reading values
        public string horizontalInput;
        public string verticalInput;

        bool usingTouchscreen = false;
        public bool usingMouse;
        public float mouseButton;
        public KeyCode aimAndFireKey;

        float xInput = 0f;
        float yInput = 0f;
        public float inputSensitivity = 20f;


        // charging related values
        [SerializeField] float chargeSpeed = 2f;
        bool isCharging = false;

        [SerializeField] float arrowResetTimer = 4.5f;
        float previousResetTime;
        bool resetTimerIsActive = false;


        // arrow data
        [SerializeField] GameObject arrowPrefab;
        ArrowProjectile arrow;


        // other
        Animator playerAnimator;
        //public int coinCounter;

        public Image crosshairImage;

        public Slider chargeBar;
        public TextMeshProUGUI coinCounterText;

        #endregion

        /// 
        /// 
        /// 
        /// 

        #region Player and Arrow Setting Up
        private void Start()
        {
            if (FindObjectOfType<ShootingRangeTimer>())
            {
                ShootingRangeTimer.OnStarting += OnStartTime;
                ShootingRangeTimer.OnCountingDown += OnTimerTick;
                ShootingRangeTimer.OnStopping += OnStopTime;
            }

            chargeBar.gameObject.SetActive(false);
            coinCounterText.gameObject.SetActive(false);

            if (Application.isMobilePlatform)
            {
                usingTouchscreen = true;
            }

            xInput = Screen.width / 2;
            yInput = Screen.height / 2;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;


            // create and ready our arrow
            GameObject arrowObj = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow = arrowObj.GetComponent<ArrowProjectile>();

            if (arrow == null)
                arrow = arrowObj.AddComponent<ArrowProjectile>(); // make sure we've got the needed script on the arrow

            arrow.player = this;
            arrow.SetUp(playerData);
            arrow.gameObject.SetActive(false);


            playerAnimator = GetComponentInChildren<Animator>();
        }

        public void OnStartTime()
        {
            chargeBar.gameObject.SetActive(true);
            coinCounterText.gameObject.SetActive(true);

            chargeBar.minValue = 0;
            chargeBar.maxValue = playerData.chargeLimit;


            if (playerData.defaultArrowPos == Vector3.zero)
                playerData.defaultArrowPos = this.transform.position + (Vector3.up * 2f);

            arrow.gameObject.SetActive(true);
            arrow.ResetState(playerData);


            previousResetTime = arrowResetTimer;
        }

        public void OnStopTime()
        {
            chargeBar.gameObject.SetActive(false);
            coinCounterText.gameObject.SetActive(false);

            arrow.gameObject.SetActive(false);

            playerAnimator.ResetTrigger("Charging");
            playerAnimator.ResetTrigger("Firing");
            transform.rotation = Quaternion.Euler(transform.rotation.x, playerData.defaultArrowRot.y, transform.rotation.z);
        }
        #endregion

        /// 
        /// 
        /// 
        /// 

        // get input
        void Update()
        {
            CalculateInput();

            crosshairImage.rectTransform.position = new Vector3(xInput, yInput);
        }

        void CalculateInput()
        {
            if (usingTouchscreen)
            {
                Touch touchy = Input.GetTouch(0);
                xInput = touchy.position.x;
                yInput = touchy.position.y;
            }
            else
            {
                xInput += Input.GetAxis(horizontalInput) * inputSensitivity;
                yInput += Input.GetAxis(verticalInput) * inputSensitivity;
                xInput = Mathf.Clamp(xInput, 0, Screen.width);
                yInput = Mathf.Clamp(yInput, 0, Screen.height);
            }
        }



        // done each frame, only while timer is active
        public void OnTimerTick()
        {
            chargeBar.value = playerData.chargeValue;
            coinCounterText.text = "" + coinCount;


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }

            // check input each frame 
            // charge while holding, then fire on release

            if (isCharging)
            {
                ChargeArrow();
            }

            if (!usingTouchscreen)
            {

                if ((Input.GetKeyDown(aimAndFireKey) && !usingMouse) ^
                    (Input.GetMouseButtonDown(0) && usingMouse))
                {
                    StartChargingArrow();
                }

                if ((Input.GetKeyUp(aimAndFireKey) && !usingMouse) ^
                    (Input.GetMouseButtonUp(0) && usingMouse))
                { 
                    FireArrow();
                }

            }
            else
            {
                Touch touchy = Input.GetTouch(0);

                if (touchy.phase == TouchPhase.Began)
                    StartChargingArrow();
                

                if (touchy.phase == TouchPhase.Ended)
                    FireArrow();
            }

        }



        void StartChargingArrow()
        {
            // make sure we're locked to the screen input-wise
            Cursor.lockState = CursorLockMode.Locked;

            // ready arrow for the chargearrow function to be called
            ResetArrow();
            isCharging = true;

            // start the charging animation
            playerAnimator.SetTrigger("Charging");
            playerAnimator.ResetTrigger("Firing");
        }


        // get the hit point for the arrow and increase charge
        void ChargeArrow()
        {
            // turn the player's z axis to face where the arrow points
            transform.Rotate(Vector3.up, Mathf.DeltaAngle(transform.forward.x,
                                                              arrow.gameObject.transform.forward.x) * 10);

            // calculate the increase in charge value. clamp to not go over the limit
            playerData.chargeValue = Mathf.MoveTowards(playerData.chargeValue, 
                                                       playerData.chargeLimit, 
                                                       chargeSpeed * Time.deltaTime);
            playerData.chargeValue = Mathf.Clamp(playerData.chargeValue, 0, playerData.chargeLimit);

            // raycast to where the crosshair's onscreen position corresponds to in the world
            Ray aim = Camera.main.ScreenPointToRay(crosshairImage.transform.position);
            Physics.Raycast(aim, out playerData.hit, 50);

            // give the arrow data to use while charging (eg aim direction, charge sfx)
            arrow.Aim(playerData);
        }

        void FireArrow()
        {
            // have arrow shoot itself with the provided data
            isCharging = false;
            arrow.Fire(playerData);

            // begin counting down to resetting the arrow state
            resetTimerIsActive = true;
            StartCoroutine(CountdownToArrowReset());

            // play firing animation
            playerAnimator.SetTrigger("Firing");
            playerAnimator.ResetTrigger("Charging");
            //resetAllowed = false;
            //StartCoroutine(CountdownToResetAllow());
        }


        /// 
        /// 
        /// 
        /// 


        // call target's function, add its coin value if broken successfully
        public void CheckTargetDamage(BreakableTarget target)
        {
            coinCount += target.CalculateDamageTaken(playerData.chargeValue, this); // add coins if target broke successfully
        }


        void ResetArrow()
        {
            arrow.ResetState(playerData);

            arrowResetTimer = previousResetTime;
            resetTimerIsActive = false;

            playerData.chargeValue = 0;
        }

        IEnumerator CountdownToArrowReset()
        {
            if (!resetTimerIsActive)
            {
                arrowResetTimer = previousResetTime;
                yield break;
            }

            arrowResetTimer -= 2 * Time.deltaTime;

            if (arrowResetTimer <= 0)
            {
                ResetArrow();
                yield break;
            }

            yield return null;
            StartCoroutine(CountdownToArrowReset());

        }

    }
}
