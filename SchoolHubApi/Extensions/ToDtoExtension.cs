
using SchoolHubApi.DTOs;
using SchoolHubApi.Models.TableModels;

namespace SchoolHubApi.Extensions;

public static class ToDtoExtension
{
    public static TableDto ToDto(this Table table)
    {
        var dto = new TableDto(table.Id, table.Name, [.. table.Categories.Select(c => c.CategoryName)]);

        return dto;
    }

    public static MessageDto ToDto(this TableMessage message)
    {
        var dto = new MessageDto(
            message.Id,
            message.Date,
            message.CategoryId,
            message.Sender.Username,
            message.Pending,
            new WorkDto(message.Work.Id, message.Work.Title, message.Work.Date));

        return dto;
    }
}
