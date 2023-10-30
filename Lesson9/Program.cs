using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string[] domains = GetDomainList();
        using HttpClient client = new HttpClient();

        foreach (string domain in domains)
        {
            string url = $"https://en.wikipedia.org/api/rest_v1/page/html/{domain}";

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string article = await response.Content.ReadAsStringAsync();
                SaveArticle(domain, article);
            }
            else
            {
                Console.WriteLine($"Статья для домена {domain} не найдена.");
            }
        }
    }

    static string[] GetDomainList()
    {
        string[] domains = new string[676];

        int index = 0;
        for (char c1 = 'a'; c1 <= 'z'; c1++)
        {
            for (char c2 = 'a'; c2 <= 'z'; c2++)
            {
                domains[index] = $".{c1}{c2}";
                index++;
            }
        }

        return domains;
    }

    static void SaveArticle(string domain, string article)
    {
        string fileName = $"{domain}.html";
        File.WriteAllText(fileName, article);
        Console.WriteLine($"Статья для домена {domain} сохранена.");
    }
}