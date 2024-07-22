﻿using Azure;
using CommunityToolkit.Mvvm.ComponentModel;
using InfoterminalHost.Clients;
using InfoterminalHost.Models;
using Microsoft.UI.Xaml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfoterminalHost.ViewModels
{
    public partial class AssistantViewModel : ObservableObject
    {
        PredictionHandler predictionHandler;

        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        string prompt = "";

        public AssistantViewModel()
        {
            this.predictionHandler = new PredictionHandler();
            MakeTestPredictionAsync();
        }

        public async void MakeTestPredictionAsync()
        {
            try
            {
                Response response = await predictionHandler.MakePredictionAsync("In welchem Raum sitzt Frau Wenzel?");
                JsonResult result = JsonConvert.DeserializeObject<JsonResult>(response.Content.ToString());
            }
            catch (RequestFailedException ex)
            {
                throw new RequestFailedException(ex.ToString());
            }
        }

        public void OnButtonClick(object sender, RoutedEventArgs e)
        {
            IsLoading = true;
        }
    }
}
