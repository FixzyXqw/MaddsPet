using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MaddsPet.Settings
{
    public class Settingsmanager
    {
        public static readonly string SettingsFolder =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "MaddsPet"
            );
        public static readonly string SettingsPath =
            Path.Combine(SettingsFolder, "settings.json");
        public string Pet { get; set; } = "blackcat";
        public bool HideOnFullscreenApps { get; set; } = true;
        public bool Autostart { get; set; } = false;

        public string MovingColorHex { get; set; } = "#FF00FF";
        public string IdleColorHex { get; set; } = "#800080";

        public int? WindowX { get; set; } = null;
        public int? WindowY { get; set; } = null;

        public int WindowWidth { get; set; } = 100;
        public int WindowHeight { get; set; } = 100;

        [JsonIgnore]
        public Color MovingColor
        {
            get => ColorTranslator.FromHtml(MovingColorHex);
            set => MovingColorHex = ColorTranslator.ToHtml(value);
        }

        [JsonIgnore]
        public Color IdleColor
        {
            get => ColorTranslator.FromHtml(IdleColorHex);
            set => IdleColorHex = ColorTranslator.ToHtml(value);
        }

        public static Settingsmanager Load()
        {
            if (!File.Exists(SettingsPath))
            {
                Settingsmanager settings = new Settingsmanager();
                settings.Save();
                return settings;
            }

            try
            {
                string json = File.ReadAllText(SettingsPath);

                return JsonSerializer.Deserialize<Settingsmanager>(json)
                    ?? new Settingsmanager();
            }
            catch
            {
                return new Settingsmanager();
            }
        }

        public void Save()
        {
            if (string.Equals(Pet, "custom", StringComparison.OrdinalIgnoreCase))
                Pet = "blackcat";

            Directory.CreateDirectory(SettingsFolder);

            string json = JsonSerializer.Serialize(
                this,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }
            );

            File.WriteAllText(SettingsPath, json);
        }
    }
}