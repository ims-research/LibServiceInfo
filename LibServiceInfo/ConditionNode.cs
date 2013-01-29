namespace LibServiceInfo
{
    public class ConditionNode : Node
    {
        public ConditionNode(D3Node child) : base(child)
        {
            Condition = ServiceManager.ConditionList[child.global_guid];
        }

        public ConditionNode()
        {
        }

        private Condition Condition { get; set; }
    }
}