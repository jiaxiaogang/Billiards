using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

    //本类专门写操作的代码.用于绑定NGUI的控件所执行的方法和事件
    public GameObject helpReadme;//说明文档
    private bool helpReadmeHidd = false;//是否隐藏;
    public GameObject CameraButton;//切换3D2D摄像机按钮对象
    public GameObject ForceBar;//力量条击球条;
    public GameObject trimmingLeft;
    public GameObject trimmingRight;
    /// <summary>
    /// 这里主要写关于CameraButtonDown和CameraButtonUp和ForceBarUp的UILitener监听
    /// </summary>
    void Start()
    {
        //CameraButton = GameObject.FindGameObjectWithTag("CameraButton");
        //ForceBar = GameObject.FindGameObjectWithTag("ForceBar");
        UIEventListener.Get(CameraButton).onPress = CameraButtonDown;//监听切换3D按下
        //UIEventListener.Get(CameraButton).onPress = CameraButtonUp;//监听切换3D松开
        UIEventListener.Get(ForceBar).onPress = ForceUp;//监听力度条松开
        UIEventListener.Get(trimmingLeft).onPress = TrimmingLeft;
        UIEventListener.Get(trimmingRight).onPress = TrimmingRight;
    }
    /// <summary>
    /// 帮助按钮
    /// </summary>
    public void Help()
    {
        //bool helpReadmeHidd = false;
        if(helpReadme!=null)
        {
            helpReadme.SetActive(!helpReadmeHidd);
            helpReadmeHidd = !helpReadmeHidd;
        }
    }
    /// <summary>
    /// 切换摄像头顶视图按下时
    /// </summary>
    public void CameraButtonDown(GameObject go,bool state)
    {
        if(state)
        {
            //此时按着
            BallArm._instance.ToMainCamera();
        }
        else if(!state)
        {
            //此时松开了
            BallArm._instance.ToViceCamera();
        }
    }
    ///// <summary>
    ///// 切换摄像头3D视图松开时
    ///// </summary>
    //public void CameraButtonUp(GameObject go,bool state)
    //{

    //}
    /// <summary>
    /// 力度条松开时记录value并且发力
    /// </summary>
    public void ForceUp(GameObject go,bool state)
    {
        if(state)
        {
            BallArm._instance.canMoveBallArm = false;
        }
        //把球杆类里发力MouseButtonDown改写成一个发力方法,这里传value为参数过去.
        else if (!state)
        {
            BallArm._instance.canMoveBallArm = true;
            //此时松开了，执行击球方法
            UISlider slider = go.GetComponent<UISlider>();
            float value = slider.value;
            if (value > 0.03)
            {
                BallArm._instance.FireBall(value);
            }
            //SpringBack(slider,value);
            slider.value = 0;
        }
    }
    /// <summary>
    /// 向左微调
    /// </summary>
    public void TrimmingLeft(GameObject go,bool state)
    {
        if (state)
        {
            BallArm._instance.BallArmRotateAround(-0.2f);
        }
    }
    /// <summary>
    /// 向右微调
    /// </summary>
    public void TrimmingRight(GameObject go,bool state)
    {
        if (state)
        {
            BallArm._instance.BallArmRotateAround(0.2f);
        }
    }
    ///// <summary>
    ///// slider弹簧回弹的方法
    ///// </summary>
    ///// <param name="value"></param>
    //void SpringBack(UISlider slider,float value)
    //{
    //        value= Mathf.Lerp(value, 0, 0.1f);
    //        slider.value = value;
    //}
}
