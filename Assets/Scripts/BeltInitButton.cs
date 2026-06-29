using UnityEngine;
using ROS2;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.UI;
using std_msgs.msg;
public class BeltInitButton : MonoBehaviour
{
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<std_msgs.msg.Bool> belt_init_pub;
    bool buttonflag = false;
    void Start()
    {
        if (TryGetComponent(out ros2Unity))
        {
            if (ros2Unity.Ok())
            {
                ros2Node = ros2Unity.CreateNode("BeltInitNode");
                belt_init_pub = ros2Node.CreatePublisher<std_msgs.msg.Bool>("/belt/init");
            }
        }
    }
    void FixedUpdate()
    {
        if (buttonflag)
        {
            std_msgs.msg.Bool boolmsg = new std_msgs.msg.Bool();
            boolmsg.Data = true;
            belt_init_pub.Publish(boolmsg);
        }
        if (!buttonflag)
        {
            std_msgs.msg.Bool boolmsg = new std_msgs.msg.Bool();
            boolmsg.Data = false;
            belt_init_pub.Publish(boolmsg);
        }
    }
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
