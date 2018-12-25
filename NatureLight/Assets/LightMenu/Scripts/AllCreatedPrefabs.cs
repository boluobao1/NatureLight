using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CreateAndOperate
{
    //保存所有物体，保存选中的物体，将这个脚本放在父亲节点上
    public class AllCreatedPrefabs : MonoBehaviour
    {

     //   public GameObject Wall;
      //  public Material ClipMaterial;

        //所有物体
        [HideInInspector]
        public List<GameObject> AllCreatedGameObject;

        //所有选择的物体
        [HideInInspector]
        public List<GameObject> SeletedGameObject;

        //保存平移，旋转，缩放的状态
        private enum ToggleStateEnum { Translation , Rotaion , Scaling }
        private ToggleStateEnum ToggleState = ToggleStateEnum.Translation;
 
        //窗户的参数，用于挖洞
        [HideInInspector]
        public Vector4[] SectionDirX = new Vector4[10];
        [HideInInspector]
        public Vector4[] SectionDirY = new Vector4[10];
        [HideInInspector]
        public Vector4[] SectionDirZ = new Vector4[10];
        [HideInInspector]
        public Vector4[] SectionCentre = new Vector4[10];
        [HideInInspector]
        public Vector4[] SectionScale = new Vector4[10];

        //光照平面
        public GameObject OutputPlane;
        private Material PlaneMaterial;
        private CreatedWindow.WindowParameter WindowPara;

        //引用DataManager组件的物体
        public GameObject Canvas;

        //实际窗户的数量
        private int index = 0;
        // Use this for initialization
        void Start()
        {
            //   ClipMaterial = Wall.GetComponent<Renderer>().material;

            PlaneMaterial = OutputPlane.GetComponent<Renderer>().material;
           
        }

        // Update is called once per frame
        void Update()
        {

            if(SeletedGameObject.Count>0)
            {
              if(SeletedGameObject[0].GetComponent<CreatedWindow>()!=null)
                {
                    //设置光照平面上的材质参数
                    WindowPara = SeletedGameObject[0].GetComponent<CreatedWindow>().GetWindowParameter();
                    PlaneMaterial.SetVector("_WindowPosition", WindowPara.WindowPosition);
                    PlaneMaterial.SetFloat("_WindowWidth", WindowPara.WindowWidth);
                    PlaneMaterial.SetFloat("_WindHeight", WindowPara.WindowHeight);

                    //设置面板上的参数
                    Canvas.GetComponent<DataManager>().SetWinderPara(WindowPara);
                }

            }


            //将窗户参数组传入shader中
            Shader.SetGlobalVectorArray("_SectionDirX", SectionDirX);
            Shader.SetGlobalVectorArray("_SectionDirY", SectionDirY);
            Shader.SetGlobalVectorArray("_SectionDirZ", SectionDirZ);
            Shader.SetGlobalVectorArray("_SectionCentre", SectionCentre);
            Shader.SetGlobalVectorArray("_SectionScale", SectionScale);

            //按下Delete键删除选择的物体
            if(Input.GetKeyDown(KeyCode.Delete))
            {
                DeleteObjects();
            }


        }


        //添加物体
        public void AddChild(GameObject Child)
        {
            Child.transform.parent = transform;
            AllCreatedGameObject.Add(Child);
            //NewSeletedGameObject(Child);

            //设置变量用来 挖洞
            Child.GetComponentInChildren<CappedSectionFollow>().FatherScript = this;
            UpdateArray();


        }

        //删掉物体
        public void DestroyChild(GameObject Child)
        {
            AllCreatedGameObject.Remove(Child);
            UpdateArray();
        }

        //Delete删除物体
        public void DeleteObjects()
        {
            for (int i = 0; i < SeletedGameObject.Count; i++)
            {
                DestroyChild(SeletedGameObject[i]);
                SeletedGameObject[i].GetComponent<CreatedObject>().Delete();
            }
            SeletedGameObject.Clear();
        }


        //根据窗户数组更新参数组，用于挖洞
        void UpdateArray()
        {
            index = 0;
            for (int i = 0; i < AllCreatedGameObject.Count; i++)
            {
                if (AllCreatedGameObject[i].GetComponent<CreatedObject>().ObjectType == CreatedObject.Type.Window)
                {
                    AllCreatedGameObject[i].GetComponentInChildren<CappedSectionFollow>().index = index;
                    index++;
                }
            }
            Shader.SetGlobalInt("_Count", index);
        }


        //加到以选中的物体上
        public void AddSeletedGameObject(GameObject NewGameObject)
        {
            SeletedGameObject.Add(NewGameObject);
        }

        //重新选中物体
        public void NewSeletedGameObject(GameObject NewGameObject)
        {
            for (int i = 0; i < SeletedGameObject.Count; i++)
            {
                SeletedGameObject[i].GetComponent<CreatedObject>().SwitchState(CreatedObject.State.Normal);
            }
            SeletedGameObject.Clear();
            SeletedGameObject.Add(NewGameObject);

            switch (ToggleState)
            {
                case ToggleStateEnum.Translation:
                    NewGameObject.GetComponent<CreatedObject>().SwitchState(CreatedObject.State.Translation);
                    break;
                case ToggleStateEnum.Rotaion:
                    NewGameObject.GetComponent<CreatedObject>().SwitchState(CreatedObject.State.Rotation);
                    break;
                case ToggleStateEnum.Scaling:
                    NewGameObject.GetComponent<CreatedObject>().SwitchState(CreatedObject.State.Scaling);
                    break;
                default:
                    break;
            }
            
        }


        //用于在Toggle上调用，改为平移状态
        public void ChangeTranslation(bool Toggle)
        {
            if (Toggle)
            {
                for (int i = 0; i < SeletedGameObject.Count; i++)
                {
                    SeletedGameObject[i].GetComponent<CreatedObject>().SwitchState(CreatedObject.State.Translation);
                }
                ToggleState = ToggleStateEnum.Translation;
            }
        }

        //改为旋转状态
        public void ChangeRotaion(bool Toggle)
        {
            if (Toggle)
            {
                for (int i = 0; i < SeletedGameObject.Count; i++)
                {
                    SeletedGameObject[i].GetComponent<CreatedObject>().SwitchState(CreatedObject.State.Rotation);
                }
                ToggleState = ToggleStateEnum.Rotaion;
            }
        }

        //改为放缩状态
        public void ChangeScaling(bool Toggle)
        {
            if (Toggle)
            {
                for (int i = 0; i < SeletedGameObject.Count; i++)
                {
                    SeletedGameObject[i].GetComponent<CreatedObject>().SwitchState(CreatedObject.State.Scaling);
                }
                ToggleState = ToggleStateEnum.Scaling;
            }
        }

        //是否显示等照线
        public void ShowIll(bool IfShow)
        {
            if(IfShow)
            {
                PlaneMaterial.SetInt("_IfFloor", 1);
            }
            else
            {
                PlaneMaterial.SetInt("_IfFloor", 0);
            }
        }
    }
}

