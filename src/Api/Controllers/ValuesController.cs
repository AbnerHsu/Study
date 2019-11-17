using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IA _a;

        public ValuesController(IA a)
        {
            _a = a;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            // return "value";
            return _a.Name;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        { 
            var config = new ProducerConfig { BootstrapServers = "192.168.1.2:9092" };

            // If serializers are not specified, default serializers from 
            // `Confluent.Kafka.Serializers` will be automatically used where 
            // available. Note: by default strings are encoded as UTF8. 
            using (var p = new ProducerBuilder<Null, string>(config).Build()) 
            { 
                try 
                { 
                    var dr = p.ProduceAsync("test-topic", new Message<Null, string> { Value=value }).GetAwaiter().GetResult(); 
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'"); 
                } 
                catch (ProduceException<Null, string> e) 
                { 
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}"); 
                }
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
