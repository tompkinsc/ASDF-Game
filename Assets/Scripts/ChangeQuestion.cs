using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UNITY_API_Demo;

//namespace UNITY_API_Demo
//{
public class ChangeQuestion : MonoBehaviour
{
    public DebugAnsController debug;

    public GameObject myQ;
    public GameObject A;
    public GameObject S;
    public GameObject D;
    public GameObject F;
    public string correctAns;

    public void changeQ()
    {
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        String difficulty;

        using (var webClient = new WebClient())
        {
            string categories = webClient.DownloadString("https://opentdb.com/api_category.php");

            JSON_Trivia categoryList = JSON_Trivia.FromJson(categories);

            string questions = webClient.DownloadString("https://opentdb.com/api.php?amount=10&type=multiple&difficulty=easy");

            JSON_Qs qList = JSON_Qs.FromJson(questions);

            int qArrange = Random.Range(0, 3);
            myQ.GetComponent<Text>().text = System.Net.WebUtility.HtmlDecode(qList.Results[0].Question);

            if (qArrange == 0)
            {
                A.GetComponent<Text>().text = "A. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].CorrectAnswer);
                S.GetComponent<Text>().text = "S. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].IncorrectAnswers[0]);
                D.GetComponent<Text>().text = "D. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].IncorrectAnswers[1]);
                F.GetComponent<Text>().text = "F. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].IncorrectAnswers[2]);
                correctAns = "A";
            }
            else if (qArrange == 1)
            {
                A.GetComponent<Text>().text = "A. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].IncorrectAnswers[0]);
                S.GetComponent<Text>().text = "S. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].CorrectAnswer);
                D.GetComponent<Text>().text = "D. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].IncorrectAnswers[1]);
                F.GetComponent<Text>().text = "F. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].IncorrectAnswers[2]);
                correctAns = "S";
            }
            else if (qArrange == 2)
            {
                A.GetComponent<Text>().text = "A. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].IncorrectAnswers[0]);
                S.GetComponent<Text>().text = "S. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].IncorrectAnswers[1]);
                D.GetComponent<Text>().text = "D. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].CorrectAnswer);
                F.GetComponent<Text>().text = "F. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].IncorrectAnswers[2]);
                correctAns = "D";
            }
            else
            {
                A.GetComponent<Text>().text = "A. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].IncorrectAnswers[0]);
                S.GetComponent<Text>().text = "S. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].IncorrectAnswers[1]);
                D.GetComponent<Text>().text = "D. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].IncorrectAnswers[2]);
                F.GetComponent<Text>().text = "F. " + System.Net.WebUtility.HtmlDecode(qList.Results[0].CorrectAnswer);
                correctAns = "F";
            }
            debug.UpdateAnswer();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        changeQ();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
//}

namespace UNITY_API_Demo
{
    public partial class JSON_Trivia
    {
        [JsonProperty("trivia_categories")]
        public TriviaCategory[] TriviaCategories { get; set; }
    }

    public partial class JSON_Qs
    {
        [JsonProperty("response_code")]
        public long ResponseCode { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("difficulty")]
        public Difficulty Difficulty { get; set; }

        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("correct_answer")]
        public string CorrectAnswer { get; set; }

        [JsonProperty("incorrect_answers")]
        public string[] IncorrectAnswers { get; set; }
    }

    public enum Difficulty { Hard, Medium, Easy };
    public enum TypeEnum { Boolean, Multiple };

    public partial class TriviaCategory
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class JSON_Trivia
    {
        public static JSON_Trivia FromJson(string json) => JsonConvert.DeserializeObject<JSON_Trivia>(json, UNITY_API_Demo.Converter.Settings);
    }

    public partial class JSON_Qs
    {
        public static JSON_Qs FromJson(string json) => JsonConvert.DeserializeObject<JSON_Qs>(json, UNITY_API_Demo.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this JSON_Trivia self) => JsonConvert.SerializeObject(self, UNITY_API_Demo.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class DifficultyConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Difficulty) || t == typeof(Difficulty?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "hard":
                    return Difficulty.Hard;
                case "medium":
                    return Difficulty.Medium;
                case "easy":
                    return Difficulty.Easy;
            }
            throw new Exception("Cannot unmarshal type Difficulty");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Difficulty)untypedValue;
            switch (value)
            {
                case Difficulty.Hard:
                    serializer.Serialize(writer, "hard");
                    return;
                case Difficulty.Medium:
                    serializer.Serialize(writer, "medium");
                    return;
                case Difficulty.Easy:
                    serializer.Serialize(writer, "easy");
                    return;
            }
            throw new Exception("Cannot marshal type Difficulty");
        }

        public static readonly DifficultyConverter Singleton = new DifficultyConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "boolean":
                    return TypeEnum.Boolean;
                case "multiple":
                    return TypeEnum.Multiple;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.Boolean:
                    serializer.Serialize(writer, "boolean");
                    return;
                case TypeEnum.Multiple:
                    serializer.Serialize(writer, "multiple");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }
    }
}