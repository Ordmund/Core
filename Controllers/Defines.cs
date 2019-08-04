namespace Core.Controllers
{
    public static class Defines
    {
#if UNITY_EDITOR
        public static bool UnityEditor => true;
#else
        public static bool UnityEditor => false;
#endif

#if DEPLOYMENT
        public static bool Deployment => true;
#else
        public static bool Deployment => false;
#endif
    }
}