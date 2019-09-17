using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class MobLogic : MonoBehaviour
{
    public enum MobBahaviour
    {
        Attack,
        Run
    }

    [SerializeField]
    private AnimationCurve MoveSpeed;

    [SerializeField]
    private MobBahaviour Behaviour = MobBahaviour.Attack; 

    public float WaitingTime = 1;
    public float AtackTime = 2;

    public AtackModule Atack;

    [SerializeField]
    private MobVision Vision;

    [SerializeField]
    private LineRenderer Line;

    [SerializeField]
    private PlatformerCharacter2D Char;

    public Transform followingObject;

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
        switch (Behaviour)
        {
            case MobBahaviour.Attack:
                followingObject = null;
                break;
            case MobBahaviour.Run:
                break;
        }      
    }

    private void TriggerMob(PlayerIdentity obj)
    {
        followingObject = obj.transform;
    }

    private IEnumerator LoopMove()
    {
        while (true)
        {
            switch (Behaviour)
            {
                case MobBahaviour.Attack:
                    if (Atack.Grabbing)
                    {
                        Vector3 nearestPoint = Vector3.positiveInfinity;
                        foreach (Transform t in Line.transform)
                        {
                            if (Vector3.Distance(transform.position, t.position) < Vector3.Distance(nearestPoint, transform.position))
                            {
                                nearestPoint = t.position;
                            }
                        }

                        if (Line.transform.childCount == 0)
                        {
                            nearestPoint = transform.position;
                        }

                        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(nearestPoint.x, nearestPoint.z)) > 0.05f)
                        {
                            MoveInDir(nearestPoint);
                        }
                        else
                        {
                            Atack.Release();
                        }
                    }
                    else
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
                                if (Atack.Grabbing)
                                {
                                    yield return null;
                                }
                                else
                                {
                                    yield return new WaitForSeconds(WaitingTime);
                                }

                                if (forward)
                                {
                                    currentPoint++;
                                }
                                else
                                {
                                    currentPoint--;
                                }

                                Atack.Release();

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
                            if (Vector3.Distance(Atack.transform.position, followingObject.transform.position) > 1.5f)
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
                    }
                    break;
                case MobBahaviour.Run:
                    if (followingObject)
                    {
                        float dist = Vector3.Distance(transform.position, followingObject.transform.position);
                        Char.m_MaxSpeed = MoveSpeed.Evaluate(dist);
                        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(Line.GetPosition(currentPoint).x, Line.GetPosition(currentPoint).z)) > 0.15f)
                        {
                            MoveInDir(Line.GetPosition(currentPoint));
                        }
                        else
                        {
                            yield return new WaitForSeconds(WaitingTime);
                            currentPoint++;
                            if (currentPoint == Line.positionCount)
                            {
                                Char.Move(0, 0, false, false);
                                StopAllCoroutines();
                               break;
                            }
                        }
                    }
                    break;
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
