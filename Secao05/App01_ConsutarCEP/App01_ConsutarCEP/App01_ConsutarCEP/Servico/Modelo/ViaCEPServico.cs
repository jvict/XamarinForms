using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using App01_ConsutarCEP.Servico.Modelo;


namespace App01_ConsutarCEP.Servico.Modelo
{
    public class ViaCEPServico
    {
        //Metodo com URL que pesquisa CEP no site Via CEP
        public static string EnderecoURL = "http://viacep.com.br/ws/{0}/json/";

        public static Endereco BuscarEnderecoURL (string cep)
        {
            //Passando para a URL que pesquisa o endereço o CEP que o usuario deseja pesquisar
            string NovoEndercoURL = string.Format(EnderecoURL, cep);

            //Realizando a pesquisa na internet
            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(NovoEndercoURL);

            //Convertendo o resultado da internet em umm obj da Classe Endereço
            Endereco end = JsonConvert.DeserializeObject<Endereco>(conteudo);

            if (end.cep == null) return null;

            return end;            
        }
    }
}
