using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceAndMove : MonoBehaviour
{

    public GameObject terminal;

    public Transform root;

    public Transform target;

    public bool enable = false;

    private Transform terminalTrans;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable) return;

        if (target.gameObject.activeInHierarchy && terminalTrans != null) 
        {
            var dis = Vector3.Distance(target.position, terminalTrans.position);
            if (dis <= 0.02f)
            {
                Destroy(terminalTrans.gameObject);
            }
            else {
                target.SetPositionAndRotation(
                    Vector3.MoveTowards(target.position, terminalTrans.position, 0.04f), 
                    Quaternion.RotateTowards(target.rotation,
                    Quaternion.LookRotation(terminalTrans.position - target.position, Vector3.up), 10f));
            }
        }

        if (Input.touchCount == 1) 
        {
            var touch = Input.GetTouch(0);
            var state = touch.phase;
            if (state == TouchPhase.Began) 
            {
                if (!IsPointerOverUIObject(touch.position))
                {
                    this.PlaceAction(touch.position);
                }
            }
        }
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (!IsPointerOverUIObject(pos))
            {
                PlaceAction(pos);
            }
        }
#endif
    }

    public void PlaceAction(Vector2 posOnScreen) 
    {
        var ray = Camera.main.ScreenPointToRay(posOnScreen);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, 1 << LayerMask.NameToLayer("Plane")))
        {
            if (hitInfo.transform.name == "Plane")
            {
                if (root.childCount > 0) 
                {
                    for (int i = 0; i < root.childCount; i++) 
                    {
                        Destroy(root.GetChild(root.childCount - 1 - i).gameObject);
                    }
                }
                var obj = Instantiate(terminal, hitInfo.point, Quaternion.identity, root);
                obj.name = "terminal";
                obj.SetActive(true);

                terminalTrans = obj.transform;
            }
        }
    }

    public void Revert() {
        if (root.childCount > 0)
        {
            for (int i = 0; i < root.childCount; i++)
            {
                Destroy(root.GetChild(root.childCount - 1 - i).gameObject);
            }
        }
        terminalTrans = null;
    }

    private bool IsPointerOverUIObject(Vector2 posOnScreen) {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = posOnScreen;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
