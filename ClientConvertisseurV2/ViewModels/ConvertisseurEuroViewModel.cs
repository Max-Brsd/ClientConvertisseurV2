﻿using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClientConvertisseurV2.ViewModels
{
    internal class ConvertisseurEuroViewModel : ObservableObject
    {
        private ObservableCollection<Devise> devises;

        public ObservableCollection<Devise> Devises {
            get { return devises; }
            set
            {
                devises = value;
                OnPropertyChanged();
            }
        }

        public IRelayCommand BtnSetConversion { get; }
        public IRelayCommand BtnSetConversionInEuro { get; }

        private WSService wSService;

        private string montantSaisie;
        public string MontantSaisie
        {
            get { return montantSaisie; }
            set
            {
                montantSaisie = value;
                OnPropertyChanged();
            }
        }

        private Devise deviseSelected;
        public Devise DeviseSelected
        {
            get { return deviseSelected; }
            set
            {
                deviseSelected = value;
                OnPropertyChanged();
            }
        }

        private string montantDevise;
        public string MontantDevise
        {
            get { return montantDevise; }
            set
            {
                montantDevise = value;
                OnPropertyChanged();
            }
        }


        public ConvertisseurEuroViewModel()
        {
            wSService = WSService.GetInstance();
            ActionGetDataSync();
            BtnSetConversion = new RelayCommand(ActionSetConversion);
            BtnSetConversionInEuro = new RelayCommand(ActionSetConversionInEuro);
        }

        private void ActionSetConversion()
        {
            if ((deviseSelected == null))
            {
                throw new Exception("Il faut choisir une devise vers laquelle convertir!");
            }
            var result = 0.0;
            try
            {
                result = Convert.ToDouble(MontantSaisie) * deviseSelected.Taux;
            }
            catch (Exception ex)
            {
                throw new Exception("Le montant passé n'est pas un nombre décimal !");
            }
            MontantDevise = Convert.ToString(result);
        }

        private void ActionSetConversionInEuro()
        {
            if ((deviseSelected == null))
            {
                throw new Exception("Il faut choisir une devise depuis laquelle convertir!");
            }
            var result = 0.0;
            try
            {
                result = Convert.ToDouble(MontantSaisie) / deviseSelected.Taux;
            }
            catch (Exception ex)
            {
                throw new Exception("Le montant passé n'est pas un nombre décimal !");
            }
            MontantDevise = Convert.ToString(result);
        }

        private async void ActionGetDataSync()
        {
            var devises = new List<Devise>();
            try
            {
                devises = await wSService.GetDevisesAsync("devises");
            }
            catch (Exception ex)
            {
                throw new Exception("L'api n'est pas accessible !");
            }
            Devises = new ObservableCollection<Devise>(devises);
        }
    }
}
