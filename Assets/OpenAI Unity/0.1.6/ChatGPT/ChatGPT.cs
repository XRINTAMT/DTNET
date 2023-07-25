using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Meta.WitAi.TTS.Utilities;
using System.Linq;

public class RandomPool<T>
{
    private List<T> List;
    private T[] Array;

    public RandomPool(T[] _arr)
    {
        Array = _arr.Clone() as T[];
        List = new List<T>(_arr);
    }

    public T Draw()
    {
        if(List.Count == 0)
            List = new List<T>(Array);
        int _randint = Random.Range(0, List.Count - 1);
        T elem = List.ElementAt(_randint);
        List.RemoveAt(_randint);
        return elem;
    }

}

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        
        //[SerializeField] private InputField inputField;
        [SerializeField] private Text playerUtterance;
        [SerializeField] private Text textArea;
        [SerializeField] private Toggle pauseOnCommas;

        [SerializeField] private TTSSpeaker _speaker;
        [SerializeField] private string Instruction;

        [SerializeField] private WitAutoReactivation WitReact;
        [SerializeField] private Oculus.Voice.Demo.InteractionHandler InterHandler;
        [SerializeField] private string Name = "Elisa";


        [SerializeField] private AudioClip[] WaitingPhrases;

        private RandomPool<AudioClip> PhrasesPool; 

        private string[] sentences;
        private List<string> ChatHistory;
        [SerializeField] private int PhrasesToSend = 4;
        int instrLen;
        [SerializeField] private bool JustTestingDontSend = false;

        private int secret = 0;
        string[] errorReplies = { "Chat GPT is down (or we didn't pay for it) so have this:",
                    "We're no strangers to love",
                    "You know the rules and so do I (do I)",
                    "A full commitment's what I'm thinking of",
                    "You wouldn't get this from any other guy" };
        List<string> forbiddenPhrases = new List<string>
        {
            "Press activation to talk...",
            "Processing...", "", " "
        };

        private OpenAIApi openai = new OpenAIApi("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");

        private string userInput;
        //private string Instruction = "Act as a random stranger in a chat room and reply to the questions.\nQ: ";
        private string finalInstruction;

        private void Awake()
        {
            ChatHistory = new List<string>();
            PhrasesPool = new RandomPool<AudioClip>(WaitingPhrases); 
        }

        void Start()
        {
            //finalInstruction = $"{Instruction}\nQ: ";
            //instrLen = finalInstruction.Length - 4;
        }

        private string ComposeFinalString()
        {
            string _finalString = Instruction;
            foreach (string _str in ChatHistory.Skip(Mathf.Max(0, ChatHistory.Count() - PhrasesToSend)))
            {
                _finalString += "\n" + _str;
            }
            _finalString += "\nElisa: ";
            return _finalString;
        }

        private string FullHistory()
        {
            string _finalString = "";
            foreach (string _str in ChatHistory)
            {
                Debug.Log(_str);
                _finalString += _str + '\n';
            }
            return _finalString;
        }

        public async void SendReply()
        {
            userInput = InterHandler.LastNonNullPhrase;
            InterHandler.LastNonNullPhrase = "";
            if (forbiddenPhrases.Contains(userInput))
            {
                Debug.Log("Heard nothing / did not have enough time to process the speech");
                return;
            }

            WitReact.temporarilyIgnore = true;

            Debug.Log("userInput:: "+userInput);


            //finalInstruction += $"{userInput}\nA: ";
            ChatHistory.Add("Nurse: " + userInput);

            textArea.text = FullHistory() + "\n...";
            Debug.Log("Full chat history: " + FullHistory());
            playerUtterance.text = "";
            finalInstruction = ComposeFinalString();

            if (!JustTestingDontSend)
            {
                // Complete the instruction
                var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
                {
                    Prompt = finalInstruction,
                    Model = "text-davinci-003",
                    MaxTokens = 128
                });

                

                if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
                {
                    //finalInstruction += $"{completionResponse.Choices[0].Text}\nQ: ";

                    ChatHistory.Add("Elisa: " + completionResponse.Choices[0].Text);
                    textArea.text = FullHistory();
                    Debug.Log("Instruction :: " + finalInstruction);
                    Debug.Log("Response :: " + completionResponse.Choices[0].Text);
                    if (pauseOnCommas.isOn)
                        sentences = completionResponse.Choices[0].Text.Split(new char[] { ',', '\n', '.', '?', ';', '!' });
                    else
                        sentences = completionResponse.Choices[0].Text.Split(new char[] { '\n', '.', '?', ';', '!' });
                    StartCoroutine(PlayAndWait());
                    //WitReact.temporarilyIgnore = false;
                }
                else
                {
                    textArea.text = FullHistory();
                    Debug.LogWarning("No text was generated from this prompt.");
                    //finalInstruction += $"{errorReplies[secret]}\nQ: ";
                    _speaker.Speak(errorReplies[secret]);
                    secret++;
                    if (secret >= errorReplies.Length)
                        secret = 0;
                    //WitReact.temporarilyIgnore = false;
                }
                //inputField.enabled = true;
            }
            else
            {
                Debug.LogWarning("We're just testing, but if I was to send this to GPT, that would be: " + finalInstruction);
                sentences = new string[2] { "This is very cool", "tell me more"};
                StartCoroutine(PlayAndWait());
            }
        }
        IEnumerator PlayAndWait()
        {
            foreach (string sentence in sentences)
            {
                if (sentence == string.Empty)
                    continue;
                _speaker.AudioSource.clip = PhrasesPool.Draw();
                _speaker.AudioSource.Play();
                _speaker.Speak(sentence);
                
                while (!_speaker.IsSpeaking)
                {
                    yield return 0;
                }
                while (_speaker.IsSpeaking)
                {
                    yield return 0;
                }
                WitReact.temporarilyIgnore = true;
            }
            WitReact.temporarilyIgnore = false;
            Debug.Log("Giving control back to the stt");
        }
    }
}
