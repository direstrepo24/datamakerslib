using System.ComponentModel.DataAnnotations;

namespace datamakerslib.Repository
{
    public interface IEntityBase<Tkey>
    {
         [Key]
         Tkey Id { get; set; }
    }
}