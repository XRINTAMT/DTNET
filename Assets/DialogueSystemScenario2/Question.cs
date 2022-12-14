public class Question
{
    public string Tag { get; set; }
    public string EnglishText { get; set; }
    public string GermanText { get; set; }
    public string SwedishText { get; set; }
    public string LithuanianText { get; set; }
    public string LatvianText { get; set; }
    public int MoodChanges { get; set; }
    public string AnimationType { get; set; }
    public Question Prerequisite { get; set; }
    public bool IsAsked { get; set; }
    public string EnglishAnswer { get; set; }
    public string GermanAnswer { get; set; }
    public string SwedishAnswer { get; set; }
    public string LithuanianAnswer { get; set; }
    public string LatvianAnswer { get; set; }

    public bool PrerequisiteMet()
    {
        return Prerequisite == null || Prerequisite.IsAsked;
    }
}
