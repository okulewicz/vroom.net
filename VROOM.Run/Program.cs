using VROOM.API;
using VROOM.Docker;

namespace VROOM.Run
{
    internal class Program
    {
        public static async Task Run()
        {
            VroomDockerRunner.RunDockerContainer().Wait();
            VroomApiClient apiClient = new VroomApiClient("http://localhost:3000");
            uint id = 0;

            var input = new VroomInput()
            {
                Jobs = new List<Job>()
                {
                    new Job()
                    {
                        Id = id++,
                        Location = new Coordinate(151.7735849, -32.9337431),
                        LocationIndex = 1,
                        TimeWindows = new List<int[]>()
                        {
                            new int[] {0, 1000 }
                        }
                    },
                    new Job()
                    {
                        Id = id++,
                        Location = new Coordinate(151.7617514, -32.9351314),
                        LocationIndex = 2
                    },
                    new Job()
                    {
                        Id = id++,
                        Location = new Coordinate(151.7105484, -32.9338793),
                        LocationIndex = 3
                    }
                },
                Vehicles = new List<Vehicle>()
                {
                    new Vehicle()
                    {
                        Id = id++,
                        Start = new Coordinate(151.7005484, -32.9331793),
                        StartIndex = 0
                    }
                },
                Matrices = new Dictionary<string, Matrix>()
                {{
                    "car",
                    new Matrix()
                    {
                        Durations = [
                            [0, 600, 600, 600],
                            [600, 0, 600, 600],
                            [600, 600, 0, 600],
                            [600, 600, 600, 0],
                        ],
                        Distances = [
                            [0, 10000, 10000, 10000],
                            [10000, 0, 10000, 10000],
                            [10000, 10000, 0, 10000],
                            [10000, 10000, 10000, 0],
                        ],
                    }
                }
                },
                Options = new VROOM.Models.Options()
                {
                    Threads = 16,
                    Explore = 5
                }
            };

            var result = await apiClient.PerformRequest(input);

            if (result.WasSuccessful)
            {
                foreach (var route in result.Routes)
                {
                    Console.WriteLine($"Route {route.VehicleId}");
                    foreach (var step in route.Steps)
                    {
                        Console.WriteLine($"{step.StepType} {step.LocationIndex}");
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Run().Wait();
        }
    }
}
