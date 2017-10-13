using UnityEngine;

[System.Serializable]
public class year1Quiz
{

    public string foodName;
    public Texture image;
    public bool isCorrect;
    public AudioClip questionAudio;
}

[System.Serializable]
public class year2Quiz
{
    public string question;
    public string answer;

    public Texture image;
    public Texture image2;
    public Texture image3;

    public string imageLabel;
    public string image2Label;
    public string image3Label;

    public bool isCorrect;
    public bool isCorrect2;
    public bool isCorrect3;
}

[System.Serializable]
public class year2Quiz2
{
    public string question;
    public string Answer;

    public Texture image;
    public Texture image2;
    public Texture image3;
    public Texture image4;

    public string imageLabel;
    public string image2Label;
    public string image3Label;
    public string image4Label;

    public bool isCorrect;
    public bool isCorrect2;
    public bool isCorrect3;
    public bool isCorrect4;
}


[System.Serializable]
public class year3Quiz
{
    public Texture image;
    public Texture image2;

    public string imageLabel;
    public string image2Label;
    public string question;
    public string answer;

    public bool isCorrect;
    public bool isCorrect2;

}

[System.Serializable]
public class year4Quiz
{
    public string question;
    public string foodCarbohydrates;
    public Texture image;
    public Texture image2;
    public bool isCorrect;
}

[System.Serializable]
public class year5Quiz
{
    public Texture image;
    public bool isCorrect;
    public string value;
}

[System.Serializable]
public class year5Quiz2
{
    public string question;
    public string carbohydrateAnswer;

    public bool isCorrect;
}

[System.Serializable]
public class year5Quiz3
{
    public Texture image;
    public bool isCorrect;
    public string value;
}

[System.Serializable]
public class year6Quiz
{
    public Texture image;
    public string imageLabel;
    public string question;
    public string carbohydrateAnswer;

    public bool isCorrect;
    public bool isCorrect2;
    public bool isCorrect3;
    public bool isCorrect4;
    public bool isCorrect5;
}

[System.Serializable]
public class year6Quiz2
{
    public string question;
    public string carbohydrateAnswer;
    public bool isCorrect;
}

[System.Serializable]
public class year6Quiz3
{
    public string foodLabel;
    public Texture foodImage;
    public int minAnswer;
    public int maxAnswer;
}

[System.Serializable]
public class year6Quiz4
{
    public string foodLabel;
    public Texture foodImage;
    public string userAnswer;
}

