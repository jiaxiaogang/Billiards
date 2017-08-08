using UnityEngine;
using System.Collections;

public class SonBall : BallSuper
{
    public override void SetState()
    {
        base.SetState();
    }
    /// <summary>
    /// 子球开始函数
    /// </summary>
    void Awake()
    {
        
    }

    /// <summary>
    /// 子球更新函数
    /// </summary>
    void FixedUpdate()
    {
        SetStop();
        SetState();
        
        //判断当前状态如果不是Movement的话.上一状态是Movement则判断AllStop();
        if (tempstate == Ballstate.Movement && this.state != Ballstate.Movement)
        {

            BallSuper.isAllStop = AllStop();
            if(BallSuper.isAllStop)
            {
                print(this.gameObject.name + "号球最后停止了;");
            }
            //这里写初始化球杆的代码:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        }

        //在最后记录下当前有状态和下一状态对比:(如果当前状态为Movement下一状态不是,则转入AllStop()来判断所有状态;以初始化球杆用;
        tempstate = this.state;
    }
}
