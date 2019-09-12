using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class MobLogic : MonoBehaviour
{
    public float WaitingTime = 1;
    public float AtackTime = 2;

    public AtackModule Atack;

    [SerializeField]
    private MobVision Vision;

    [SerializeField]
    private LineRenderer Line;

    [SerializeField]
    private PlatformerCharacter2D Char;

    private Transform followingObject;

    private int currentPoint = 0;
    private bool forward = true;


    private void Start()
    {
        Vision.OnInside += TriggerMob;
        Vision.OnOutside += UnTriggerMob;
        StartCoroutine(LoopMove());
    }

    private void UnTriggerMob(PlayerIdentity obj)
    {
        followingObject = null;
    }

    private void TriggerMob(PlayerIdentity obj)
    {
        followingObject = obj.transform;
    }

    private IEnumerator LoopMove()
    {
        while (true)
        {
            if (followingObject == null)
            {
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(Line.GetPosition(currentPoint).x, Line.GetPosition(currentPoint).z)) > 0.05f)
                {
                    MoveInDir(Line.GetPosition(currentPoint));
                }
                else
                {
                    MoveInDir(transform.position);
                    yield return new WaitForSeconds(WaitingTime);
                    if (forward)
                    {
                        currentPoint++;
                    }
                    else
                    {
                        currentPoint--;
                    }

                    if (currentPoint == 0)
                    {
                        forward = true;
                        currentPoint = 1;
                    }

                    if (currentPoint == Line.positionCount)
                    {
                        forward = false;
                        currentPoint = Line.positionCount - 1;
                    }
                }
            }
            else
            {
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(followingObject.transform.position.x,followingObject.transform.position.z)) > 1.5f)
                {
                    MoveInDir(followingObject.transform.position);
                }
                else
                {
                    MoveInDir(transform.position);
                    yield return new WaitForSeconds(AtackTime);
                    Atack.Atack();              
                }
            }
            yield return null;
        }
    }

    private void MoveInDir(Vector3 currentPoint)
    {
        Vector2 aim = new Vector3(currentPoint.x-transform.position.x, currentPoint.z-transform.position.z);
        aim = aim.normalized;
        Char.Move(aim.x, aim.y, false, false);
    }
}
