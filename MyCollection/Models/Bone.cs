namespace MyCollection.Models
{
    public class Bone
    {
        public int Id { get; set; }
        public string Name { get; set; } = null;
        public int Value { get; set; }
        public int Year { get; set; }
        public int SignatureId { get; set; }
        public Signature Signature { get; set; } = null;
        public int CountryId { get; set; }
        public Country Country { get; set; } = null;
        public int ImageId { get; set; }
        public Image Image { get; set; } = null;
        public int GradeId { get; set; }
        public Grade Grade { get; set; } = null;
        public double Price { get; set; }
    }
}
