using HtmlAgilityPack;
using InfoterminalHost.Interfaces;
using InfoterminalHost.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;

namespace InfoterminalHost.Services
{
    public class RoomsDataService : IRoomsDataService
    {
        public RoomsDataService() { }

        public  List<Person> GetPersonList()
        {
            // Lade die Webseite
            var url = "https://www.hochschule-stralsund.de/host/im-portrait/mitarbeitende/";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            // Wähle alle <div> Container mit class="contact-list__person" aus
            var personNodes = doc.DocumentNode.SelectNodes("//div[@class='contact-list__person']");

            // Erstelle eine Liste von Personen
            var personList = new List<Person>();

            // Loop durch alle Personen (Mapping!)
            foreach (var personNode in personNodes)
            {
                // Neues Personenobjekt 
                Person person = new Person();

                /*
                 * Map FirstName, LastName und Title
                 */
                var nameWrapper = personNode.SelectSingleNode(".//div[@class='contact-list__person-name']");
                var name = nameWrapper?.SelectSingleNode(".//h4[@class='h4-style']");

                if (name != null && name.ChildNodes.Count > 0)
                {
                    string fullname = string.Join("", Regex.Split(name.ChildNodes[0].InnerText, @"(?:\r\n\t|\n|\r|\t)"));
                    string[] splitFullname = fullname.Split(",");
                    if (splitFullname.Length >= 2)
                    {
                        person.LastName = splitFullname[0].TrimEnd();
                        person.FirstName = splitFullname[1].TrimStart();
                    }
                    else
                    {
                        person.LastName = fullname;
                    }

                }

                if (name != null && name.ChildNodes.Count > 1)
                {
                    person.Title = name.ChildNodes[1].InnerText;
                }

                /*
                 * Map Images
                 */
                var imageWrappers = personNode.SelectNodes(".//div[@class='contact-list__person-image-wrapper']");

                if (imageWrappers != null)
                {
                    var imageUrls = new List<string>();
                    foreach (var imageWrapper in imageWrappers)
                    {
                        var imgNode = imageWrapper.SelectSingleNode(".//img");
                        if (imgNode != null)
                        {
                            string srcValue = imgNode.GetAttributeValue("src", string.Empty);
                            imageUrls.Add(srcValue);
                        }
                    }
                    person.Images = imageUrls.ToList();
                }

                /*
                 * Map Role und Faculty
                 */
                var departmentWrapper = personNode.SelectSingleNode(".//div[@class='contact-list__person-department']");
                if (departmentWrapper != null && departmentWrapper.ChildNodes.Count == 1)
                {
                    person.Faculty = departmentWrapper.ChildNodes[0].InnerText;
                }
                if (departmentWrapper != null && departmentWrapper.ChildNodes.Count > 1)
                {
                    person.Role = departmentWrapper.ChildNodes[0].InnerText;
                    person.Faculty = departmentWrapper.ChildNodes[1].InnerText;
                }

                /*
                 * Map PhoneNumber
                 */
                var phoneWrapper = personNode.SelectSingleNode(".//div[@class='contact-list__person-phone']");
                if (phoneWrapper != null && phoneWrapper.ChildNodes.Count > 1)
                {
                    person.PhoneNumber = phoneWrapper.ChildNodes[1].InnerText;
                }

                /*
                 * Map Fax
                 */
                var faxWrapper = personNode.SelectSingleNode(".//div[@class='contact-list__person-fax']");
                if (faxWrapper != null && faxWrapper.ChildNodes.Count > 1)
                {
                    person.Fax = faxWrapper.ChildNodes[1].InnerText;
                }

                /*
                 * Map Building und Room
                 */
                var roomWrapper = personNode.SelectSingleNode(".//div[@class='contact-list__person-room']");
                if (roomWrapper != null && roomWrapper.ChildNodes.Count > 1)
                {
                    string fullStr = roomWrapper.ChildNodes[0].InnerText + " " + roomWrapper.ChildNodes[1].InnerText;
                    string[] splitFullStr = fullStr.Split(",");
                    if (splitFullStr.Length >= 2)
                    {
                        person.Room = splitFullStr[0].TrimEnd();
                        person.Building = splitFullStr[1].TrimStart();
                    }
                }

                /*
                 * Map Email
                 */
                var emailWrapper = personNode.SelectSingleNode(".//div[@class='contact-list__person-emailbr']");
                var mailList = emailWrapper?.SelectSingleNode(".//ul[@class='listmail']");
                if (mailList != null && mailList.ChildNodes.Count > 0)
                {
                    List<string> emails = new List<string>();

                    foreach (var mail in mailList.ChildNodes)
                    {
                        emails.Add(mail.InnerText);
                    }

                    person.Emails = emails;
                }

                // Hinzufügen von Person in Liste
                personList.Add(person);
            }

            // Rückgabe der Personen als Liste
            return personList;
        }
    }
}
