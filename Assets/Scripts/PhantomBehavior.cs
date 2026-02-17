using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PhantomBehavior : MonoBehaviour
{
    public GameObject nonPhantomObject;
    public String nonPhantomObjectName;
    public Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // find mouse position relative to camera
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // set the phantom's position
        transform.position = worldPosition;

        if (Input.GetMouseButtonDown(0))
        {
            // create the non phantom object
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            nonPhantomObject = GameObject.Instantiate(nonPhantomObject);
            nonPhantomObject.transform.position = spawnPosition;
            nonPhantomObject.name = nonPhantomObjectName;

            // delete self
            Destroy(this.gameObject);
        }

    }

}
