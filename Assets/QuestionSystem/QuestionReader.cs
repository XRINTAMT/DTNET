using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Text.RegularExpressions;

namespace QuestionSystem
{
    public class QuestionReader
    {
        public static List<Question> ReadQuestionsFromCsv(string csvFilePath)
        {
            List<Question> _questions;
            _questions = new List<Question>();

            using (var reader = new StreamReader(csvFilePath))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    Debug.Log(line);
                    string[] values = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");


                    Question question = new Question(values);

                    _questions.Add(question);
                }
            }

            return _questions;
        }
    }
}
