using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace API_read_Json.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("api/Crud_JsonRead")]
    public class Crud_JsonReadController : ApiController
    {

        public class PhoneBook
        {
            public int id { get; set; }
            public string Name { get; set; }
            public string type { get; set; }
            public long number { get; set; }
        }

        private string _pPath = @"C:\Program Files (x86)\IIS Express\file.json";

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

        //htttp:localhot:porta/api/Crud_JsonRead/Post?Parametrat
        [System.Web.Http.Route("Post")]
        [System.Web.Http.HttpPost]
        public void Post(string name, string type, long number, int id)
        {
            Orderby();

            string jsonii;
            var a = LoadJson(_pPath);

            PhoneBook phoneBook = new PhoneBook();
            phoneBook.id = id;
            phoneBook.Name = name;
            phoneBook.type = type;
            phoneBook.number = number;

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
            System.IO.File.WriteAllText(_pPath, jsonii);


        }


        //htttp:localhot:porta/api/Crud_JsonRead/edit?Parametrat
        [System.Web.Http.Route("edit")]
        [System.Web.Http.HttpPost]
        public void Edit(int id, string emer, string type, long number)
        {
            List<PhoneBook> PhoneBook = new List<PhoneBook>();

            string jsonii;
            var a = LoadJson(_pPath);
            foreach (var item in a)
            {
                if (item.id == id)
                {
                    item.id = id;
                    item.Name = emer;
                    item.type = type;
                    item.number = number;
                }
            }


            jsonii = new JavaScriptSerializer().Serialize(a);

            System.IO.File.WriteAllText(_pPath, jsonii);


        }

        //htttp:localhot:porta/api/Crud_JsonRead/delete?Parametrat
        [System.Web.Http.Route("delete")]
        [System.Web.Http.HttpDelete]
        public HttpResponseMessage fshij(int id)
        {
            List<PhoneBook> PhoneBook = new List<PhoneBook>();

            string jsonii;
            var a = LoadJson(_pPath);

            var item = a.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                a.Remove(item);
                jsonii = new JavaScriptSerializer().Serialize(a);
                System.IO.File.WriteAllText(_pPath, jsonii);
                return Request.CreateResponse(HttpStatusCode.OK, jsonii);
            }
            else
            {
                string nkgjendet = "nuk gjen ndonje me kte id";
                return Request.CreateResponse(HttpStatusCode.OK, nkgjendet);
            }

        }




        public void Orderby()
        {
            var listof_phoneBooks = LoadJson(_pPath);
            if (listof_phoneBooks != null)
            {
                List<PhoneBook> SortedList = listof_phoneBooks.OrderBy(o => o.Name).ToList();

                string jsonii = new JavaScriptSerializer().Serialize(SortedList);

                System.IO.File.WriteAllText(_pPath, jsonii);

            }
            else
            {
               
            }
          

        }

    }
}

