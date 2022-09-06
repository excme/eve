using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class NameModel<T>
    {
        public NameModel()
        {

        }
        public NameModel(T _id, string _name)
        {
            id = _id;
            name = _name;
        }
        [JsonPropertyName("i")]
        public T id { get; set; }
        [JsonPropertyName("n")]
        public string name { get; set; }
    }
}
