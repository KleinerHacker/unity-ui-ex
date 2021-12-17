namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime
{
    internal static class UnityUIExConstants
    {
        public const string Root = "Unity UI Extension";

        public static class Menus
        {
            private const string ComponentMenu = Root + "/Component";
            
            public static class Components
            {
                private const string UiMenu = ComponentMenu + "/UI";

                public static class Ui
                {
                    public const string ComponentMenu = UiMenu + "/Components";
                    public const string InputMenu = UiMenu + "/Input";
                    public const string WindowMenu = UiMenu + "/Window";

                    public static class Window
                    {
                        public const string MiscMenu = WindowMenu + "/Misc";
                    }
                }
            }
        }
    }
}
