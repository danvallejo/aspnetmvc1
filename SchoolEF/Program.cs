using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEF
{
    class Program
    {
        static void Main(string[] args)
        {
            var school = new SchoolEntities();

            school.Database.Connection.Open();

            foreach (var userClass in school.RetrieveClassesForStudent(100))
            {
                Console.WriteLine("ClassId {0}", userClass.ClassId);
            }

            foreach (var classMaster in school.ClassMasters)
            {
                Console.WriteLine("{0}-{1}-{2}-{3}",
                    classMaster.ClassId,
                    classMaster.ClassName,
                    classMaster.ClassDescription,
                    classMaster.ClassPrice);
            }

            // Exercise: Display all Users and their classes
            foreach (var user in school.Users)
            {
                Console.WriteLine("{0}", user.UserName);

                foreach (var classMaster in user.ClassMasters)
                {
                    Console.WriteLine("\t{0}", classMaster.ClassName);
                }
            }

            // Exercise: Display all classes and their users
            foreach (var classMaster in school.ClassMasters)
            {
                Console.WriteLine("{0}", classMaster.ClassName);

                foreach (var user in classMaster.Users)
                {
                    Console.WriteLine("\t{0}", user.UserName);
                }
            }

            school.Database.Connection.Close();

            Console.Write("Done.");
            Console.ReadLine();
        }
    }
}
