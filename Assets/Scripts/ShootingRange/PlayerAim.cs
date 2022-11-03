using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{

    string horizontalInput = "Mouse X";
    string verticalInput = "Mouse Y";
    float xInput = 0f;
    float yInput = 0f;

    // charging related values
    float chargeValue = 0f;
    public float chargeSpeed = 2f;
    public float chargeLimit = 50f;
    public float arrowSpeedMultiplier = 1f;

    // arrow data
    public GameObject arrowPrefab;
    GameObject arrowInstance;
    Rigidbody arrowRB;

    public Vector3 defaultArrowPos;
    public Quaternion defaultArrowRot;


    public float arrowResetTimer = 4.5f;
    float previousResetTime;
    bool resetTimerIsActive = false;


    public LayerMask targetLayer;
    RaycastHit hit;
    bool hasHitTarget;


    int coinCounter;

    public Image cursorImage;

    public Slider chargeBar;
    public TextMeshProUGUI testTextForCoinCounter;


    private void Awake()
    {
        chargeBar.minValue = 0;
        chargeBar.maxValue = chargeLimit;

        arrowInstance = Instantiate(arrowPrefab, defaultArrowPos, Quaternion.identity);
        arrowRB = arrowInstance.GetComponent<Rigidbody>();

        if (arrowRB == null)
            arrowRB = arrowInstance.AddComponent<Rigidbody>(); // make sure we've got a rigidbody on the arrow
        

        previousResetTime = arrowResetTimer;
    }

    private void Start()
    {
        hit = new RaycastHit();

        Cursor.visible = false;

        arrowRB.isKinematic = true;
        arrowRB.useGravity = false;

    }

    void Update()
    {
        xInput += Input.GetAxis(horizontalInput);
        yInput += Input.GetAxis(verticalInput);


        //cursorImage.rectTransform.position = new Vector3(xInput * 6, yInput * 6);
        cursorImage.rectTransform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        chargeBar.value = chargeValue;

        testTextForCoinCounter.text = "" + coinCounter;


        // reset arrow
        if (Input.GetMouseButtonDown(0))
        {
            ResetArrow();
        }

        if (Input.GetMouseButton(0))
        {
            chargeValue = Mathf.MoveTowards(chargeValue, chargeLimit, chargeSpeed * Time.deltaTime);
            chargeValue = Mathf.Clamp(chargeValue, 0, chargeLimit);

            arrowInstance.transform.LookAt(Input.mousePosition);

            //Vector3 aimPoint = Camera.main.ScreenToViewportPoint(cursorImage.transform.position);

            //arrowInstance.transform.Rotate(Vector3.up, xInput * 0.25f);
            //arrowInstance.transform.Rotate(Vector3.left, yInput * 0.25f);

            //arrowInstance.transform.rotation = Quaternion.Euler(aimPoint.x * Input.mousePosition.x, aimPoint.y * Input.mousePosition.y, 0);

        }

        if (Input.GetMouseButtonUp(0))
        {

            Ray aim = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(aim, out hit, 50); // set the hit variable via raycasting


            arrowRB.useGravity = true;
            arrowRB.isKinematic = false;

            arrowInstance.transform.LookAt(hit.collider.transform.position);
            arrowRB.AddForce(arrowInstance.transform.forward * chargeValue * arrowSpeedMultiplier);
            hasHitTarget = false;


            resetTimerIsActive = true;
            StartCoroutine(CountdownToArrowReset());

            /*
            if (Physics.Raycast(aim, out hit, 50, targetLayer))
            {

                ShooterTarget target = hit.collider.gameObject.GetComponent<ShooterTarget>();

                coinCounter += target.CalculateDamageTaken(chargeValue, this); // add coins if target broke successfully
            }
            else
                Debug.Log("didn't hit anything");
            */

        }

        // if collided with something on the target layer
        if (Physics.CheckBox(arrowInstance.transform.position, new Vector3(.5f, .5f, .5f), arrowInstance.transform.rotation, targetLayer) && !hasHitTarget)
        {

            ShooterTarget target = hit.collider.gameObject.GetComponent<ShooterTarget>();

            coinCounter += target.CalculateDamageTaken(chargeValue, this); // add coins if target broke successfully

            //chargeValue = 0;

            hasHitTarget = true;
            hit = new RaycastHit();
        }

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


    void ResetArrow()
    {

        //arrowRB.isKinematic = false;
        arrowRB.useGravity = false;
        arrowRB.isKinematic = true;
        arrowRB.velocity = Vector3.zero; // stop the arrow

        arrowInstance.transform.position = defaultArrowPos; // reset the arrow's transform
        arrowInstance.transform.rotation = defaultArrowRot;

        arrowResetTimer = previousResetTime; // reset timer and charge
        resetTimerIsActive = false;

        chargeValue = 0;

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
