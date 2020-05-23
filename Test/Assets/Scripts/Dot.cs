using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MotherDot
{
    public MotherDot targetDot;
    public MotherDot motherDot;
    void OnEnable()
    {
        base.OnEnable();
        HP = 1;
        transform.localScale = new Vector3(0.25f,0.25f,0.25f);
    }
    void Update()
    {
        if(motherDot == null)
        {
            Destroy(this.gameObject);
        }
        if (targetDot == null) // Доты в режиме покоя
        {
            transform.RotateAround(motherDot.transform.position, -Vector3.forward, Time.deltaTime * 50.0f); // Вращается вокруг своей MotherDot
            if (team == teams.Team1)
            {
                renderer.material.color = Color.blue;
            }
            else {
                renderer.material.color = Color.red;
            }
        }
        else // Доты в режиме атаки
        {
            transform.position = Vector3.MoveTowards(transform.position, targetDot.transform.position, Time.deltaTime * 10.0f);
        }
    }
    public void OnCollisonEnter(Collision collision)
    {
        Debug.Log("111");
        if (collision.gameObject.GetComponent<MotherDot>() != null && collision.gameObject.GetComponent<MotherDot>().team != team)
        {
            HP--;
            collision.gameObject.GetComponent<MotherDot>().HP--;
        }
    }
    void OnDisable()
    {
        motherDot.childDots.Remove(this);
    }
}
