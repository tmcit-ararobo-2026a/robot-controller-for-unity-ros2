using geometry_msgs.msg;
using ROS2;
using sensor_msgs.msg;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class armcontroller : MonoBehaviour
{
    public VariableJoystick joystickUp;
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<std_msgs.msg.Float32> arm_vertical;
    private IPublisher<std_msgs.msg.Float32> arm_horizontal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (TryGetComponent(out ros2Unity))
        {
            if (ros2Unity.Ok())
            {
                ros2Node = ros2Unity.CreateNode("ArmNode");
                arm_vertical = ros2Node.CreatePublisher<std_msgs.msg.Float32>("/arm/vertical");
                arm_horizontal = ros2Node.CreatePublisher<std_msgs.msg.Float32>("/arm/horizontal");
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
        std_msgs.msg.Float32 verticalmsg = new std_msgs.msg.Float32();
        std_msgs.msg.Float32 horizontalmsg = new std_msgs.msg.Float32();
        verticalmsg.Data = joystickUp.Vertical;
        horizontalmsg.Data = joystickUp.Horizontal;
        arm_vertical.Publish(verticalmsg);
        arm_horizontal.Publish(horizontalmsg);

    }
}
