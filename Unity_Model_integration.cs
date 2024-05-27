using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// using kylebehaviorcontroller;

using UnityEngine.Networking;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;


public class OpenAIController : MonoBehaviour
{
    public TMP_Text textField;
    public TMP_InputField inputField;
    public Button okButton;

    //private OpenAIAPI api;
    //private List<ChatMessage> messages;

    private Animator anim;
    public bool ethical_value;

    public static string API_URL = "https://final-version-01.onrender.com/predict";

    private HttpClient client = new();

    // private kylebehaviorcontroller imported_Class;

    async void Start()
    {
        anim = GetComponent<Animator>();
        StartConversation();
        okButton.enabled = true;
        okButton.onClick.AddListener(() => GetResponse());
        // GetResponse();
        ///
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
            );
    }

    
    public class APIResponse
    {
        public Prediction[] predictions;
    }

    [System.Serializable]
    public class Prediction
    {
        public string Predicted_Label;
        public string Text;
    }

    public class Predictions
    {
        public List<Prediction> predictions;
    }

    public class RequestData
    {
        public List<string> text = new();
    }


    private async Task<List<Prediction>> TestRequest()
    {
        RequestData data = new();
        //data.text.Add("is beating someone ethical");
        data.text.Add(inputField.text);
        

        var json = JsonConvert.SerializeObject(data);

        var res = await client.PostAsync(API_URL, new StringContent(json));

        var resString = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Predictions>(resString);

        return response.predictions;
    }

    ///========================================
    private void StartConversation()
    {
       
        string startString = "Welcome to the presidential palace!!! .";
        textField.text = startString;
        Debug.Log(startString);
    }

    public async void GetResponse()
    {
    

        // Disable the OK button
        okButton.enabled = false;
        //Debug.Log("disabling ok");

        // Update the text field with the user message
        textField.text = string.Format("You: {0}", inputField.text);
        var data = await TestRequest();
        Debug.Log(data[0].Predicted_Label);

        // Clear the input field
        inputField.text = "";
        okButton.enabled = true;
      

        if (data[0].Predicted_Label == "Ethical" || data[0].Predicted_Label == "ethical")
        {
            // imported_Class.ethical_value = true;
            anim.SetBool("ethical", true);

        }
        else{
            anim.SetBool("ethical",false);
    
        }

        // Add the response to the list of messages

        // Update the text field with the response
        textField.text = data[0].Predicted_Label;
    }
}















// using OpenAI_API;
// using OpenAI_API.Chat;
// using OpenAI_API.Models;
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
// // using kylebehaviorcontroller;


// public class OpenAIController : MonoBehaviour
// {
//     EthicsAPI EAPI = new EthicsAPI();

//     public TMP_Text textField;
//     public TMP_InputField inputField;
//     public Button okButton;
//     private List<ChatMessage> messages;

//     private Animator anim;
//     public  bool ethical_value;

//      void Start()
//     {
//          anim = GetComponent<Animator>();
//         StartConversation();
//         okButton.enabled = true;
//         okButton.onClick.AddListener(() => GetResponse());

//     }

//     private void StartConversation()
//     {
//         inputField.text = "";
//         string startString = "Welcome to the presidential palace!!! .";
//         textField.text = startString;
//         Debug.Log(startString);
//     }

//     public void GetResponse()
//     {
//         Debug.Log(inputField.text.Length);
//         if (inputField.text.Length < 1)
//         {
//             return;
//         }

//         // Disable the OK button
//         okButton.enabled = false;
//         Debug.Log("disabling ok");
//         inputField.text = "";

//         string txt = inputField.text;
//          Debug.Log("Getting input message");
//         Debug.Log(txt);
//         var pred = await EAPI.TestRequest(txt);
       
//         if (pred[0].Text == "Ethical" || pred[0].Text == "ethical"){
//             // imported_Class.ethical_value = true;
//             anim.SetBool("ethical",true);

//             Debug.Log("Printing1"+ pred[0].Text);



//         }
       

//         // Add the response to the list of messages
//         // messages.Add(pred[0].Text);

//         // Update the text field with the response
//         textField.text = string.Format("You: {0}\n\nJudge: {1}", userMessage.Content, pred[0].Text);

//         // Re-enable the OK button
//         okButton.enabled = true;
//     }
// }



