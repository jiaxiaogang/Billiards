using UnityEngine;
using System.Collections;

public class BallArm : MonoBehaviour
{
    //此代码作为母球的组件存在；；；；；；；；；；；；；；；；；；；
    //球杆实例化
    //球杆发力打球
    //白球LookAt桌子中心
    public GameObject ballArm;//球杆
    public Transform lookAtTarget;//默认瞄准目标;
    public float fireForce;//发射力
    public Transform fireForcePoint;//发力点（球杆子物体）
    public float dontVisTime;//发射后隐藏掉球杆倒计时;
    public float ballArmMoveSpeed;//球杆灵敏度
    [HideInInspector]
    public static bool canVis = true; //显示开关，默认为不显示
    bool onVis = true;//已显示标记，默认为已经显示;
    //float timer = 0;//计时器
    public GameObject ballArmFather;
    public Camera viceCamera;//3d摄像机
    private Camera mainCamera;//2d摄像机

    //外界要访问下面FireBall();ToMainCamera();和ToViceCamera();三个方法，所以写可否击球标记和单例模式；
    public bool canFireBall = false;//可以击球标记
    public bool canMoveBallArm = true;//可以瞄准开关
    public static BallArm _instance;
    void Awake()
    {
        //viceCamera = GameObject.FindGameObjectWithTag("3DCamera").GetComponent<Camera>();//这里把21行改行public了.因为同场景运行的问题;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //ballArmFather = GameObject.Find("BallArmFather") as GameObject;//这里把20行改为public了,因为同场景运行的问题
        _instance = this;
    }
    void Update()
    {
        if (canVis && !onVis)
        {
            //ballArm.renderer.enabled = true;//因为有子物体所以这个代码无效;但可以使用SetActiveRecursively(false)来递归控制子物体是否显示////////////////20140922早上
            ballArmFather.transform.position = transform.position;
            ballArmFather.transform.LookAt(lookAtTarget);
            ballArm.SetActive(true);
            //这里显示出球杆时，需要将球杆位置移动到母球附近并且。指向中心位置；
            //解决方案:用3dmax直接制作出轴心在前方50cm的球杆。并且斜10度
            //解决方案2:使用父物体对齐到母球

            //this.gameObject.transform.LookAt(lookAtTarget);
            onVis = true;
            ToViceCamera();//显示球杆时切换为3D摄像机
        }
        else if (!canVis && onVis)
        {
            //ballArm.renderer.enabled = false;
            ballArm.SetActive(false);
            onVis = false;
        }
        else if (canVis && onVis)
        {
            canFireBall = true;
            //if (canFireBall)
            //{
            //    //this.rigidbody.AddForce((transform.position - new Vector3(fireForcePoint.position.x, transform.position.y, fireForcePoint.position.z)) * fireForce, ForceMode.Force);
            //    this.rigidbody.velocity = (transform.position - new Vector3(fireForcePoint.position.x, transform.position.y, fireForcePoint.position.z)).normalized * 40;
            //    //this.rigidbody.AddForce(Vector3.forward * fireForce, ForceMode.Force);
            //    //timer += Time.deltaTime;
            //    StartCoroutine(DontVisTimer());
            //}

            //if (timer > dontVisTime)
            //{
            //    canVis = false;
            //    timer = 0;
            //    ToMainCamera();//隐藏球杆时显示为2D摄像机
            //}

            //ballArm.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * ballArmMoveSpeed);
            if (canMoveBallArm)
            {
                float moveSpeed = ballArmMoveSpeed * (Input.GetTouch(0).deltaPosition.x);
                //float moveSpeed = ballArmMoveSpeed * Input.GetAxis("Mouse X");
                BallArmRotateAround(moveSpeed);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ToMainCamera();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ToViceCamera();
            }
        }
        else
        {
            canFireBall = false;
        }
    }

    public void BallArmRotateAround(float moveSpeed)
    {
        ballArm.transform.RotateAround(transform.position, Vector3.up, moveSpeed);
    }
    /// <summary>
    /// 击球方法，让NGUI的Slider调用
    /// </summary>
    /// <param name="force"></param>
    public void FireBall(float force)
    {
        if (canFireBall)
        {
            this.rigidbody.velocity = (transform.position - new Vector3(fireForcePoint.position.x, transform.position.y, fireForcePoint.position.z)).normalized * (30 * force + 10f);
            StartCoroutine(DontVisTimer());
            canFireBall = false;
        }
    }
    IEnumerator DontVisTimer()
    {//这里应该有可以连续击球的bug；；；；；20140927发现。。。随后再解决先留着；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；
        yield return new WaitForSeconds(dontVisTime);
        canVis = false;
        print("等待了0.4秒并且稳藏掉了球杆;");
        ToMainCamera();//隐藏球杆时显示为2D摄像机
    }
    /// <summary>
    /// 切换到2D摄像机
    /// </summary>
    public void ToMainCamera()
    {
        //viceCamera.depth = -2;
        mainCamera.depth = 10;
    }
    /// <summary>
    /// 切换到3D摄像机
    /// </summary>
    public void ToViceCamera()
    {
        //viceCamera.depth = 1;
        mainCamera.depth = -10;
    }
}
