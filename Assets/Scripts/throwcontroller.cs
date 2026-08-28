using UnityEngine;
using ROS2;
using UnityEngine.UI;
using TMPro;

public class throwcontroller : MonoBehaviour
{
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<std_msgs.msg.Bool> throw_pub;
    private IPublisher<geometry_msgs.msg.Twist> throw_pub_isaac;
    private IPublisher<std_msgs.msg.Float32> throw_speed;
    
    public Slider throw_slider;
    public TextMeshProUGUI textMeshPro;
    bool buttonflag = false;
    public Slider throw_sliderX;
    public Slider throw_sliderY;

    void Start()
    {
        if (TryGetComponent(out ros2Unity))
        {
            if (ros2Unity.Ok())
            {
                ros2Node = ros2Unity.CreateNode("ThrowNode");
                throw_pub = ros2Node.CreatePublisher<std_msgs.msg.Bool>("/belt/throw");
                throw_pub_isaac = ros2Node.CreatePublisher<geometry_msgs.msg.Twist>("/launcher/release_twist");
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
        else
        {
            std_msgs.msg.Bool boolmsg = new std_msgs.msg.Bool();
            boolmsg.Data = false;
            throw_pub.Publish(boolmsg);
        }

        std_msgs.msg.Float32 speedmsg = new std_msgs.msg.Float32();
        speedmsg.Data = (float)throw_slider.value;
        
        if (textMeshPro != null)
        {
            textMeshPro.text = "throw_speed    " + throw_slider.value;
        }
        
        if (throw_speed != null)
        {
            throw_speed.Publish(speedmsg);
        }
    }

    public void OnButtonDown()
    {
        buttonflag = true;

        
    }

    public void OnButtonDownisaac()
    {
       
        geometry_msgs.msg.Twist msg = new geometry_msgs.msg.Twist();
        msg.Linear.X = throw_sliderX.value;
        msg.Linear.Y = 0.0;
        msg.Linear.Z = throw_sliderY.value;
        throw_pub_isaac.Publish(msg);
    }

    public void OnButtonUp()
    {
        buttonflag = false;
    }
}