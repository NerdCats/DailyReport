using FetchDailyReport.Model;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace FetchDailyReport.Utility
{
    class HttpRequest
    {
        public string getAuthToken()
        {
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var authUrl = URLs.authEndPoint + "?userName=riyad&password=123456&grant_type=password&client_id=GoFetchDevWebApp";

            var loginCredential = JsonConvert.DeserializeObject<LoginCredentials>(File.ReadAllText(URLs.LoginCredentialsPath));
            var values = new NameValueCollection();
            values["userName"] = loginCredential.userName;
            values["password"] = loginCredential.password;
            values["grant_type"] = loginCredential.grant_type;
            values["client_id"] = loginCredential.client_id;


            var authenticationResponse = client.UploadValues(URLs.authEndPoint, values);
            var responseString = Encoding.Default.GetString(authenticationResponse);

            var token = JsonConvert.DeserializeObject<Token>(responseString);
            return token.access_token;
        }

        public CountReport getReport(string url, string access_token)
        {
            WebClient client = new WebClient();
            client.Headers.Add("Authorization", "Bearer " + access_token);

            var reportResponse = client.DownloadString(url);
            var report = JsonConvert.DeserializeObject<CountReport>(reportResponse);
            return report;
        }
    }
}
