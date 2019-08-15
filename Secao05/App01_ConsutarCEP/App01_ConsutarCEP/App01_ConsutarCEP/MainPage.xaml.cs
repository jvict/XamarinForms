using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsutarCEP.Servico.Modelo;

namespace App01_ConsutarCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        int count = 0;
        Editor obj = new Editor();
   

        string _filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.txt");
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;

            if (File.Exists(_filename))
            {
               
               obj.Text = File.ReadAllText(_filename);
            }
        }

        private void BuscarCEP (object sender, EventArgs e)
        {
            //Logica do Programa

            //Validações
            string cep = CEP.Text.Trim(); // Remove todo espaço do inicio e do fim caso seja espaço em branco
            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoURL(cep);

                    if (end != null)
                    {

                        RESULTADO.Text = String.Format("Endereço: {2} - {3} - {0},{1} ", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " + cep,"OK");
                    }
                }catch(Exception ex)
                {
                    DisplayAlert("Erro Critico", ex.Message, "Ok");
                }
            }
            
        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;

            if(cep.Length != 8)
            {
                DisplayAlert("ERRO","CEP inválido ! O CEP deve conter 8 carateres.","OK");

                valido = false;
            }
            int NovoCEP = 0;
            if(!int.TryParse(cep, out NovoCEP))//Tente converter algo
            {
                
                DisplayAlert("ERRO", "CEP inválido ! O CEP deve ser composto apenas por numeros.", "OK");
                valido = false;
            }
            return valido;
        }

        public void OnSaveButtonClicked(object sender, EventArgs e)
        {
            File.WriteAllText(_filename, obj.Text);
        }

        public void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (File.Exists(_filename))
            {
                File.Delete(_filename);
            }
            obj.Text = string.Empty;
        }
        public void Button_Clicked(object sender, System.EventArgs e)
        {
            count++;
            ((Button)sender).Text = $"Você Clicou {count} vezes.";
        }
    }
}
