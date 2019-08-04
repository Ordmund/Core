namespace Core.Controllers
{
    public static class Defines
    {
#if UNITY_EDITOR
        public const bool UnityEditor = true;
#else
        public const bool UnityEditor = false;
#endif

#if PRODUCTION
        public const bool Production = true;
#else
        public const bool Production = false;
#endif
    }
}