namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime
{
    internal static class UnityUIExConstants
    {
        public const string Root = "Unity UI Extension";

        public static class Menu
        {
            private const string ComponentMenu = Root + "/Component";
            
            public static class Component
            {
                private const string UIMenu = ComponentMenu + "/UI";

                public static class UI
                {
                    public const string ComponentMenu = UIMenu + "/Components";
                    public const string InputMenu = UIMenu + "/Input";
                    public const string WindowMenu = UIMenu + "/Window";
                    public const string AudioMenu = UIMenu + "/Audio";

                    public static class Window
                    {
                        public const string MiscMenu = WindowMenu + "/Misc";
                    }
                }
            }
        }
    }
}
