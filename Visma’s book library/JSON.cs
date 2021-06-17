using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using System.IO;

namespace Visma_s_book_library
{
    
    class JSON
    {
        protected const string library_file_path = "..\\..\\..\\Library.json";
        protected const string user_file_path = "..\\..\\..\\Users.json";
        protected static JsonSerializer serializer = new JsonSerializer();
        
        private void createFile(string path)
        {
            FileStream fs = File.Create(path);
            fs.Close();
        }
        public void write<T>(IList<T> list)
        {
            string path = "";
            if (typeof(T) == typeof(Book))
            {
                path = library_file_path;
            }
            else if (typeof(T) == typeof(User))
            {
                path = user_file_path;
            }
            if (!File.Exists(path))
            {
                createFile(path);
            }
            serializer.Formatting = Formatting.Indented;
            using (StreamWriter streamWriter = new StreamWriter(path))
                using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(jsonWriter, list);
                }
        }

        
        public IList<T> readJson<T>()
        {

            string path = "";
            if(typeof(T) == typeof(Book))
            {
                path = library_file_path;
            }
            else if(typeof(T) == typeof(User))
            {
                path = user_file_path;
            }
            if (!File.Exists(path))
            {
                createFile(path);
            }
            IList<T> return_list = new List<T>();
            serializer.Formatting = Formatting.Indented;
            using (StreamReader streamReader = new StreamReader(path))
            using (JsonReader jsonReader = new JsonTextReader(streamReader))
            {
                jsonReader.SupportMultipleContent = true;
                while (true)
                {
                    if (!jsonReader.Read())
                        break;
                    return_list = serializer.Deserialize<IList<T>>(jsonReader);                    
                }

            }
            return return_list;
        }
    }
}
