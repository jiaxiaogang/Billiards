using UnityEngine;
using System.Collections;

public class MotherBall : BallSuper
{
    //20140922下午测试三条射线的辅助算法有效性................................................
    //20140923辅助瞄准线的3D实时线............................................................
    //20140923下午更改1:大于10发射线改为1.......2:更改发射线水平方向.........3:把DoCollider里motherSpeed去掉相乘,因为得出的速度向量就是根据motherSpeed计算得出的;不需要再关联此向量...................
    //Ballstate state;
    Vector3 vecSpeed;//小球当前速度
    //Vector3 nowDirection;//小球当前方向

    public GameObject rayC;//发射射线中心点
    public GameObject rayL;//发射射线左点
    public GameObject rayR;//发射射线右点

    private GameObject ballC;
    private GameObject ballL;
    private GameObject ballR;//hitinfoR.transform.gameObject;

    private float distanceC = 0.15f;
    float distanceL = 0;
    float distanceR = 0;//hitinfoR距离

    //碰撞的角度
    float angleC;
    float angleL;
    float angleR;

    bool needColliderC = true;
    bool needColliderL = true;
    bool needColliderR = true;//60度标记位

    //记录射线碰撞位置
    Vector3 pointC;
    Vector3 pointL;
    Vector3 pointR;

    //碰撞点击目标球向量
    Vector3 colliderC;
    Vector3 colliderL;
    Vector3 colliderR;

    private bool alreadyVis = false;//已经显示出球杆标志符;////////////////////////此标记位解决了许久的实例化球杆Bug;;;;;;;;;记于20140922上午
    private LineRenderer linerenderer;//通过定义线的Z长度来调节线长;
    private float lineZ = 1;//辅助瞄准线长度;
    public Transform lineBallArm;//母球-球杆来定义方向

    void Awake()
    {
        tempstate = Ballstate.Stop;
        linerenderer = GameObject.Find("Line").GetComponent<LineRenderer>();
    }

    /// <summary>
    /// 母球设置状态重写
    /// </summary>
    public override void SetState()
    {
        base.SetState();
        if (this.state == Ballstate.InPocket)
        {
            this.state = Ballstate.WaitHoming;
        }
    }

