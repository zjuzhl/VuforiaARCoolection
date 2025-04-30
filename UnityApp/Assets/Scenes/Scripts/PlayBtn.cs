using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


public class PlayBtn : MonoBehaviour
{
    public GameObject modelTarget;

    public GameObject modelButton;

    public GameObject modelPets;

    public AnchorInputListenerBehaviour anchorInputListenerBehaviour;

    public PlaneFinderBehaviour planeFinderBehaviour;

    private void Start()
    {
        modelTarget.SetActive(false);
        modelPets.SetActive(false);
    }
    public void OnPlay() 
    {
        Debug.Log("arvuforia-" + "OnPlay");
        anchorInputListenerBehaviour.OnInputReceivedEvent?.Invoke(new Vector2(0.5f * Screen.width, 0.5f * Screen.height));

        //anchorInputListenerBehaviour.enabled = false;
        //anchorInputListenerBehaviour.OnInputReceivedEvent.RemoveAllListeners();
        Invoke(nameof(CloseHitTest), 0.5f);
        modelButton.SetActive(false);
    }

    private void CloseHitTest() 
    {
        var pos = new Vector3(Camera.main.transform.position.x, modelTarget.transform.position.y, Camera.main.transform.position.z);
        modelTarget.transform.LookAt(pos, Vector3.up);
        modelTarget.SetActive(true);
        modelPets.SetActive(true);
        var count = GameController.Instance.DogCollections.Length;
        GameController.Instance.RollDog(UnityEngine.Random.Range(0, count));
        TipManager.Instance.ActiveTip(TipType.TouchTip, 3);

        planeFinderBehaviour.enabled = false;
        anchorInputListenerBehaviour.OnInputReceivedEvent.RemoveAllListeners();
        anchorInputListenerBehaviour.enabled = false;
    }

    public void OnHittedResult(HitTestResult hitTestResult)
    {
        //Debug.Log("arvuforia-" + "OnHittedToPlaced£¬ " + hitTestResult.Position.x + "," + hitTestResult.Position.y + "," + hitTestResult.Position.z);
    }
}
