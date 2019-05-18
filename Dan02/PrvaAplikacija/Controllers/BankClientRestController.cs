using PrvaAplikacija.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;

namespace PrvaAplikacija.Controllers
{
    // [RoutePrefix("api/clients")]
    public class BankClientRestController : ApiController
    {
        private List<BankClientModel> GetDb()
        {
            return new List<BankClientModel>
            {
                new BankClientModel(1, "Dusko", "Dugousko", "bb@gmail.com", new DateTime(1938, 4, 30), "New York"),
                new BankClientModel(2, "Homer", "Simpson", "homer@gmail.com", new DateTime(1956, 3, 17), "Springfield"),
                new BankClientModel(3, "Lisa", "Simpson", "lisa@gmail.com", new DateTime(1980, 5, 11), "Springfield"),
                new BankClientModel(4, "Bart", "Simpson", "bart@gmail.com", new DateTime(1978, 4, 25), "Springfield")
            };
        }

        [Route("api/clients")]
        [HttpGet]
        public List<BankClientModel> GetByName([FromUri] string name)
        {
            List<BankClientModel> statClients = GetDb();
            return statClients.FindAll(x => x.Name == name);
        }


        // GET: api/BankClientRest
        [Route("api/clients")]
        [HttpGet]
        public IEnumerable<BankClientModel> Get()
        {
            return new List<BankClientModel>
            {
                new BankClientModel(1, "Mladen", "Kanostrevac", "mk@gmail.com"),
                new BankClientModel(2, "Ivan", "Vasiljevic", "iv@gmail.com")

            };
        }

        [Route("api/clients/{id}")]
        // GET: api/BankClientRest/5
        public BankClientModel Get(int id)
        {
            var clients = new List<BankClientModel>
            {
                new BankClientModel(1, "Mladen", "Kanostrevac", "mk@gmail.com"),
                new BankClientModel(2, "Ivan", "Vasiljevic", "iv@gmail.com"),
                new BankClientModel(3, "Caba", "Varga", "cv@gmail.com")
            };

            return (clients.Exists(x => x.Id == id)) ? clients.Find(x => x.Id == id) : null;
        }

        [Route("api/clients")]
        [HttpPost]
        // POST: api/BankClientRest
        public void AddClient([FromBody]BankClientModel client)
        {
            Debug.WriteLine($"{client.Name}, {client.Id}");
        }


        [Route("api/clients/{id:int}")]
        [HttpPut]
        // PUT: api/clients/5
        public BankClientModel Put(int id, [FromBody]BankClientModel client)
        {
            var clients = new List<BankClientModel>
    {
        new BankClientModel(1, "Mladen", "Kanostrevac", "mk@gmail.com"),
        new BankClientModel(2, "Ivan", "Vasiljevic", "iv@gmail.com"),
        new BankClientModel(3, "Caba", "Varga", "cv@gmail.com")
    };

            Debug.WriteLine($"Id: {client.Id}\nName: {client.Name}\nSurname: {client.Surname}\nEmail: {client.Email}");

            BankClientModel existingClient = clients.Find(x => x.Id == id);
            if (existingClient != null)
            {
                existingClient.Name = client.Name;
                existingClient.Surname = client.Surname;
                existingClient.Email = client.Email;
                return existingClient;
            }
            else
            {
                return null;
            }

        }

        [Route("api/clients/{id:int}")]
        [HttpDelete]
        // DELETE: api/clients/5
        public BankClientModel Delete(int id)
        {
            var clients = new List<BankClientModel>
    {
        new BankClientModel(1, "Mladen", "Kanostrevac", "mk@gmail.com"),
        new BankClientModel(2, "Ivan", "Vasiljevic", "iv@gmail.com"),
        new BankClientModel(3, "Caba", "Varga", "cv@gmail.com")
    };

            BankClientModel existingClient = clients.Find(x => x.Id == id);

            if (existingClient != null)
            {
                clients.Remove(existingClient);
                return existingClient;
            }

            return new BankClientModel();
        }

        //[Route("api/clients/{firstLetter:alpha}")]
        //[HttpGet]
        //public List<string> GetNames(char firstLetter)
        //{
        //    var clients = new List<BankClientModel>
        //    {
        //        new BankClientModel(1, "Mladen", "Kanostrevac", "mk@gmail.com"),
        //        new BankClientModel(2, "Ivan", "Vasiljevic", "iv@gmail.com"),
        //        new BankClientModel(3, "Caba", "Varga", "cv@gmail.com")
        //    };