    /// <summary>
    /// 这里遇到一些问题:::::::::::::::::::::::::::::::::::::::如果三条射线检测的话.有可能三条射线检测到两个球.而这两个球分前后顺序;并且球在什么时候触发碰撞也是个问题.....
    /// //////////;:;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;有解决方案了:判断发射源和hitinfo的distance然后最近的就是碰撞的球体....然后白球加个触发器大点.5cm或以上....触发两个球的改变力;
    /// </summary>
    void FixedUpdate()
    {
        SetStop();
        SetState();

        if (BallSuper.isAllStop)
        {
            DoBallArmToVis();
        }

        //判断当前状态如果不是Movement的话.上一状态是Movement则判断AllStop();
        if (tempstate == Ballstate.Movement && this.state != Ballstate.Movement)
        {
            isAllStop = AllStop();
            //这里写初始化球杆的代码:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
            print(this.state);
            print(isAllStop);
            alreadyVis = false;//标志当前未显示出球杆;//此标记解决了实例化球杆的Bug;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        }
        vecSpeed = transform.rigidbody.velocity;
        Debug.DrawLine(rayC.transform.position, new Vector3(vecSpeed.x, 3.327809f, vecSpeed.z), Color.red);
        Debug.DrawLine(rayR.transform.position, new Vector3(vecSpeed.x, 3.327809f, vecSpeed.z), Color.red);
        Debug.DrawLine(rayL.transform.position, new Vector3(vecSpeed.x, 3.327809f, vecSpeed.z), Color.red);

        #region 设置当前球状态(已被父方法SetState()取代);
        //if(transform.position.y<0.3f&&vecSpeed.magnitude<0.01f)
        //{
        //    this.state = MotherBallState.InPocket;
        //}
        //else if(vecSpeed.magnitude<0.01f)
        //{
        //    this.state = MotherBallState.Stop;
        //}
        //else
        //{
        //    this.state = MotherBallState.Movement;
        //} 
        #endregion
        if (vecSpeed.magnitude > 1f)//速度大于10发射射线
        {
            //初始化RayC的坐标为白球,射线朝向为运动方向;
            rayC.transform.position = new Vector3(transform.position.x, 3.327809f, transform.position.z);//设置为同一子物体了;
            rayC.transform.LookAt(vecSpeed);
            
            
            #region 三条射线代码
            //三条射线输出hitpoint距离和射中的GameObject;
            RaycastHit hitinfoC;
            RaycastHit hitinfoL;
            RaycastHit hitinfoR;
            //20140923上午更改日志：1：60度改为45度(因为真实碰撞点角度更大)
            //在射线里计算出向量和角度
            //中线肯定是不需要辅助碰撞的,所以中线只用于判断R和L的hitinfo.gameObject是否与中线是同一个球体

            if (Physics.Raycast(rayC.transform.position, new Vector3(vecSpeed.x, 0, vecSpeed.z), out hitinfoC))
            {
               
                if (hitinfoC.collider.tag == "Ball")
                {
                    distanceC = Vector3.Distance(hitinfoC.point, rayC.transform.position);
                    ballC = hitinfoC.transform.gameObject;
                    //pointC = hitinfoC.point;
                    //angleC = Vector3.Angle(vecSpeed, (hitinfoC.transform.position - hitinfoC.point));
                    //angleC = 45f + angleC / 2;//用45度+角/2得出真实击球方向差
                    //colliderC = Quaternion.Euler(new Vector3(0, angleC, 0)) * vecSpeed;//根据原向量和偏移角求出目标向量
                    //if (angleC < 60)
                    //{
                    needColliderC = false;//如果不是擦边则不需要辅助碰撞
                    //}
                }
                else
                {
                    distanceC = 0.15f;
                    ballC = null;
                    angleC = 0;
                    colliderC = Vector3.zero;
                }
            }
            if (Physics.Raycast(rayL.transform.position, new Vector3(vecSpeed.x, 0, vecSpeed.z), out hitinfoL))
            {
                if (hitinfoL.collider.tag == "Ball")
                {
                    distanceL = Vector3.Distance(hitinfoL.point, rayL.transform.position);
                    ballL = hitinfoL.transform.gameObject;
                    pointL = hitinfoL.point;
                    angleL = Vector3.Angle(vecSpeed, (hitinfoL.transform.position - hitinfoL.point));
                    angleL = 45f + angleL / 2;
                    colliderL = Quaternion.Euler(new Vector3(0, -angleL, 0)) * vecSpeed;//根据原向量和偏移角求出目标向量
                    if (angleL < 60f)
                    {
                        needColliderL = false;//如果不是擦边则不需要辅助碰撞
                    }
                    else
                    {
                        needColliderL = true;
                    }
                }
                else
                {
                    distanceL = 0;
                    ballL = null;
                    angleL = 0;
                    colliderL = Vector3.zero;
                }
            }
            if (Physics.Raycast(rayR.transform.position, new Vector3(vecSpeed.x, 0, vecSpeed.z), out hitinfoR))
            {
                if (hitinfoR.collider.tag == "Ball")
                {
                    distanceR = Vector3.Distance(hitinfoR.point, rayL.transform.position);
                    ballR = hitinfoR.transform.gameObject;
                    pointR = hitinfoR.point;
                    angleR = Vector3.Angle(vecSpeed, (hitinfoR.transform.position - hitinfoR.point));
                    angleR = 45f + angleR / 2;
                    colliderR = Quaternion.Euler(new Vector3(0, angleR, 0)) * vecSpeed;//根据原向量和偏移角求出目标向量
                    if (angleR < 60f)
                    {
                        needColliderR = false;//如果不是擦边则不需要辅助碰撞
                    }
                    else
                    {
                        needColliderR = true;
                    }
                }
                else
                {
                    distanceR = 0;
                    ballR = null;
                    angleR = 0;
                    colliderR = Vector3.zero;
                }
            }
            #endregion
            if (transform.rigidbody.velocity.magnitude < 1f)//当球速度小于10f则不进行辅助碰撞并且归0
            {
                ballC = null; ballL = null; ballR = null;
                distanceC = 0.15f; distanceL = 0; distanceR = 0;
            }
        }
       
        //检测射线设置瞄准辅助线的长度;
        RaycastHit hitinfoLine;
        if(Physics.Raycast(transform.position,(transform.position-new Vector3(lineBallArm.position.x,transform.position.y,lineBallArm.position.z)),out hitinfoLine))
        {
            //这里写辅助线结束点的代码;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;20140922中午记
            lineZ = Vector3.Distance(hitinfoLine.point, transform.position);
        }
        linerenderer.SetPosition(0, new Vector3(0, 0, 0));
        linerenderer.SetPosition(1, new Vector3(0, 0, lineZ));


        //在最后记录下当前有状态和下一状态对比:(如果当前状态为Movement下一状态不是,则转入AllStop()来判断所有状态;以初始化球杆用;
        tempstate = this.state;
    }
    /// <summary>
    /// 让球杆显示出来方法
    /// </summary>
    void DoBallArmToVis()
    {
        if (this.state == Ballstate.WaitHoming)
        {
            this.transform.position = new Vector3(-1.012217f, 3.327809f, 0.9877503f);
        }
        if (this.state == Ballstate.Stop&&!alreadyVis)
        {
            BallArm.canVis = true;//先将白球归位,再显示出球杆;
            alreadyVis = true;//此标记解决了实例化球杆的Bug;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        }
    }

    /// <summary>
    /// 触发器进入时发生的事件
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        GameObject go = GetCollisionBall(distanceC - 0.15f, distanceL, distanceR);
        if (go != null)//需要辅助碰撞
        {
            if (other.transform.gameObject == go)
            {
                //这里先写执行碰撞的代码和方向改变速度改变
                //根据不同的角度产生不同的速度给目标球;0.14小边为0.11速度/0.13为0.2速度/0.12为0.3速度
                //20140922下午这里发现两个Bug：1：中线为射线检测点的时候，不需要辅助2：目标球方向并不是hitinfo至hitball.transform.position的方向;
                //已知母球运动方向，和hitinfo可求出母球运动方向MotherBallDirection和hitinfo到目标球中心的方向的夹角a角
                //用90-a角即可得对角b//b/2即可得到碰撞点的角度
                float MotherSpeed = transform.rigidbody.velocity.magnitude;
                //if (go == ballC)
                //{
                //    DoCollider(ballC, colliderC, MotherSpeed, angleC);
                //}
                if (go == ballL)
                {
                    DoCollider(ballL, colliderL, MotherSpeed, angleL);
                }
                else if (go == ballR)
                {
                    DoCollider(ballR, colliderR, MotherSpeed, angleR);
                }
            }
        }
        //这里原本有三线数据归零语句,转到update里了...
    }

    /// <summary>
    /// 触发器进入后发生的事件
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other)
    {
        GameObject go = GetCollisionBall(distanceC - 0.15f, distanceL, distanceR);
        if (go != null)//需要辅助碰撞
        {
            if (other.transform.gameObject == go)
            {
                float MotherSpeed = transform.rigidbody.velocity.magnitude;
                if (go == ballL)
                {
                    DoCollider(ballL, colliderL, MotherSpeed, angleL);
                }
                else if (go == ballR)
                {
                    DoCollider(ballR, colliderR, MotherSpeed, angleR);
                }
            }
        }
    }

