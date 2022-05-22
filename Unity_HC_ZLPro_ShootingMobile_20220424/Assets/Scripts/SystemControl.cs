using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

//命名空間
namespace remiel
{
    /// <summary>
    /// 控制系統:荒野亂鬥移動功能
    /// 虛擬搖桿控制角色移動
    /// </summary>
    public class SystemControl : MonoBehaviourPun
    {
        [SerializeField, Header("移動速度"), Range(0, 300)] private float speed = 3.5f;
        [SerializeField, Header("角色方向圖示範圍"), Range(0, 5)] private float rangeOirectionIcon = 2.5f;
        [SerializeField, Header("角色旋轉速度"), Range(0, 100)] private float speedTurn = 1.5f;
        [SerializeField, Header("動畫參數走路")] private string parameterWalk;
        [SerializeField, Header("畫布")] private GameObject goCanvas;
        [SerializeField, Header("畫布玩家資訊")] private GameObject goCanvasPlayerInfo;
        [SerializeField, Header("角色方向圖示")] private GameObject goDirection;


        private Rigidbody rigidBody;
        private Animator animator;
        private Joystick joystick;
        private Transform traDirectionIcon;
        private CinemachineVirtualCamera cvc;
        private SystemAttack systemAttack;


        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            systemAttack = GetComponent<SystemAttack>();

            if (photonView.IsMine)
            {
                PlayerUIFollow follow = Instantiate(goCanvasPlayerInfo).GetComponent<PlayerUIFollow>();
                follow.traPlayer = transform;

                traDirectionIcon = Instantiate(goDirection).transform; // 取得角色方向圖示
                Instantiate(goCanvasPlayerInfo);

                GameObject tempCavans = Instantiate(goCanvas);
                joystick = tempCavans.transform.Find("Floating Joystick").GetComponent<Joystick>(); // 取得畫布內的虛擬搖桿             
                systemAttack.btnFire = tempCavans.transform.Find("發射").GetComponent<Button>();

                cvc = GameObject.Find("CM管理器").GetComponent<CinemachineVirtualCamera>();
                cvc.Follow = transform;
            }
            else 
            {
                enabled = false;
            }
        }

        private void Update()
        {
            LookDirectionIconPos();
            UpdateDirectionPos();
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            Move();
            GetJoystickValue();
        }
            
        private void GetJoystickValue()
        {
            print("<color=yellow>水平:" + joystick.Horizontal + "</color>");
        }

        private void Move()
        {
            // 剛體.加速度 = 三維向量(x,y,z)
            rigidBody.velocity = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * speed;
        }

        // 更新角色方向圖示的座標
        private void UpdateDirectionPos()
        {
            // 新座標 = 角色的座標 + 三維向量(虛擬搖桿的水平與垂直 * 方向圖示的範圍)
            Vector3 pos = transform.position + new Vector3(joystick.Horizontal, 0, joystick.Vertical)*rangeOirectionIcon;
            // 更新方向圖示的座標 = 新作標
            traDirectionIcon.position = pos;
        }

        private void LookDirectionIconPos()
        {
            // 取得面相角度資訊 = 四位元.面相角度(方向圖示 - 角色) - 方向圖示與角色的向量
            Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
            // 角色的角度 = 四位元.插值(角色的角度.電像角度. 旋轉速度 * 一幀的時間)
            transform.rotation = Quaternion.Lerp(transform.rotation, look, speedTurn * Time.deltaTime);
            //角色的歐拉角度 = 三維向量(0, 原本的歐拉角度y , 0)nh
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        private void UpdateAnimation()
        {
            // 是否跑步 = 虛擬搖桿.水平.不為零 或 垂直 不為零
            bool run = joystick.Horizontal != 0 || joystick.Vertical != 0;
            animator.SetBool(parameterWalk, run);
        }
    }
}

