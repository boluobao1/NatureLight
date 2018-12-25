using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdvancedGizmo;

namespace CreateAndOperate
{
    //放在window 的prefab下
    public class CreatedWindow : CreatedObject
    {

        //区分不同的状态
        public GameObject Gizmo;
        public GameObject CrossSectionBox;
        public GameObject Window;
        public GameObject ClickBox;
        public GameObject SetVisibleX;
        public GameObject SetVisibleXN;
        public GameObject SetVisibleY;
        public GameObject SetVisibleYN;

        
        public struct WindowParameter
        {
            public Vector3 WindowPosition;
            public float WindowWidth;
            public float WindowHeight;
        }

        private bool First = true;
 
        // Use this for initialization
        void Start()
        {
 
        }

        // Update is called once per frame
        void Update()
        {
            //用于设置位置和旋转
            transform.position = Gizmo.transform.position;
            transform.rotation = Gizmo.transform.rotation;
        }


        //切换状态
        public override State SwitchState(State NewState) 
        {
            CurrentState = NewState;

            int i = Gizmo.GetComponent<Gizmo>().i;
            if (First)
            {
                First = false;
                i = (i + 1) % 2;
            }

            switch (NewState)
            {
                case State.Preview:
                    break;
                case State.Normal:

                    Gizmo.SetActive(false);
                    //  CrossSectionBox.SetActive(false);
                    SetVisibleX.SetActive(false);
                    SetVisibleXN.SetActive(false);
                    SetVisibleY.SetActive(false);
                    SetVisibleYN.SetActive(false);


                    ClickBox.SetActive(true);
                    break;
                case State.Choiced:
                    ClickBox.SetActive(false);
                    break;
                case State.Translation:
                    Gizmo.SetActive(true);
                    ClickBox.SetActive(false);
                    Debug.Log("平移状态");
                    if (i == 1)
                    {
                        Gizmo.GetComponent<Gizmo>().ChangeMode();
                    }
                    //   CrossSectionBox.SetActive(false);
                    SetVisibleX.SetActive(false);
                    SetVisibleXN.SetActive(false);
                    SetVisibleY.SetActive(false);
                    SetVisibleYN.SetActive(false);


                    break;
                case State.Rotation:
                    Gizmo.SetActive(true);
                    ClickBox.SetActive(false);
                    if (i == 0)
                    {
                        Gizmo.GetComponent<Gizmo>().ChangeMode();
                    }

                    // CrossSectionBox.SetActive(false);
                    SetVisibleX.SetActive(false);
                    SetVisibleXN.SetActive(false);
                    SetVisibleY.SetActive(false);
                    SetVisibleYN.SetActive(false);


                    break;
                case State.Scaling:
                    Gizmo.SetActive(false);

                    //  CrossSectionBox.SetActive(true);
                    SetVisibleX.SetActive(true);
                    SetVisibleXN.SetActive(true);
                    SetVisibleY.SetActive(true);
                    SetVisibleYN.SetActive(true);

                    ClickBox.SetActive(false);
                    break;

                default:
                    break;
            }
            return NewState;
        }

        //点击物体
        public override void OnClick()
        {
            Debug.Log("点击物体");
            //发送消息到父物体
            transform.parent.GetComponent<AllCreatedPrefabs>().NewSeletedGameObject(gameObject);
        }

        public override void Delete()
        {
            Destroy(gameObject);
        }

        //得到窗户的参数
        public WindowParameter GetWindowParameter()
        {
            WindowParameter WindowPara = new WindowParameter();
            WindowPara.WindowPosition = transform.position;

            WindowPara.WindowWidth = CrossSectionBox.transform.localScale.x;
            WindowPara.WindowHeight = CrossSectionBox.transform.localScale.y;
            return WindowPara;
        }

        //通过参数设置窗户
        public void SetWindowPara(WindowParameter NewWindowPara)
        {
            transform.position = NewWindowPara.WindowPosition;
            CrossSectionBox.transform.localScale = new Vector3( NewWindowPara.WindowWidth , NewWindowPara.WindowHeight , CrossSectionBox.transform.localScale.z);
        }

    }

}

