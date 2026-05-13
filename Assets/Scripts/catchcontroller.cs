using UnityEngine;
using ROS2;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.UI;
using std_msgs.msg;
public class catchcontroller : MonoBehaviour
{
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<std_msgs.msg.Bool> throw_pub;
    bool buttonflag = false;
    void Start()
    {
        if (TryGetComponent(out ros2Unity))
        {
            if (ros2Unity.Ok())
            {
                ros2Node = ros2Unity.CreateNode("CathchNode");
                throw_pub = ros2Node.CreatePublisher<std_msgs.msg.Bool>("/arm/hold");
            }
        }
    }
    public void OnButtonDown()
    {
        std_msgs.msg.Bool boolmsg = new std_msgs.msg.Bool();
        boolmsg.Data = true;
        throw_pub.Publish(boolmsg);
    }
    // ボタンを離したときの処理
    public void OnButtonUp()
    {
        std_msgs.msg.Bool boolmsg = new std_msgs.msg.Bool();
        boolmsg.Data = false;
        throw_pub.Publish(boolmsg);
        buttonflag = false;
    }
}
