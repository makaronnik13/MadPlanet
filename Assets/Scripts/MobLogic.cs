using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets._2D;

public class MobLogic : MonoBehaviour
{
    public enum MobBahaviour
    {
        Attack,
        Run
    }

    public UnityEvent OnPathCompleted, OnPathStarted, OnPlayerSpoted;

    [SerializeField]
    private AnimationCurve MoveSpeed;

    [SerializeField]
    private MobBahaviour Behaviour = MobBahaviour.Attack; 

    public float WaitingTime = 1;
    public float AtackTime = 2;
    [SerializeField]
    private bool CanJump = false;
    public AtackModule Atack;

    private bool canAtack = true;

    [SerializeField]
    private MobVision Vision;

    [SerializeField]
    private LineRenderer Line;

    [SerializeField]
    private PlatformerCharacter2D Char;

    public Transform followingObject;

    private Coroutine movement;

    private Vector3 nextPos = Vector3.zero;
    private int _currentPoint = 0;
    private int currentPoint
    {
        get
        {
            return _currentPoint;
        }
        set
        {
            _currentPoint = value;
            if (_currentPoint<Line.positionCount && _currentPoint>=0)
            {
                nextPos = Line.transform.TransformPoint(Line.GetPosition(_currentPoint));
            }
            
        }
    }
    private bool forward = true;
    private bool jump = false;

    public void SetAgressice(bool v)
    {
        canAtack = v;
    }

    public void Run()
    {
        TriggerMob(FindObjectOfType<PlayerIdentity>());
    }

    private void OnEnable()
    {
        Vision.OnInside += TriggerMob;
        Vision.OnOutside += UnTriggerMob;
        currentPoint = 0;
        movement = StartCoroutine(LoopMove());
    }

    private void OnDisable()
    {
        StopCoroutine(movement);
        Vision.OnInside -= TriggerMob;
        Vision.OnOutside -= UnTriggerMob;
        StopCoroutine(LoopMove());
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
        OnPlayerSpoted.Invoke();
    }

    private IEnumerator LoopMove()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (Game.Instance.Paused.State)
            {
                yield return null;
            }
            switch (Behaviour)
            {
                case MobBahaviour.Attack:
                    if (Atack.GrabbingTransform)
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

                        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(nearestPoint.x, nearestPoint.z)) > 0.2f)
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
                        if (canAtack && followingObject == null || (followingObject!=null && followingObject.GetComponent<PlatformerCharacter2D>().Grabed))
                        {
                            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(nextPos.x, nextPos.z)) > 0.2f)
                            {
                                MoveInDir(nextPos);
                            }
                            else
                            {
                                MoveInDir(transform.position);
                                if (Atack.GrabbingTransform)
                                {
                                    yield return null;
                                }
                                else
                                {
                                    yield return new WaitForSeconds(WaitingTime);
                                }

                                if (currentPoint == 0)
                                {
                                    OnPathStarted.Invoke();
                                }

       

                                if (forward)
                                {
                                    currentPoint++;
                                }
                                else
                                {
                                    currentPoint--;
                                }


                                if (currentPoint == -1)
                                {
                                    forward = true;
                                    currentPoint = 1;
                                }

                                if (currentPoint == Line.positionCount)
                                {
                                    forward = false;
                                    currentPoint = Line.positionCount - 1;
                                    OnPathCompleted.Invoke();
                                }
                            }
                        }
                        else
                        {
                            if (Vector3.Distance(new Vector3(Atack.transform.position.x, 0, Atack.transform.position.z) , new Vector3(followingObject.transform.position.x, 0, followingObject.transform.position.z)) > 0.2f)
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

                        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(nextPos.x, nextPos.z)) > 0.15f)
                        {
                            MoveInDir(nextPos);
                        }
                        else
                        {
                            yield return new WaitForSeconds(WaitingTime);
                            currentPoint++;
                            if (currentPoint == Line.positionCount)
                            {
                                Char.Move(0, 0, false, false);
                                StopAllCoroutines();
                                OnPathCompleted.Invoke();
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
        Vector3 nextPos = Line.transform.TransformPoint(Line.GetPosition(this.currentPoint));

        Vector2 aim = new Vector3(currentPoint.x-transform.position.x, currentPoint.z-transform.position.z);
        aim = aim.normalized;
        Char.Move(aim.x, aim.y, false, nextPos.y>transform.position.y && CanJump);
    }
}
