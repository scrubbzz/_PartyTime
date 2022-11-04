using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{
    // input reading values
    public string horizontalInput = "Mouse X";
    public string verticalInput = "Mouse Y";
    public float inputSensitivity = 5f;
    float xInput = 0f;
    float yInput = 0f;

    // charging related values
    float chargeValue = 0f;
    public float chargeSpeed = 2f;
    public float chargeLimit = 50f;

    // arrow data
    public GameObject arrowPrefab;
    ArrowProjectile arrow;

    public float arrowResetTimer = 4.5f;
    float previousResetTime;
    bool resetTimerIsActive = false;


    RaycastHit hit;

    int coinCounter;

    public Image cursorImage;

    public Slider chargeBar;
    public TextMeshProUGUI testTextForCoinCounter;


    private void Awake()
    {
        chargeBar.minValue = 0;
        chargeBar.maxValue = chargeLimit;

        GameObject arrowObj = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        arrow = arrowObj.GetComponent<ArrowProjectile>();

        if (arrow == null)
            arrow = arrowObj.AddComponent<ArrowProjectile>(); // make sure we've got a rigidbody on the arrow

        arrow.player = this;

        previousResetTime = arrowResetTimer;
    }

    private void Start()
    {
        hit = new RaycastHit();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        xInput = Screen.width / 2;
        yInput = Screen.height / 2;
    }

    void Update()
    {
        xInput += Input.GetAxis(horizontalInput) * inputSensitivity;
        yInput += Input.GetAxis(verticalInput) * inputSensitivity;


        cursorImage.rectTransform.position = new Vector3(xInput, yInput);

        chargeBar.value = chargeValue;
        testTextForCoinCounter.text = "" + coinCounter;


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        // reset arrow, timer and charge
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            ResetArrow();
        }

        // increase charge, rotate arrow to raycast point
        if (Input.GetMouseButton(0))
        {
            chargeValue = Mathf.MoveTowards(chargeValue, chargeLimit, chargeSpeed * Time.deltaTime);
            chargeValue = Mathf.Clamp(chargeValue, 0, chargeLimit);

            Ray aim = Camera.main.ScreenPointToRay(cursorImage.transform.position);
            Physics.Raycast(aim, out hit, 50);
            arrow.Aim(hit.point);

        }

        // launch arrow, activate reset timer
        if (Input.GetMouseButtonUp(0))
        {

            //Ray aim = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Physics.Raycast(aim, out hit, 50); // set the hit variable via raycasting
            arrow.Fire(chargeValue);

            resetTimerIsActive = true;
            StartCoroutine(CountdownToArrowReset());

        }

        // if collided with something on the target layer
        /*if (Physics.CheckBox(arrow.transform.position, new Vector3(.5f, .5f, .5f), arrow.transform.rotation, targetLayer) && !hasHitTarget)
        {

            ShooterTarget target = hit.collider.gameObject.GetComponent<ShooterTarget>();

            coinCounter += target.CalculateDamageTaken(chargeValue, this); // add coins if target broke successfully

            //chargeValue = 0;

            hasHitTarget = true;
            hit = new RaycastHit();
        }*/

    }

    public void CheckTargetDamage(ShooterTarget target)
    { 
        coinCounter += target.CalculateDamageTaken(chargeValue, this); // add coins if target broke successfully
    }

    void ResetArrow()
    {
        arrow.ResetState();

        arrowResetTimer = previousResetTime; // reset timer and charge
        resetTimerIsActive = false;

        chargeValue = 0;

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


    /*void InstantiateArrow(Ray aim, RaycastHit hit)
    {
        GameObject arrowInstance = Instantiate(arrowObj, aim.origin, Quaternion.identity);
        arrowInstance.transform.LookAt(hit.collider.transform.position);

        Rigidbody arrowRB = arrowInstance.GetComponent<Rigidbody>();
        if (arrowRB == null)
        {
            arrowRB = arrowInstance.AddComponent<Rigidbody>();
        }

        arrowRB.velocity *= chargeValue * arrowSpeedMultiplier;
    }*/

}
