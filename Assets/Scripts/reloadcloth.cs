using UnityEngine;
using ROS2;
using UnityEngine.UI;
using TMPro;

public class reloadcloth : MonoBehaviour
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
                ros2Node = ros2Unity.CreateNode("ReloadClothNode");
                throw_pub = ros2Node.CreatePublisher<std_msgs.msg.Bool>("/reset_cloth");
            }
        }
    }

    void FixedUpdate()
    {
   
    }

    public void OnButtonDown()
    {
        buttonflag = true;
           std_msgs.msg.Bool boolmsg = new std_msgs.msg.Bool();
            boolmsg.Data = true;
            throw_pub.Publish(boolmsg);        
    }

    public void OnButtonUp()
    {
        buttonflag = false;
        std_msgs.msg.Bool boolmsg = new std_msgs.msg.Bool();
            boolmsg.Data = false;
            throw_pub.Publish(boolmsg);
    }
}