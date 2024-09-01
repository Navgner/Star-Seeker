[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers = new string[4];
    public int correctAnswerIndex; // L'index de la réponse correcte (0 à 3)
}
