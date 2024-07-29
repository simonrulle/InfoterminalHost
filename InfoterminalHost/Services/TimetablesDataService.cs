using HtmlAgilityPack;
using InfoterminalHost.Interfaces;
using InfoterminalHost.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoterminalHost.Services
{
    public class TimetablesDataService : ITimetablesDataService
    {
        public TimetablesDataService() { }

        public ObservableCollection<CourseOfStudy> GetCoursesOfStudy()
        {
            // Lade die Webseite
            var url = "https://www.hochschule-stralsund.de/ws/studienorganisation/";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            // Wähle alle <div> Container mit class="accordion-751937 accordion" aus
            var coursesNodes = doc.DocumentNode.SelectNodes("//div[@class='accordion-751937 accordion']");

            // Erstelle eine Liste von Kursen
            var coursesList = new ObservableCollection<CourseOfStudy>();

            // Loop durch alle Kurse (Mapping!)
            foreach (var courseNode in coursesNodes)
            {
                // Neues Kursobjekt 
                CourseOfStudy course = new CourseOfStudy();

                /*
                 * Map Name
                 */
                var courseNameWrapper = courseNode.SelectSingleNode(".//a[@class='accordion__header']");
                course.Name = string.Join("", Regex.Split(courseNameWrapper.InnerText, @"(?:\r\n\t|\n|\r|\t)")).Replace("amp;", string.Empty).Trim();

                /*
                  * Map Semester
                  */
                var semesterWrapper = courseNode.SelectSingleNode(".//div[@class='csc-frame csc-frame-default frame-type-text frame-layout-0']");
                if (semesterWrapper != null && semesterWrapper.ChildNodes.Count > 0)
                {
                    List<Semester> semesters = new List<Semester>();

                    foreach (var semester in semesterWrapper.ChildNodes)
                    {
                        if (semester.ChildNodes.Count > 0)
                        {
                            Semester semesterInfo = new Semester();
                            semesterInfo.Name = semester.ChildNodes[0].InnerText;
                            semesterInfo.Timetable = "https://www.hochschule-stralsund.de/" + semester.ChildNodes[0].GetAttributeValue("href", string.Empty);
                            semesters.Add(semesterInfo);
                        }                      
                    }

                    course.Semester = semesters;
                }

                // Hinzufügen von Kurs in Liste
                coursesList.Add(course);
            }

            // Rückgabe der Kurse als Liste
            return coursesList;
        }
    }
}
