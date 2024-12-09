using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionManager : MonoBehaviour
{
    public GameObject SnapTurn;
    public GameObject ContinousTurn;

    private ContinuousTurnProviderBase _continousTurnProvider;
    private SnapTurnProviderBase _snapTurnProvider;


    void Start()
    {
        _snapTurnProvider = GetComponent<SnapTurnProviderBase>();
        _continousTurnProvider = GetComponent<ContinuousTurnProviderBase>();

    }

    public void SwitchTurning(int turnValue)
    {
        if(turnValue == 0) 
        {
            DisableContinuous();
            EnableSnap();
        }
        else if (turnValue == 1) 
        {
            DisableSnap();
            EnableContinuous();       
        }
    }

    private void DisableSnap()
    {
        SnapTurn.SetActive(false);
        ContinousTurn.SetActive(false);
        _snapTurnProvider.enabled = false;
    }

    private void EnableSnap()
    {
        SnapTurn.SetActive(true);
        ContinousTurn.SetActive(true);
        _snapTurnProvider.enabled = true;
    }

    private void DisableContinuous()
    {
        _continousTurnProvider.enabled = false;
    }

    private void EnableContinuous()
    {
        _continousTurnProvider.enabled = true;
    }
}
