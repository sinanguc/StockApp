namespace Assessment.Stock.Entities.Concrete
{
    /// <summary>
    /// Yazılımla silinen (aslında depodan silmeden) bir varlığı temsil eder
    /// </summary>
    public interface ISoftDeleteEntity
    {
        bool Deleted { get; set; }
    }
}
