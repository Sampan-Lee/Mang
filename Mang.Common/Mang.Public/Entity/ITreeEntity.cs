using System.Collections.Generic;

namespace Mang.Public.Entity
{
    public interface ITreeEntity<T>
    {
        int Id { get; set; }
        int? ParentId { get; set; }
        List<T> Children { get; set; }
    }
}