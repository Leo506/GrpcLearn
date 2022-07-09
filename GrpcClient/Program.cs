using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace GrpcClient
{
    class Program
    {
        private const string MenuString = "[1] - Get all users\n" +
                                              "[2] - add new one\n" +
                                              "[3] - exit\n" +
                                              "Your choose: ";
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new DbService.DbServiceClient(channel);

            int choose = -1;
            do
            {
                Console.Write(MenuString);
                if (int.TryParse(Console.ReadLine(), out choose))
                    await PerformeAction(choose, client);
            } while (choose != 3);
        }

        private static async Task PerformeAction(int choose, DbService.DbServiceClient client)
        {
            switch (choose)
            {
                case 1:
                    var users = await client.GetAllUsersAsync(new EmptyUserRequest()
                    {
                        Database = "test",
                        Collection = "users"
                    });

                    int i = 0;
                    foreach (var user in users.Users)
                    {
                        Console.WriteLine($"#{i} user info:\n" +
                                          $"Name: {user.Name}\n" +
                                          $"Age: {user.Age}\n" +
                                          $"Class: {user.Class}");
                        
                        Console.Write("Marks: ");
                        foreach (var mark in user.Marks.ToList())
                        {
                            Console.Write($"\"{mark.Key}\" : {mark.Value} ");
                        }
                        Console.WriteLine();
                        Console.WriteLine();

                        i++;
                    }
                    break;
                
                case 2:
                    Console.WriteLine("Enter a follow data to add new user");
                    
                    Console.Write("Name: ");
                    var name = Console.ReadLine();
                    
                    Console.Write("Age: ");
                    var age = int.Parse(Console.ReadLine());
                    
                    Console.Write("Class: ");
                    var className = Console.ReadLine();

                    var marks = new Dictionary<string, int>();

                    string curse;
                    do
                    {
                        Console.Write("Curse: ");
                        curse = Console.ReadLine();
                        
                        Console.Write("Mark: ");
                        if (int.TryParse(Console.ReadLine(),out var curseMark))
                            marks.Add(curse, curseMark);
                        
                    } while (!string.IsNullOrEmpty(curse));

                    var request = new User()
                    {
                        Name = name,
                        Age = age,
                        Class = className
                    };
                    request.Marks.Add(marks);

                    var reply = await client.AddNewUserAsync(request);
                    
                    if (reply.Status == StatusReply.Types.Status.Ok)
                        Console.WriteLine("User added successful");
                    else
                        Console.WriteLine("Error on adding user");
                    
                    break;
                
                default:
                    return;;
            }
        }
    }
}