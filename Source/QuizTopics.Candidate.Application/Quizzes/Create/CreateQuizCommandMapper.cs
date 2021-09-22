﻿using System;
using System.Linq;
using QuizTopics.Candidate.Domain;

namespace QuizTopics.Candidate.Application.Quizzes.Create
{
    public static class CreateQuizCommandMapper
    {
        public static Quiz AsQuiz(this CreateQuizCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return new Quiz(
                command.Name, 
                command.Exam, 
                command.ExamQuestionCollection.Select(x => 
                    new Question(
                        x.Text, 
                        x.Tag, 
                        (Difficulty)x.Difficulty, 
                        x.ExamAnswerCollection.Select(y => 
                            new Answer(
                                y.Text, 
                                y.IsCorrect)))));
        }
    }
}