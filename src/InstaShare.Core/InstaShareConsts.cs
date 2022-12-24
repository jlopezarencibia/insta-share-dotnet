using InstaShare.Debugging;

namespace InstaShare
{
    public class InstaShareConsts
    {
        public const string LocalizationSourceName = "InstaShare";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "b8cfce033aff4fdf8d963bb162e85655";
    }
}
