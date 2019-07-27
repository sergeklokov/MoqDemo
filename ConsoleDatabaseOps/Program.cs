using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDatabaseOps
{
    /// <summary>
    /// This example is reworked EF usage example from MSDN
    /// In the other project in this solution I'm testing it with usage of Moq library
    /// 
    /// Serge Klokov, wrote in 2019
    /// </summary>
    public class Program  // we need it to be publick in order to Moq test
    {
        static void Main(string[] args)
        {
            var dbContext = new AdventureWorks2016CTP3Entities();

            // let's print name
            int personID = 1;
            string name = GetPersonName(personID, dbContext);
            Console.WriteLine("Name = " + name);

            Console.WriteLine("Count = " + GetPeopleCount(dbContext));

            Console.WriteLine("New test person added, with new ID = " + AddAPerson("Test", dbContext).PersonID);

            Console.WriteLine("Done. Please press any key..");
            Console.ReadKey();
        }

        public static string GetPersonName(int personID, AdventureWorks2016CTP3Entities dbContext)
        {
            Person person = dbContext.People.FirstOrDefault(p => p.PersonID == personID);
            return person.Name;
        }

        public static int GetPeopleCount(AdventureWorks2016CTP3Entities dbContext)
        {
            return dbContext.People.Count();
        }

        // normally we don't want to insert dummy records to the real databasae
        public static Person AddAPerson(string name, AdventureWorks2016CTP3Entities dbContext)
        {
            var newPersonID = GetPeopleCount(dbContext) + 1;
            var newPerson = dbContext.People.Add(new Person { PersonID = newPersonID, Name = name });
            dbContext.SaveChanges();
            return newPerson;
        }

    }
}
