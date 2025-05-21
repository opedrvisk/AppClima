
using Newtonsoft.Json;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Digite o nome da cidade: ");
        string cidade = Console.ReadLine();

        string apiKey = "a minha key não está aqui por motivos de segurança";
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={apiKey}&units=metric&lang=pt_br";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage resposta = await client.GetAsync(url);
                resposta.EnsureSuccessStatusCode();

                string respostaJson = await resposta.Content.ReadAsStringAsync();

                var clima = JsonConvert.DeserializeObject<Clima>(respostaJson);

                Console.WriteLine($"\n📍 Cidade: {clima.Name}");
                Console.WriteLine($"🌡️ Temperatura: {clima.Main.Temp}°C");
                Console.WriteLine($"☁️ Clima: {clima.Weather[0].Description}");
                Console.WriteLine($"💨 Vento: {clima.Wind.Speed} m/s");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar a previsão: " + ex.Message);
            }
        }
    }
}

