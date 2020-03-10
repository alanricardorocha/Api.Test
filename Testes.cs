using NUnit.Framework;
using System;
using System.Net.Http;

namespace Api.Test
{
    public class Testes
    {
        private const string pathApi1 = "https://alanricardorochaapi1.azurewebsites.net/";
        private const string pathApi2 = "https://alanricardorochaapi2.azurewebsites.net/";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TaxaJuros()
        {
            decimal taxaJuros;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync($"{pathApi1}/taxaJuros").Result;
                string conteudo = response.Content.ReadAsStringAsync().Result;
                taxaJuros = Convert.ToDecimal(conteudo.Replace(".", ","));
            }
            Assert.AreEqual(0.01D, taxaJuros);
        }

        [Test]
        public void CalculoJuros()
        {
            decimal resultado;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync($"{pathApi2}/calculaJuros?valorInicial=100&meses=5").Result;
                string conteudo = response.Content.ReadAsStringAsync().Result;
                resultado = Convert.ToDecimal(conteudo.Replace(".", ","));
            }
            Assert.AreEqual(105.10D, resultado);
        }

        [Test]
        public void ShowMeTheCode()
        {
            String resultado;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync($"{pathApi2}/showmethecode").Result;
                resultado = response.Content.ReadAsStringAsync().Result;
            }
            Assert.AreEqual("https://github.com/alanricardorocha", resultado);
        }

        [Test]
        public void TestaTruncamentoDuasCasasDecimais()
        {
            decimal resultado;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync($"{pathApi2}/calculaJuros?valorInicial=100&meses=500").Result;
                string conteudo = response.Content.ReadAsStringAsync().Result;
                resultado = Convert.ToDecimal(conteudo.Replace(".", ","));
            }
            Assert.AreEqual(14477.27M, resultado);
        }
    }
}