using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace API_read_Json.Controllers
{

    public class Crud_JsonReadController : Controller
    {

        public class PhoneBook
        {
            public string Name { get; set; }
            public string type { get; set; }
            public long number { get; set; }
        }



        public List<PhoneBook> LoadJson(string path)
        {
            List<PhoneBook> persons = new List<PhoneBook>();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();

                JavaScriptSerializer js = new JavaScriptSerializer();
                persons = js.Deserialize<List<PhoneBook>>(json);
                return persons;

            }

        }

        // GET: Crud_JsonRead
        [System.Web.Http.HttpPost]
        public void INSERT(/*string name, string type, int number*/)
        {
            string jsonii;
            var a = LoadJson(@"C:\Program Files (x86)\IIS Express\file.json");

            PhoneBook phoneBook = new PhoneBook();

            phoneBook.Name = "asdf";
            phoneBook.type = "asdf";
            phoneBook.number = 4448454845548;

            if (a == null)
            {
                List<PhoneBook> phoneBooks = new List<PhoneBook>();
                phoneBooks.Add(phoneBook);
                 jsonii = new JavaScriptSerializer().Serialize(phoneBooks);
            }
            else
            {
                a.Add(phoneBook);
                jsonii = new JavaScriptSerializer().Serialize(a);
            }
           



          


            //shkruaj nje string json at specifik path :P
            System.IO.File.WriteAllText(@"C:\Program Files (x86)\IIS Express\file.json", jsonii);


        }


        [System.Web.Http.HttpPost]
        public void Edit(/*string emer, string type, int number*/)
        {
            List<PhoneBook> PhoneBook = new List<PhoneBook>();
            PhoneBook.Add(new PhoneBook()
            {
                Name = "asdf",
                type = "asdf",
                number = 4448454845548
            });

            var ekstract = LoadJson(@"C:\Program Files (x86)\IIS Express\file.json");
            foreach (var item in ekstract)
            {

            }


        }

    }
}