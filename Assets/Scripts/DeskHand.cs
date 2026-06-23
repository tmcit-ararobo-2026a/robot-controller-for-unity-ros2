using UnityEngine;
using ROS2;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.UI;
using std_msgs.msg;


public class DeskHand : MonoBehaviour
{
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<std_msgs.msg.Float32> finger_pub;
    public VariableJoystick joystickFinger;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (TryGetComponent(out ros2Unity))
        {
            if (ros2Unity.Ok())
            {
                ros2Node = ros2Unity.CreateNode("DeskHandNode");
                finger_pub = ros2Node.CreatePublisher<std_msgs.msg.Float32>("/desk/finger");
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
        std_msgs.msg.Float32 fingermsg = new std_msgs.msg.Float32();
        fingermsg.Data = joystickFinger.Vertical;
        finger_pub.Publish(fingermsg);
    }
}
