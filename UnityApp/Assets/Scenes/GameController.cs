using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public HandClicked handClicked;
    public float disFromCamera = 1.0f; // 通过距离相机远近调整大小
    public List<Transform> disactivedTargets = new List<Transform>();
    private List<Transform> activedTargets = new List<Transform>();
    private Transform movingTarget = null;
    private bool isMoving = false;

    private Vector3 startPos = Vector3.up;
    private Vector3 targetPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = Vector3.zero + new Vector3(0, 0, disFromCamera);
        activedTargets.Clear();
        handClicked.onClicked = (Transform t) =>
        {
            if (t.name == "hezi") 
            {
                if (isMoving) return;

                if (disactivedTargets.Count == 0)
                {
                    for (int i = 0; i < activedTargets.Count; i++)
                    {
                        disactivedTargets.Add(activedTargets[i]);
                    }
                    activedTargets.Clear();
                }

                if (movingTarget)
                {
                    activedTargets.Add(movingTarget);
                    movingTarget.gameObject.SetActive(false);
                }

                var idx = UnityEngine.Random.Range(0, disactivedTargets.Count);
                movingTarget = disactivedTargets[idx];
                if (movingTarget.name == "Root3" || movingTarget.name == "Root4")
                {
                    startPos = new Vector3(0, 1.8f, disFromCamera);
                    isMoving = true;
                }
                else {
                    //startPos = new Vector3(0, 2.4f, disFromCamera);
                    isMoving = false;
                }
                movingTarget.localPosition = startPos;
                movingTarget.gameObject.SetActive(true);
                disactivedTargets.RemoveAt(idx);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            var y = movingTarget.localPosition.y - 0.03f;
            movingTarget.localPosition = new Vector3(startPos.x, y, startPos.z);

            if (movingTarget.localPosition.y <= targetPos.y) {
                isMoving = false;
                movingTarget.localPosition = targetPos;
            }
        }
    }
}
