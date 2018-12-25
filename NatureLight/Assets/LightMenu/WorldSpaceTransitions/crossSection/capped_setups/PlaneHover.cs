using UnityEngine;
using System.Collections;

//只实现了外观的改变
public class PlaneHover : MonoBehaviour {

	public Color hovercolor;
	private Color original;
	private Renderer rend;
	private bool selected;
    private Vector3 a;
    private Transform father;
    public bool LockY_X = true;

    float Factor = 1.0f;

    void Start(){
       
		rend = transform.GetComponent<Renderer>();
		original = rend.material.color;
        father = transform.parent;


        if(LockY_X)
        {
            Factor =  transform.localScale.x / father.localScale.x ;
        }
        else
        {
            Factor = transform.localScale.y / father.localScale.y;
        }


    }

	void OnMouseEnter () {

        //GetComponent<Renderer>().enabled = true;
        SetHovered();
	}

	void OnMouseExit () {
		if(!selected)
			SetOriginal();
	}

	void SetHovered(){

		rend.material.color = hovercolor;
	}

	void SetOriginal(){

		rend.material.color = original;
	}

	void Update(){


        if (LockY_X)
        {
            transform.localScale = new Vector3(Factor * 1.0f / father.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, Factor * 1.0f / father.localScale.y, transform.localScale.z);
        }



        //选中状态结束
        if (selected && Input.GetMouseButtonUp(0)){
			SetOriginal();
			selected = false;

		}
	}

}