        //    List<string> names = clients.Where(x => Char.ToLower(x.Name[0]) == Char.ToLower(firstLetter)).Select(x => x.Name).ToList();
        //    return names;
        //}

        [Route("{nameFirstLetter:alpha}")]
        public List<string> GetNamesByFirstLetter(char nameFirstLetter)
        {
            List<BankClientModel> clients = new List<BankClientModel>();
            clients.Add(new BankClientModel(1, "Goran", "Alasov", "alasov@gmail.com"));
            clients.Add(new BankClientModel(2, "Petar", "Petrovic", "petrovic@gmail.com"));
            clients.Add(new BankClientModel(3, "Mitar", "Miric", "mitar@gmail.com"));
            clients.Add(new BankClientModel(4, "Svetlana", "Raznjatovic", "srpska.majka@gmail.com"));
            clients.Add(new BankClientModel(5, "Marko", "Kraljevic", "junak.srpski@gmail.com"));

            List<string> firstNames = new List<string>();

            if (Char.IsLetter(nameFirstLetter) == false)
            {
                return null;
            }

            foreach (var item in clients)
            {
                if (char.ToLower(nameFirstLetter) == char.ToLower(item.Name[0]))
                {
                    firstNames.Add(item.Name);
                }
            }

            return firstNames;
        }

        // Domaci zadatak.

        // Zadatak 1.1
        [Route("api/clients/emails")]
        [HttpGet]
        public List<string> GetEmails()
        {
            List<BankClientModel> clients = new List<BankClientModel>();
            clients.Add(new BankClientModel(1, "Goran", "Alasov", "alasov@gmail.com"));
            clients.Add(new BankClientModel(2, "Petar", "Petrovic", "petrovic@gmail.com"));
            clients.Add(new BankClientModel(3, "Mitar", "Miric", "mitar@gmail.com"));
            clients.Add(new BankClientModel(4, "Svetlana", "Raznjatovic", "srpska.majka@gmail.com"));
            clients.Add(new BankClientModel(5, "Marko", "Kraljevic", "junak.srpski@gmail.com"));

            return clients.Select(x => x.Email).ToList();
        }

        // Zadatak 1.2
        [Route("api/clients/{firstLetter:alpha}")]
        [HttpGet]
        public List<string> GetNames(char firstLetter)
        {
            var clients = new List<BankClientModel>
            {
                new BankClientModel(1, "Mladen", "Kanostrevac", "mk@gmail.com"),
                new BankClientModel(2, "Ivan", "Vasiljevic", "iv@gmail.com"),
                new BankClientModel(3, "Caba", "Varga", "cv@gmail.com")
            };

            List<string> names = clients.Where(x => Char.ToLower(x.Name[0]) == Char.ToLower(firstLetter)).Select(x => x.Name).ToList();
            return names;
        }

        // Zadatak 1.3
        [Route("api/clients/firstLetters")]
        [HttpGet]
        public List<List<string>> GetNamesSurnames([FromUri]char nameChar, [FromUri]char surnameChar)
        {
            List<BankClientModel> clients = new List<BankClientModel>();
            clients.Add(new BankClientModel(1, "Goran", "Alasov", "alasov@gmail.com"));
            clients.Add(new BankClientModel(2, "Petar", "Petrovic", "petrovic@gmail.com"));
            clients.Add(new BankClientModel(3, "Mitar", "Miric", "mitar@gmail.com"));
            clients.Add(new BankClientModel(4, "Svetlana", "Raznjatovic", "srpska.majka@gmail.com"));
            clients.Add(new BankClientModel(5, "Marko", "Kraljevic", "junak.srpski@gmail.com"));

            /*
            return clients
                .Where(x => Char.ToLower(x.Name[0]) == Char.ToLower(nameChar) &&
                    Char.ToLower(x.Surname[0]) == Char.ToLower(surnameChar))
                .Select(x => new Tuple<string, string>(x.Name, x.Surname))
                .ToList();
            */
            return clients
                .Where(x => Char.ToLower(x.Name[0]) == Char.ToLower(nameChar) &&
                    Char.ToLower(x.Surname[0]) == Char.ToLower(surnameChar))
                .Select(x => new List<string> { x.Name, x.Surname })
                .ToList();
        }

