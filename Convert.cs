using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Converter;
namespace Converterr
{
    public class ReadNWrite
    {
        public static void Open(string path) // deserialize
        {
            
            do
            {
                if (path.Contains("json"))
                {
                    string json = File.ReadAllText(path);
                    Company_model amazon = JsonConvert.DeserializeObject<Company_model>(json);
                    Company_model amaz = new Company_model("Amazon", "Bellevue", 50000000, 6000000000);
                    string name = amaz.CompanyName;
                    string hq = amaz.CompanyHeadquarter;
                    long nofe = amaz.CompanyNumOFEmployees;
                    long rev = amaz.CompanyRevenue;
                    Console.WriteLine($"{name}\n{hq}\n{nofe}\n{rev}");
                }
                else if (path.Contains("txt"))
                {
                    using (FileStream read = File.OpenRead(path))
                    {
                        byte[] buff = new byte[read.Length];
                        read.Read(buff, 0, buff.Length);
                        string txt = Encoding.UTF8.GetString(buff);
                        Console.WriteLine(txt);
                    }
                }
                else if (path.Contains("xml"))
                {
                    XmlSerializer parse = new XmlSerializer(typeof(Company_model));
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        Company_model amazon;
                        amazon = (Company_model)parse.Deserialize(fs);
                        Console.WriteLine($"{amazon.CompanyName}\n{amazon.CompanyHeadquarter}\n{amazon.CompanyNumOFEmployees}\n{amazon.CompanyRevenue}");
                    }
                }
                ConsoleKeyInfo exit_key = Console.ReadKey();
                switch (exit_key.Key)
                {
                    case ConsoleKey.F1:
                        Console.Clear();
                        Company_model amaz = new Company_model("Amazon", "Bellevue", 50000000, 6000000000);
                        Console.Write("Enter filename to save file in XML, JSON, TXT: ");
                        string fileP = Console.ReadLine();
                        Write(fileP, amaz);
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            } while (true);
        }
        private static void Write(string path, Company_model a) // serialize
        {
            Console.Clear();
            List<Company_model> amaz = new List<Company_model>();
            amaz.Add(a);
            
            if (path.Contains("json"))
            {
                string json = JsonConvert.SerializeObject(amaz) + '\n';
                File.WriteAllText(path, json);
            }
            else if (path.Contains("txt"))
            {
                string text = a.CompanyName + '\n' + a.CompanyHeadquarter + '\n' + a.CompanyNumOFEmployees + '\n' + a.CompanyRevenue + '\n';
                File.WriteAllText(path, text);
            }
            else if (path.Contains("xml"))
            {
                Company_model am = new Company_model("Amazon", "Bellevue", 50000000, 6000000000);
                XmlSerializer ss = new XmlSerializer(typeof(Company_model));
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    ss.Serialize(fs, am);
                }
            }
        }
    }
    public class RunTime
    {
        static void Main()
        {
            Company_model amaz = new Company_model("Amazon", "Bellevue", 50000000, 6000000000);
            string dd = JsonConvert.SerializeObject(amaz);
            Console.Write("Enter new file name: ");
            
            string fl = Console.ReadLine();
            if (fl == "")
            {
                fl = null;
            }
            Console.Write("Enter filename to open: ");
            try
            {
                string flpath = Console.ReadLine();
                ReadNWrite.Open(flpath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("No such file\n");
                Environment.Exit(1);
            }
        }
    }
}