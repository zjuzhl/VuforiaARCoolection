using UnityEngine;
using UnityEngine.UI;
using Vuforia;


public class PlayBtn : MonoBehaviour
{
    public GameObject modelTarget;

    public Button playBtn;

    public AnchorInputListenerBehaviour anchorInputListenerBehaviour;

    public PlaneFinderBehaviour planeFinderBehaviour;

    private void Start()
    {
        modelTarget.SetActive(false);
        playBtn.onClick.AddListener(()=> {
            this.OnPlay();
        });
    }

    public void OnPlay() 
    {
        Debug.Log("arvuforia-" + "OnPlay");
        anchorInputListenerBehaviour.OnInputReceivedEvent?.Invoke(new Vector2(0.5f * Screen.width, 0.5f * Screen.height));

        Invoke(nameof(CloseHitTest), 0.1f);
    }

    private void CloseHitTest() 
    {
        playBtn.gameObject.SetActive(false);

        var pos = new Vector3(Camera.main.transform.position.x, modelTarget.transform.position.y, Camera.main.transform.position.z);
        modelTarget.transform.LookAt(pos, Vector3.up);
        modelTarget.SetActive(true);

        planeFinderBehaviour.enabled = false;
        //anchorInputListenerBehaviour.OnInputReceivedEvent.RemoveAllListeners();
        anchorInputListenerBehaviour.enabled = false;
    }

    public void OnHittedResult(HitTestResult hitTestResult)
    {
        //Debug.Log("arvuforia-" + "OnHittedToPlaced£¬ " + hitTestResult.Position.x + "," + hitTestResult.Position.y + "," + hitTestResult.Position.z);
    }
}
