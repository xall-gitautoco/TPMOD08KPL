using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

public class Program
{
    public static void Main(string[] args)
    {
        // Memanggil Class CovidConfig
        // Berfungsi untuk integrasi json, dan runtime configuration
        CovidConfig Konfig = new CovidConfig();

        // Menerima inputan 1 dalam satuan suhu celcius
        Console.WriteLine("Berapa suhu badan anda saat ini? Dalam nilai " + Konfig.configuration.satuan_suhu);
        double inputSuhu = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala deman?");
        int inputHari = Convert.ToInt32(Console.ReadLine());

        // Pengkondisian pesan yang ingin ditampilkan
        if (Konfig.configuration.satuan_suhu == "celcius")
        {
            if ((inputSuhu >= 36.5 || inputSuhu <= 37.5) && inputHari < Konfig.configuration.batas_hari_demam)
            {
                Console.WriteLine(Konfig.configuration.pesan_diterima);
            }
            else
            {
                Console.WriteLine(Konfig.configuration.pesan_ditolak);
            }
        }
        else if (Konfig.configuration.satuan_suhu == "fahrenheit")
        {
            if ((inputSuhu >= 97.7 || inputSuhu <= 99.5) && inputHari < Konfig.configuration.batas_hari_demam)
            {
                Console.WriteLine(Konfig.configuration.pesan_diterima);
            }
            else
            {
                Console.WriteLine(Konfig.configuration.pesan_ditolak);

            }
        }

        // Menjalankan method UbahSatuan pada class CovidConfig
        Konfig.UbahSatuan();
        Console.WriteLine();

        // Menerima inputan 2 dalam satuan suhu fahreinhet (diubah oleh method UbahSatuan)
        Console.WriteLine("Berapa suhu badan anda saat ini? Dalam nilai " + Konfig.configuration.satuan_suhu);
        inputSuhu = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala deman?");
        inputHari = Convert.ToInt32(Console.ReadLine());

        // Pengkondisian pesan yang ingin ditampilkan
        if (Konfig.configuration.satuan_suhu == "celcius")
        {
            if ((inputSuhu >= 36.5 || inputSuhu <= 37.5) && inputHari < Konfig.configuration.batas_hari_demam)
            {
                Console.WriteLine(Konfig.configuration.pesan_diterima);
            }
            else
            {
                Console.WriteLine(Konfig.configuration.pesan_ditolak);
            }
        }
        else if (Konfig.configuration.satuan_suhu == "fahrenheit")
        {
            if ((inputSuhu >= 97.7 || inputSuhu <= 99.5) && inputHari < Konfig.configuration.batas_hari_demam)
            {
                Console.WriteLine(Konfig.configuration.pesan_diterima);
            }
            else
            {
                Console.WriteLine(Konfig.configuration.pesan_ditolak);

            }
        }
    }

    // Class Config
    // Berfungsi untuk menampung konfigurasi
    public class Config
    {
        // Atribute untuk diserialisasi
        public string satuan_suhu { get; set; }
        public int batas_hari_demam { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }

        // Constructor kosong untuk deserialisasi
        public Config() { }

        // Menerima masukan dari deserialisasi
        public Config(string suhu, int batasDemam, string pesanDitolak, string pesanDiterima)
        {
            satuan_suhu = suhu;
            batas_hari_demam = batasDemam;
            pesan_ditolak = pesanDitolak;
            pesan_diterima = pesanDiterima;
        }
    }

    // Class CovidConfig
    // Berfungsi untuk membaca dan menulis file konfigurasi
    public class CovidConfig
    {
        // Deklarasi nama variabel configuration dengan tipe data Config
        public Config configuration;

        // Pada filePath ini SENGAJA tidak diarahkan ke file JSON dengan benar agar code Catch dibawah ini dijalankan
        // File Baru akan terbantuk dengan sendirinya dan dinamai sesuai nama file ujungnya yakni Covid_Config.json
        public const string filePath = "C:\\Users\\RD FAISAL\\source\\repos" +
            "\\xall-gitautoco\\TPMOD08KPL\\tpmod08_1302220093\\bin\\Debug\\net8.0\\Covid_config.json";

        // Method untuk membaca dan menulis file Covid_Config.json baru jika belum ada/dibuat
        public CovidConfig()
        {
            try
            {
                ReadConfigFile();
            }
            catch (Exception)
            {
                SetDefault();
                WriteNewConfigFile();
            }
        }

        // untuk set nilai Default-nya dari CovidConfig
        public void SetDefault()
        {
            configuration = new Config("celcius", 14, "Anda tidak diperbolehkan masuk ke dalam gedung ini", 
                "Anda diperbolehkan masuk ke dalam gedung ini");
        }

        // Method untuk membaca file configurasi
        private Config ReadConfigFile()
        {
            String configJsonData = File.ReadAllText(filePath);
            configuration = JsonSerializer.Deserialize<Config>(configJsonData);
            return configuration;
        }

        // Method untuk menulis file configurasi
        public void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            String jsonString = JsonSerializer.Serialize(configuration, options);
            File.WriteAllText(filePath, jsonString);
        }

        // Method ubah satuan 
        public void UbahSatuan()
        {
            if (configuration.satuan_suhu == null)
            {
                configuration.satuan_suhu = "celcius";
            }
            else if (configuration.satuan_suhu == "celcius")
            {
                configuration.satuan_suhu = "fahrenheit";
            }
            else if (configuration.satuan_suhu == "fahrenheit")
            {
                configuration.satuan_suhu = "celcius";
            }
        }
    }
}