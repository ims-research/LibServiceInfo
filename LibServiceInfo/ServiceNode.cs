namespace LibServiceInfo
{
    public class ServiceNode : Node
    {
        public ServiceNode(D3Node child) : base(child)
        {
            Service = ServiceManager.ServiceList[child.global_guid];
        }

        public ServiceNode()
        {
        }

        public Service Service { get; set; }
    }
}