using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Xml;
using System.Net.WebSockets;
using Newtonsoft.Json;

namespace Rechner
{

    public partial class MainWindow : Window
    {



        public MainWindow()
        {
            InitializeComponent();

        }





        private void LivePriceButton_Click(object sender, RoutedEventArgs e)
        {



            try
            {
                //Verdopplung des Einsatzes
                int value = 2;
                //Eingaben auslesen
                double num = double.Parse(SetStartValue.Text);
                double Equity = double.Parse(SetStartValueEquity.Text);
                double leverage = double.Parse(LeverageSliderTextbox.Text);
                //Berechnung des Liquiditionspreises
                double risk = (100 - leverage / ((leverage + 1) / (100 + (0.24 * (leverage / (0.5 + (0.48 * leverage))))))) / 100;
                double eventrisk = risk;
                //Cryptowährung wählen
                string currencyTyp = CurrencyCombobox.Text;
                //Items löschen aus der Liste
                ListView2.Items.Clear();

                //Setze Live Label Preis
                Bitstamp_Api cryptoCurrencyLive;
                using (WebClient w = new WebClient())
                {
                    var s = w.DownloadString(String.Format("https://www.bitstamp.net/api/v2/ticker/{0}usd/", currencyTyp));
                    cryptoCurrencyLive = JsonConvert.DeserializeObject<Bitstamp_Api>(s);
                }
                LivePriceLabel.Content = cryptoCurrencyLive.bid;

                //Starte die Berechnung
                for (double x = num; x < Equity; x = num)
                {
                    //Neues Objekt
                    Bitstamp_Api cryptoCurrency;
                    using (WebClient w = new WebClient())
                    {
                        var s = w.DownloadString(String.Format("https://www.bitstamp.net/api/v2/ticker/{0}usd/", currencyTyp));
                        cryptoCurrency = JsonConvert.DeserializeObject<Bitstamp_Api>(s);
                    }
                    // setzt den 
                    cryptoCurrency.num = num;

                    ListView2.Items.Add(cryptoCurrency);

                    cryptoCurrency.bid -= (eventrisk * cryptoCurrency.bid) / 2;
                    eventrisk += risk;
                    num = value * num;
                }


            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }





        }



        private void SetPriceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Verdopplung des Einsatzes
                int value = 2;
                //Eingaben auslesen
                double num = double.Parse(SetStartValue.Text);
                double Equity = double.Parse(SetStartValueEquity.Text);
                double leverage = double.Parse(LeverageSliderTextbox.Text);
                //Berechnung des Liquiditionspreises
                double risk = (100 - leverage / ((leverage + 1) / (100 + (0.24 * (leverage / (0.5 + (0.48 * leverage))))))) / 100;
                double eventrisk = risk;
                //Cryptowährung wählen
                string currencyTyp = CurrencyCombobox.Text;
                //Items löschen aus der Liste
                ListView2.Items.Clear();

                for (double x = num; x < Equity; x = num)
                {
                    //Objekt erstellen
                    Bitstamp_Api cryptoCurrency = new Bitstamp_Api(double.Parse(SetPriceBox.Text));

                    cryptoCurrency.num = num;

                    ListView2.Items.Add(cryptoCurrency);

                    cryptoCurrency.bid -= (eventrisk * cryptoCurrency.bid) / 2;
                    eventrisk += risk;
                    num = value * num;
                }


            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            var slider = sender as Slider;

            int value = (int)slider.Value;
            LeverageSliderTextbox.Text = value.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListView2.Items.Clear();
        }

    }

}
