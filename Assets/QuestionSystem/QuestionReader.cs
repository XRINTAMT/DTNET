using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

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
                    var values = line.Split(',');

                    Question question = new Question
                    {
                        Tag = values[0],
                        EnglishText = values[1],
                        GermanText = values[2],
                        SwedishText = values[3],
                        LithuanianText = values[4],
                        LatvianText = values[5],
                        MoodChanges = int.Parse(values[6]),
                        AnimationType = values[7],
                        Prerequisite = null,
                        IsAsked = false,
                        EnglishAnswer = values[8],
                        GermanAnswer = values[9],
                        SwedishAnswer = values[10],
                        LithuanianAnswer = values[11],
                        LatvianAnswer = values[12],
                        PrerequisiteTag = values[13]
                    };

                    _questions.Add(question);
                }
            }

            // Now we need to link the questions with their prerequisites.
            // We do this by iterating over the list of questions again and
            // setting the Prerequisite property of each question to the
            // corresponding question with the tag specified in the CSV file.
            foreach (var question in _questions)
            {
                if (question.PrerequisiteTag != string.Empty)
                {
                    question.Prerequisite = _questions.Single(q => q.Tag == question.PrerequisiteTag);
                }
                else
                {
                    question.Prerequisite = null;
                }
            }

            return _questions;
        }
    }
}

