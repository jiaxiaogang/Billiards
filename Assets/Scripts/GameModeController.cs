using UnityEngine;
using System.Collections;
/// <summary>
/// 游戏模式控制器：单机模式，服务器模式，客户端模式，
/// 其中单机模式显示出所有球及球杆等即可开始游戏
/// 其中服务器模式需要先建立服务器并且自己加入，显示一个等待客户端加入的界面。然后可以在加入后点开始游戏。再和客户端同时显示出球及球杆等
/// 其中客户端模式需要先出现加入服务器界面，然后点服务器名称加入，加入后。等待对方点开始游戏。开始后即可与服务端同时显示出球及球杆等
/// 三个模式全由这个控制器控制，继承出这个游戏控制器；
/// </summary>
public class GameModeController : MonoBehaviour
{
    public GameObject[] gos;//隐藏起来的所有球,射线rayc,地面和lookattarget目标物体(这里面不包括球杆:因为球杆只有击球主角才可以显示)
    public GameObject ballArm;//球杆
    public GameObject Menu;//Menu菜单
    public GameObject TouchControl;//TouchControl菜单
    public Camera motherBallCamera;//母球摄像机
    /// <summary>
    /// 显示创建服务器或加入服务器NGUI界面
    /// </summary>
    public virtual void ServerNGUI()
    {
        //这里写一个虚方法,
        //如果服务器则显示创建
        //如果是客户端则显示加入
    }
    /// <summary>
    /// 开始游戏方法,
    /// 三种模式共同点为显示出gos;
    /// </summary>
    public virtual void StartGame()
    {
        //这里写单机游戏的模式先//////////////////////////////////////////////////
        MenuHidden();
        Displaygos();
        DisplayTouchControl();
    }
    /// <summary>
    /// 显示出开始游戏的物体
    /// </summary>
    public void Displaygos()
    {
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i].SetActive(true);
        }
    }
    /// <summary>
    /// 击球者开场
    /// </summary>
    public void DisplayTouchControl()
    {
        motherBallCamera.GetComponent<Camera>().depth = 0;
        motherBallCamera.GetComponent<Animator>().SetTrigger("Transition");
        TouchControl.SetActive(true);
        ballArm.SetActive(true);
    }
    /// <summary>
    /// 游戏菜单隐藏
    /// </summary>
    public void MenuHidden()
    {
        //第一步:先把UI三球显示深度调节回2以免后面出错
        GameObject[] gos = GameObject.FindGameObjectsWithTag("threeball");
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i].gameObject.GetComponent<UISprite>().depth = 2;
        }
        Menu.SetActive(false);
    }
}
