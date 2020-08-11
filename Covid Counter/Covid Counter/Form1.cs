using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Covid_Counter
{
    public partial class Form1 : Form
    {
        readonly HttpClient client;
        public Form1()
        {
            InitializeComponent();
            client = new HttpClient();
        }
        public async Task<string> GetSource()
        {
            var response = await client.GetAsync("https://www.google.com/search?q=%D0%A7%D0%B8%D1%81%D0%BB%D0%BE+%D0%B7%D0%B0%D0%B1%D0%BE%D0%BB%D0%B5%D0%B2%D1%88%D0%B8%D0%B9+%D0%BA%D0%BE%D0%B2%D0%B8%D0%B4+19+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F&oq=%D0%A7%D0%B8%D1%81%D0%BB%D0%BE+%D0%B7%D0%B0%D0%B1%D0%BE%D0%BB%D0%B5%D0%B2%D1%88%D0%B8%D0%B9+%D0%BA%D0%BE%D0%B2%D0%B8%D0%B4+19+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F&aqs=chrome..69i57j0l2.9855j0j7&sourceid=chrome&ie=UTF-8");
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }
            return source;
        }
        public string DoParse(IHtmlDocument document)
        {
            var result = document.QuerySelectorAll("div[jsname='fUyIqc']");
            return result.Count().ToString();
        }
        public async void StartParse()
        {
            var domParser = new HtmlParser();
            var source = await GetSource();
            Console.Items.Add("Loaded");
            var document = await domParser.ParseDocumentAsync(source);
            Console.Items.Add("Got document");
            var result =  DoParse(document);
            Console.Items.Add(result);
        }

        private void ParseButton_Click(object sender, EventArgs e)
        {
            StartParse();
        }
    }
}
