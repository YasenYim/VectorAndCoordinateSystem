using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerCharacter character;

	void Start () {
        character = GetComponent<PlayerCharacter>();

        Vector3 a = new Vector3(1, 0, 0);
        Vector3 b = new Vector3(0, 1, 0);

        Vector3 c = Vector3.Cross(a, b);
        Debug.Log("C=" + c);
	}

    void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        character.Move(v);
        character.Turn(h);
        character.EngineAudio(v, h);

        if (Input.GetButtonDown("Fire1"))
        {
            character.Fire();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouse = Input.mousePosition;  // 鼠标的位置
            Ray ray = Camera.main.ScreenPointToRay(mouse);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 300, Color.red, 2);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // 射线击中的点hit.point
                Debug.Log(hit.point);
            }
        }
    }
}
