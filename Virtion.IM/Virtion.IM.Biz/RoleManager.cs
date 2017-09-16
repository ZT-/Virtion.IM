namespace Virtion.IM.View
{
    enum RoleMode
    {
        Two,
        Multi
    }

    class RoleManager
    {
        public RoleMode PlayRoleMode
        {
            get;
            set;
        }

        public RoleManager()
        {
            PlayRoleMode = RoleMode.Two;
        }

    }
}
