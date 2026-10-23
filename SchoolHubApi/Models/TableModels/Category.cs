
using SchoolHubApi.Models.SchoolModels;

namespace SchoolHubApi.Models.TableModels;

public class Category
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TableId { get; set; }
    public Table Table { get; set; } = null!;
    
    public string CategoryName { get; set; } = "";

    public ICollection<TableMessage> Messages { get; set; } = [];
}
