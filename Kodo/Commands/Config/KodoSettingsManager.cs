using System.Text.Json;
using System.Text.Json.Nodes;

namespace Kodo.Commands.Config
{
    public class KodoSettingsManager
    {
        private static string GetAppSettingsPath()
        {
            var kodoHome = Environment.GetEnvironmentVariable("KODO_HOME");

            if (string.IsNullOrWhiteSpace(kodoHome))
                throw new InvalidOperationException("A variável de ambiente 'KODO_HOME' não está definida.");

            return Path.Combine(kodoHome, "appsettings.json");
        }

        private static JsonObject LoadSettings()
        {
            var path = GetAppSettingsPath();

            if (!File.Exists(path))
                throw new FileNotFoundException("Arquivo appsettings.json não encontrado em: " + path);

            var json = File.ReadAllText(path);
            var jsonObj = JsonNode.Parse(json)?.AsObject()
                          ?? throw new InvalidOperationException("Falha ao interpretar o appsettings.json.");

            return jsonObj;
        }

        public static void UpdateKodoSettings(Action<JsonObject> updateCallback)
        {
            var path = GetAppSettingsPath();
            var jsonObj = LoadSettings();

            if (jsonObj["KodoSettings"] is not JsonObject kodoSettings)
            {
                kodoSettings = new JsonObject();
                jsonObj["KodoSettings"] = kodoSettings;
            }

            updateCallback(kodoSettings);

            File.WriteAllText(path, jsonObj.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
