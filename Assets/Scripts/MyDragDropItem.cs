using UnityEngine;
using System.Collections;

public class MyDragDropItem : UIDragDropItem
{
    public GameObject[] fiveBall;
    GameObject[] arrows;
    //[HideInInspector]
    //public static GameObject surfaceMenuBall;//滑动开始球碰撞到的边球;用不到这个全局静态变量了，前台放大动画结束执行不同的事件即可；
    void Awake()
    {
        //int whiteringWidth= (int)(GameObject.Find("whitering").GetComponent<UISprite>().width+100);
        arrows = GameObject.FindGameObjectsWithTag("arrow");
        //for (int i = 0; i < fiveBall.Length; i++)
        //{
        //    fiveBall[i].GetComponent<UISprite>().width = whiteringWidth / 4;
        //}
    }
    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);

        if (surface == null)
        {
            SlideStartToZero();
        }
        else if (surface.tag == "threeball")
        {
            //surfaceMenuBall = surface.gameObject;//用不到这个全局静态变量了，前台放大动画结束执行不同的事件即可；
            //这里开始将surface.gameObject变大并播放转动whitering的动画zfrom0to90
            TweenScale tweenScale = surface.gameObject.GetComponent<TweenScale>();
            surface.gameObject.GetComponent<TweenScale>().ResetToBeginning();
            //注意://////////////////////////这里写这样的代码很麻烦.我更喜欢在动画属性的On Finished里就把这个直接来一个ResetToBegining();

            tweenScale.enabled = true;
            surface.gameObject.GetComponent<TweenScale>().ResetToBeginning();
            surface.gameObject.GetComponent<UISprite>().depth = 4;//UI播放完毕要恢复回2

            TweenRotation tweenRot = GameObject.Find("whitering").GetComponent<TweenRotation>();
            tweenRot.ResetToBeginning();
            tweenRot.enabled = true;

            SlideStartToZero();
        }
    }
    protected override void OnDragDropMove(Vector2 delta)
    {//这里先把三个箭头都变绿，后期可根据vector2判断下坐标把坐标最近的箭头变绿，用户体验更友好些
        base.OnDragDropMove(delta);
       
        for (int i = 0; i < arrows.Length; i++)
        {
            UISprite uiSprite = arrows[i].GetComponent<UISprite>();
            if (uiSprite != null)
            {
                uiSprite.color = Color.green;
            }
        }
    }
    /// <summary>
    /// 这个方法里写了UI滑动开始球的位置归零和三箭头颜色归白
    /// </summary>
    void SlideStartToZero()
    {
        this.transform.localPosition = Vector3.zero;
        for (int i = 0; i < arrows.Length; i++)
        {
            UISprite uiSprite = arrows[i].GetComponent<UISprite>();
            if (uiSprite != null)
            {
                uiSprite.color = Color.white;
            }
        }
    }

    public void StartSingleGame()
    {
        Application.LoadLevel(1);
    }
    public void StartCreateBattle()
    {
        Application.LoadLevel(2);
    }
    public void StartJoinBattle()
    {
        Application.LoadLevel(3);
    }
}
