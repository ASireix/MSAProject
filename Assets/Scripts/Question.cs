public class Question
{
    public string intitule;
    public string[] reponses;
    public int bonne_Reponse;

    public Question(string intitule, string[] reponses, int bonne_Reponse) {
        this.intitule = intitule;
        this.reponses = reponses;
        this.bonne_Reponse = bonne_Reponse;
    }
}

