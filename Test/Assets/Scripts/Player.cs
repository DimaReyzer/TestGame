using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private MotherDot motherDot;
    private Camera camera;
    void OnEnable()
    {
        camera = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SelectDot();
        }
    }
    void SelectDot() // Возвращает позицию на игровом поле от положения мышки на экране
    {
        MotherDot returnDot = null;
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            returnDot = hit.collider.gameObject.GetComponent<MotherDot>();
        }
        if (motherDot == null)
        {
            motherDot = returnDot;
        }
        else
        {
            if (motherDot != returnDot)
            {
                motherDot.Attack(returnDot);
            }
            motherDot = null;
        }
    }
}
