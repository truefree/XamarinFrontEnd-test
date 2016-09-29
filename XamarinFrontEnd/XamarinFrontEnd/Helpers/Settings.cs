// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace XamarinFrontEnd.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants
        private const string ServerKey = "servers";
        private static readonly string ServerKeyDefault = "http://localhost:5845/api/users/";

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string UserEmailKey = "emailKey";
        private static readonly string UserEmailKeyDefault = string.Empty;

        private const string InternalIDKey = "internalIDKey";
        private static readonly string InternalIDDefault = string.Empty;

        private const string OTPIDKey = "OTPIDKey";
        private static readonly string OTPIDDefault = string.Empty;

        private const string IsUserEnrolledKey = "IsUserEnrolled";
        private static readonly bool IsUserEnrolledDefault = false;

        #endregion

        public static string ServerURL
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(ServerKey, ServerKeyDefault);
            }
        }

        public static string OTPID
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(OTPIDKey, OTPIDDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(OTPIDKey, value);
            }
        }

        public static bool IsUserEnrolled
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(IsUserEnrolledKey, IsUserEnrolledDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(IsUserEnrolledKey, value);
            }
        }

        public static string InternalID
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(InternalIDKey, InternalIDDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(InternalIDKey, value);
            }
        }

        public static string UserEmail
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(UserEmailKey, UserEmailKeyDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(UserEmailKey, value);
            }
        }

        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
            }
        }

    }
}