    /// <summary>
    /// 执行目标球的被碰撞实现与母球变向变速实现;
    /// </summary>
    /// <param name="go(目标球)"></param>
    /// <param name="colliderDir(碰撞目标向量)"></param>
    /// <param name="speed(母球速度)"></param>
    /// <param name="angle(碰撞点与中心角度)"></param>
    void DoCollider(GameObject goTarget, Vector3 colliderDir, float motherSpeed, float angle)
    {
        print("进入DoCollider");
        if (colliderDir != Vector3.zero)
        {
            if (angle < 70)//60-70度继承速度的3/10
            {
                goTarget.rigidbody.velocity = colliderDir * 0.5f;
                this.rigidbody.AddForce(-colliderDir * 70);
            }
            else if (angle < 80)//70-80度继承速度的2/10
            {
                goTarget.rigidbody.velocity = colliderDir  * 0.3f;
                this.rigidbody.AddForce(-colliderDir * 50);
            }
            else
            {
                goTarget.rigidbody.velocity = colliderDir  * 0.15f;
                this.rigidbody.AddForce(-colliderDir * 30);
            }
        }
    }
    /// <summary>
    /// 获得辅助碰撞的球对象;如果不需要辅助则返回null
    /// </summary>
    /// <param name="c"></param>
    /// <param name="l"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    GameObject GetCollisionBall(float c, float l, float r)
    {
        GameObject go = null;
        if (c == 0 && l == 0 && r == 0) { }//三个都为0
        else if (c == 0)//一个为0
        {
            if (l != 0 && r != 0) go = l < r ? ballL : ballR;
            else if (l == 0) { if (needColliderR) go = ballR; }//两个为0
            else if (r == 0) { if (needColliderL) go = ballL; }
        }
        else if (l == 0)
        {
            if (c != 0 && r != 0) go = c < r ? ballC : ballR;
            else if (c == 0) { if (needColliderR) go = ballR; }
            else if (r == 0) { if (needColliderC) go = ballC; }
        }
        else if (r == 0)
        {
            if (c != 0 && l != 0) go = c < l ? ballC : ballL;
            else if (c != 0) { if (needColliderC) go = ballC; }
            else if (l != 0) { if (needColliderL) go = ballL; }
        }
        else//三个都不为0
        {
            float temp = Mathf.Min(c, l, r);
            if (c == temp) { if (needColliderC) go = ballC; }
            else if (l == temp) { if (needColliderL) go = ballL; }
            else if (r == temp) { if (needColliderR) go = ballR; }
            else print("木有最近的!?警告错误!");
        }
        //假如两个球C可以射住，L或R也可以射住，那么肯定是正面射线比侧面射线近。如果侧面近则肯定不是同一个球；
        if(go==ballC)
        {
            go = null;
        }
        return go;
    }
    //void OnGUI()
    //{
    //    GUILayout.Label("按下空格切换到顶部观察视角");
    //}
}
