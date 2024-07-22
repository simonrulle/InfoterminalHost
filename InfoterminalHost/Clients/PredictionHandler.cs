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

namespace InfoterminalHost.Clients
{
    public class PredictionHandler
    {
        public PredictionHandler()
        {
            predictionClient = InitializeClient();
        }

        public ConversationAnalysisClient predictionClient { get; private set; }

        public async Task<Response> MakePredictionAsync(string input)
        {
            // CLU-Projekt credentials
            string projectName = "InformationTerminal";
            string deploymentName = "InfoTerminalProdDeployment";

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

        private static ConversationAnalysisClient InitializeClient()
        {
            // TODO credentials
            Uri endpoint = new Uri("https://informationterminalhostlanguage01.cognitiveservices.azure.com/");

            AzureKeyCredential credential = new AzureKeyCredential("3036306bd67d49b3bc24621977f73a46");

            ConversationAnalysisClient client = new ConversationAnalysisClient(endpoint, credential);

            return client;
        }
    }
}
