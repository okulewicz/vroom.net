using Docker.DotNet;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VROOM.Docker
{
    public class VroomDockerRunner
    {
        public const int VROOM_PORT = 3000;
        public static async Task RunDockerContainer()
        {
            using (var client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient())
            {
                var images = await client.Images.ListImagesAsync(new ImagesListParameters() { All = true });
                var imageExists = images.Any(img => img.RepoTags.Contains("ghcr.io/vroom-project/vroom-docker:v1.14.0"));

                if (!imageExists)
                {
                    Console.WriteLine("Downloading Docker image...");
                    await client.Images.CreateImageAsync(
                        new ImagesCreateParameters { FromImage = "ghcr.io/vroom-project/vroom-docker", Tag = "v1.14.0" },
                        null,
                        new Progress<JSONMessage>(message => Console.WriteLine(message.Status))
                    );
                }
                var containers = await client.Containers.ListContainersAsync(new ContainersListParameters() { All = true });
                var existingContainer = containers.FirstOrDefault(c => c.Image == "ghcr.io/vroom-project/vroom-docker:v1.14.0");

                if (existingContainer != null)
                {
                    Console.WriteLine("Container is already defined.");
                    if (existingContainer.State != "running")
                        await client.Containers.StartContainerAsync(existingContainer.ID, null);
                    return;
                }

                var currentFolder = new DirectoryInfo(".").FullName;
                var confFolder = Path.Combine(currentFolder, "conf");
                if (!Directory.Exists(confFolder))
                    Directory.CreateDirectory(confFolder);
                var logFolder = Path.Combine(confFolder, "log");
                if (!Directory.Exists(logFolder))
                    Directory.CreateDirectory(logFolder);
                var parameters = new CreateContainerParameters
                {
                    Image = "ghcr.io/vroom-project/vroom-docker:v1.14.0",
                    Hostname = "7d790644176f",
                    MacAddress = "02:42:ac:11:00:02",
                    Env = new[]
                    {
                        "VROOM_ROUTER=osrm",
                        "NODE_VERSION=20.11.0",
                        "YARN_VERSION=1.22.19",
                        "VROOM_DOCKER=osrm",
                        "VROOM_LOG=/log"
                    },
                    HostConfig = new HostConfig
                    {
                        Binds = new[] {
                            $"{confFolder}:/conf",
                            $"{logFolder}:/log"
                        },
                        PortBindings = new Dictionary<string, IList<PortBinding>>
                        {
                            { "3000/tcp", new List<PortBinding> { new PortBinding { HostPort = VROOM_PORT.ToString() } } }
                        }
                    }                    
                };

                var response = await client.Containers.CreateContainerAsync(parameters);
                await client.Containers.StartContainerAsync(response.ID, null);
            }
        }
    }
}
