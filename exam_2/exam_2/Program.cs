using System;
using System.Collections.Generic;
public abstract class Question
{
    public string Header{get;set;}
    public string Body{get;set;}
    public int Mark{get;set;}
    public Answer[] Answers{get;set;}
    public Answer RightAnswer{get;set;}
    public Question(string header,string body,int mark)
    {
        Header=header;
        Body=body;
        Mark=mark;
    }

    public abstract void ShowQuestion();
}
public class TrueFalseQuestion : Question
{
    public TrueFalseQuestion(string header,string body,int mark)
        : base(header,body,mark)
    {
        Answers=new Answer[]
        {
            new Answer(1,"True"),
            new Answer(2,"False")
        };
    }
    public override void ShowQuestion()
    {
        Console.WriteLine(Header);
        Console.WriteLine(Body);
        foreach (var ans in Answers)
        {
            Console.WriteLine($"{ans.AnswerId}.{ans.AnswerText}");
        }
    }
}
public class MCQquestion:Question
{
    public MCQquestion(string header,string body,int mark,Answer[] answers,Answer rightAnswer)
        : base(header, body, mark)
    {
        Answers=answers;
        RightAnswer=rightAnswer;
    }
    public override void ShowQuestion()
    {
        Console.WriteLine(Header);
        Console.WriteLine(Body);
        foreach (var ans in Answers)
        {
            Console.WriteLine($"{ans.AnswerId}. {ans.AnswerText}");
        }
    }
}
public class Answer
{
    public int AnswerId{get;set;}
    public string AnswerText{get; set;}

    public Answer(int id,string text)
    {
        AnswerId=id;
        AnswerText=text;
    }
    public override string ToString()
    {
        return $"{AnswerId}.{AnswerText}";
    }
}
public abstract class Exam
{
    public int Time{get;set;}
    public int NumberOfQuestions{get;set;}
    public List<Question>Questions{get;set;}=new List<Question>();

    public Exam(int time,int numQuestions)
    {
        Time=time;
        NumberOfQuestions=numQuestions;
    }

    public abstract void ShowExam();
}
public class PracticalExam:Exam
{
    public PracticalExam(int time,int numQuestions):base(time,numQuestions){}

    public override void ShowExam()
    {
        Console.WriteLine("*Practical Exam*");
        foreach (var q in Questions)
        {
            q.ShowQuestion();
            Console.Write("your answer is:");
            int choice=int.Parse(Console.ReadLine());
            Console.WriteLine($"correct answer is: {q.RightAnswer.AnswerText}\n");
        }
    }
}
public class FinalExam:Exam
{
    public FinalExam(int time,int numQuestions):base(time,numQuestions){}

    public override void ShowExam()
    {
        Console.WriteLine("*Final Exam*");
        int totalGrade=0;
        foreach(var q in Questions)
        {
            q.ShowQuestion();
            Console.Write("your answer is:");
            int choice=int.Parse(Console.ReadLine());
            if(choice==q.RightAnswer.AnswerId)
            {
                totalGrade += q.Mark;
            }
        }
        Console.WriteLine($"ur grade is:{totalGrade}");
    }
}
public class Subject
{
    public int SubjectId{get;set;}
    public string SubjectName{get;set;}
    public Exam Exam{get;set;}

    public Subject(int id,string name)
    {
        SubjectId=id;
        SubjectName=name;
    }

    public void CreateExam(Exam exam)
    {
        Exam=exam;
    }

    public override string ToString()
    {
        return $"Subject:{SubjectName}(ID:{SubjectId})";
    }
}
class Program
{
    static void Main()
    {
        
        Subject subj=new Subject(1,"DSA");
        FinalExam finalExam=new FinalExam(60,2);
        Answer[] mcqAnswers = {
            new Answer(1,"Stack"),
            new Answer(2,"Queue"),
            new Answer(3,"linked list")
        };
        finalExam.Questions.Add(new TrueFalseQuestion("Q1","at stack is the last element inserted is the first element removed?",1)
        { RightAnswer = new Answer(1,"True") });
        finalExam.Questions.Add(new MCQquestion("Q2","Which DS use FIFO ?",2,mcqAnswers,mcqAnswers[1]));
        subj.CreateExam(finalExam);
        subj.Exam.ShowExam();
        /*
        Subject subj =new Subject(1,"DSA");
        PracticalExam practicalExam=new PracticalExam(10,2);

        Answer[] mcqAnswers={
    new Answer(1,"Stack"),
    new Answer(2,"Queue"),
    new Answer(3,"Linked List")
};
        practicalExam.Questions.Add(
            new MCQquestion("Q1","Which DS use LIFO?",2,mcqAnswers,mcqAnswers[1])
        );
        practicalExam.Questions.Add(new TrueFalseQuestion("Q2", "IS stack work on LIFO ?",1)
            {RightAnswer=new Answer(1,"True") }
        );
        subj.CreateExam(practicalExam);
        subj.Exam.ShowExam();
        */
    }
}

