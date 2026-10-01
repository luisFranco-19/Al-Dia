using AlDia.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AlDia.DAL.Common
{
    public interface IConfiguracionDal
    {
        bool ExisteConfiguracion();
        bool Guardar(ConexionEntity config);
        ConexionEntity? Obtener();
    }

    public sealed class ConfiguracionDal : IConfiguracionDal
    {
        private readonly string rutaArchivo;

        public ConfiguracionDal(string? ruta = null)
        {
            rutaArchivo = ruta ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "configuracion.json");
        }

        public bool ExisteConfiguracion()
        {
            return File.Exists(rutaArchivo);
        }

        public bool Guardar(ConexionEntity config)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(rutaArchivo)!);

                string json = JsonSerializer.Serialize(config,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                File.WriteAllText(rutaArchivo, json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ConexionEntity? Obtener()
        {
            if (!File.Exists(rutaArchivo))
                return null;

            string json = File.ReadAllText(rutaArchivo);

            return JsonSerializer.Deserialize<ConexionEntity>(json);
        }
    }
}
