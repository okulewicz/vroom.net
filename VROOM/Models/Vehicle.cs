using Newtonsoft.Json;
using System.Collections.Generic;

namespace VROOM
{
    public class Vehicle
    {
        /// <summary>
        /// Vehicle ID.
        /// </summary>
        [JsonProperty("id")]
        public uint Id { get; set; }

        /// <summary>
        /// The routing profile to use. Defaults to car.
        /// </summary>
        [JsonProperty("profile")]
        public string Profile { get; set; }

        /// <summary>
        /// A description of this vehicle.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The start coordinate of the vehicle.
        /// </summary>
        [JsonProperty("start")] 
        public Coordinate? Start { get; set; }

        /// <summary>
        /// The start index of the vehicle in custom matrices. Only needed if supplying custom matrix.
        /// </summary>
        [JsonProperty("start_index")]
        public int? StartIndex { get; set; }

        /// <summary>
        /// The end coordinate of the vehicle.
        /// </summary>
        [JsonProperty("end")]
        public Coordinate? End { get; set; }

        /// <summary>
        /// The end index of the vehicle in custom matrices. Only needed if supplying custom matrix.
        /// </summary>
        [JsonProperty("end_index")]
        public int? EndIndex { get; set; }

        /// <summary>
        /// List of integers describing multidimensional qualities.
        /// </summary>
        [JsonProperty("capacity")]
        public List<int> Capacity { get; set; }

        /// <summary>
        /// List of ints defining skills.
        /// </summary>
        [JsonProperty("skills")]
        public List<int> Skills { get; set; }

        /// <summary>
        /// The possible working hours of the vehicle.
        /// </summary>
        [JsonProperty("time_window")]
        public int[] TimeWindow { get; set; }

        /// <summary>
        /// an integer defining the maximum number of tasks in a route for this vehicle
        /// </summary>
        [JsonProperty("max_tasks")]
        public int? MaxTasks { get; set; }
        /// <summary>
        /// an integer defining the maximum travel time for this vehicle
        /// </summary>
        [JsonProperty("max_travel_time")]
        public int? MaxTravelTime { get; set; }
        /// <summary>
        /// an integer defining the maximum distance for this vehicle/// </summary>
        [JsonProperty("max_distance")]
        public int? MaxDistance { get; set; }


        /// <summary>
        /// A list of break objects.
        /// </summary>
        [JsonProperty("breaks")]
        public List<Break> Breaks { get; set; }

        /// <summary>
        /// A value used to scale all vehicle travel times.
        /// The respected precision is limited to two digits after the decimal point.
        /// </summary>
        [JsonProperty("speed_factor")]
        public double? SpeedFactor { get; set; }

        /// <summary>
        /// A list of VehicleStep objects describing a custom route for this vehicle (only makes sense when using -c)
        /// </summary>
        [JsonProperty("steps")]
        public List<VehicleStep> Steps { get; set; }
    }
}