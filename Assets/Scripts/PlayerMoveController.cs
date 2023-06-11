using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float movespeed = 5f;
    public float maxSpeed = 5f;
    private Vector3 movement;
    // public GameObject proyectil2;

    private Arma1 proyectile1;



    public float fireRatioAttack1 = 0.1f;
    public float fireRatioAttack2 = 1f;
    private bool attack1Wait;
    private bool attack2Wait;




    private Transform characterTransform;
    private Camera mainCamera;
    public float rotationSpeed = 180f;

    public LayerMask roofLayerMask;
    public GameObject player;
    private GameObject currentRoof;
    private GameObject previousRoof = null;



    private Rigidbody proyectileRb;
    public Rigidbody bullet;
    public Transform fireposition;
    public float proyectileForce = 20;
    void Start()
    {
        characterTransform = GetComponent<Transform>();
        mainCamera = Camera.main;

        //transform.localPosition = new Vector3(0, 1, 0);
        playerRb = GetComponent<Rigidbody>();
        attack1Wait = false;
        //   movement.z = 0;
    }

    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        Vector3 rayDirection = (player.transform.position - mainCamera.transform.position).normalized;
        /*
        Ray rayCamera = new Ray(mainCamera.transform.position, rayDirection);
        RaycastHit hitRoof;

        Debug.DrawRay(rayCamera.origin, rayCamera.direction, Color.green);
        if (Physics.Raycast(rayCamera, out hitRoof, Mathf.Infinity, roofLayerMask))
        {
            currentRoof = hitRoof.collider.gameObject;
            Transparent transparent = hitRoof.collider.GetComponent<Transparent>();
            bool isTransparent = transparent.IsTransparent;
            if (!isTransparent)
            {
                //string objectName = hitRoof.collider.gameObject.name;
                // Debug.Log("Raycast intersectó con: " + objectName);
                StartCoroutine(hitRoof.collider.GetComponent<ITransparent>().TransparentOn());
                previousRoof = currentRoof;
            }
            if (previousRoof != currentRoof)
            {
                StartCoroutine(previousRoof.GetComponent<Transparent>().TransparentOff());
            }
        }
        */



        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, characterTransform.position.y, hit.point.z);
            Vector3 direction = targetPosition - characterTransform.position;

            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction);
                characterTransform.rotation = Quaternion.RotateTowards(characterTransform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("attack");
            proyectileRb = Instantiate(bullet, fireposition.position, Quaternion.identity);
            proyectileRb.AddRelativeForce(fireposition.forward * proyectileForce, ForceMode.Impulse);
            //StartCoroutine(Attack1());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + movement * movespeed * Time.fixedDeltaTime);
    }


}
