using NPC;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject npc;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitPos = hit.point;

                GameObject gO = hit.transform.gameObject;

                if (gO && gO.GetComponent<Brain>())
                    npc = gO;
                else if (npc)
                {
                    hitPos.y = npc.transform.position.y;
                    FieldInfo<Vector3> travelInfo = new FieldInfo<Vector3>(npc, hitPos);
                    npc.GetComponent<Brain>().RequestStateChange<FieldInfo<Vector3>>(typeof(Settler_Travel), typeof(Action_Walk), travelInfo);
                    npc = null;
                }
            }
        }
    }
}