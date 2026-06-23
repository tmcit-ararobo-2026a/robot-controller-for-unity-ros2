using UnityEngine;
using ROS2;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.UI;
using std_msgs.msg;

public class DeskArm : MonoBehaviour
{
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    public VariableJoystick joystickLiftAndDepth;
    private IPublisher<std_msgs.msg.Float32> lift_pub;
    private IPublisher<std_msgs.msg.Float32> depth_pub;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (TryGetComponent(out ros2Unity))
        {
            if (ros2Unity.Ok())
            {
                ros2Node = ros2Unity.CreateNode("DeskArmNode");
                lift_pub = ros2Node.CreatePublisher<std_msgs.msg.Float32>("/desk/lift");
                depth_pub = ros2Node.CreatePublisher<std_msgs.msg.Float32>("/desk/depth");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ros2Unity == null || !ros2Unity.Ok() || ros2Node == null)
        {
            return;
        }
        std_msgs.msg.Float32 liftmsg = new std_msgs.msg.Float32();
        std_msgs.msg.Float32 depthmsg = new std_msgs.msg.Float32();
        liftmsg.Data = joystickLiftAndDepth.Vertical;
        depthmsg.Data = joystickLiftAndDepth.Horizontal;
        lift_pub.Publish(liftmsg);
        depth_pub.Publish(depthmsg);
    }
}
