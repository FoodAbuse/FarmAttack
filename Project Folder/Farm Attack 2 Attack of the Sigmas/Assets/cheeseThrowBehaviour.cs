using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseThrowBehaviour : MonoBehaviour
{
    [Header("Cheese Throwing")]
    public GameObject cheesePrefab;
    public Transform throwPoint;
    public float throwForce = 15f;
    public Animator anim;


    [Header("Trajectory Arc")]
    public LineRenderer trajectoryLine;
    public int arcPoints = 50;
    public float timeStep = 0.05f;
    public LayerMask groundMask;

    private bool isAiming;

    void Start()
    {
        if (!trajectoryLine) trajectoryLine = GetComponent<LineRenderer>();

        // Make it smooooth
        trajectoryLine.enabled = false;
        trajectoryLine.positionCount = 0;
        trajectoryLine.widthMultiplier = 0.05f;
        trajectoryLine.numCapVertices = 8;
        trajectoryLine.numCornerVertices = 8;
        trajectoryLine.alignment = LineAlignment.View;
        trajectoryLine.material = new Material(Shader.Find("Sprites/Default")); // soft glow
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("Aiming", true);
            isAiming = true;
            trajectoryLine.enabled = true;
        }

        if (Input.GetMouseButton(1) && isAiming)
        {
            ShowTrajectory();
        }

        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("Aiming", false);
            anim.SetBool("Throw", false);

            isAiming = false;
            trajectoryLine.enabled = false;
        }

        if (Input.GetMouseButtonDown(0) && isAiming)
        {
            anim.SetBool("Throw", true);
            isAiming = false;
            trajectoryLine.enabled = false;
        }
    }

    void ShowTrajectory()
    {
        Vector3 start = throwPoint.position;
        Vector3 velocity = throwPoint.forward * throwForce;

        trajectoryLine.positionCount = arcPoints;

        for (int i = 0; i < arcPoints; i++)
        {
            float time = i * timeStep;
            Vector3 point = start + velocity * time + 0.5f * Physics.gravity * time * time;
            trajectoryLine.SetPosition(i, point);

            // Optional: stop early if it hits terrain
            if (i > 0 && Physics.Raycast(trajectoryLine.GetPosition(i - 1), point - trajectoryLine.GetPosition(i - 1), out RaycastHit hit, Vector3.Distance(point, trajectoryLine.GetPosition(i - 1)), groundMask))
            {
                trajectoryLine.positionCount = i + 1;
                trajectoryLine.SetPosition(i, hit.point);
                break;
            }
        }
    }

    void ThrowCheese()
    {

        GameObject cheese = Instantiate(cheesePrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = cheese.GetComponent<Rigidbody>();
        rb.velocity = throwPoint.forward * throwForce;
    }

    public void StopThrowingAnim()
    {
        anim.SetBool("Throw", false);
    }
    public void StartThrowingAnim()
    {
        ThrowCheese();
    }
}
