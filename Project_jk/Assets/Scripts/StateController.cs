using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCurrentState
{
    Attack,
    Defend,
    Heal
}

public class StateController : MonoBehaviour
{
    private MeshRenderer mMeshRenderer;

    [SerializeField]
    private Material[] mStateMtr;

    [SerializeField]
    private eCurrentState eCurrentState;

    private void Start()
    {
        mMeshRenderer = GetComponent<MeshRenderer>();
        mMeshRenderer.material = mStateMtr[(int)eCurrentState];
    }

    public void NextState()
    {
        eCurrentState++;
        if ((int)eCurrentState > 2)
        {
            eCurrentState = 0;
        }

        switch(eCurrentState)
        {
            case eCurrentState.Attack:
                mMeshRenderer.material = mStateMtr[(int)eCurrentState];
                break;
            case eCurrentState.Defend:
                mMeshRenderer.material = mStateMtr[(int)eCurrentState];
                break;
            case eCurrentState.Heal:
                mMeshRenderer.material = mStateMtr[(int)eCurrentState];
                break;
            default:
                Debug.LogError("Wrong State : " + eCurrentState);
                break;
        }
    }
}
