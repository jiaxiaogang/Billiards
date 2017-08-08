using UnityEngine;
using System.Collections;
/// <summary>
/// 母球状态:运动,停止,入袋;
/// </summary>
public enum Ballstate
{
    Movement,
    Stop,
    InPocket,
    WaitHoming
}
public class BallSuper : MonoBehaviour
{
    //[HideInInspector] 
    public BallSuper[] balls;//获得所有球以判断状态提交给球杆类

    public Ballstate state;//球的状态

    protected Ballstate tempstate = Ballstate.Stop;//上一桢状态默认为Stop;

    public static bool isAllStop = false;//是否全部停止运动

    public float stopSpeed = 0.02f;

 
    ///// <summary>
    ///// 构造函数:此时其它球还未生成,所以这里是取不到其它球的包括0.<MotherBall>();
    ///// </summary>
    //public Ball()
    //{
    //    GetAllBalls();
    //}
    ///// <summary>
    ///// 给Ball[] balls赋值/获取到16个球代码组件
    ///// </summary>
    //void GetAllBalls()
    //{
    //    balls[0] = GameObject.Find("0").GetComponent<MotherBall>();
    //    for (int i = 1; i < balls.Length; i++)
    //    {
    //        balls[i] = GameObject.Find(i.ToString()).GetComponent<SonBall>();
    //    }
    //}


    /// <summary>
    /// 设置状态方法;//写成虚方法:因为子类切换状态后的行为不同,所有球离开Movement状态都需要判断所有球的当前状态以初始化球杆,但白球进入球袋状态,需要转入待归位状态;
    /// </summary>
    public virtual void SetState()
    {
        //假如其中一颗球进入Stop状态或进入InPocket状态,则判断其它球是否都不是Movement状态,只要全部不是Movement则等待0.5秒实例化球杆
        float nowSpeed = transform.rigidbody.velocity.magnitude;
        if (transform.position.y < 3f && nowSpeed < stopSpeed)
        {
            this.state = Ballstate.InPocket;
        }
        else if (nowSpeed < stopSpeed)
        {
            this.state = Ballstate.Stop;
        }
        else
        {
            this.state = Ballstate.Movement;
        }
    }
    /// <summary>
    /// 低于最小速度则测试速度为0
    /// </summary>
    public void SetStop()
    {
        if(transform.rigidbody.velocity.magnitude<stopSpeed)
        {
            transform.rigidbody.velocity = new Vector3(0, 0, 0);
            transform.rigidbody.angularVelocity = new Vector3(0, 0, 0);
        }
    }
    /// <summary>
    /// 判断所有球是否已全非Movement状态
    /// </summary>
    protected bool AllStop()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            if(balls[i].state == Ballstate.Movement)
            {
                return false;
            }
        }
        return true;
    }
}
