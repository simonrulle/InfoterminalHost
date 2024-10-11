using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure;
using Azure.AI.Language.Conversations;
using Azure.Core;
using Azure.Core.Serialization;
using InfoterminalHost.Services;

namespace InfoterminalHost.Clients
{
    public class PredictionHandler
    {
        ConfigurationHelperService configHelper = new ConfigurationHelperService();

        public ConversationAnalysisClient predictionClient { get; private set; }

        public PredictionHandler()
        {
            predictionClient = InitializeClient();
        }

        public async Task<Response> MakePredictionAsync(string input)
        {
            // CLU-Projekt credentials
            string projectName = configHelper.GetConfigurationValue("AzureLanguageService:ProjectName");
            string deploymentName = configHelper.GetConfigurationValue("AzureLanguageService:DeploymentName");

            // Request data aus anonymen Klassen
            var data = new
            {
                AnalysisInput = new
                {
                    ConversationItem = new
                    {
                        Text = input,
                        Id = "1",
                        ParticipantId = "1",
                    }
                },
                Parameters = new
                {
                    ProjectName = projectName,
                    DeploymentName = deploymentName,

                    // Nutze Utf16CodeUnit für string in .NET.
                    StringIndexType = "Utf16CodeUnit",
                },
                Kind = "Conversation",
            };

            // Konfiguriere JSON Serializer-Optionen für CamelCase
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var serializer = new JsonObjectSerializer(options);

            // Sende den Request an Prediction Endpunkt
            BinaryData binaryData = serializer.Serialize(data);
            RequestContent content = RequestContent.Create(binaryData);
            Response response = await predictionClient.AnalyzeConversationAsync(content);

            // Rückgabe der Prediction Antwort
            return response;
        }

        private ConversationAnalysisClient InitializeClient()
        {
            Uri endpoint = new Uri(configHelper.GetConfigurationValue("AzureLanguageService:ServiceEndpointUri"));

            AzureKeyCredential credential = new AzureKeyCredential(configHelper.GetConfigurationValue("AzureLanguageService:ServiceSecret"));

            ConversationAnalysisClient client = new ConversationAnalysisClient(endpoint, credential);

            return client;
        }
    }
}
