using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudSponge;
using System.Diagnostics;
using System.Configuration;

namespace CloudSpongeLib
{
    public class GatherEmailByCloudSponge 
    {
        public List<string> GetFriendsEmail()
        {
            List<string> lstEmail = new List<string>();
            try
            {
                try
            {
                //Client ID 	767237347813-0decm2cgu5sab8pr50rr0hmerh9e0q73.apps.googleusercontent.com
                //Client secret 	DQNCzBuiwo0U58rkAbo38Jvs

                string domainKey = ConfigurationSettings.AppSettings["DomainKey"];
                string domainPassword = ConfigurationSettings.AppSettings["DomainPassword"];

                //var api = new Api(Settings.Default.DomainKey, Settings.Default.DomainPassword);

                var api = new Api(domainKey, domainPassword);

                //var api = new Api("QHBFU6D43BDCTQJT3XVZ", "vtFIAaMaGkSYVZMY");

                var consent = api.Consent(ContactSource.Gmail);
                //var consent = api.Consent(ContactSource.Yahoo);

                //Process.Start("https://accounts.google.com/o/oauth2/auth?response_type=code&client_id=767237347813-0decm2cgu5sab8pr50rr0hmerh9e0q73.apps.googleusercontent.com&scope=https%3A%2F%2Fwww.google.com%2Fm8%2Ffeeds&redirect_uri=https%3A%2F%2Fapi.cloudsponge.com%2Fauth&state=import_id%3D30938548");
                Process.Start(consent.Url);

                bool complete = false;
                while (!complete)
                {
                    var events = api.Events(consent.ImportId);

                    foreach (var item in events.Events)
                        Console.WriteLine("{0}-{1}", item.Type, item.Status);

                    complete = events.IsComplete;
                }

                var contacts = api.Contacts(consent.ImportId);

                foreach (var item in contacts.Contacts)
                {
                    try
                    {
                        Console.WriteLine("{1}, {0} ({2})", item.FirstName, item.LastName, item.EmailAddresses.FirstOrDefault());
                        string emailAddresses = item.FirstName + "<~>" + item.LastName + "<~>" + item.EmailAddresses.FirstOrDefault();
                        lstEmail.Add(emailAddresses);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error : " + ex.StackTrace);
                    }
                }
            }
            catch (Exception)
            {
               
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return lstEmail;
        }

        public List<string> GetFriendsEmail(string mailType)
        {
            List<string> lstEmail = new List<string>();
            try
            {
                try
                {
                    //Client ID 	767237347813-0decm2cgu5sab8pr50rr0hmerh9e0q73.apps.googleusercontent.com
                    //Client secret 	DQNCzBuiwo0U58rkAbo38Jvs

                    string domainKey = ConfigurationSettings.AppSettings["DomainKey"];
                    string domainPassword = ConfigurationSettings.AppSettings["DomainPassword"];

                    //var api = new Api(Settings.Default.DomainKey, Settings.Default.DomainPassword);

                    var api = new Api(domainKey, domainPassword);

                    //var api = new Api("QHBFU6D43BDCTQJT3XVZ", "vtFIAaMaGkSYVZMY");

                    //var consent = api.Consent(ContactSource.Gmail);

                    ConsentResponse consent = null;

                    if (mailType == "gmail")
                    {
                        consent = api.Consent(ContactSource.Gmail);
                    }

                    if (mailType == "yahoo")
                    {
                        consent = api.Consent(ContactSource.Yahoo);
                    }

                    if (mailType == "hotmail")
                    {
                        consent = api.Consent(ContactSource.WindowsLive);
                    }
                    

                    //Process.Start("https://accounts.google.com/o/oauth2/auth?response_type=code&client_id=767237347813-0decm2cgu5sab8pr50rr0hmerh9e0q73.apps.googleusercontent.com&scope=https%3A%2F%2Fwww.google.com%2Fm8%2Ffeeds&redirect_uri=https%3A%2F%2Fapi.cloudsponge.com%2Fauth&state=import_id%3D30938548");
                    Process.Start(consent.Url);

                    bool complete = false;
                    while (!complete)
                    {
                        var events = api.Events(consent.ImportId);

                        foreach (var item in events.Events)
                            Console.WriteLine("{0}-{1}", item.Type, item.Status);

                        complete = events.IsComplete;
                    }

                    var contacts = api.Contacts(consent.ImportId);

                    foreach (var item in contacts.Contacts)
                    {
                        try
                        {
                            Console.WriteLine("{1}, {0} ({2})", item.FirstName, item.LastName, item.EmailAddresses.FirstOrDefault());
                            string emailAddresses = item.FirstName + "<~>" + item.LastName + "<~>" + item.EmailAddresses.FirstOrDefault();
                            lstEmail.Add(emailAddresses);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error : " + ex.StackTrace);
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return lstEmail;
        }

        public List<string> GetFriendsEmail(string dmnKey,string dmnPass)
        {
            List<string> lstEmail = new List<string>();
            
                try
                {
                    //Client ID 	767237347813-0decm2cgu5sab8pr50rr0hmerh9e0q73.apps.googleusercontent.com
                    //Client secret 	DQNCzBuiwo0U58rkAbo38Jvs

                    string domainKey = ConfigurationSettings.AppSettings["DomainKey"];
                    string domainPassword = ConfigurationSettings.AppSettings["DomainPassword"];

                    //var api = new Api(Settings.Default.DomainKey, Settings.Default.DomainPassword);

                    var api = new Api(domainKey, domainPassword);

                    //var api = new Api("QHBFU6D43BDCTQJT3XVZ", "vtFIAaMaGkSYVZMY");

                    var consent = api.Consent(ContactSource.Gmail);
                    //var consent = api.Consent(ContactSource.Yahoo);

                    //Process.Start("https://accounts.google.com/o/oauth2/auth?response_type=code&client_id=767237347813-0decm2cgu5sab8pr50rr0hmerh9e0q73.apps.googleusercontent.com&scope=https%3A%2F%2Fwww.google.com%2Fm8%2Ffeeds&redirect_uri=https%3A%2F%2Fapi.cloudsponge.com%2Fauth&state=import_id%3D30938548");
                    Process.Start(consent.Url);

                    bool complete = false;
                    while (!complete)
                    {
                        var events = api.Events(consent.ImportId);

                        foreach (var item in events.Events)
                            Console.WriteLine("{0}-{1}", item.Type, item.Status);

                        complete = events.IsComplete;
                    }

                    var contacts = api.Contacts(consent.ImportId);

                    foreach (var item in contacts.Contacts)
                    {
                        try
                        {
                            Console.WriteLine("{1}, {0} ({2})", item.FirstName, item.LastName, item.EmailAddresses.FirstOrDefault());
                            string emailAddresses = item.FirstName + "<~>" + item.LastName+"<~>"+item.EmailAddresses.FirstOrDefault();
                            lstEmail.Add(emailAddresses);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error : " + ex.StackTrace);
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.StackTrace);
                }
            
            
            return lstEmail;
        }
    }
}
