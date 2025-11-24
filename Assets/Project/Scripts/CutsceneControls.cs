using UnityEngine;

public class CutsceneControls : MonoBehaviour
{
    public GameObject Guard1;
    public GameObject Princess;


    [Header("GuardControls")]


    public Transform point1;
    public Transform point2;

    public float PlayTimer1;
    public float PlayTimer2;
    public float idle2;
    // princess Controls

    [Header("Princess")]

    public Transform waypoint1;
    public Transform waypoint2;

    public float durationTimer1;
    public float durationTimer2;
    public float idle;

    

    // Update is called once per frame
    void Update()
    {
        GuardMovement();
    }

    void GuardMovement()
    {
        Vector3 targetPosition;

        if (Guard1.transform.position != point2.position)
        {
            targetPosition = point2.position - Guard1.gameObject.transform.position;
        }
        if (Guard1.transform.position == point2.position)
        {
            return;
        }
    }
}
