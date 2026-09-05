using ROS2;
using UnityEngine;

public class cmdcontroller : MonoBehaviour
{
    public VariableJoystick joystickL;
    public VariableJoystick joystickR;
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<geometry_msgs.msg.Twist> twist_pub;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (TryGetComponent(out ros2Unity))
        {
            if (ros2Unity.Ok())
            {
                ros2Node = ros2Unity.CreateNode("cmd_vel_node");
                twist_pub = ros2Node.CreatePublisher<geometry_msgs.msg.Twist>("/cmd_vel");

            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ros2Unity == null || !ros2Unity.Ok() || ros2Node == null)
        {
            return;
        }
        geometry_msgs.msg.Twist msg = new geometry_msgs.msg.Twist();
        msg.Linear.X = (double)joystickL.Vertical;
        msg.Linear.Y = (double)joystickL.Horizontal;
        msg.Angular.Z = (double)joystickR.Horizontal;
        twist_pub.Publish(msg);

    }
}
