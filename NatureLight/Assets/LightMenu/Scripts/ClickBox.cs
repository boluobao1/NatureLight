using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreateAndOperate
{
    //使用一个碰撞体来添加物体的点击事件
    public class ClickBox : MonoBehaviour
    {
        //用于添加点击事件的物体
        public GameObject CreatObject;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //调用物体点击事件，
        private void OnMouseDown()
        {
            CreatObject.GetComponent<CreatedObject>().OnClick();
        }


    }
}

