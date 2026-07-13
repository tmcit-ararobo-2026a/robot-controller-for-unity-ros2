using UnityEngine;
using ROS2;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.UI;
using std_msgs.msg;
using TMPro;
public class throwcontroller : MonoBehaviour
{
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<std_msgs.msg.Bool> throw_pub;
    private IPublisher<std_msgs.msg.Float32> throw_speed;
    public Slider throw_slider;
    public TextMeshProUGUI textMeshPro;
    bool buttonflag = false;
    void Start()
    {
        if (TryGetComponent(out ros2Unity))
        {
            if (ros2Unity.Ok())
            {
                ros2Node = ros2Unity.CreateNode("ThrowNode");
                throw_pub = ros2Node.CreatePublisher<std_msgs.msg.Bool>("/belt/throw");
                throw_speed = ros2Node.CreatePublisher<std_msgs.msg.Float32>("/belt/speed_ratio");
            }
        }
    }

    void FixedUpdate()
    {
        if (buttonflag)
        {
            std_msgs.msg.Bool boolmsg = new std_msgs.msg.Bool();
            boolmsg.Data = true;
            throw_pub.Publish(boolmsg);
        }
        if (!buttonflag)
        {
            std_msgs.msg.Bool boolmsg = new std_msgs.msg.Bool();
            boolmsg.Data = false;
            throw_pub.Publish(boolmsg);
        }
        std_msgs.msg.Float32 speedmsg = new std_msgs.msg.Float32();
        speedmsg.Data = (float)throw_slider.value;
        textMeshPro.text = "throw_speed    " + throw_slider.value;
        throw_speed.Publish(speedmsg);
        Debug.Log(speedmsg.Data);
    }
    // Update is called once per frame
    // ボタンを押したときの処理
    public void OnButtonDown()
    {
        buttonflag = true;
    }
    // ボタンを離したときの処理
    public void OnButtonUp()
    {
        buttonflag = false;
    }
}
