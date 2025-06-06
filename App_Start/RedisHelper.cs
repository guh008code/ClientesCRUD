using StackExchange.Redis;
using Newtonsoft.Json;
using ClientesCRUD.Models;
using System;
using System.Configuration;

namespace ClientesCRUD.App_Start
{
    public static class RedisHelper
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            var config = new ConfigurationOptions
            {
                EndPoints = { ConfigurationManager.AppSettings["RedisConfig"].ToString() },
                AbortOnConnectFail = true, // <-- Aqui é onde você controla se ele aborta na falha inicial
                ConnectRetry = 3,
                ConnectTimeout = 2000
            };
            return ConnectionMultiplexer.Connect(config);
        });

        public static ConnectionMultiplexer Connection => LazyConnection.Value;
        public static IDatabase RedisDb => Connection.GetDatabase();

        public static void SetClienteCache(Cliente cliente)
        {
            try
            {
                var json = JsonConvert.SerializeObject(cliente);
                RedisDb.StringSet($"cliente:{cliente.Id}", json);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static Cliente GetClienteCache(int id)
        {
            try
            {
                var json = RedisDb.StringGet($"cliente:{id}");
                if (json.IsNullOrEmpty) return null;
                return JsonConvert.DeserializeObject<Cliente>(json);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void RemoveClienteCache(int id)
        {
            try
            {
                RedisDb.KeyDelete($"cliente:{id}");
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
