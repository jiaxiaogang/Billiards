using UnityEngine;
using System.Collections;
/// <summary>
/// 此类用于控制NGUI加入服务器场景和MyServer的交换信息所用
/// </summary>
public class JoinServerController : MonoBehaviour
{
    
    private string itemIP;


    /// <summary>
    /// 点击item按钮事件/这里将选中的item的ip信息取出并赋值给itemIP变量;
    /// </summary>
    public void GetSelectItemIPEvent()
    {
        itemIP = "每个item都有一个ip和roomName属性,我需要写一个itemValueSuper父类.然后写一个itemValue继承自父类.每实例化一个item的时候就给值;以使item的属性可以get出ip信息";
        //问题在于我在这个事件里怎么才能得到选中的item呢?只有item知道自己被选中了.所以我应该定义一个全局变量,让选中的item自己告诉我.所以我应该再在itemValue子类中定义一个传出ip值的方法;
    }

    /// <summary>
    /// 加入服务器按钮事件/这里需要一个IP参数为点击到的Item下的ipvalue;
    /// </summary>
    /// <param name="ipValue选中的item.ipvalue参数1"></param>
    public void ConnectServerEvent()
    {
        if (itemIP == null || itemIP == "")
        {
            print("这里没有选定item提示错误界面并将加入按钮闪一下红警");
        }
        else
        {
            MyServer._instance.ConServer(itemIP);
        }
    }
}
//关于这里一些设计不合理的记录：
/*问题描述：我在写一个NGUI的ScrollView-Grid-item的时候需要将每个Item下写一个IP属性，一个roomName属性。
*需要在加入服务器按钮事件中使用选中的item.IP
*我这里在JoinServer.cs代码里定义了全局变量,然后在item的itemValue.cs里定义了传出IP的方法,只要选中item就会将其ip传出。
*但感觉上这并不合理,这里为什么不是点加入服务器的时候再去主动到选中的item里取。而是每次选中item项都往外传。因为我未必在选中一项后就一定会去连击join.
*另外如果有一天我的需求变化：我需要传出的是itemValue.roomName;难道我也要去itemValue里去再添加一个传出roomName的方法吗?
 *思考：关于item当前选中项，只有item自己知道，是否应该给itemSuper父类制定一个全局变量。来随时取到当前被选中的itemValue.IP和itemValue.roomName呢？
*解决思路：此题目前我善未理解，等俺学了23式。再来看这题。肯定是小菜一蝶。。。目前选写一个传出吧。请上帝原谅新手的无奈。项目还是要继续；
*/