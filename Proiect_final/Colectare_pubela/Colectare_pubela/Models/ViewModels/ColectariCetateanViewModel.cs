namespace Colectare_pubela.Models.ViewModels
{
    public class ColectariCetateanViewModel 
    {
        public int CitizenId { get; set; }
        public string CitizenName { get; set; }
        public string CitizenSurname { get; set; }
        public List<ColectareViewModel> Colectari { get; set; }
    }
}
