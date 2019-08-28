using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using BusinessLogicLayer;

namespace CharacterGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // establish connection to the database through a connection string
            //using (ContextDAL ctx = new ContextDAL())
            //{
            //    ctx.ConnectionString = $@"Data Source=.\SQLEXPRESS;Initial Catalog=CharacterGenerator;Integrated Security=True";

            //    Console.WriteLine(ctx.ObtainRoleCount());
            //    Console.WriteLine(ctx.FindRoleByRoleID(5));
            //    Console.WriteLine(ctx.FindRoleByRoleID(1));

            //    List<UserDAL> answer = ctx.GetUsers(0, 100);
            //    Console.WriteLine("***********");
            //    foreach (var x in answer)
            //    {
            //        Console.WriteLine(x);
            //    }
            // }

            // this code was used to test my random dice roller before my mvc layer was implemented
            Dictionary<int, int> test = new Dictionary<int, int>();
            using (ContextBLL ctx = new ContextBLL())
            {
                // the params in the for loop are setting the variable to zero and as long as x is less than 6 it will add one to the variable
                for(int x=0; x<6; x++)
                {
                    // this roll is the variable that will be called since the roll method was assigned to it
                    int thisroll = ctx.Roll();
                    Console.Write(thisroll);
                    Console.Write(' ');
                    int count;
                    // trygetvalue is calling the roll method and returning the count of numbers rolled ie 10:1, 11:2, 12:1, 16:2
                    bool isthere = test.TryGetValue(thisroll, out count);
                    if (isthere)
                    {
                        test[thisroll]++;
                       
                    }
                    else
                    {
                        test[thisroll] = 1;
                    }
                }
                // the 2 writelines are for spacing
                Console.WriteLine();
                Console.WriteLine();
                // this foreach loop is ordering the results of the dice roll by the lowest value to the highest
                foreach(var x in test.OrderBy(y=>y.Key))
                {
                    Console.WriteLine($"{x.Key}:{x.Value}");
                }
                    
            }
        }
    }
}
