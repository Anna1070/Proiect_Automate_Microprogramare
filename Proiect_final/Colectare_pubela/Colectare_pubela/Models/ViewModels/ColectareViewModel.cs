namespace Colectare_pubela.Models.ViewModels
{
    public class ColectareViewModel
    {
        public int Id { get; set; }
        public string TagId { get; set; }
        public DateTime CollectionTime { get; set; }
        public string Address { get; set; }
        public bool HasError { get; set; }
        public string ErrorType { get; set; }
    }
}
