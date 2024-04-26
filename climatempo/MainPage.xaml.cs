using System.Text.Json;
namespace climatempo;

public partial class MainPage : ContentPage
{
	Resposta resposta;
	const string URL = "https://api.hgbrasil.com/weather?woeid&key=eeb4ae8c";
	
	public MainPage(){
		InitializeComponent();
		AtualizaTempo();
	}

	async void AtualizaTempo()
	{
		try
		{
			var HttpClient = new HttpClient();
			var Response = await HttpClient.GetAsync(URL);

			if(Response.IsSuccessStatusCode)
			{
				var Content = await Response.Content.ReadAsStringAsync();
				resposta = JsonSerializer.Deserialize<Resposta>(Content);
			}
		}

		catch(Exception e){
			System.Diagnostics.Debug.WriteLine(e);
		}

		PreencherTela();
	}
	

	void PreencherTela(){
		Temperatura.Text = resposta.results.temp + "ºC".ToString();
		Humidade.Text = resposta.results.humidity.ToString();
		
		Forca.Text = resposta.results.wind_speedy.ToString();
		Chuva.Text = resposta.results.rain.ToString();
		
		Tempo.Text = resposta.results.description;
		Direcao.Text = resposta.results.wind_cardinal;
		Fase.Text = resposta.results.moon_phase;
		Hora.Text = resposta.results.time;
		Dia.Text = resposta.results.sunrise;
		Noite.Text = resposta.results.sunset;

		int temp = 31;
		int sunrise = 0620;
		int sunset = 1850;
		int humidity = 25;

		string date = "11/11/2005";
		string time = "00:00";
		string description = "Tempo nublado";
		string currently = "dia";
		string city = "Apucarana";
		string wind_cardinal = "NE";
		string moon_phase = "Minguante";

		double cloudiness = 30;
		double rain = 11;	
		double wind_speedy = 5;

		if(resposta.results.currently == "dia"){
			if(resposta.results.rain > 10)
			chuva.Source = "chuva.png";
		
			else if(resposta.results.cloudiness > 20)
				chuva.Source = "nublado.png";

			else
				chuva.Source = "diaclaro.jpeg";
		}
		
		else {
			
			if(resposta.results.rain > 10)
			Chuva.Source = "noitechuvosa.jpeg";
		
			else if(resposta.results.cloudiness > 20)
				Chuva.Source = "nublado.jpg";

			else
				Chuva.Source = "nublado.jpg";
		}
		
	}

}