        // Zadatak 1.4
        [Route("api/clients/sort/{order:regex(asc|desc)}")]
        [HttpGet]
        public List<string> GetClientNamesOrdered(string order)
        {
            List<BankClientModel> clients = new List<BankClientModel>();
            clients.Add(new BankClientModel(1, "Goran", "Alasov", "alasov@gmail.com"));
            clients.Add(new BankClientModel(2, "Petar", "Petrovic", "petrovic@gmail.com"));
            clients.Add(new BankClientModel(3, "Mitar", "Miric", "mitar@gmail.com"));
            clients.Add(new BankClientModel(4, "Svetlana", "Raznjatovic", "srpska.majka@gmail.com"));
            clients.Add(new BankClientModel(5, "Marko", "Kraljevic", "junak.srpski@gmail.com"));

            switch (order)
            {
                case "asc":
                    {
                        return clients
                            .Select(x => x.Name)
                            .OrderBy(name => name)
                            .ToList();
                    }
                case "desc":
                    {
                        return clients
                            .Select(x => x.Name)
                            .OrderByDescending(name => name)
                            .ToList();
                    }
                default:
                    return null;
            }
        }

        [Route("api/clients/bonitet")]
        [HttpPut]
        public List<BankClientModel> UpdateBonitet()
        {
            List<BankClientModel> clients = GetDb();
            foreach (var b in clients)
            {
                TimeSpan ts = DateTime.Now - b.DatumRodjenja;
                int age = ts.Days / 365;
                if (age < 65)
                {
                    b.Bonitet = 'P';
                }
                else
                {
                    b.Bonitet = 'N';
                }
            }

            return clients;
        }


        // Zadatak 2.2
        [Route("api/clients/delete")]
        [HttpDelete]
        public List<BankClientModel> DeleteNonComplete()
        {
            List<BankClientModel> clients = GetDb();
            BankClientModel mod1 = new BankClientModel
            {
                Name = null
            };
            clients.Add(mod1);

            // List<BankClientModel> nonComplete = clients.Where(x => x.Name == null || x.Surname == null).Select(x => x).ToList();
            clients.RemoveAll(x => x.Name == null || x.Surname == null);

            return clients;
        }

        // Zadatak 2.3
        [Route("api/clients/countLess/{years:int}")]
        [HttpGet]
        public List<BankClientModel> GetClientsWithGivenAge(int years)
        {
            List<BankClientModel> clients = GetDb();

            //bool CompareAges(DateTime a, int b)
            //{
            //    TimeSpan ts = DateTime.Now - a;
            //    int age = ts.Days / 365;
            //    return age < b ? true : false;
            //}

            Func<DateTime, int, bool> CompareAges = (a, b) =>
            {
                TimeSpan ts = DateTime.Now - a;
                int age = ts.Days / 365;
                return age < b ? true : false;
            };

            return clients.Where(x => CompareAges(x.DatumRodjenja, years)).ToList();
        }

        // Zadatak 2.4
        [Route("api/clients/averageYears")]
        [HttpGet]
        public double GetAverageAge()
        {
            List<BankClientModel> clients = GetDb();
            DateTime now = DateTime.Now;

            int ClientAge(DateTime a)
            {
                TimeSpan ts = now - a;
                return ts.Days / 365;
            }

            return clients.Average(x => ClientAge(x.DatumRodjenja));
        }

        // Zadatak 3.1
        [Route("api/clients/changelocation/{clientId}")]
        [HttpPut]
        public BankClientModel ChangeClientLocation(int clientId, [FromUri]string location)
        {
            List<BankClientModel> clients = GetDb();

            BankClientModel client = clients.Find(x => x.Id == clientId);

            if (client != null)
            {
                client.Grad = location;
            }

            return client;
        }

        // Zadatak 3.2
        [Route("api/clients/from/{city}")]
        [HttpGet]
        public List<BankClientModel> GetClientsFromCity(string city)
        {
            return GetDb().Where(x => x.Grad.ToLower() == city).ToList();
        }
    }


}
