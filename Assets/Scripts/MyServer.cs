using UnityEngine;
using System.Collections;

public class MyServer : MonoBehaviour
{
    public int connections = 2;
    public int listenPort = 8899;//选择一个不常用的端口(端口有范围)
    public bool useNat = false;
    public string ip = "127.0.0.1";
    //public GameObject playerPrefab;//引起预置件为player



    //用于反馈至UI上的bool变量
    public static MyServer _instance;
    [HideInInspector]
    public bool createServerSucessful = false;//创建服务器反馈变量
    [HideInInspector]
    public bool joinServerSucessful = false;//加入服务器反馈变量
    [HideInInspector]
    public bool playerBJoin = false;//有玩家加入服务器
    [HideInInspector]
    public string roomName;//创建的服务器房间名//应从UI传入
    [HideInInspector]
    public string serverIP;//服务器IP地址
    //public int serverPort;//服务器端口
    [HideInInspector]
    public bool disconnectServer;//服务器已取消
    [HideInInspector]
    public bool serverGaming;//正在游戏中....
    
    public string[] roomNames;
    private string RoomNamevalue;//创建房间里传入的房间名,未创建成功之时的保留字符串,用来创建服务器成功后建hashtabel所用
    private string RoomIPKey;//加入房间时传入的IP,key,在UI里选择了房间名(即value)其实是把key传过来;用来加入服务器时保留IP地址以免加错
    [HideInInspector]
    public Hashtable roomNameWithIPHashTable;

    void Awake()
    {
        _instance = this;
        roomNameWithIPHashTable.Add("roomname", "192.168.1.1");
    }
    /// <summary>
    /// 断开服务器
    /// </summary>
    public void DisconnectServer()
    {
        Network.Disconnect();//这里方法不一定正确.先这么写
        disconnectServer = true;
    }
    /// <summary>
    /// 创建服务器
    /// </summary>
    public void InitServer(string roomName)
    {
        Server(0, roomName);//参数0表示创建服务器
        RoomNamevalue = roomName;//将传来的房间名保留;

    }
    /// <summary>
    /// 加入服务器//由加入服务器OnButtonClick()方法调用,传过来item.IP;即roomIP;
    /// </summary>
    public void ConServer(string roomIP)
    {
        Server(1, roomIP);//参数1表示加入服务器
        RoomIPKey = roomIP;//将传来的IP地址保留;
    }
    /// <summary>
    /// 服务器类,参数int为0则为创建,为0则为加入
    /// </summary>
    /// <param name="i"></param>
    void Server(int i, string roomIP)
    {
        //Network.peerType有四个状态
        //NetworkPeerType.Disconnected未连接状态;
        //NewworkPeerType.Server服务器创建完成状态;
        //NewworkPeerType.Client客户端连接成功;
        //NewworkPeerType.Connecting正在连接;
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            if (i == 0)
            {
                //在这里创建Server;
                NetworkConnectionError error = Network.InitializeServer(connections, listenPort, useNat);

                print(error);/////////////////////////////////////////////////这里不知怎么把传入的房间名在创建成功后和此服务器绑定到一起?难道是绑定到IP吗?写个哈希表?然后如果创建成功就把hash[roomName]="192.168.1.108"?
                ///////////////////////////////////////////////////////////////但这里还有一个重复key的问题.难道把IP设置为key?然后把房间名设置为value?
                //创建成功后需要把此服务端IP取出并存到hashtable中;
            }
            if (i == 1)
            {
                //在这里加入Server;
                NetworkConnectionError error = Network.Connect(roomIP, listenPort);
                print(error);
            }
        }
        else if (Network.peerType == NetworkPeerType.Server)
        {
            print("已在作为服务器运行中....");
        }
        else if (Network.peerType == NetworkPeerType.Client)
        {
            print("客户端已接入");
            joinServerSucessful = true;//反馈变量加入成功
        }
    }
    //注意这两个方法都是在服务端调用的
    void OnServerInitialized()
    {
        print("Server完成初始化");
        //Network.player;//访问到当前的player,客户端//Network.Instantiate就是在局域网内创建一个物体;
        //int group = int.Parse(Network.player + "");//直接访问Network.player会得到当前客户端的索引是唯一的。
        //Network.Instantiate(playerPrefab, new Vector3(0, 10, 0), Quaternion.identity, group);
        createServerSucessful = true;//反馈变量创建成功
        roomNameWithIPHashTable.Add(Network.player.ipAddress, RoomNamevalue);//将创建服务器的IP和roomName添加到hash表中,以备用//////////////先这么写,未必正确
    }
    void OnPlayerConnected(NetworkPlayer player)
    {
        print("一个客户端连接过来:Index Number:" + player);
        playerBJoin = true;//玩家2接入成功
    }
    //注意这个是客户端确认方法
    void OnConnectedToServer()
    {
        print("我已成功连接到服务器");
        //int group = int.Parse(Network.player + "");//直接访问Network.player会得到当前客户端的索引是唯一的。

        //Network.Instantiate(playerPrefab, new Vector3(0, 10, 0), Quaternion.identity, group);
    }
}
