using UnityEngine;
using System.Collections;
/// <summary>
/// 此类用于控制NGUI创建服务器场景和MyServer的交换信息所用
/// </summary>
public class CreateServerController : MonoBehaviour
{

    private bool firstCanGreen = false;
    private bool secondCanGreen = false;//用于标识两个指示灯是否可绿
    public TweenColor firstLight;
    public TweenColor secondLight;//两个指示灯物体
    public string roomNameLableText;//房间名Lable.text
    private string defaultRoomName = "我的桌球厅";



    void Start()
    {
        roomNameLableText = GameObject.Find("roomNameLabel").GetComponent<UILabel>().text.Trim();
    }



    void Update()
    {
        if (MyServer._instance.createServerSucessful == true)//创建服务器成功//亮起第一个指示灯
        {
            firstCanGreen = true;
        }
        if (firstCanGreen && !firstLight.enabled)
        {
            firstLight.enabled = true;
        }
        if (MyServer._instance.playerBJoin)//有用户加入进来//亮起第二个指示灯
        {
            secondCanGreen = true;
        }
        if (secondCanGreen && !secondLight.enabled)
        {
            secondLight.enabled = true;
        }
    }
    /// <summary>
    /// 按下创建房间事件//这里创建一个服务器并且将房间名传过去;
    /// </summary>
    public void CreateServerEvent()
    {
        if (roomNameLableText == "输入房间名" || roomNameLableText == null || roomNameLableText == "")
        {
            MyServer._instance.InitServer(defaultRoomName);
        }
        else
        {
            MyServer._instance.InitServer(roomNameLableText);
        }

    }
    /// <summary>
    /// 按下返回键事件//这里断开服务器,将所有绿灯设置为红色,将红绿灯动画关闭//>>>>>>>>>>>>>>>>>>>>将hashtabel的这个IP去掉此功能随后添加>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    /// </summary>
    public void BackButtonEvent()
    {
        MyServer._instance.DisconnectServer();
        if (firstLight.enabled)
        {
            firstLight.ResetToBeginning();
            firstLight.enabled = false;
        }
        if (secondLight.enabled)
        {
            secondLight.ResetToBeginning();
            secondLight.enabled = false;
        }
    }
}
