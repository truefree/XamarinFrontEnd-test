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

    private const string SettingsKey = "settings_key";
    private static readonly string SettingsDefault = string.Empty;

        private const string UserEmailKey = "emailKey";
        private static readonly string UserEmailKeyDefault = string.Empty;
    #endregion

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