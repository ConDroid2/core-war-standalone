using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

// The class that manages clicking things on the board 
public class MouseManager : MonoBehaviour
{
    private LineRenderer line;
    [HideInInspector] public TargetLinePool linePool;

    private bool lineOn = false;

    [HideInInspector] public GameObject hoveredObject;
    [HideInInspector] public GameObject selectedObject = null;

    public SequenceSystem.Target selectedAction = null;

    public Action OnRightClick;

    public static MouseManager Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        line = GetComponent<LineRenderer>();
        linePool = GetComponent<TargetLinePool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnRightClick?.Invoke();
            Player.Instance.hand.ReconfigureCardPos(false);
        }
        if (hoveredObject != null || selectedObject !=null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if(selectedObject != null)
                {
                    InPlayCardController card;
                    if (card = selectedObject.GetComponent<InPlayCardController>())
                    {
                        if (selectedAction == null)
                        {
                            ClearSelected();
                        }   
                    }
                }
            } 
            // Release on something
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (selectedObject != null)
                {
                    if (hoveredObject != null)
                    {
                        if (selectedAction == null)
                        {
                            UnitController card;
                            if ((Player.Instance.currentMode == Player.Mode.Normal) && (card = selectedObject.GetComponent<UnitController>()) && Player.Instance.myTurn)
                            {
                                if (hoveredObject.GetComponent<Zone>())
                                {
                                    if(card.unitAdvance != null && card.moveState == UnitController.ActionState.CanAct)
                                    {
                                        MainSequenceManager.Instance.Add(card.AdvanceStack);
                                    }   
                                }
                                else if (hoveredObject.GetComponent<InPlayCardController>())
                                {
                                    if (!hoveredObject.GetComponent<InPlayCardController>().photonView.IsMine)
                                    {
                                        if (card.unitAttack != null && card.attackState == UnitController.ActionState.CanAct)
                                        {
                                            object[] rpcData = { hoveredObject.GetComponent<InPlayCardController>().photonView.ViewID };
                                            card.photonView.RPC("SetAttackTarget", RpcTarget.All, rpcData);
                                            MainSequenceManager.Instance.Add(card.AttackStack);
                                        }
                                    }
                                }
                            }

                            ClearSelected();
                        } 
                        else
                        {
                            PotentialTarget target;
                            if (target = hoveredObject.GetComponent<PotentialTarget>())
                            {
                                if(target.isSelectable)
                                {
                                    selectedAction.SetTarget(hoveredObject);
                                }  
                            }
                        }
                    } 
                }
            }
        }

        if (lineOn)
        {
            line.SetPosition(1, MouseWorldPos());
        }
    }


    /** Utility Functions **/
    private Vector3 MouseWorldPos() {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public void SetSelected(GameObject selected) 
    {
        if(selectedAction != null) { return; }
        if (hoveredObject == selected)
        {
            hoveredObject = null;
        }
        selectedObject = selected;
        line.enabled = true;
        line.SetPosition(0, selected.transform.position);
        lineOn = true;
    }

    public void SetSelected(GameObject selected, SequenceSystem.Target action) 
    {
        if(selectedAction != null) { return; }
        if (hoveredObject == selected)
        {
            hoveredObject = null;
        }
        selectedObject = selected;
        line.enabled = true;
        line.SetPosition(0, selected.transform.position);
        linePool.startPosition = selected.transform.position;
        lineOn = true;

        selectedAction = action;
    }

    public void ClearSelected() 
    {
        selectedObject = null;
        selectedAction = null;
        line.enabled = false;
        lineOn = false;
    }
}
