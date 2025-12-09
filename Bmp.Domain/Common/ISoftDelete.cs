namespace Bmp.Domain.Common;

public interface ISoftDelete
{
    public DateTime? DeletedAt { get; set; }
}