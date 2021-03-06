using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PortourgalAdmin.Model;

namespace PortourgalAdmin.Pages
{
    public class DistritoModel : PageModel
    {
        public void OnGet(string ascii)
        {
            Distrito = GetDistrito(ascii).Result;
        }

        public IActionResult OnPostDelete(string asciiname)
        {
            DeleteDistrito(asciiname);
            return new RedirectToPageResult("/Distritos");
        }

        public async Task<Distrito> GetDistrito(string ascii)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://portourgalapi2020.azurewebsites.net/api/distritos/nome/" + ascii).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                String json = await response.Content.ReadAsStringAsync();
                Distrito d = JsonConvert.DeserializeObject<Distrito>(json);
                return d;
            }
            else return null;
        }
        public async void DeleteDistrito(string asciiname)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync("https://portourgalapi2020.azurewebsites.net/api/distritos/nome/" + asciiname).ConfigureAwait(false);
        }

        public Distrito Distrito { get; set; }
    }
}
