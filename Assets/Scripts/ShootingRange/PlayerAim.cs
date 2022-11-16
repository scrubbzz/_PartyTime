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

        // input reading values
        public string horizontalInput;
        public string verticalInput;
        public KeyCode aimAndFireKey;

        public float inputSensitivity = 20f;
        public float chargeValue { get; set; }


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
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] PassablePlayerAimData playerData;

        // charging related values
        [SerializeField] float chargeSpeed = 2f;
        [SerializeField] float chargeLimit = 50f;


        // arrow data
        [SerializeField] GameObject arrowPrefab;
        ArrowProjectile arrow;


        float xInput = 0f;
        float yInput = 0f;


        public float arrowResetTimer = 4.5f;
        float previousResetTime;
        bool resetTimerIsActive = false;


        public int coinCounter;

        public Image crosshairImage;
        //public RectTransform crosshair;

        public Slider chargeBar;
        public TextMeshProUGUI testTextForCoinCounter;


        private void Start()
        {
            //playerData = new PassablePlayerAimData();

            chargeBar.minValue = 0;
            chargeBar.maxValue = chargeLimit;

            xInput = Screen.width / 2;
            yInput = Screen.height / 2;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (playerData.defaultArrowPos == Vector3.zero)
                playerData.defaultArrowPos = this.transform.position + (Vector3.up * 2f);



            GameObject arrowObj = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow = arrowObj.GetComponent<ArrowProjectile>();

            if (arrow == null)
                arrow = arrowObj.AddComponent<ArrowProjectile>(); // make sure we've got a rigidbody on the arrow

            arrow.player = this;
            arrow.ResetState(playerData);

            previousResetTime = arrowResetTimer;
        }


        void Update()
        {
            xInput += Input.GetAxis(playerData.horizontalInput) * playerData.inputSensitivity;
            yInput += Input.GetAxis(playerData.verticalInput) * playerData.inputSensitivity;
            xInput = Mathf.Clamp(xInput, 0, Screen.width);
            yInput = Mathf.Clamp(yInput, 0, Screen.height);


            crosshairImage.rectTransform.position = new Vector3(xInput, yInput);

            chargeBar.value = playerData.chargeValue;
            testTextForCoinCounter.text = "" + coinCounter;


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }

            // check input each frame 
            // charge while holding, then fire on release

            if (Input.GetKeyDown(playerData.aimAndFireKey))
            {
                Cursor.lockState = CursorLockMode.Locked;
                ResetArrow();
            }

            if (Input.GetKey(playerData.aimAndFireKey))
            {
                CalculateCharge();

                Ray aim = Camera.main.ScreenPointToRay(crosshairImage.transform.position);
                Physics.Raycast(aim, out playerData.hit, 50);
                arrow.Aim(playerData);
            }

            if (Input.GetKeyUp(playerData.aimAndFireKey))
            {
                arrow.Fire(playerData);

                resetTimerIsActive = true;
                StartCoroutine(CountdownToArrowReset());
            }


        }



        public void CheckTargetDamage(ShooterTarget target)
        {
            coinCounter += target.CalculateDamageTaken(playerData.chargeValue, this); // add coins if target broke successfully
        }


        void CalculateCharge()
        {
            playerData.chargeValue = Mathf.MoveTowards(playerData.chargeValue, chargeLimit, chargeSpeed * Time.deltaTime);
            playerData.chargeValue = Mathf.Clamp(playerData.chargeValue, 0, chargeLimit);
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
