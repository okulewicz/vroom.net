using Docker.DotNet;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VROOM.Docker
{
    public class VroomDockerRunner
    {
        public const int VROOM_PORT = 3000;
        private const string DOCKER_IMAGE_NAME = "ghcr.io/vroom-project/vroom-docker";
        private const string DOCKER_IMAGE_VERSION = "v1.14.0";

        public static async Task RunDockerContainer()
        {
            SetupVolumes(out string confFolder, out string logFolder);

            string dockerUri = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "npipe://./pipe/docker_engine"
                : "unix:///var/run/docker.sock";

            using (var client = new DockerClientConfiguration(new Uri(dockerUri)).CreateClient())
            {
                var images = await client.Images.ListImagesAsync(new ImagesListParameters() { All = true });
                var imageExists = images.Any(img => img.RepoTags.Contains($"{DOCKER_IMAGE_NAME}:{DOCKER_IMAGE_VERSION}"));

                if (!imageExists)
                {
                    Console.WriteLine("Downloading Docker image...");
                    await client.Images.CreateImageAsync(
                        new ImagesCreateParameters { FromImage = DOCKER_IMAGE_NAME, Tag = DOCKER_IMAGE_VERSION },
                        null,
                        new Progress<JSONMessage>(message => Console.WriteLine(message.Status))
                    );
                }
                var containers = await client.Containers.ListContainersAsync(new ContainersListParameters() { All = true });
                var existingContainer = containers.FirstOrDefault(c => c.Image == $"{DOCKER_IMAGE_NAME}:{DOCKER_IMAGE_VERSION}");

                if (existingContainer != null)
                {
                    Console.WriteLine("Container is already defined.");
                    if (existingContainer.State != "running")
                        await client.Containers.StartContainerAsync(existingContainer.ID, null);
                    return;
                }

                var parameters = new CreateContainerParameters
                {
                    Image = $"{DOCKER_IMAGE_NAME}:{DOCKER_IMAGE_VERSION}",
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
                            { $"{VROOM_PORT}/tcp", new List<PortBinding> { new PortBinding { HostPort = VROOM_PORT.ToString() } } }
                        }
                    }
                };

                var response = await client.Containers.CreateContainerAsync(parameters);
                var containerStatus = client.Containers.StartContainerAsync(response.ID, null);
                containerStatus.Wait();
                HealthCheck();
            }
        }

        private static void SetupVolumes(out string confFolder, out string logFolder)
        {
            var currentFolder = new DirectoryInfo(".").FullName;
            confFolder = Path.Combine(currentFolder, "conf");
            if (!Directory.Exists(confFolder))
                Directory.CreateDirectory(confFolder);
            logFolder = Path.Combine(confFolder, "log");
            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);
        }

        public static bool HealthCheck()
        {
            using (var httpClient = new HttpClient())
            {
                var healthCheckUrl = $"http://localhost:{VROOM_PORT}/health";
                HttpResponseMessage response = null;
                bool isHealthy = false;
                Thread.Sleep(5000);
                while (!isHealthy)
                {
                    try
                    {
                        var getTask = httpClient.GetAsync(healthCheckUrl);
                        getTask.Wait();
                        response = getTask.Result;
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            isHealthy = true;
                        }
                    }
                    catch (HttpRequestException)
                    {
                        // Ignore exceptions and retry
                    }

                    if (!isHealthy)
                    {
                        Thread.Sleep(1000);
                    }
                }
                return isHealthy;
            }


        }
    }
}
