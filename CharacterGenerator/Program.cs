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

            Dictionary<int, int> test = new Dictionary<int, int>();
            using (ContextBLL ctx = new ContextBLL())
            {
                for(int x=0; x<6; x++)
                {
                    int thisroll = ctx.Roll();
                    Console.Write(thisroll);
                    Console.Write(' ');
                    int count;
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
                Console.WriteLine();
                Console.WriteLine();
                foreach(var x in test.OrderBy(y=>y.Key))
                {
                    Console.WriteLine($"{x.Key}:{x.Value}");
                }
                    
            }
        }
    }
}
