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

            if (coursesNodes != null)
            {
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
            }
            else
            {
                /*
                 * Dummy zum testen (TODO Delete!!!)
                 */
                // Course1
                CourseOfStudy course1 = new CourseOfStudy();
                Semester semester = new Semester() {
                    Name = "WINF Sommersemester",
                    Timetable = "https://www.hochschule-stralsund.de/storages/hs-stralsund/FAK_WS/Allgemein/FaK_WS_Stundenplaene/Sommersemester/Sommersemester_WInf-M.pdf",                   
                };
                course1.Name = "Wirtschaftsinformatik - Master";
                course1.Semester = new List<Semester> { semester };
                course1.Semester.Add(semester);
                coursesList.Add(course1);

                // Course2
                CourseOfStudy course2 = new CourseOfStudy();
                Semester semester21 = new Semester()
                {
                    Name = "2. Fachsemester BWL ",
                    Timetable = "https://www.hochschule-stralsund.de/storages/hs-stralsund/FAK_WS/Allgemein/FaK_WS_Stundenplaene/Sommersemester/Sommersemester_WInf-M.pdf",
                };
                Semester semester22 = new Semester()
                {
                    Name = "4. Fachsemester BWL ",
                    Timetable = "https://www.hochschule-stralsund.de/storages/hs-stralsund/FAK_WS/Allgemein/FaK_WS_Stundenplaene/Sommersemester/Sommersemester_WInf-M.pdf",
                };
                Semester semester23 = new Semester()
                {
                    Name = "6. Fachsemester BWL ",
                    Timetable = "https://www.hochschule-stralsund.de/storages/hs-stralsund/FAK_WS/Allgemein/FaK_WS_Stundenplaene/Sommersemester/Sommersemester_WInf-M.pdf",
                };
                course2.Name = "BWL - Bachelor";
                course2.Semester = new List<Semester> { semester21, semester22, semester23 };
                coursesList.Add(course2);

                // Course3
                CourseOfStudy course3 = new CourseOfStudy();
                Semester semester31 = new Semester()
                {
                    Name = "2. Fachsemester GOEK ",
                    Timetable = "https://www.hochschule-stralsund.de/storages/hs-stralsund/FAK_WS/Allgemein/FaK_WS_Stundenplaene/Sommersemester/Sommersemester_WInf-M.pdf",
                };
                Semester semester32 = new Semester()
                {
                    Name = "4. Fachsemester GOEK ",
                    Timetable = "https://www.hochschule-stralsund.de/storages/hs-stralsund/FAK_WS/Allgemein/FaK_WS_Stundenplaene/Sommersemester/Sommersemester_WInf-M.pdf",
                };
                course3.Name = "GOEK - Bachelor";
                course3.Semester = new List<Semester> { semester31, semester32 };
                coursesList.Add(course3);
            }


            // Rückgabe der Kurse als Liste
            return coursesList;
        }
    }
}
