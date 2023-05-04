using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 360f;
    public float raycastDistance = 8f;
    public float raycastAngle = 60f;

    // Update is called once per frame
    void Update()
    {
        

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        RaycastHit hitInfo;

        Quaternion rotationLeft = Quaternion.AngleAxis(-10f, Vector3.up);
        Vector3 directionLeft = rotationLeft * this.transform.forward;

        Quaternion rotationRight = Quaternion.AngleAxis(10f, Vector3.up);
        Vector3 directionRight = rotationRight * this.transform.forward;

        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red);
        Debug.DrawRay(transform.position, directionLeft * raycastDistance, Color.red);
        Debug.DrawRay(transform.position, directionRight * raycastDistance, Color.red);



        if (Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, 15.0f) ||
            Physics.Raycast(this.transform.position, directionLeft, out hitInfo, 15.0f) ||
            Physics.Raycast(this.transform.position, directionRight, out hitInfo, 15.0f))
        {
            RaycastHit[] hitInfos;
            Vector3[] directions = new Vector3[] {
                transform.forward,
                Quaternion.Euler(0, raycastAngle, 0) * transform.forward,
                Quaternion.Euler(0, -raycastAngle, 0) * transform.forward
            };

            List<RaycastHit> hitsList = new List<RaycastHit>();
            foreach (Vector3 dir in directions)
            {
                hitInfos = Physics.RaycastAll(transform.position, dir, raycastDistance);
                hitsList.AddRange(hitInfos);
            }

            if (hitsList.Count > 0)
            {
                bool hitObstacle = false;
                bool hitObstacleRight = false;
                bool hitObstacleLeft = false;

                foreach (RaycastHit hit in hitsList)
                {
                    if (hit.collider.gameObject.CompareTag("Obstacle"))
                    {
                        hitObstacle = true;
                        if (Vector3.Angle(transform.forward, hit.point - transform.position) < 90f)
                        {
                            hitObstacleRight = true;
                        }
                        else
                        {
                            hitObstacleLeft = true;
                        }
                    }
                }
                if (hitObstacle)
                {
                if (hitObstacleRight)
                {
                transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
                }
                else if (hitObstacleLeft)
                {
                transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
                }
                }
                else
                {
                transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
                }
            }
        }
    }
}